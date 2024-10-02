using MauiForeignKey.Helpers;
using MauiForeignKey.Views;

namespace MauiForeignKey;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		RegisterRoutes();
	}

	void RegisterRoutes()
	{
		Routing.RegisterRoute(Constants.DepartmentDetailsRoute,
			typeof(DepartmentDetailsView));

		Routing.RegisterRoute(Constants.EmployeeDetailsRoute,
			typeof(EmployeeDetailsView));
	}
}
