export class TaxiMeter {
    constructor(public RideDateTime: Date, public MinutesAboveThresholdOrIdle: number, public MilesBelowThreshold) {

    }
}