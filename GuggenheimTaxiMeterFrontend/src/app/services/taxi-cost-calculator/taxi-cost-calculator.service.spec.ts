import { TestBed } from '@angular/core/testing';

import { TaxiCostCalculatorService } from './taxi-cost-calculator.service';
import { HttpClientModule } from '@angular/common/http';
import { TaxiMeter } from 'src/models/TaxiMeter.model';
import { TaxiMeterCostBreakdown } from 'src/models/TaxiMeterCostBreakdown.model';

describe('TaxiCostCalculatorService', () => {
	beforeEach(() => TestBed.configureTestingModule({
		imports: [
			HttpClientModule
		],
	}));

	it('should be created', () => {
		const service: TaxiCostCalculatorService = TestBed.get(TaxiCostCalculatorService);
		expect(service).toBeTruthy();
	});
	/** This might be more of an integration test, should instead set up a mock 'endpoint' for this service to 'hit' */
	it('should return 9.75', (done) => {
		const service: TaxiCostCalculatorService = TestBed.get(TaxiCostCalculatorService);
		let meter: TaxiMeter = new TaxiMeter(new Date('2010-10-08T17:30:00'), 5, 2);
		service.GetCalculatedCost(meter).then((result) => {
			expect(result.Total).toEqual(9.75);
			done();
		})
	});
});
