export class TaxiBaseCosts {
    constructor(public BaselineCost : number, public UnitCost : number, public NightSurcharge : number, public PeakSurcharge : number, public StateSurcharges : TaxiBaseCostStateMap) {

    }
}
export class TaxiBaseCostStateMap{
    [index:string]: number;
}