using GuggenheimTaxiMeter.Models;

namespace GuggenheimTaxiMeter.Services {
    /// <summary>
    /// Service responsible for calculating the cost of a <see cref="TaxiMeterModel"/>.
    /// </summary>
    /// <author>Amine Hosseini</author>
    /// <created>2/11/2019</created>
    /// <modified>2/11/2019</modified>
    public interface ICostCalculatorService {
        /// <summary>
        /// Calculates the cost of a taxi ride based on a provided <see cref="TaxiMeterModel"/> model. 
        /// Fetches the freshest <see cref="TaxiBaseCostsModel"/> from the database.
        /// </summary>
        /// <param name="Model">The ride model to calculate the cost of.</param>
        /// <returns>The cost of the trip in USD.</returns>
        TaxiCostBreakdown CalculateCost(TaxiMeterModel Model);
    }
}
