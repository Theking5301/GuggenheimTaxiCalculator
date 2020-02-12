using GuggenheimTaxiMeter.Models;
namespace GuggenheimTaxiMeter.Services {
    /// <inheritdoc/>
    public class DatabaseInterfaceService : IDatabaseInterfaceService {
        /// <inheritdoc/>
        public int GetSpeedThreshold() {
            return 6;
        }
        /// <inheritdoc/>
        public TaxiBaseCostsModel GetBaseCosts() {
            return new TaxiBaseCostsModel();
        }
    }
}
