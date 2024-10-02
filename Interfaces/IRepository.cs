namespace MauiForeignKey.Interfaces
{
    public interface IRepository
    {
        Task SaveData<T>(T toStore);

        Task SaveListData<T>(List<T> toStore);

        Task<List<T>> GetList<T>(int top = 0) where T : class, new();

        Task<List<T>> GetList<T, TU>(string para, TU val, int top = 0) where T : class, new();

        Task<T> GetData<T>() where T : class, new();

        Task<T> GetData<T, TU>(string para, TU val) where T : class, new();

        Task<T> GetData<T, TU, TV>(string para1, TU val1, string para2, TV val2) where T : class, new();

        Task Delete<T>(T stored);

        Task DeleteList<T>(List<T> list);

        Task<int> GetID<T>() where T : class, new();

        Task<int> Count<T>() where T : class, new();

        Task<int> Count<T, U>(string p, U val) where T : class, new();

        Task DeleteAll();
        
        Task CreateTableWithForeignKey<T>(string key, string key2, T tableName) where T : class, new();

        Task<List<TV>> JoinOn<TV>(string prop, string asName, Tuple<string, string> comp, string? table1, string? table2)
            where TV : class, new();
        
        Task<List<TV>> JoinOn<T, TU, TV>(string prop, string asName, Tuple<string, string> comp2)
            where TV : class, new();
    }
}
