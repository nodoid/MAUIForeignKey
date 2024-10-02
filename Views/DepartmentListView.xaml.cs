using MauiForeignKey.Helpers;
using MauiForeignKey.Models;
using MauiForeignKey.ViewModels;

namespace MauiForeignKey.Views;

public partial class DepartmentListView : ContentPage
{
	private GenericListViewModel<Department> Vm { get; } = new GenericListViewModel<Department>();

	public DepartmentListView()
	{
		InitializeComponent();
		BindingContext = Vm;
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        Vm.Route = Constants.DepartmentDetailsRoute;
        
		await Vm.GetItemsCommand.ExecuteAsync(null);
    }
}