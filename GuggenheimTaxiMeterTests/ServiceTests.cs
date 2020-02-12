using GuggenheimTaxiMeter.Models;
using GuggenheimTaxiMeter.Services;
using System;
using Xunit;

namespace GuggenheimTaxiMeterTests {
    public class ServiceTests {
        [Fact]
        public void CostCalculatorServiceValidReturnType() {
            // Get mock model.
            TaxiMeterModel model = new TaxiMeterModel();

            // Create service.
            ICostCalculatorService costCalculator = new CostCalculatorService(new DatabaseInterfaceService());

            // Execute action.
            TaxiCostBreakdown cost = costCalculator.CalculateCost(model);

            // Validate results.
            Assert.IsType<TaxiCostBreakdown>(cost);
        }
        [Fact]
        public void CostCalculatorControllerBaseCost() {
            // Get mock model.
            TaxiMeterModel model = new TaxiMeterModel();
            model.MilesBelowThreshold = 0;
            model.MinutesAboveThresholdOrIdle = 0;
            model.RideDateTime = DateTime.Parse("2019-02-09 12:00");

            // Create service.
            ICostCalculatorService costCalculator = new CostCalculatorService(new DatabaseInterfaceService());

            // Execute action.
            TaxiCostBreakdown cost = costCalculator.CalculateCost(model);

            // Validate results.
            Assert.Equal(3.5m, cost.Total);
        }
        [Fact]
        public void CostCalculatorControllerExampleCost() {
            // Get mock model.
            TaxiMeterModel model = new TaxiMeterModel();
            model.MilesBelowThreshold = 2;
            model.MinutesAboveThresholdOrIdle = 5;
            model.RideDateTime = DateTime.Parse("2010-10-08 17:30");

            // Create service.
            ICostCalculatorService costCalculator = new CostCalculatorService(new DatabaseInterfaceService());

            // Execute action.
            TaxiCostBreakdown cost = costCalculator.CalculateCost(model);

            // Validate results.
            Assert.Equal(9.75m, cost.Total);
        }
        [Fact]
        public void DatabaseInterfaceValidReturnType() {
            // Create service.
            IDatabaseInterfaceService databaseInterface = new DatabaseInterfaceService();

            // Execute action.
            TaxiBaseCostsModel cost = databaseInterface.GetBaseCosts();

            // Validate results.
            Assert.IsType<TaxiBaseCostsModel>(cost);
        }
    }
}


