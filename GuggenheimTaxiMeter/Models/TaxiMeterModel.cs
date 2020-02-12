using System;

namespace GuggenheimTaxiMeter.Models {
    /// <summary>
    /// Data model encapsulating the data required to calculate the cost of a taxi trip.
    /// </summary>
    /// <author>Amine Hosseini</author>
    /// <created>2/11/2019</created>
    /// <modified>2/11/2019</modified>
    public class TaxiMeterModel {
        /// <summary>
        /// The amount of minutes spent traveling above the threshold or not moving.
        /// </summary>
        public int MinutesAboveThresholdOrIdle { get; set; }
        /// <summary>
        /// The number of miles spent traveling below the threshold.
        /// </summary>
        public int MilesBelowThreshold { get; set; }
        /// <summary>
        /// The ride date and start time.
        /// </summary>
        public DateTime RideDateTime { get; set; }


        /// <summary>
        /// Default constructor for object deserialization.
        /// </summary>
        public TaxiMeterModel() {}

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="MinutesAboveThreshold">The amount of minutes traveling over the threshold or not moving.</param>
        /// <param name="MilesBelowThreshold">The number of miles traveling below the threshold.</param>
        /// <param name="RideDateTime">The ride date and time.</param>
        public TaxiMeterModel(int MinutesAboveThresholdOrIdle, int MilesBelowThreshold, DateTime RideDateTime) {
            this.MinutesAboveThresholdOrIdle = MinutesAboveThresholdOrIdle;
            this.MilesBelowThreshold         = MilesBelowThreshold;
            this.RideDateTime                = RideDateTime;
        }
    }
}
