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

        public VehiclesViewModel()
        {
            SearchCommand = new RelayCommand(async () => await SearchAsync(), () => CanSearch);
        }

        private async Task SearchAsync()
        {
            IVehicleRepository vehicleRepository = new FakeVehicleRepository();

            var vehicles = await vehicleRepository.GetAsync();

            if (Code!=null)
            {
                vehicles = vehicles
               .Where(v => v.Vin.StartsWith(Code.Substring(1, 2)));
            }

            Vehicles = vehicles.ToList();
        }

        public bool CanSearch => true;
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
