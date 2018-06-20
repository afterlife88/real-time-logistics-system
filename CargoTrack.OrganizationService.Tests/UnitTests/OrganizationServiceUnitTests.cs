using System.Collections.Generic;
using System.Linq;
using CargoTrack.OrganizationService.Configuration;
using CargoTrack.OrganizationService.Contracts.Models.DTO;
using CargoTrack.OrganizationService.Contracts.Models.Service;
using CargoTrack.OrganizationService.Contracts.Models.Service.Organization;
using CargoTrack.OrganizationService.Data.Contracts;
using CargoTrack.OrganizationService.Data.Contracts.Base;
using CargoTrack.OrganizationService.Data.Entities;
using Moq;
using NUnit.Framework;

namespace CargoTrack.OrganizationService.Tests.UnitTests
{
    [TestFixture]
    public class OrganizationServiceUnitTests
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private OrganizationService _organizationService;

        [SetUp]
        public void Setup()
        {
            // Setup unit of work with fake data to all including repos that using in unit of work
            Mock<IOrganizationRepository> organizationRepository = new Mock<IOrganizationRepository>();
            Mock<IOrganizationTypeRepository> organizationTypeRepository = new Mock<IOrganizationTypeRepository>();
            organizationRepository.Setup(repo => repo.GetAll()).Returns(OrganizationsFakeItems);
            organizationTypeRepository.Setup(rep => rep.GetAll()).Returns(OrganizationTypeFakeItems);
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(r => r.OrganizationRepository).Returns(organizationRepository.Object);
            _mockUnitOfWork.Setup(r => r.OrganizationTypeRepository).Returns(organizationTypeRepository.Object);
            _organizationService = new OrganizationService(_mockUnitOfWork.Object);
            // Load mapping
            AutomapperConfiguration.Load();
        }

        [Test]
        public void GetOrganizationById_ReturnOrganizationDto_Successful()
        {
            // Arrange

            // Act
            var request = _organizationService.GetOrganizationById(new GetOrganizationByIdRequest() { Id = 1 });

            // Assert
            Assert.AreEqual(ServiceStatus.Success, request.ServiceStatus);
            Assert.AreEqual(string.Empty, request.ErrorMessage);
            Assert.IsInstanceOf<OrganizationDetailedDTO>(request.OrganizationDetailed);
            Assert.AreEqual(request.OrganizationDetailed.Country, OrganizationsFakeItems().First().Country);
        }

        [Test]
        public void GetOrganizationById_ReturnNotFound_WhenRequestedNotExistedValue()
        {
            // Arrange

            // Act
            var request = _organizationService.GetOrganizationById(new GetOrganizationByIdRequest() { Id = 10 });

            // Assert
            Assert.AreEqual(ServiceStatus.NotFound, request.ServiceStatus);
        }
        [Test]
        public void GetOrganizationById_ReturnServiceError_WhenRequestedWithNull()
        {
            // Arrange

            // Act
            var request = _organizationService.GetOrganizationById(null);

            // Assert
            Assert.AreEqual(ServiceStatus.ServiceError, request.ServiceStatus);
        }

        [Test]
        public void GetOrganizationsByKardex_ReturnCollectionOfOrganizationsDto_Successfull()
        {
            // Arrange

            // Act
            var request = _organizationService.GetOrganizationsByKardex(new GetOrganizationsByKardexRequest { Kardex = "10203050" });

            // Assert
            Assert.AreEqual(ServiceStatus.Success, request.ServiceStatus);
            Assert.AreEqual(string.Empty, request.ErrorMessage);
            Assert.AreEqual(2, request.Organizations.Count);
            Assert.AreEqual(1, request.Organizations.First().Id);
            Assert.IsInstanceOf<ICollection<OrganizationDTO>>(request.Organizations);
        }
        [Test]
        public void GetOrganizationsByKardex_ReturnServiceError_WhenRequestedWithNull()
        {
            // Arrange

            // Act
            var request = _organizationService.GetOrganizationsByKardex(null);

            // Assert
            Assert.AreEqual(ServiceStatus.ServiceError, request.ServiceStatus);
        }

        #region FakeItems

        /// <summary>
        /// Data helper method
        /// </summary>
        /// <returns></returns>
        private IQueryable<OrganizationType> OrganizationTypeFakeItems()
        {
            return new List<OrganizationType>
            {
                new OrganizationType { Id = 1, Name = "Store"  },
                new OrganizationType { Id = 2, Name = "Warehouse" },
                new OrganizationType { Id = 3, Name = "Supplier"}

            }.AsQueryable();
        }

        /// <summary>
        /// Data helper method
        /// </summary>
        /// <returns></returns>
        private IQueryable<Organization> OrganizationsFakeItems()
        {
            return new List<Organization>
            {
                new Organization()
                {
                    Id = 1,
                    OrganizationType = OrganizationTypeFakeItems().ToArray()[0],
                    Name = "Rozetka",
                    Address = "Stepan Bandery Avenue, 6",
                    City = "Kiev",
                    Country = "UA",
                    Cvr = "4050394",
                    Lattitude = 50.487545,
                    Longitude = 30.494055,
                    Kardex = "10203050",
                    Zipcode = "02000"
                },
                new Organization()
                {
                    Id = 2,
                    OrganizationType = OrganizationTypeFakeItems().ToArray()[1],
                    Name = "Atlantyk",
                    Address = "Stolichne highway, 100",
                    City = "Kiev",
                    Country = "UA",
                    Cvr = "5000",
                    Lattitude = 50.348114,
                    Longitude = 30.544554,
                    Kardex = "10203041",
                    Zipcode = "02000"
                },
                new Organization()
                {
                    Id = 2,
                    OrganizationType = OrganizationTypeFakeItems().ToArray()[0],
                    Name = "Comfy",
                    Address = "Vadym Hetman Str. 6",
                    City = "Kiev",
                    Country = "UA",
                    Cvr = "5000",   
                    Lattitude = 50.451034,
                    Longitude = 30.440797,
                    Kardex = "10203050",
                    Zipcode = "02000"
                }
            }.AsQueryable();
        }

        #endregion
    }
}
