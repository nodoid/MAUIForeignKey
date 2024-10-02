namespace MauiForeignKey.Views;

public partial class DepartmentDetailsView : ContentPage
{
	public DepartmentDetailsView()
	{
		InitializeComponent();
		
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();

		await viewModel.InitializeCommand.ExecuteAsync(null);
	}
}