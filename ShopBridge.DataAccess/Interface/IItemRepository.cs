using ShopBridge.BusinessObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopBridge.DataAccess.Interface
{
    public interface IItemRepositrioy
    {
        Task<IEnumerable<Item>> GetItems();
        Task<Item> GetItemByID(int itemID);
        Task<Item> GetItemByName(string name);
        Task<int> AddItem(Item item);
        Task<bool> DeleteItem(int itemID);
        Task<bool> udpateItem(Item item);
    }
}
