import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TaxiMeter } from 'src/models/TaxiMeter.model';
import { TaxiMeterCostBreakdown } from 'src/models/TaxiMeterCostBreakdown.model';
import { environment } from 'src/environments/environment';


@Injectable({
	providedIn: 'root'
})
export class TaxiCostCalculatorService {

	constructor(private _Http: HttpClient) { }

	/**
	 * 
	 * @param Model The {@link TaxiMeter} object to calculate the cost for.
	 * @returns A promise containing the {@link TaxiMeterCostBreakdown} for the provided {@link TaxiMeter} model.
	 */
	public GetCalculatedCost(Model: TaxiMeter): Promise<TaxiMeterCostBreakdown> {
		let endpoint = environment.api_endpoint + '/api/calculator/calculate?';
		endpoint += "RideDateTime=" + Model.RideDateTime.toLocaleString() + "&";
		endpoint += "MinutesAboveThresholdOrIdle=" + Model.MinutesAboveThresholdOrIdle + "&";
		endpoint += "MilesBelowThreshold=" + Model.MilesBelowThreshold;
		return this._Http.get<TaxiMeterCostBreakdown>(endpoint).toPromise();
	}
}
