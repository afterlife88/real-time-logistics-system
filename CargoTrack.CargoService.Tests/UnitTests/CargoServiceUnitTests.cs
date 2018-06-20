using System.Collections.Generic;
using System.Linq;
using CargoTrack.CargoService.Configuration;
using CargoTrack.CargoService.Contracts.Models.DTO;
using CargoTrack.CargoService.Contracts.Models.Service;
using CargoTrack.CargoService.Contracts.Models.Service.Cargo;
using CargoTrack.CargoService.Data.Contracts;
using CargoTrack.CargoService.Data.Contracts.Base;
using CargoTrack.CargoService.Data.Entities;
using Moq;
using NUnit.Framework;

namespace CargoTrack.CargoService.Tests.UnitTests
{
    [TestFixture]
    public class CargoServiceUnitTests
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private CargoService _cargoService;

        [SetUp]
        public void Setup()
        {
            // Setup unit of work with fake data to all including repos that using in unit of work
            Mock<ICargoTypeCategoryRepository> cargoTypeCategoryRepo = new Mock<ICargoTypeCategoryRepository>();
            Mock<ICargoTypeRepository> cargoTypeRepo = new Mock<ICargoTypeRepository>();
            cargoTypeCategoryRepo.Setup(repo => repo.GetAll()).Returns(CargoTypeCategoryFakeItems);
            cargoTypeRepo.Setup(rep => rep.GetAll()).Returns(CargoTypesFakeItems);
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(r => r.CargoTypeCategories).Returns(cargoTypeCategoryRepo.Object);
            _mockUnitOfWork.Setup(r => r.CargoTypes).Returns(cargoTypeRepo.Object);
            _cargoService = new CargoService(_mockUnitOfWork.Object);
            // Load mapping
            AutomapperConfiguration.Load();
        }
        [Test]
        public void ListCargo_ReturnCollectionOfCargoDTO_Successfull()
        {
            // Arrange

            // Act
            var request = _cargoService.ListCargo(new ListCargoRequest());

            // Assert
            Assert.AreEqual(ServiceStatus.Success, request.ServiceStatus);
            Assert.AreEqual(string.Empty, request.ErrorMessage);
            Assert.AreEqual(2, request.CargoTypes.Count);
            Assert.AreEqual(1, request.CargoTypes.First().Id);
            Assert.IsInstanceOf<ICollection<CargoTypeDTO>>(request.CargoTypes);
        }

        #region GetCargoById Tests
        [Test]
        public void GetCargoById_ReturnCargoDTO_Successfull()
        {
            // Arrange

            // Act
            var request = _cargoService.GetCargoById(new GetCargoByIdRequest() { Id = 1 });

            // Assert
            Assert.AreEqual(ServiceStatus.Success, request.ServiceStatus);
            Assert.AreEqual(string.Empty, request.ErrorMessage);
            Assert.IsInstanceOf<CargoTypeDetailedDTO>(request.CargoTypeDetailed);
            Assert.AreEqual(request.CargoTypeDetailed.Description, CargoTypesFakeItems().First().Description);
        }
        [Test]
        public void GetCargoById_ReturnNotFound_WhenRequestedValueNotExist()
        {
            // Arrange

            // Act
            var request = _cargoService.GetCargoById(new GetCargoByIdRequest() { Id = 10 });

            // Assert
            Assert.AreEqual("REQUESTED_ITEM_NOT_FOUND", request.ErrorMessage);
            Assert.AreEqual(ServiceStatus.NotFound, request.ServiceStatus);
            Assert.IsNull(request.CargoTypeDetailed);
        }
        [Test]
        public void GetCargoById_ReturnServiceError_WhenRequestIsNull()
        {
            // Arrange

            // Act
            var request = _cargoService.GetCargoById(null);

            // Assert
            Assert.AreEqual(ServiceStatus.ServiceError, request.ServiceStatus);
        }

        #endregion

