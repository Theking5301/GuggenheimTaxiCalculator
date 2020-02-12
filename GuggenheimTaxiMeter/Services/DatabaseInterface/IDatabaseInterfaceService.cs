using GuggenheimTaxiMeter.Models;

namespace GuggenheimTaxiMeter.Services {
    /// <summary>
    /// Services responsible for pulling data from the database.
    /// </summary>
    /// <author>Amine Hosseini</author>
    /// <created>2/11/2019</created>
    /// <modified>2/11/2019</modified>
    public interface IDatabaseInterfaceService {
        /// <summary>
        /// Gets the speed (in miles per hour) that is used in calculating certain fees.
        /// </summary>
        /// <returns></returns>
        int GetSpeedThreshold();
        /// <summary>
        /// Gets the freshest <see cref="TaxiBaseCostsModel"/> from the database.
        /// </summary>
        /// <returns>An instance of a <see cref="TaxiBaseCostsModel"/> containing the data required to calculate the total cost of a <see cref="TaxiMeterModel"/>.</returns>
        TaxiBaseCostsModel GetBaseCosts();
    }
}
