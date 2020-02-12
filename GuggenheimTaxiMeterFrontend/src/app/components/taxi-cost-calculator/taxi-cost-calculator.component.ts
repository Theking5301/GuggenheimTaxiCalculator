import { Component, OnInit } from '@angular/core';
import { TaxiMeter } from 'src/models/TaxiMeter.model';
import { DatePipe } from '@angular/common';
import { TaxiCostCalculatorService } from 'src/app/services/taxi-cost-calculator/taxi-cost-calculator.service';
import { TaxiMeterCostBreakdown } from 'src/models/TaxiMeterCostBreakdown.model';

@Component({
	selector   : 'app-taxi-cost-calculator',
	templateUrl: './taxi-cost-calculator.component.html',
	styleUrls  : ['./taxi-cost-calculator.component.less']
})
/**
 * Component responsible for the rendering and logic of the taxi cost calculator.
 * 
 * @author Amine Hosseini
 * @created_date 02/11/19
 * @modified_date 02/11/19
 */
export class TaxiCostCalculatorComponent implements OnInit {
	/** Represents the user's inputted ride date. */
	public RideDate: string;
	/** Represents the user's inputted ride start time. */
	public RideStartTime: string;
	/** Represents the user's inputted miles below the speed threshold. */
	public MilesBelowThreshold: string;
	/** Represents the user's inputted minutes above the speed threshold or idle. */
	public MinutesAboveThresholdOrIdle: string;
	/** Popualted with the cost breakdown onced it is calculated. */
	public CostBreakdown: TaxiMeterCostBreakdown;
	/** Indicates if the ride date is valid. */
	public RideDateValid: boolean;
	/** Indicates if the ride start time is valid. */
	public RideStartTimeValid: boolean;
	/** Indicates if the miles below the speed threshold value is valid. */
	public MilesBelowThresholdValid: boolean;
	/** Indicates if the minutes above the speed threshold or idle is valid. */
	public MinutesAboveThresholdOrIdleValid: boolean;

	/**
	 * Constructor with DI.
	 * @param _DatePipe Date time formatter used for populated the date and time field.
	 * @param _CalculatorService The calculater service responsible for forwarding reuqests to the backend.
	 */
	constructor(private _DatePipe: DatePipe, private _CalculatorService: TaxiCostCalculatorService) {
		this.CostBreakdown = undefined;

		// Set the ride date and time to right now.
		let now: Date          = new Date();
		    this.RideDate      = _DatePipe.transform(now, 'yyyy-MM-dd');
		    this.RideStartTime = _DatePipe.transform(now, 'hh:mm');

		// Mark all fields as initially valid so that they don't initially render with a red border.
		this.RideDateValid                    = true;
		this.RideStartTimeValid               = true;
		this.MilesBelowThresholdValid         = true;
		this.MinutesAboveThresholdOrIdleValid = true;
	}
	ngOnInit() {

	}
	/**
	 * Attemps to create a well formed {@link TaxiMeter} model.
	 */
	public CreateModel(): TaxiMeter {
		// If a value is INVALID, return undefined, otherwise return a fully formed model.
		if (this.IsValidForCalculation()) {
			let rideDate: Date = new Date(Date.parse(this.RideDate + " " + this.RideStartTime));
			return new TaxiMeter(rideDate, +this.MinutesAboveThresholdOrIdle, +this.MilesBelowThreshold);
		}
		return undefined;
	}
	/**
	 * Checks and caches the validity of all fields.
	 * If any invalid fields are encountered, they are marked invalid and undefined is returned.
	 */
	public ValidateFields() {
		// Check the miles below threshold validity.
		if (isNaN(+this.MilesBelowThreshold) || +this.MilesBelowThreshold < 0) {
			console.warn("There was an attempt to process a cost evaluation with an invalid value for 'MilesBelowThreshold'.");
			this.MilesBelowThresholdValid = false;
		} else {
			this.MilesBelowThresholdValid = true;
		}

		// Check the minutes above threshold validity.
		if (isNaN(+this.MinutesAboveThresholdOrIdle) || +this.MinutesAboveThresholdOrIdle < 0) {
			console.warn("There was an attempt to process a cost evaluation with an invalid value for 'MinutesAboveThresholdOrIdle'.");
			this.MinutesAboveThresholdOrIdleValid = false;
		} else {
			this.MinutesAboveThresholdOrIdleValid = true;
		}

		// Check the ride date validity.
		if (this.RideDate == "") {
			console.warn("There was an attempt to process a cost evaluation with an invalid value for 'RideDate'.");
			this.RideDateValid = false;
		} else {
			this.RideDateValid = true;
		}

		// Check the ride start time validity.
		if (this.RideStartTime == "") {
			console.warn("There was an attempt to process a cost evaluation with an invalid value for 'RideStartTimeValid'.");
			this.RideStartTimeValid = false;
		} else {
			this.RideStartTimeValid = true;
		}

	}
	/**
	 * Check to see if all the required fields are valid for calculation.
	 */
	public IsValidForCalculation() : boolean {
		return this.RideDateValid && this.RideStartTimeValid && this.MilesBelowThresholdValid && this.MinutesAboveThresholdOrIdleValid;
	}
	/**
	 * Whenever any input field is changed, check and cache new validitiy states.
	 * This is to ensured that as soon as a user corrects an invalid field, the red outline on that field is removed.
	 */
	public OnInputChanged() {
		this.ValidateFields();
	}
	/**
	 * This method is raised when the submit button is clicked.
	 * @param Event The mouse clicked event.
	 */
	public SubmitClicked(Event): void {
		// Trigger the field validation.
		this.ValidateFields();

		// Regardless of whether or not we are well formed, mark the cost breakdown as undefined so that the view is hidden.
		this.CostBreakdown = undefined;

		// Check to see if we have a well formed model, if not, return early.
		let model: TaxiMeter = this.CreateModel();
		if (model === undefined) {
			return;
		}

		// Make the API call to calculate the cost and then populate the UI model.
		this._CalculatorService.GetCalculatedCost(model).then((result: TaxiMeterCostBreakdown) => {
			this.CostBreakdown = result;
		});
	}
}
