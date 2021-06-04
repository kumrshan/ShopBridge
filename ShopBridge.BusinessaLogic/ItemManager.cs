using ShopBridge.BusinessaLogic.Interface;
using ShopBridge.BusinessObject;
using ShopBridge.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.BusinessaLogic
{


    public class ItemManager : IItemManager
    {

        private readonly IItemRepositrioy _itemRepositrioy;
        public ItemManager(IItemRepositrioy itemRepositrioy)
        {
            _itemRepositrioy = itemRepositrioy;
        }
        public async Task<HttpResponse> AddItem(Item item)
        {
            try
            {
                HttpResponse httpResponse = await ValidateItem(item);
                if (httpResponse.StatusCode == 200)
                {
                    item.Id = await _itemRepositrioy.AddItem(item);
                    if (item.Id > 0)
                    {
                        httpResponse.StatusCode = 200;
                        httpResponse.Message = $"item { item.Id} inserted successfully";
                    }
                }
                return httpResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async Task<HttpResponse> ValidateItem(Item item)
        {
            HttpResponse httpResponse = new HttpResponse();
            httpResponse.StatusCode = 200;
            if (string.IsNullOrWhiteSpace(item.Name) || item.Name=="string")
            {
                httpResponse.Message = "Name cannot be empty";
                httpResponse.StatusCode = 400;
            }
            else if (item.Price == 0)
            {
                httpResponse.Message = "Price cannot be empty";
                httpResponse.StatusCode = 400;
            }
            else if (item.Quantity == 0)
            {
                httpResponse.Message = "Quantity cannot be empty";
                httpResponse.StatusCode = 400;
            }
            var existItem = await _itemRepositrioy.GetItemByName(item.Name);
            if (existItem!=null && existItem.Name != null)
            {
                httpResponse.Message = "Item Name Already Exist in inventory";
                httpResponse.StatusCode = 400;
            }
            return httpResponse;
        }
        public async Task<HttpResponse> DeleteItem(int itemID)
        {
            try
            {
                HttpResponse httpResponse = new HttpResponse();
                httpResponse.StatusCode = 400;
                if (itemID <= 0)
                {
                    httpResponse.Message = "ID should be va valid number";
                }
                else
                {
                    var item = await _itemRepositrioy.GetItemByID(itemID);
                    if (item != null && item.Id!=0)
                    {
                        var isDeleted = await _itemRepositrioy.DeleteItem(itemID);
                        httpResponse.Message = $"ID {itemID} Deleted Successfully";
                        httpResponse.StatusCode = 200;
                    }
                    else
                    {
                        httpResponse.Message = "item does not exist";
                    }
                }

                return httpResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Item>> GetItems()
        {
            try
            {
                var result = await _itemRepositrioy.GetItems();
                return result.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HttpResponse> updateItem(int itemID, Item item)
        {
            try
            {
                HttpResponse httpResponse = new HttpResponse();
                httpResponse.StatusCode = 400;
                if (itemID != item.Id)
                {
                    httpResponse.Message = "ID doesnt match with model id";
                }
                else
                {

                    var isupdated = await _itemRepositrioy.udpateItem(item);
                    httpResponse.Message = $"ID {itemID} updated Successfully";
                    httpResponse.StatusCode = 200;
                }
                return httpResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
