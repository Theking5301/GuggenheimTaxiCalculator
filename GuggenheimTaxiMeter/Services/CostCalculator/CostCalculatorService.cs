using GuggenheimTaxiMeter.Models;
using Microsoft.Extensions.Logging;
using System;

namespace GuggenheimTaxiMeter.Services {
    /// <inheritdoc/>
    public class CostCalculatorService : ICostCalculatorService {
        /// <summary>
        /// Logger.
        /// </summary>
        protected readonly ILogger<CostCalculatorService> _Log;
        /// <summary>
        /// Interface to the data source.
        /// </summary>
        private IDatabaseInterfaceService _DatabaseInterface;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="DatabaseInterfaceService">Reference to databse interface singleton.</param>
        public CostCalculatorService(IDatabaseInterfaceService DatabaseInterfaceService) {
            _DatabaseInterface = DatabaseInterfaceService;
        }

        /// <inheritdoc/>
        public TaxiCostBreakdown CalculateCost(TaxiMeterModel Model) {
            try {
                // Get the cost model (currently hard coded but later may be pulled from a data source).
                TaxiBaseCostsModel costs = _DatabaseInterface.GetBaseCosts();

                // First calculate the initial baseline cost of the ride.
                decimal baseCost = costs.BaselineCost;

                // Calculate the state surcharge. If later expanded, we should check if the dictionary 
                // first contains this value before we potentially append it to the total cost.
                decimal stateCost = costs.StateSurcharges["NY"];

                // Check to see the the ride day of the week falls between Monday and Friday inclusive.
                // Then, check if the ride time is after 4PM inclusive and before 8PM exclusive.
                // If so, calculate the peak charge.
                decimal peakCost = 0.0m;
                if (Model.RideDateTime.DayOfWeek >= DayOfWeek.Monday && Model.RideDateTime.DayOfWeek <= DayOfWeek.Friday) {
                    if (Model.RideDateTime.Hour >= 16 && Model.RideDateTime.Hour < 20) {
                        peakCost = costs.PeakSurcharge;
                    }
                }

                // Check to see the ride hour is either after 8PM inclusive or before 6AM exclusive (ie. in the range of those two times)
                // If so, calculate the night charge.
                decimal nightCost = 0.0m;
                if (Model.RideDateTime.Hour >= 20 || Model.RideDateTime.Hour < 6) {
                    nightCost = costs.NightSurcharge;
                }

                // Calculate the number of cost unites for the 1/5th miles traveled below the threshold.
                // Calculate the number of minutes spent not in motion or traveling greater than the threshold.
                decimal milesCostUnits  = Math.Floor(Model.MilesBelowThreshold * 5.0m);
                decimal minutesCostUnits = Model.MinutesAboveThresholdOrIdle;

                // Accumulate the total cost;
                decimal totalCost = baseCost +stateCost + peakCost + nightCost + costs.UnitCost * (milesCostUnits + minutesCostUnits);

                // Capture the breakdown and return it.
                return new TaxiCostBreakdown(totalCost, costs.UnitCost * milesCostUnits, costs.UnitCost * minutesCostUnits, nightCost, peakCost, stateCost, baseCost, costs);
            } catch(Exception e) {
                _Log.LogError(e, "An error occurred when attempting to calculate the cost of a TaxiMeterModel.");

                // Rethrow the exception so that the front end does not receive a 200. 
                // One future change should be to implement middleware that will handle translating exceptions to HTTP error status codes.
                throw e;
            }
        }
    }
}
