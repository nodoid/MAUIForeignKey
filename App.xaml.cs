namespace MauiForeignKey;
using SQLite;

public partial class App : Application
{
    public static IServiceProvider Service { get; set; }
    public static SQLiteAsyncConnection SQLConnection { get; set; }
    public static App Self { get; private set; }
    public App()
    {
        App.Self = this;

        Service = Startup.Init();
        
        InitializeComponent();

        SetupSQLiteConnection();

        
        MainPage = new AppShell();
    }
    
    void SetupSQLiteConnection()
    {
        var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        path = Path.Combine(path, "linked.db3");

        var options = new SQLiteConnectionString(path, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite, true, "a26e9a9b2b36428996cd63fd725de71e");
        try
        {
            App.SQLConnection = new SQLiteAsyncConnection(options);
        }
        catch (Exception ex)
        {
#if DEBUG
            Console.WriteLine($"Connection could  not be made : ex.Message = {ex.Message} - inner = {ex.InnerException?.Message}");
#endif
        }
    }
}