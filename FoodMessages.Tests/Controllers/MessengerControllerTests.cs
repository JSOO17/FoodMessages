using FoodMessages.Controllers;
using FoodMessages.Interfaces;
using FoodMessages.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FoodMessages.Tests.Controllers
{
    [TestClass]
    public class MessengerControllerTests
    {
        [TestMethod]
        public void PostMessageSuccessfull()
        {
            var mockMessenger = new Mock<IMessenger>();

            mockMessenger
                .Setup(p => p.SendMessage(It.IsAny<MessageModel>()))
                .Returns(Task.CompletedTask);

            var controller = new MessageController(mockMessenger.Object);

            var result = (OkObjectResult)controller.PostMessage(new MessageModel
            {
                Body = "Buenatalde",
                To = "+414141231"
            });

            Assert.AreEqual(result.StatusCode , 200);
        }

        [TestMethod]
        public void PostMessageError()
        {
            var mockMessenger = new Mock<IMessenger>();

            mockMessenger
                .Setup(p => p.SendMessage(It.IsAny<MessageModel>()))
                .Throws(new Exception("message error"));

            var controller = new MessageController(mockMessenger.Object);

            var result = (ObjectResult)controller.PostMessage(new MessageModel
            {
                Body = "Buenatalde",
                To = "+414141231"
            });

            Assert.AreEqual(result.StatusCode, 500);
        }
    }
}
