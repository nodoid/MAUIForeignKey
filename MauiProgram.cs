using MauiForeignKey.Helpers;
using MauiForeignKey.Models;
using MauiForeignKey.ViewModels;
using MauiForeignKey.Views;
using Microsoft.Extensions.Logging;

namespace MauiForeignKey;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif


		builder.Services.AddTransient<BaseViewModel>().AddTransient<DepartmentDetailsViewModel>()
			.AddTransient<EmployeeDetailsViewModel>().AddTransient<DepartmentDetailsView>()
			.AddTransient<EmployeeDetailsView>();
		
		builder.Services.AddTransient(
			s => ActivatorUtilities.CreateInstance<GenericListViewModel<Department>>(
				s));

		builder.Services.AddTransient(
			s => ActivatorUtilities.CreateInstance<GenericListViewModel<EmployeeWithDepartment>>(
				s));
		
		return builder.Build();
	}
}
