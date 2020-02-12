import { TaxiBaseCosts } from './TaxiBaseCosts.model';

export class TaxiMeterCostBreakdown {
    constructor(
        public Total: number,
        public MilesUnderThreshold: number,
        public IdleOrOverThreshold: number,
        public NightSurcharge: number,
        public PeakSurcharge: number,
        public StateSurcharge: number,
        public BaseCost: number,
        public UnitCosts: TaxiBaseCosts) {
    }
}