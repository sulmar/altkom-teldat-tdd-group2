using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp.Models;
using WpfApp.Repositories;

namespace WpfApp.ViewModels
{
    public class VehiclesViewModel : BaseViewModel
    {
        #region Vehicles
        private IEnumerable<Vehicle> vehicles;
        public IEnumerable<Vehicle> Vehicles
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

        public ICommand SearchCommand { get; }

        private IVehicleRepository vehicleRepository;

        public VehiclesViewModel()
            : this(new FakeVehicleRepository())
        {

        }

        public VehiclesViewModel(IVehicleRepository vehicleRepository)
        {
            this.vehicleRepository = vehicleRepository;

            SearchCommand = new RelayCommand(async () => await SearchAsync(), () => CanSearch);
           
        }

        public async Task SearchAsync()
        {
            var vehicles = await vehicleRepository.GetAsync();

            vehicles = await vehicles.GetAsync(Code);

            Vehicles = vehicles.ToList();
        }

        public bool CanSearch => !string.IsNullOrWhiteSpace(Code);
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
            execute?.Invoke();
        }

        public void OnCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
