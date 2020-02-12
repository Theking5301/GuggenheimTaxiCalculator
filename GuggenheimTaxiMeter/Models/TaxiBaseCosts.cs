using System.Collections.Generic;

namespace GuggenheimTaxiMeter.Models {
    /// <summary>
    /// Model that encapsulates all the information required to calculate the cost of a <see cref="TaxiMeterModel"/>.
    /// </summary>
    /// <author>Amine Hosseini</author>
    /// <created>2/11/2019</created>
    /// <modified>2/11/2019</modified>
    public class TaxiBaseCostsModel {
        /// <summary>
        /// The base cost of a trip.
        /// </summary>
        public decimal BaselineCost { get; set; }
        /// <summary>
        /// The cost per unit.
        /// </summary>
        public decimal UnitCost { get; set; }
        /// <summary>
        /// The surcharge applied when the ride meets the criteria of a night ride.
        /// </summary>
        public decimal NightSurcharge { get; set; }
        /// <summary>
        /// The surcharge applied when the ride meets the criteria of a peak ride.
        /// </summary>
        public decimal PeakSurcharge { get; set; }
        /// <summary>
        /// Hash map of states/territories to surcharges.
        /// </summary>
        public Dictionary<string, decimal> StateSurcharges { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public TaxiBaseCostsModel() {
            // Hard coded values should be populated from the data source when done properly.
            BaselineCost     = 3;
            UnitCost         = 0.35m;
            NightSurcharge   = 0.5m;
            PeakSurcharge    = 1;

            // Initialize the state surcharge map with just NY. Ideally, when done properly, 
            // this map should be initialized with all 50 states and any additional territories, 
            // with any territory without a surcharge mapping to a value of 0.0m.
            StateSurcharges = new Dictionary<string, decimal>();
            StateSurcharges.Add("NY", 0.5m);
        }
    }
}
