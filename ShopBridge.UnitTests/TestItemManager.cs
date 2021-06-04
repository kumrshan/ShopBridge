using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopBridge.BusinessaLogic.Interface;
using Moq;
using ShopBridge.DataAccess.Interface;
using ShopBridge.BusinessaLogic;
using System.Collections.Generic;
using ShopBridge.BusinessObject;
using System.Threading.Tasks;
using System.Linq;

namespace ShopBridge.UnitTest
{
    [TestClass]
    public class TestItemManager
    {
        private IItemManager _itemManager;
        Mock<IItemRepositrioy> _mockIItemRepository = new Mock<IItemRepositrioy>();
        [TestInitialize]
        public void Setup()
        {
            _itemManager = new ItemManager(_mockIItemRepository.Object);
        }
        
        [TestMethod]
        public void TestGetITems()
        {

            //Arrange
            Item item = new Item() { Id = 1, Name = "test", Description = "testDe", Quantity = 1, Price = 10 };
            IEnumerable<Item> items = new List<Item>() { item };
            _mockIItemRepository.Setup(x => x.GetItems()).Returns(Task.FromResult(items));

            //Act
            var result = _itemManager.GetItems().GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(result.Any());
            Assert.IsTrue(result.Count == 1);
        }
        [TestMethod]
        public void TestDeleteITem()
        {

            //Arrange
            Item item = new Item() { Id = 1, Name = "test", Description = "testDe", Quantity = 1, Price = 10 };
            _mockIItemRepository.Setup(x => x.GetItemByID(It.IsAny<int>())).Returns(Task.FromResult(item));
            _mockIItemRepository.Setup(x => x.DeleteItem(It.IsAny<int>())).Returns(Task.FromResult(true));

            //Act
            var result = _itemManager.DeleteItem(item.Id).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(result.StatusCode==200);
            Assert.IsTrue(result.Message == $"ID {item.Id} Deleted Successfully");
        }
        [TestMethod]
        public void TestDeleteITem_InvalidID()
        {

            //Arrange
            Item item = new Item() { Id = -1, Name = "test", Description = "testDe", Quantity = 1, Price = 10 };
            _mockIItemRepository.Setup(x => x.GetItemByID(It.IsAny<int>())).Returns(Task.FromResult(item));
            _mockIItemRepository.Setup(x => x.DeleteItem(It.IsAny<int>())).Returns(Task.FromResult(true));

            //Act
            var result = _itemManager.DeleteItem(item.Id).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(result.StatusCode == 400);
            Assert.IsTrue(result.Message == $"ID should be va valid number");
        }
        [TestMethod]
        public void TestDeleteITem_IDDoesntExist()
        {

            //Arrange
            Item item = null;
            Item item1 = new Item() { Id = 1, Name = "test", Description = "testDe", Quantity = 1, Price = 10 };
            _mockIItemRepository.Setup(x => x.GetItemByID(It.IsAny<int>())).Returns(Task.FromResult(item));
            _mockIItemRepository.Setup(x => x.DeleteItem(It.IsAny<int>())).Returns(Task.FromResult(true));

            //Act
            var result = _itemManager.DeleteItem(item1.Id).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(result.StatusCode == 400);
            Assert.IsTrue(result.Message == $"item does not exist");
        }
        [TestMethod]
        public void TestUpdateITem()
        {

            //Arrange
            Item item = new Item() { Id = 1, Name = "test", Description = "testDe", Quantity = 1, Price = 10 };
            _mockIItemRepository.Setup(x => x.udpateItem( It.IsAny<Item>())).Returns(Task.FromResult(true));

            //Act
            var result = _itemManager.updateItem(item.Id,item).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(result.StatusCode == 200);
            Assert.IsTrue(result.Message == $"ID {item.Id} updated Successfully");
        }
        [TestMethod]
        public void TestUpdateITem_IDMissmatch()
        {

            //Arrange
            Item item = new Item() { Id = 1, Name = "test", Description = "testDe", Quantity = 1, Price = 10 };
            _mockIItemRepository.Setup(x => x.udpateItem(It.IsAny<Item>())).Returns(Task.FromResult(true));

            //Act
            var result = _itemManager.updateItem(item.Id-1, item).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(result.StatusCode == 400);
            Assert.IsTrue(result.Message == $"ID doesnt match with model id");
        }
        [TestMethod]
        public void TestAddItem()
        {

            //Arrange
            Item item = new Item() { Id = 1, Name = "test", Description = "testDe", Quantity = 1, Price = 10 };
            Item item1 = null;
            _mockIItemRepository.Setup(x => x.GetItemByName(It.IsAny<string>())).Returns(Task.FromResult(item1));
            _mockIItemRepository.Setup(x => x.AddItem(It.IsAny<Item>())).Returns(Task.FromResult(10));

            //Act
            var result = _itemManager.AddItem(item).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(result.StatusCode == 200);
            Assert.IsTrue(result.Message == $"item { item.Id} inserted successfully");
        }
        [TestMethod]
        public void TestAddItem_InvalidName()
        {

            //Arrange
            Item item = new Item() { Id = 11, Name = "test", Description = "testDe", Quantity = 1, Price = 10 };
            _mockIItemRepository.Setup(x => x.GetItemByName(It.IsAny<string>())).Returns(Task.FromResult(item));

            //Act
            var result = _itemManager.AddItem(item).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(result.StatusCode == 400);
            Assert.IsTrue(result.Message == $"Item Name Already Exist in inventory");
        }

        [TestMethod]
        public void TestAddItem_EmptyName()
        {

            //Arrange
            Item item = new Item() { Id = 11, Name = "", Description = "testDe", Quantity = 1, Price = 10 };

            //Act
            var result = _itemManager.AddItem(item).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(result.StatusCode == 400);
            Assert.IsTrue(result.Message == $"Name cannot be empty");
        }
        [TestMethod]
        public void TestAddItem_EmptyPrice()
        {

            //Arrange
            Item item = new Item() { Id = 11, Name = "test", Description = "testDe", Quantity = 1, Price = 0 };

            //Act
            var result = _itemManager.AddItem(item).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(result.StatusCode == 400);
            Assert.IsTrue(result.Message == $"Price cannot be empty");
        }
        [TestMethod]
        public void TestAddItem_EmptyQuantity()
        {

            //Arrange
            Item item = new Item() { Id = 11, Name = "test", Description = "testDe", Quantity = 0, Price = 10 };

            //Act
            var result = _itemManager.AddItem(item).GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(result.StatusCode == 400);
            Assert.IsTrue(result.Message == $"Quantity cannot be empty");
        }
    }
}
