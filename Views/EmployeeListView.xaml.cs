using MauiForeignKey.Helpers;
using MauiForeignKey.Models;
using MauiForeignKey.ViewModels;

namespace MauiForeignKey.Views;

public partial class EmployeeListView : ContentPage
{
    private GenericListViewModel<EmployeeWithDepartment> Vm { get; } = new GenericListViewModel<EmployeeWithDepartment>();


    public EmployeeListView()
	{
		InitializeComponent();
        
        BindingContext = Vm;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        Vm.Route = Constants.EmployeeDetailsRoute;
        await Vm.GetItemsCommand.ExecuteAsync(null);
    }
}