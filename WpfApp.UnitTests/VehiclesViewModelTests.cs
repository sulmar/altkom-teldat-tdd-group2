using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using WpfApp.Models;
using WpfApp.Repositories;
using WpfApp.ViewModels;

namespace WpfApp.UnitTests
{

    // Install-Package NUnit3TestAdapter

    public class TestVehicleRepository : IVehicleRepository
    {
        private IEnumerable<Vehicle> vehicles;

        public TestVehicleRepository()
        {
            vehicles = new List<Vehicle>
            {
                new Vehicle { Id = 1, Manufacturer = "Fiat", Model = "126p", Vin="ZFADD10A9B2566189" },

                new Vehicle { Id = 2, Manufacturer = "Ford", Model = "Focus", Vin="1FMCU9H9XDUVV0001" },

              new Vehicle { Id = 3, Manufacturer = "Ferrari", Model = "Testarossa", Vin="ZFF65LJA5A0174623" },
            };
        }

        public Task<IEnumerable<Vehicle>> GetAsync()
        {
            return Task.FromResult(vehicles);
        }
    }

    public class VehiclesViewModelTests
    {
        private VehiclesViewModel vehiclesViewModel;
        private IVehicleRepository vehicleRepository;

        [SetUp]
        public void Setup()
        {
            vehicleRepository = new TestVehicleRepository();
            vehiclesViewModel = new VehiclesViewModel(vehicleRepository);
        }

        [Test]
        public void CanSearch_EmptyCode_ReturnFalse()
        {
            // Arrange
            vehiclesViewModel.Code = string.Empty;

            // Act
            var result = vehiclesViewModel.CanSearch;

            // Assets
            Assert.That(result, Is.False);
        }

        [Test]
        public void CanSearch_NotEmptyCode_ReturnFalse()
        {
            // Arrange
            vehiclesViewModel.Code = "AA";

            // Act
            var result = vehiclesViewModel.CanSearch;

            // Assets
            Assert.That(result, Is.True);
        }


        [Test]
        public async Task SearchAsync_ZFCode_ReturnVehicles()
        {
            // Arrange
            vehiclesViewModel.Code = "ZF";

            // Act
            await vehiclesViewModel.SearchAsync();

            // Assert
            Assert.That(vehiclesViewModel.Vehicles, Is.Not.Empty);
        }
    }


}
