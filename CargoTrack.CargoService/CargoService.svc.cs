using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using AutoMapper;
using CargoTrack.CargoService.Contracts;
using CargoTrack.CargoService.Contracts.Models.DTO;
using CargoTrack.CargoService.Contracts.Models.Service;
using CargoTrack.CargoService.Contracts.Models.Service.Cargo;
using CargoTrack.CargoService.Data.Contracts.Base;
using CargoTrack.CargoService.Data.Entities;
using CargoTrack.CargoService.Infrastructure;
using log4net;

namespace CargoTrack.CargoService
{
    [AutomapServiceBehavior]
    public class CargoService : ICargoService
    {
        #region Fields

        private readonly ILog _log = LogManager.GetLogger(typeof(CargoService));
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructors

        public CargoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        /// <summary>
        /// Create a new cargo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CreateCargoResponse CreateCargo(CreateCargoRequest request)
        {
            _log.Info($"Entry {GetType().FullName}.{MethodBase.GetCurrentMethod().Name}");

            try
            {
                var model = Mapper.Map<CargoType>(request);
                var id = _unitOfWork.CargoTypes.AddCargoType(model);

                _unitOfWork.Commit();
                return new CreateCargoResponse(id, ServiceStatus.Success, string.Empty);
            }
            catch (Exception ex)
            {
                _log.Error(request, ex);
                return new CreateCargoResponse(0, ServiceStatus.ServiceError, ex.Message);
            }
            finally
            {
                _log.Info($"Exit {GetType().FullName}.{MethodBase.GetCurrentMethod().Name}");
            }
        }

        /// <summary>
        /// Delete a cargo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DeleteCargoResponse DeleteCargo(DeleteCargoRequest request)
        {
            _log.Info($"Entry {GetType().FullName}.{MethodBase.GetCurrentMethod().Name}");

            try
            {
                var cargo =
                 _unitOfWork.CargoTypes.GetAll().Include(r => r.Category).FirstOrDefault(r => r.Id == request.Id);
                if (cargo == null)
                    return new DeleteCargoResponse(ServiceStatus.NotFound, "REQUESTED_ITEM_NOT_FOUND");

                _unitOfWork.CargoTypes.Delete(cargo);
                _unitOfWork.Commit();

                return new DeleteCargoResponse(ServiceStatus.Success, string.Empty);
            }
            catch (Exception ex)
            {
                _log.Error(request, ex);
                return new DeleteCargoResponse(ServiceStatus.ServiceError, ex.Message);
            }
            finally
            {
                _log.Info($"Exit {GetType().FullName}.{MethodBase.GetCurrentMethod().Name}");
            }
        }

        /// <summary>
        /// Get cargo based on its id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public GetCargoByIdResponse GetCargoById(GetCargoByIdRequest request)
        {
            _log.Info($"Entry {GetType().FullName}.{MethodBase.GetCurrentMethod().Name}");

            try
            {
                var cargo =
                    _unitOfWork.CargoTypes.GetAll().Include(r => r.Category).FirstOrDefault(r => r.Id == request.Id);
                if (cargo == null)
                    return new GetCargoByIdResponse(null, ServiceStatus.NotFound, "REQUESTED_ITEM_NOT_FOUND");

                return new GetCargoByIdResponse(Mapper.Map<CargoTypeDetailedDTO>(cargo), ServiceStatus.Success, string.Empty);
            }
            catch (Exception ex)
            {
                _log.Error(request, ex);
                return new GetCargoByIdResponse(null, ServiceStatus.ServiceError, ex.Message);
            }
            finally
            {
                _log.Info($"Exit {GetType().FullName}.{MethodBase.GetCurrentMethod().Name}");
            }
        }

        /// <summary>
        /// List all cargo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ListCargoResponse ListCargo(ListCargoRequest request)
        {
            _log.Info($"Entry {GetType().FullName}.{MethodBase.GetCurrentMethod().Name}");

            try
            {
                var cargoCollection = Mapper.Map<ICollection<CargoTypeDTO>>(_unitOfWork.CargoTypes.GetAll().Include(r => r.Category));
                return new ListCargoResponse(cargoCollection, ServiceStatus.Success, string.Empty);
            }
            catch (Exception ex)
            {
                _log.Error(request, ex);
                return new ListCargoResponse(null, ServiceStatus.ServiceError, ex.Message);
            }
            finally
            {
                _log.Info($"Exit {GetType().FullName}.{MethodBase.GetCurrentMethod().Name}");
            }

        }

        /// <summary>
        /// Update a cargo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public UpdateCargoResponse UpdateCargo(UpdateCargoRequest request)
        {
            _log.Info($"Entry {GetType().FullName}.{MethodBase.GetCurrentMethod().Name}");

            try
            {
                var cargo = _unitOfWork.CargoTypes.GetAll().Include(r => r.Category).FirstOrDefault(r => r.Id == request.Id);
                if (cargo == null)
                    return new UpdateCargoResponse(ServiceStatus.NotFound, "REQUESTED_ITEM_NOT_FOUND");

                var getCategory =
                    _unitOfWork.CargoTypeCategories.GetAll().FirstOrDefault(r => r.Name == request.Category);
                if (getCategory == null)
                    return new UpdateCargoResponse(ServiceStatus.NotFound, "REQUESTED_CARGO_CATEGORY_NOT_FOUND");

                cargo.Id = request.Id;
                cargo.Abbreviation = request.Abbreviation;
                cargo.Category = getCategory;
                cargo.Description = request.Description;
                cargo.Ean = request.Ean;
                cargo.Leased = request.Leased;
                cargo.Name = request.Name;
                cargo.Price = request.Price;

                _unitOfWork.CargoTypes.Update(cargo);
                _unitOfWork.Commit();

                return new UpdateCargoResponse(ServiceStatus.Success, string.Empty);
            }
            catch (Exception ex)
            {
                _log.Error(request, ex);
                return new UpdateCargoResponse(ServiceStatus.ServiceError, ex.Message);
            }
            finally
            {
                _log.Info($"Exit {GetType().FullName}.{MethodBase.GetCurrentMethod().Name}");
            }
        }
    }
}
