using MauiForeignKey.Interfaces;
using MauiForeignKey.Models;
using Newtonsoft.Json;
using SQLite;

#if DEBUG
using System.Diagnostics;
#endif

namespace MauiForeignKey.Database
{
    public class SqLiteRepository : IRepository
    {
        readonly SQLiteAsyncConnection connection;

        public SqLiteRepository()
        {
            connection = App.SQLConnection;

            Task.Run(async () => await CreateTables());
        }

        public async Task SaveData<T>(T toStore)
        {
            try
            {
                await connection.InsertOrReplaceAsync(toStore);
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex.Message);
#endif
            }
        }

        public async Task SaveListData<T>(List<T> toStore)
        {
            try
            {
                foreach (var ts in toStore)
                    await connection.InsertOrReplaceAsync(ts);
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine($"{ex.Message}--{ex.InnerException?.Message}");
#endif
            }
        }

        public async Task<int> Count<T>() where T : class, new()
        {
            var data = await GetList<T>();
            return data.Count;
        }

        public async Task<List<T>> GetList<T>(int top = 0) where T : class, new()
        {
            var sql = string.Format("SELECT * FROM {0}", GetName(typeof(T).ToString()));
            var list = await connection.QueryAsync<T>(sql, string.Empty);
            if (list.Count != 0)
            {
                if (top != 0)
                {
                    list = list.Take(top).ToList();
                }
            }

            return list;
        }

        public async Task<List<T>> GetList<T, TU>(string para, TU val, int top = 0) where T : class, new()
        {
            var sql = string.Format("SELECT * FROM {0} WHERE {1}=?", GetName(typeof(T).ToString()), para);
            var list = await connection.QueryAsync<T>(sql, val);
            if (list.Count != 0)
            {
                if (top != 0)
                {
                    list = list.Take(top).ToList();
                }
            }

            return list;
        }

        public async Task<T> GetData<T>() where T : class, new()
        {
            var sql = string.Format("SELECT * FROM {0}", GetName(typeof(T).ToString()));
            var list = await connection.QueryAsync<T>(sql, string.Empty);
            return list != null ? list.FirstOrDefault() : default;
        }

        public async Task<T> GetData<T, TU>(string para, TU val) where T : class, new()
        {
            var sql = string.Format("SELECT * FROM {0} WHERE {1}=?", GetName(typeof(T).ToString()), para);
            var list = await connection.QueryAsync<T>(sql, val);
            return list != null ? list.FirstOrDefault() : default;
        }

        public async Task DeleteAll()
        {
            try
            {
                await connection.DeleteAllAsync<BaseTable>();
                await connection.DeleteAllAsync<Employee>();
                await connection.DeleteAllAsync<Department>();
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine($"{ex.Message}--{ex.InnerException?.Message}");
#endif
            }

        }

        public async Task Delete<T>(T stored)
        {
            try
            {
                await connection.DeleteAsync(stored);
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine($"{ex.Message}--{ex.InnerException?.Message}");
#endif
            }
        }

        public async Task DeleteList<T>(List<T> list)
        {
            try
            {
                foreach (var l in list)
                    await connection.DeleteAsync(l);
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine($"{ex.Message}--{ex.InnerException?.Message}");
#endif
            }
        }

        public async Task<T> GetData<T, TU, TV>(string para1, TU val1, string para2, TV val2) where T : class, new()
        {
            var sql = string.Format("SELECT * FROM {0} WHERE {1}=? AND {2}=?", GetName(typeof(T).ToString()), para1, para2);
            var list = await connection.QueryAsync<T>(sql, val1, val2);
            return list != null ? list.FirstOrDefault() : default;
        }

        public async Task<int> GetID<T>() where T : class, new()
        {
            string sql = string.Format("SELECT last_insert_rowid() FROM {0}", GetName(typeof(T).ToString()));
            var id = await connection.ExecuteScalarAsync<int>(sql, string.Empty);
            return id;
        }

        async Task CreateTables()
        {
            await connection.CreateTableAsync<BaseTable>();
            await connection.CreateTableAsync<Employee>();
            await connection.CreateTableAsync<Department>();
        }

        string GetName(string name)
        {
            var list = name.Split('.').ToList();
            if (list.Count == 1)
            {
                return list[0];
            }

            return list[^1];
        }

        public async Task<int> Count<T, U>(string p, U val) where T : class, new()
        {
            var sql = string.Format("SELECT * FROM {0} WHERE {1}={2}", GetName(typeof(T).ToString()), p, val);
            var list = await connection.QueryAsync<T>(sql, string.Empty);
            return list.Count;
        }

        public async Task CreateTableWithForeignKey<T>(string key, string key2, T tableName) where T : class, new()
        {
            var serialised = JsonConvert.SerializeObject(tableName);
            var classname = GetName(typeof(T).ToString());
            var sql =
                $"CREATE TABLE IF NOT EXISTS {classname} ({serialised} FOREIGN KEY({key}) REFERENCES {classname}({key2})";
            await connection.ExecuteAsync(sql);
        }

        public async Task<List<TV>> JoinOn<T, TU, TV>(string prop, string asName, Tuple<string, string> comp, 
            string? table1, string? table2) where TV : class, new()
        {
            var tbl1 = GetName(typeof(T).ToString());
            var tbl2 = GetName(typeof(TU).ToString());
            try
            {
                var sql = table1 is null ? $"SELECT e.*, d.{prop} as {asName} FROM {tbl1} e JOIN {tbl2} d ON e.{comp.Item1}=d.{comp.Item2}" :
                        $"SELECT e.*, d.{prop} as {asName} FROM {table1} e JOIN {table2} d ON e.{comp.Item1}=d.{comp.Item2}";
                var list = await connection.QueryAsync<TV>(sql);
                return list ?? new List<TV>();
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine($"{ex.Message}--{ex.InnerException?.Message}");
#endif
                return new List<TV>();
            }
        }
    }
}
