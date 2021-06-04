using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopBridge.BusinessObject;
using ShopBridge.DataAccess;
using ShopBridge.DataAccess.Interface;
using System.Linq;
using System.Transactions;

namespace ShopBridge.UnitTest
{
    [TestClass]
    public class TestItemRepositrioy
    {
        private IItemRepositrioy _itemRepository;
        [TestInitialize]
        public void Setup()
        {
            _itemRepository = new ItemRepositrioy();
        }

        [TestMethod]
        public void TestGetITems()
        {
            //Arrange
            Item item = new Item() { Id = 100, Name = "test1234", Description = "testDe", Quantity = 1, Price = 10 };
            _itemRepository.AddItem(item);

            //Act
            var result = _itemRepository.GetItems().GetAwaiter().GetResult().ToList();

            //Assert
            Assert.IsTrue(result.Any(x => x.Name == item.Name));
            _itemRepository.DeleteItem(result.First(x => x.Name == item.Name).Id).GetAwaiter().GetResult();

        }

        [TestMethod]
        public void TestGetITemsByID_ItemsByName()
        {

            //Arrange
            Item item = new Item() { Id = 100, Name = "test1236", Description = "testDe", Quantity = 1, Price = 10 };
            _itemRepository.AddItem(item);
            item.Id = _itemRepository.GetItemByName(item.Name).GetAwaiter().GetResult().Id;

            //Act

            var result = _itemRepository.GetItemByID(item.Id).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(result.Id == item.Id);
            _itemRepository.DeleteItem(item.Id).GetAwaiter().GetResult();
        }
        [TestMethod]
        public void TestUpdateItem()
        {

            //Arrange
            Item item = new Item() { Id = 100, Name = "test1236", Description = "testDe", Quantity = 1, Price = 10 };
            _itemRepository.AddItem(item);
            item.Id = _itemRepository.GetItemByName(item.Name).GetAwaiter().GetResult().Id;
            item.Name = "testUpdate";

            //Act
            var result = _itemRepository.udpateItem(item).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(result);
            var updatedResult = _itemRepository.GetItemByID(item.Id).GetAwaiter().GetResult();
            Assert.IsTrue(updatedResult.Name == item.Name);
            _itemRepository.DeleteItem(item.Id).GetAwaiter().GetResult();
        }
        [TestMethod]
        public void TestDeleteItem()
        {

            //Arrange
            Item item = new Item() { Id = 100, Name = "test1236", Description = "testDe", Quantity = 1, Price = 10 };
            _itemRepository.AddItem(item);
            item.Id = _itemRepository.GetItemByName(item.Name).GetAwaiter().GetResult().Id;

            //Act
            var result = _itemRepository.DeleteItem(item.Id).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(result);
            var updatedResult = _itemRepository.GetItemByID(item.Id).GetAwaiter().GetResult();
            Assert.IsNull(updatedResult);

        }
    }
}
