namespace GuggenheimTaxiMeter.Models {
    /// <summary>
    /// Model that encapsulates all the information breaking down the cost calculation associated with a <see cref="TaxiMeterModel"/>.
    /// </summary>
    /// <author>Amine Hosseini</author>
    /// <created>2/11/2019</created>
    /// <modified>2/11/2019</modified>
    public class TaxiCostBreakdown {
        /// <summary>
        /// The total calculated cost.
        /// </summary>
        public decimal Total { get; set; }
        /// <summary>
        /// The cost associated with the miles traveled under the threshold speed.
        /// </summary>
        public decimal MilesUnderThreshold { get; set; }
        /// <summary>
        /// The cost associated with idle time or time over the speed threshold.
        /// </summary>
        public decimal IdleOrOverThreshold { get; set; }
        /// <summary>
        /// The surcharge applied for night rides (if applicable).
        /// </summary>
        public decimal NightSurcharge { get; set; }
        /// <summary>
        /// The surcharge applied from peak (if applicable).
        /// </summary>
        public decimal PeakSurcharge { get; set; }
        /// <summary>
        /// The surcharge applied by the state (if applicable).
        /// </summary>
        public decimal StateSurcharge { get; set; }
        /// <summary>
        /// The base cost of the ride.
        /// </summary>
        public decimal BaselineCost { get; set; }
        /// <summary>
        /// The unit costs used in calculating these values.
        /// </summary>
        public TaxiBaseCostsModel UnitCosts { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public TaxiCostBreakdown() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Total">Total cost.</param>
        /// <param name="MilesUnderThreshold">Cost from miles under threshold.</param>
        /// <param name="IdleOrOverThreshold">Cost from minutes idle or over threshold.</param>
        /// <param name="NightSurcharge">Cost from night surcharge.</param>
        /// <param name="PeakSurcharge">Cost from peak surcharge.</param>
        /// <param name="StateSurcharge">Cost from state surcharge.</param>
        /// <param name="BaseCost">Base cost.</param>
        /// <param name="UnitCost">Cost per unit.</param>
        public TaxiCostBreakdown(decimal Total, decimal MilesUnderThreshold, decimal IdleOrOverThreshold, decimal NightSurcharge, decimal PeakSurcharge, decimal StateSurcharge, decimal BaselineCost, TaxiBaseCostsModel UnitCosts) {
            this.Total  = Total;
            this.MilesUnderThreshold = MilesUnderThreshold;
            this.IdleOrOverThreshold = IdleOrOverThreshold;
            this.NightSurcharge = NightSurcharge;
            this.PeakSurcharge = PeakSurcharge;
            this.StateSurcharge = StateSurcharge;
            this.BaselineCost = BaselineCost;
            this.UnitCosts = UnitCosts;
        }
    }
}
