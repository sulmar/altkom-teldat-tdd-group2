using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WpfApp.Models;
using WpfApp.ViewModels;

namespace WpfApp.Repositories
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<Vehicle>> GetAsync();
        

    }

    public static class IVehicleRepositoryExtensions
    {
        public static Task<IEnumerable<Vehicle>> GetAsync(this IEnumerable<Vehicle> vehicles, string code) => Task.FromResult(vehicles
                .Where(v => v.Vin.StartsWith(code)));
    }

    public class FakeVehicleRepository : IVehicleRepository
    {
        private IEnumerable<Vehicle> vehicles;

        public FakeVehicleRepository()
        {
            VehicleFaker vehicleFaker = new VehicleFaker();

            vehicles = vehicleFaker.Generate(50);
        }

        public Task<IEnumerable<Vehicle>> GetAsync()
        {
            return Task.FromResult(vehicles);
        }
    }

    public class VehicleFaker : Faker<Vehicle>
    {
        public VehicleFaker()
        {
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.Manufacturer, f => f.Vehicle.Manufacturer());
            RuleFor(p => p.Model, f => f.Vehicle.Model());
            RuleFor(p => p.Vin, f => f.Vehicle.Vin());
        }
    }
}
