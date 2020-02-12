import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TaxiCostCalculatorComponent } from './taxi-cost-calculator.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { TaxiCostCalculatorService } from 'src/app/services/taxi-cost-calculator/taxi-cost-calculator.service';
import { DateValidatorDirective } from 'src/app/directives/date-validator/date-validator.directive';
import { TimeValidatorDirective } from 'src/app/directives/time-validator/time-validator.directive';
import { NumberValidatorDirective } from 'src/app/directives/number-validator/number-validator.directive';

describe('TaxiCostCalculatorComponent', () => {
	let component: TaxiCostCalculatorComponent;
	let fixture: ComponentFixture<TaxiCostCalculatorComponent>;

	beforeEach(async(() => {
		TestBed.configureTestingModule({
			imports: [
				HttpClientModule,
				FormsModule
			],
			declarations: [
				TaxiCostCalculatorComponent,
				DateValidatorDirective,
				TimeValidatorDirective,
				NumberValidatorDirective
			],
			providers: [
				DatePipe,
				TaxiCostCalculatorService
			]
		})
			.compileComponents();
	}));

	beforeEach(() => {
		fixture = TestBed.createComponent(TaxiCostCalculatorComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});

	/** Checks to see if the component is not considered valid when the miles and minutes fields are left blank. */
	it('should return false due to invalid MilesBelowThreshold and MinutesAboveThresholdOrIdle', () => {
		component.ValidateFields();
		expect(component.IsValidForCalculation()).toBeFalsy();
	});

	/** Checks to see if the component is not considered valid when the miles field is left blank. */
	it('should return false due to invalid MilesBelowThreshold', () => {
		component.RideDate = '';
		component.RideStartTime = '17:30';
		component.MinutesAboveThresholdOrIdle = '5';
		component.ValidateFields();
		expect(component.IsValidForCalculation()).toBeFalsy();
	});

	/** Checks to see if the component is not considered valid when the minutes field is left blank. */
	it('should return false due to invalid MinutesAboveThresholdOrIdle', () => {
		component.RideDate = "";
		component.RideStartTime = '17:30';
		component.MilesBelowThreshold = '2';
		component.ValidateFields();
		expect(component.IsValidForCalculation()).toBeFalsy();
	});

	/** Checks to see if the component is not considered valid when the ride date is left blank. */
	it('should return false due to invalid RideDate', () => {
		component.RideDate = '';
		component.RideStartTime = '17:30';
		component.MilesBelowThreshold = '2';
		component.MinutesAboveThresholdOrIdle = '5';
		component.ValidateFields();
		expect(component.IsValidForCalculation()).toBeFalsy();
	});

	/** Checks to see if the component is not considered valid when the start time is left blank. */
	it('should return false due to invalid RideStartTime,', () => {
		component.RideStartTime = '';
		component.RideDate = '2010-10-08';
		component.MilesBelowThreshold = '2';
		component.MinutesAboveThresholdOrIdle = '5';
		component.ValidateFields();
		expect(component.IsValidForCalculation()).toBeFalsy();
	});

	/** Checks to see if the component is considered valid when proper values are supplied. */
	it('should return valid due to all fields being properly populated', () => {
		component.MilesBelowThreshold = '2';
		component.MinutesAboveThresholdOrIdle = '5';
		component.RideDate = '2010-10-08';
		component.RideStartTime = '17:30';
		component.ValidateFields();
		expect(component.IsValidForCalculation()).toBeTruthy();
	});
});
