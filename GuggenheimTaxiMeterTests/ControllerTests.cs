using GuggenheimTaxiMeter.Controllers;
using GuggenheimTaxiMeter.Models;
using GuggenheimTaxiMeter.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace GuggenheimTaxiMeterTests {
    public class ControllerTests {
        [Fact]
        public void CostCalculatorControllerValidReturnType() {
            // Get mock model.
            TaxiMeterModel model = new TaxiMeterModel();

            // Create mock service.
            Mock<ICostCalculatorService> mockCalculator = new Mock<ICostCalculatorService>();
            mockCalculator.Setup(calculator => calculator.CalculateCost(model)).Returns(MockCalculateCost());

            // Create controller instance.
            CostCalculatorController controller = new CostCalculatorController(mockCalculator.Object, null);

            // Execute action.
            TaxiCostBreakdown cost = controller.CalculateCost(model);

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

            // Create mock service.
            Mock<IDatabaseInterfaceService> mockDataBaseInterface = new Mock<IDatabaseInterfaceService>();
            mockDataBaseInterface.Setup(dbInterface => dbInterface.GetBaseCosts()).Returns(MockBaseCosts());

            // Create service.
            ICostCalculatorService costCalculator = new CostCalculatorService(mockDataBaseInterface.Object);

            // Create controller instance.
            CostCalculatorController controller = new CostCalculatorController(costCalculator, mockDataBaseInterface.Object);

            // Execute action.
            TaxiCostBreakdown cost = controller.CalculateCost(model);

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

            // Create mock service.
            Mock<IDatabaseInterfaceService> mockDataBaseInterface = new Mock<IDatabaseInterfaceService>();
            mockDataBaseInterface.Setup(dbInterface => dbInterface.GetBaseCosts()).Returns(MockBaseCosts());

            // Create service.
            ICostCalculatorService costCalculator = new CostCalculatorService(mockDataBaseInterface.Object);

            // Create controller instance.
            CostCalculatorController controller = new CostCalculatorController(costCalculator, mockDataBaseInterface.Object);

            // Execute action.
            TaxiCostBreakdown cost = controller.CalculateCost(model);

            // Validate results.
            Assert.Equal(9.75m, cost.Total);
        }
        private TaxiCostBreakdown MockCalculateCost() {
            return new TaxiCostBreakdown();
        }
        private TaxiBaseCostsModel MockBaseCosts() {
            TaxiBaseCostsModel baseCosts = new TaxiBaseCostsModel();
            baseCosts.BaselineCost = 3.0m;
            baseCosts.UnitCost = 0.35m;
            baseCosts.NightSurcharge = 0.5m;
            baseCosts.PeakSurcharge = 1.0m;

            Dictionary<string, decimal> stateSurcharges = new Dictionary<string, decimal>();
            stateSurcharges.Add("NY", 0.5m);
            baseCosts.StateSurcharges = stateSurcharges;

            return baseCosts;
        }
    }
}


