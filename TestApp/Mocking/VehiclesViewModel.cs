using Bogus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TestApp.Fundamentals;

namespace TestApp.Mocking
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Vin { get; set; }
    }

    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class VehiclesViewModel : BaseViewModel
    {
        #region Vehicles
        private ICollection<Vehicle> vehicles;
        public ICollection<Vehicle> Vehicles
        {
            get 
            {
                return vehicles;
            }
            private set
            {
                this.vehicles = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public string Code { get; set; }

        public ICommand SearchCommand { get; set; }

        private readonly IVehicleRepository vehicleRepository;

        public VehiclesViewModel()
        {
            SearchCommand = new RelayCommand(async () => await SearchAsync(), () => CanSearch);    
        }

        private async Task SearchAsync()
        {
            var vehicles = await vehicleRepository.GetAsync();

            Vehicles = vehicles.Where(v=>v.Vin.Substring(0,2) == Code).ToList();
        }
        
        public bool CanSearch => true;
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

    public class FakeVehicleRepository : IVehicleRepository
    {


        public Task<ICollection<Vehicle>> GetAsync()
        {
            throw new NotImplementedException();
        }
    }

    public interface IVehicleRepository     {
        Task<ICollection<Vehicle>> GetAsync();
    }

    public interface ICommand
    {
        event EventHandler CanExecuteChanged;

        void Execute(object parameter);
        bool CanExecute(object parameter);
    }

    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Action execute;
        private readonly Func<bool> canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute.Invoke();
        }

        public void Execute(object parameter)
        {
            //if (execute!=null)
            //    execute.Invoke();

            execute?.Invoke();
        }

        public void OnCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
