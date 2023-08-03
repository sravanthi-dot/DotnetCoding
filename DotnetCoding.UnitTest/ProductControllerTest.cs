using AutoFixture;
using DotnetCoding.Controllers;
using DotnetCoding.Core.Models;
using DotnetCoding.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCoding.UnitTest
{
    [TestClass]
    public class ProductControllerTest
    {
        private Mock<IProductService> _productServiceMock;
        private ProductsController _productsController;
        private IFixture _fixture;
        private List<ProductDetails> WebFormFields { get; set; }
        public ProductControllerTest()
        {
            _fixture = new Fixture();
            _productServiceMock = new Mock<IProductService>();
        }

       
        [TestMethod]
        public async Task PostProduct_Returns_Ok()
        {
            var product = _fixture.Create<ProductDetails>();
            _productServiceMock.Setup(repo => repo.CreateProduct(It.IsAny<ProductDetails>())).Returns(Task.FromResult(product.IsActive));
            _productsController = new ProductsController(_productServiceMock.Object);
            var result = await _productsController.CreateProduct(product);
            var obj = result as ObjectResult;
            Assert.AreEqual(200, obj.StatusCode);
        }
        [TestMethod]
        public async Task PutProduct_Returns_Ok()
        {
            var product = _fixture.Create<ProductDetails>();
            _productServiceMock.Setup(repo => repo.UpdateProduct(It.IsAny<ProductDetails>())).Returns(Task.FromResult(product.IsActive));
            _productsController = new ProductsController(_productServiceMock.Object);
            var result = await _productsController.UpdateProduct(product);
            var obj = result as ObjectResult;
            Assert.AreEqual(200, obj.StatusCode);
        }

        [TestMethod]
        public async Task DeleteProduct_Returns_Ok()
        {
            var product = _fixture.Create<ProductDetails>();
            _productServiceMock.Setup(repo => repo.DeleteProduct(It.IsAny<int>())).Returns(Task.FromResult(product.IsActive));
            _productsController = new ProductsController(_productServiceMock.Object);
            var result = await _productsController.DeleteProduct(It.IsAny<int>());
            var obj = result as ObjectResult;
            Assert.AreEqual(200, obj.StatusCode);
        }
        [TestMethod]
        public async Task GetAllProducts_Returns_Ok(string productName, DateTime createdDate, decimal productPrice)
        {

            var productlist = _fixture.CreateMany<ProductDetails>(3).ToList();
            //_productServiceMock.Setup(repo => repo.GetAllProducts()).Returns(Task.FromResult<productlist>);
            _productsController = new ProductsController(_productServiceMock.Object);
            var result = await _productsController.GetProductList(productName, createdDate, productPrice);
            var obj = result as ObjectResult;
            Assert.AreEqual(200, obj.StatusCode);
        }
        [TestMethod]
        public async Task GetAllProducts_Returns_Error(string productName, DateTime createdDate, decimal productPrice)
        {
            var productlist = _fixture.CreateMany<ProductDetails>(3).ToList();
            _productServiceMock.Setup(repo => repo.GetAllProducts(productName, createdDate, productPrice)).Throws(new Exception());
            _productsController = new ProductsController(_productServiceMock.Object);
            var result = await _productsController.GetProductList(productName, createdDate, productPrice);
            var obj = result as ObjectResult;
            Assert.AreEqual(400, obj.StatusCode);
        }
    }
}
