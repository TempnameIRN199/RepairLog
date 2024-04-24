using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using SQLite;
using SQLitePCL;

namespace RepairLog.Resources.Classes
{
    public class TodoItemDatabase
    {
        SQLiteAsyncConnection Database;

        public TodoItemDatabase()
        {
        }

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<TodoItem>();
        }

        public async Task<List<TodoItem>> GetItemsAsync()
        {
            await Init();
            return await Database.Table<TodoItem>().ToListAsync();
        }

        public async Task<List<TodoItem>> GetItemsNotDoneAsync()
        {
            await Init();
            return await Database.Table<TodoItem>().Where(t => t.Done).ToListAsync();

            // SQL queries are also possible
            //return await Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public async Task<TodoItem> GetItemAsync(int id)
        {
            await Init();
            return await Database.Table<TodoItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(TodoItem item)
        {
            await Init();
            if (item.ID != 0)
                return await Database.UpdateAsync(item);
            else
                return await Database.InsertAsync(item);
        }

        public async Task<int> DeleteItemAsync(TodoItem item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }
    }

    public class DatabaseConnection
    {
    }

    public static class Constants
    {
        public const string DatabaseFilename = "TodoSQLite.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
    }
    /*
    public class SQLiteHelper
    {
        private SQLiteConnection connection;

        public SQLiteHelper(string databasePath)
        {
            // Инициализация SQLitePCLRaw
            SQLitePCL.Batteries.Init();

            // Создание подключения к базе данных
            connection = new SQLiteConnection(databasePath);

            // Открытие подключения
            connection.Open();

            // Создание таблицы (пример)
            CreateTable();
        }

        // Метод для создания таблицы (пример)
        private void CreateTable()
        {
            using (var statement = connection.Prepare("CREATE TABLE IF NOT EXISTS MyTable (Id INTEGER PRIMARY KEY, Name TEXT)"))
            {
                statement.Step();
            }
        }

        // Метод для выполнения запроса на вставку данных
        public void InsertData(string name)
        {
            using (var statement = connection.Prepare("INSERT INTO MyTable (Name) VALUES (?)"))
            {
                statement.Bind(1, name);
                statement.Step();
            }
        }

        // Метод для выполнения запроса на выборку данных
        public List<string> GetData()
        {
            var result = new List<string>();

            using (var statement = connection.Prepare("SELECT Name FROM MyTable"))
            {
                while (statement.Step() == SQLiteResult.ROW)
                {
                    result.Add((string)statement[0]);
                }
            }

            return result;
        }

        // Метод для закрытия подключения
        public void CloseConnection()
        {
            connection.Dispose();
        }
    }*/
}
