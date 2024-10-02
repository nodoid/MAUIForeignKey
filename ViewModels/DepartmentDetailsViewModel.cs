using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using MauiForeignKey.Interfaces;
using MauiForeignKey.Models;

namespace MauiForeignKey.ViewModels
{
    [QueryProperty("Item", "Item")]
    public partial class DepartmentDetailsViewModel : BaseViewModel
    {
        IRepository? repository => Startup.ServiceProvider.GetService<IRepository>();
        [ObservableProperty]
        Department item;

        public ObservableCollection<Employee> EmployeesDepartment { get; } = new();

		[RelayCommand]
		private async Task Initialize()
        {
            var emp = await repository.GetList<Employee, int>("DepartmentId", Item.Id);

			EmployeesDepartment.Clear();

			foreach (var e in emp)
				EmployeesDepartment.Add(e);
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