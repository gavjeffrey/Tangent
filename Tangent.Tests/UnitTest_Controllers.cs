using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tangent.Web.Models;

namespace Tangent.Tests
{
    [TestClass]
    public class UnitTest_Controllers
    {
        Mock<Web.Infrastructure.IHttpClient> httpClientMock;
        [TestInitialize]
        public void Setup()
        {
            httpClientMock = new Mock<Web.Infrastructure.IHttpClient>();

            httpClientMock.Setup(call =>
                    call.PostAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => { return null; });

        }

        [TestMethod]
        public async Task Test_HomeControllerIndex_ReturnsViewModel()
        {
            //Arrange
            httpClientMock.Setup(call =>
                  call.ReadAsStringAsync(It.Is<string>(x => x == "employee")))
              .Returns(() => Task.FromResult(
                  new Web.Infrastructure.HttpResult { Content = @"[{
                      'user': {
                            'id': 8,
                            'username': 'captain',
                            'email': 'captain@gmail.com',
                            'first_name': 'Captain',
                            'last_name': 'America',
                            'is_active': true,
                            'is_staff': true
                        },
                        'position': {
                            'id': 1,
                            'name': 'Front-end Developer',
                            'level': 'Senior',
                            'sort': 0
                        },
                        'phone_number': '0824478876',
                        'email': 'captain@gmail.com',
                        'github_user': 'Captain',
                        'birth_date': '1981-07-30',
                        'gender': 'M',
                        'race': 'B',
                        'years_worked': 2,
                        'age': 36,
                        'days_to_birthday': 231
                    }]", StatusCode = 200 }));

            httpClientMock.Setup(call =>
                 call.ReadAsStringAsync(It.Is<string>(x => x == "review")))
             .Returns(() => Task.FromResult(
                 new Web.Infrastructure.HttpResult { Content = @"[{
                        'id': 9,
                        'date': '2016-06-01',
                        'salary': '100000.00',
                        'type': 'S',
                        'employee': 12,
                        'position': 4
                    }]", StatusCode = 200 }));

            Web.Controllers.HomeController homeController = new Web.Controllers.HomeController(httpClientMock.Object);

            //Act         
            var response = await homeController.Index();

            //Assert
            Assert.IsInstanceOfType(response, typeof(ViewResult));

            if (response is ViewResult vResult)
            {
                Assert.IsInstanceOfType(vResult.Model, typeof(DashboardViewModel));
                var model = vResult.Model as DashboardViewModel;
                Assert.IsNotNull(model);

                Assert.AreEqual(1, model.NumberOfEmployees);
            }
        }
    }
}
