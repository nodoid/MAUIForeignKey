using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using MauiForeignKey.Interfaces;
using MauiForeignKey.Models;

namespace MauiForeignKey.ViewModels
{
    [QueryProperty("employeeWithDepartment", "Item")]
    public partial class EmployeeDetailsViewModel : BaseViewModel
    {
        IRepository? repository => Startup.ServiceProvider.GetService<IRepository>();
		[ObservableProperty]
		Employee item;
		
        public EmployeeWithDepartment employeeWithDepartment 
        { 
            set { Item = value; OnPropertyChanged("Item"); } 
        }

		public ObservableCollection<Department> Departments { get; } = new();

        [ObservableProperty]
        Department employeeDepartment;

        [RelayCommand]
        private async Task Initialize()
        {
            var dep = await repository.GetList<Department>();
            
            Departments.Clear(); 

            foreach (var d in dep) 
                Departments.Add(d);

            EmployeeDepartment = Departments.FirstOrDefault(d =>d.Id == Item.DepartmentId);
		}

		[RelayCommand]
        private async Task SaveItem()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                await repository.SaveData(Item);
                
                await Shell.Current.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task DeleteItem()
        {
            var confirm = await Shell.Current.DisplayAlert("Confirm", "Do you want to DELETE this item?", "Yes", "No");

            if (confirm)
            {
                await repository.Delete(Item);
                
                await Shell.Current.Navigation.PopAsync();
            }
        }
    }
}