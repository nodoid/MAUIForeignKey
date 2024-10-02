using CommunityToolkit.Mvvm.Messaging;
using MauiForeignKey.Database;
using MauiForeignKey.Interfaces;
using MauiForeignKey.ViewModels;
using MauiForeignKey.Views;


namespace MauiForeignKey.Helpers
{
    public static class InjectionContainer
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            var i = new ServiceCollection();

            i.AddSingleton<IMessenger>(WeakReferenceMessenger.Default).
            AddSingleton<IRepository, SqLiteRepository>();

            services = i;

            return services;
        }
    }
}
