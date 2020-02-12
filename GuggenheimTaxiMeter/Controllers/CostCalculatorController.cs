using GuggenheimTaxiMeter.Models;
using GuggenheimTaxiMeter.Services;
using Microsoft.AspNetCore.Mvc;

namespace GuggenheimTaxiMeter.Controllers {
    /// <summary>
    /// Controller that handles all the information required to calculate the cost of a <see cref="TaxiMeterModel"/>.
    /// </summary>
    /// <author>Amine Hosseini</author>
    /// <created>2/11/2019</created>
    /// <modified>2/11/2019</modified>
    [Route("api/calculator")]
    public class CostCalculatorController : Controller {
        /// <summary>
        /// Cost Calculator singleton service.
        /// </summary>
        private ICostCalculatorService _CostCalculator;
        /// <summary>
        /// Database Interface singleton service.
        /// </summary>
        private IDatabaseInterfaceService _DatabaseInterface;
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="CostCalculatorService">DI Cost Calculator service.</param>
        /// <param name="DatabaseInterfaceService">DI Databse Interface service.</param>
        public CostCalculatorController(ICostCalculatorService CostCalculatorService, IDatabaseInterfaceService DatabaseInterfaceService) {
            _CostCalculator = CostCalculatorService;
            _DatabaseInterface = DatabaseInterfaceService;
        }

        /// <summary>
        /// Calculates the cost of the provided <see cref="TaxiMeterModel"/>.
        /// </summary>
        /// <param name="Model">The model representing the data required for calculating the cost of a trip.</param>
        /// <returns>The cost breakdown of the trip, including the total cost in USD.</returns>
        [HttpGet("calculate")]
        public TaxiCostBreakdown CalculateCost([FromQuery] TaxiMeterModel Model) {
            return _CostCalculator.CalculateCost(Model);
        }

        /// <summary>
        /// Gets the threshold used when evaluating if a portion of the trip is eligible for certain costs.
        /// </summary>
        /// <returns>The threshold in miles per hour.</returns>
        [HttpGet("threshold")]
        public int GetSpeedThreshold() {
            return _DatabaseInterface.GetSpeedThreshold();
        }
        /// <summary>
        /// Gets the base costs in the form of a <see cref="TaxiBaseCostsModel"/>.
        /// This encapsulates the values used when calculating the cost of a <see cref="TaxiMeterModel"/>.
        /// </summary>
        /// <returns>A <see cref="TaxiBaseCostsModel"/> containing all the data required to calculate the cost of a <see cref="TaxiMeterModel"/>.</returns>
        [HttpGet("basecosts")]
        public TaxiBaseCostsModel GetBaseCosts() {
            return _DatabaseInterface.GetBaseCosts();
        }
    }
}