        #region AddCargo Tests
        [Test]
        public void AddNewCargo_Successfull()
        {
            // Arrange

            // Act
            var request = _cargoService.CreateCargo(new CreateCargoRequest()
            {
                Category = "new category",
                Name = "new name",
                Ean = "1123123123",
                Abbreviation = "USD",
                Price = 15.2,
                Description = "random desc",
                Leased = true
            });

            // Assert
            _mockUnitOfWork.Verify();

            Assert.AreEqual(string.Empty, request.ErrorMessage);
            Assert.AreEqual(ServiceStatus.Success, request.ServiceStatus);
        }
        #endregion

        #region UpdateCargo Tests

        [Test]
        public void UpdateCargo_Successfull()
        {
            // Arrange

            // Act
            var request = _cargoService.UpdateCargo(new UpdateCargoRequest()
            {
                Id = 1,
                Category = "Dry Goods",
                Name = "New value",
                Abbreviation = "USD",
                Description = "Desc",
                Ean = "8485838475734",
                Price = 12.0,
                Leased = true
            });

            // Assert
            _mockUnitOfWork.Verify();

            Assert.AreEqual(string.Empty, request.ErrorMessage);
            Assert.AreEqual(ServiceStatus.Success, request.ServiceStatus);
        }
        [Test]
        public void UpdateCargo_ReturnNotFound_WhenRequestedNotExistedValue()
        {
            // Arrange

            // Act
            var request = _cargoService.UpdateCargo(new UpdateCargoRequest()
            {
                Id = 0,
                Category = "Goods",
                Name = "New value",
                Abbreviation = "USD",
                Description = "Desc",
                Ean = "8485838475734",
                Price = 12.0,
                Leased = true
            });

            // Assert
            _mockUnitOfWork.Verify();

            Assert.AreEqual("REQUESTED_ITEM_NOT_FOUND", request.ErrorMessage);
            Assert.AreEqual(ServiceStatus.NotFound, request.ServiceStatus);
        }
        #endregion

        #region RemoveCargo Tests
        [Test]
        public void RemoveCargo_ReturnNotFound_WhenRequestedNotExistedValue()
        {
            // Arrange

            // Act
            var request = _cargoService.DeleteCargo(new DeleteCargoRequest() { Id = 0 });

            // Assert
            _mockUnitOfWork.Verify();
            Assert.AreEqual("REQUESTED_ITEM_NOT_FOUND", request.ErrorMessage);
            Assert.AreEqual(ServiceStatus.NotFound, request.ServiceStatus);
        }
        [Test]
        public void RemoveCargo_RemovingCargo_Successfull()
        {
            // Arrange

            // Act
            var request = _cargoService.DeleteCargo(new DeleteCargoRequest() { Id = 1 });

            // Assert
            _mockUnitOfWork.Verify();
            Assert.AreEqual(string.Empty, request.ErrorMessage);
            Assert.AreEqual(ServiceStatus.Success, request.ServiceStatus);
        }

        #endregion

        #region FakeItems

        private readonly CargoTypeCategory _cargoTypeCategory1 = new CargoTypeCategory { Id = 1, Name = "Dry Goods" };
        private readonly CargoTypeCategory _cargoTypeCategory2 = new CargoTypeCategory() { Id = 2, Name = "Fresh Goods" };

        /// <summary>
        /// Data helper method
        /// </summary>
        /// <returns></returns>
        private IQueryable<CargoTypeCategory> CargoTypeCategoryFakeItems()
        {
            return new List<CargoTypeCategory>
            {
               _cargoTypeCategory1, _cargoTypeCategory2
            }.AsQueryable();
        }

        /// <summary>
        /// Data helper method
        /// </summary>
        /// <returns></returns>
        private IQueryable<CargoType> CargoTypesFakeItems()
        {
            return new List<CargoType>
            {
                new CargoType()
                {
                    Id = 1,
                    Abbreviation = "EUR",
                    Description = "Standard EURO Pallet",
                    Ean = "8485838475734",
                    Price = 120.00,
                    Name = "Euro Pallet",
                    Leased = false,
                    Category = _cargoTypeCategory1
                },
                new CargoType()
                {
                    Id = 2,
                    Abbreviation = "RB",
                    Description = "Custom Rolling Cage for fresh goods",
                    Ean = "8458584387477",
                    Price = 2000.00,
                    Name = "Rolling Cage",
                    Leased = false,
                    Category = _cargoTypeCategory2
                }
            }.AsQueryable();
        }

        #endregion
    }
}
