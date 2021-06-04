using ShopBridge.BusinessObject;
using ShopBridge.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using Dapper;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.DataAccess
{
    public class ItemRepositrioy : IItemRepositrioy
    {
        private static string connectionString;

        public ItemRepositrioy()
        {
            SQLiteConnectionStringBuilder sQLiteConnectionStringBuilder = new SQLiteConnectionStringBuilder();
            sQLiteConnectionStringBuilder.DataSource = AppDomain.CurrentDomain.BaseDirectory + "shopBridge.db";
            connectionString = sQLiteConnectionStringBuilder.ConnectionString;
        }
        public async Task<int> AddItem(Item item)
        {
            using (IDbConnection con = new SQLiteConnection(connectionString,true))
            {
                string query = @"insert into item (Name,Price,Description,Quantity) 
                                 values(@Name,@Price,@Description,@Quantity);
                                 SELECT last_insert_rowid()";
                return await con.ExecuteScalarAsync<int>(query, item);

            }

        }

        public async Task<bool> DeleteItem(int itemID)
        {
            using (IDbConnection con = new SQLiteConnection(connectionString, true))
            {
                string query = @"Delete from Item where ID=@ID";
                return await con.ExecuteAsync(query, new { ID = itemID }) > 0;
            }
        }

        public async Task<IEnumerable<Item>> GetItems()
        {
            using (IDbConnection con = new SQLiteConnection(connectionString, true))
            {
                string query = @"select * from Item";
                return await con.QueryAsync<Item>(query, null);
            }
        }
        public async Task<Item> GetItemByID(int itemID)
        {
            using (IDbConnection con = new SQLiteConnection(connectionString, true))
            {
                string query = @"select * from Item where ID =@ID";
                var result= await con.QueryAsync<Item>(query, new { ID = itemID });
                return result.FirstOrDefault();
            }
        }
        public async Task<Item> GetItemByName(string name)
        {
            using (IDbConnection con = new SQLiteConnection(connectionString, true))
            {
                string query = @"select * from Item where name =@Name";
                var result = await con.QueryAsync<Item>(query, new { Name = name });
                return result.FirstOrDefault();
            }
        }

        public async Task<bool> udpateItem(Item item)
        {
            using (IDbConnection con = new SQLiteConnection(connectionString, true))
            {
                string query = @"Update Item set Name =@Name,Price=@Price,
                                 Description=@Description,Quantity=@Quantity 
                                 WHERE ID=@ID";
                return await con.ExecuteAsync(query, item) > 0;
            }
        }


    }
}
