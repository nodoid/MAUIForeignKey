using MauiForeignKey.ViewModels;

namespace MauiForeignKey.Views;

public partial class EmployeeDetailsView : ContentPage
{
	public EmployeeDetailsView(EmployeeDetailsViewModel vm)
	{
		InitializeComponent();
    }

	protected override async void OnAppearing()
	{
		base.OnAppearing();

		await viewModel.InitializeCommand.ExecuteAsync(null);
	}
}