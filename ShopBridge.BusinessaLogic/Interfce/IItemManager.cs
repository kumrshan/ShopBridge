using ShopBridge.BusinessObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopBridge.BusinessaLogic.Interface
{
    public interface IItemManager
    {
        Task<List<Item>> GetItems();
        Task<HttpResponse> AddItem(Item item);
        Task<HttpResponse> DeleteItem(int itemID);
        Task<HttpResponse> updateItem(int itemID, Item item);
    }
}
