
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShopBridge.BusinessaLogic.Interface;
using ShopBridge.BusinessObject;
using ShopBridge.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ShopBridge.UnitTest
{
    [TestClass]
    public class TestItemController
    {

        private ItemController _itemController;
        Mock<IItemManager> _mockIItemManager = new Mock<IItemManager>();
        [TestInitialize]
        public void Setup()
        {
            _itemController = new ItemController(_mockIItemManager.Object);
        }

        [TestMethod]
        public async Task TestGetITems()
        {
            //Arrange
            Item item = new Item() { Id = 100, Name = "test123", Description = "testDe", Quantity = 1, Price = 10 };
            List<Item> items = new List<Item>() { item };
            _mockIItemManager.Setup(x => x.GetItems()).Returns(Task.FromResult(items));

            //Act
            var result = await _itemController.Get();

            //Assert
            Assert.AreEqual(200, ((ObjectResult)result.Result).StatusCode);
        }

        [TestMethod]
        public async Task TestGetITems_NoContent()
        {
            //Arrange
            List<Item> items = new List<Item>() { };
            _mockIItemManager.Setup(x => x.GetItems()).Returns(Task.FromResult(items));

            //Act
            var result = await _itemController.Get();

            //Assert
            Assert.AreEqual(204, ((StatusCodeResult)result.Result).StatusCode);
        }
        [TestMethod]
        public async Task TestAddITem()
        {
            //Arrange
            Item item = new Item() { Id = 100, Name = "test123", Description = "testDe", Quantity = 1, Price = 10 };
            HttpResponse httpResponse = new HttpResponse() { StatusCode = 200, Message = "Success" };
            _mockIItemManager.Setup(x => x.AddItem(It.IsAny<Item>())).Returns(Task.FromResult(httpResponse));

            //Act
            var result = await _itemController.AddItem(item);

            //Assert
            Assert.AreEqual(201, ((ObjectResult)result.Result).StatusCode);
        }
        [TestMethod]
        public async Task TestAddITem_BadRequest()
        {
            //Arrange
            Item item = new Item() { Id = 100, Name = "test123", Description = "testDe", Quantity = 1, Price = 10 };
            HttpResponse httpResponse = new HttpResponse() { StatusCode = 400, Message = "BadRequest" };
            _mockIItemManager.Setup(x => x.AddItem(It.IsAny<Item>())).Returns(Task.FromResult(httpResponse));

            //Act
            var result = await _itemController.AddItem(item);

            //Assert
            Assert.AreEqual(400, ((ObjectResult)result.Result).StatusCode);
        }
        [TestMethod]
        public async Task TestUpdateITem()
        {
            //Arrange
            Item item = new Item() { Id = 100, Name = "test123", Description = "testDe", Quantity = 1, Price = 10 };
            HttpResponse httpResponse = new HttpResponse() { StatusCode = 200, Message = "Success" };
            _mockIItemManager.Setup(x => x.updateItem(It.IsAny<int>(), It.IsAny<Item>())).Returns(Task.FromResult(httpResponse));

            //Act
            var result = await _itemController.UpdateItem(item.Id, item);

            //Assert
            Assert.AreEqual(202, ((ObjectResult)result.Result).StatusCode);
        }
        [TestMethod]
        public async Task TestUpdteITem_BadRequest()
        {
            //Arrange
            Item item = new Item() { Id = 100, Name = "test123", Description = "testDe", Quantity = 1, Price = 10 };
            HttpResponse httpResponse = new HttpResponse() { StatusCode = 400, Message = "BadRequest" };
            _mockIItemManager.Setup(x => x.updateItem(It.IsAny<int>(), It.IsAny<Item>())).Returns(Task.FromResult(httpResponse));

            //Act
            var result = await _itemController.UpdateItem(item.Id, item);

            //Assert
            Assert.AreEqual(400, ((ObjectResult)result.Result).StatusCode);
        }
        [TestMethod]
        public async Task TestDeleteITem()
        {
            //Arrange
            Item item = new Item() { Id = 100, Name = "test123", Description = "testDe", Quantity = 1, Price = 10 };
            HttpResponse httpResponse = new HttpResponse() { StatusCode = 200, Message = "Success" };
            _mockIItemManager.Setup(x => x.DeleteItem(It.IsAny<int>())).Returns(Task.FromResult(httpResponse));

            //Act
            var result = await _itemController.DeleteItem(item.Id);

            //Assert
            Assert.AreEqual(202, ((ObjectResult)result.Result).StatusCode);
        }
        [TestMethod]
        public async Task TestDeleteITem_BadRequest()
        {
            //Arrange
            Item item = new Item() { Id = 100, Name = "test123", Description = "testDe", Quantity = 1, Price = 10 };
            HttpResponse httpResponse = new HttpResponse() { StatusCode = 400, Message = "BadRequest" };
            _mockIItemManager.Setup(x => x.DeleteItem(It.IsAny<int>())).Returns(Task.FromResult(httpResponse));

            //Act
            var result = await _itemController.DeleteItem(item.Id);

            //Assert
            Assert.AreEqual(400, ((ObjectResult)result.Result).StatusCode);
        }
    }
}
