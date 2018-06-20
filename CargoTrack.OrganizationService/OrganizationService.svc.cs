using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using AutoMapper;
using CargoTrack.OrganizationService.Contracts;
using CargoTrack.OrganizationService.Contracts.Models.DTO;
using CargoTrack.OrganizationService.Contracts.Models.Service;
using CargoTrack.OrganizationService.Contracts.Models.Service.Organization;
using CargoTrack.OrganizationService.Data.Contracts.Base;
using CargoTrack.OrganizationService.Infrastructure;
using log4net;

namespace CargoTrack.OrganizationService
{
    [AutomapServiceBehavior]
    public class OrganizationService : IOrganizationService
    {
        #region Fields

        private readonly ILog _log = LogManager.GetLogger(typeof(OrganizationService));
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructors
        public OrganizationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        /// <summary>
        /// Get an organization by its id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public GetOrganizationByIdResponse GetOrganizationById(GetOrganizationByIdRequest request)
        {
            _log.Info($"Entry {GetType().FullName}.{MethodBase.GetCurrentMethod().Name}");

            try
            {
                var organization =
                    _unitOfWork.OrganizationRepository.GetAll().Include(r => r.OrganizationType).FirstOrDefault(r => r.Id == request.Id);
                if (organization == null)
                    return new GetOrganizationByIdResponse(null, ServiceStatus.NotFound, "REQUESTED_ITEM_NOT_FOUND");

                return new GetOrganizationByIdResponse(Mapper.Map<OrganizationDetailedDTO>(organization), ServiceStatus.Success, string.Empty);
            }
            catch (Exception ex)
            {
                _log.Error(request, ex);
                return new GetOrganizationByIdResponse(null, ServiceStatus.ServiceError, ex.Message);
            }
            finally
            {
                _log.Info($"Exit {GetType().FullName}.{MethodBase.GetCurrentMethod().Name}");
            }
        }

        /// <summary>
        /// Get an organization by its Kardex
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public GetOrganizationsByKardexResponse GetOrganizationsByKardex(GetOrganizationsByKardexRequest request)
        {
            _log.Info($"Entry {GetType().FullName}.{MethodBase.GetCurrentMethod().Name}");

            try
            {
                var organizations = Mapper.Map<ICollection<OrganizationDTO>>(
                    _unitOfWork.OrganizationRepository.GetAll()
                    .Include(r => r.OrganizationType)
                    .Where(m => m.Kardex.Contains(request.Kardex)));

                return new GetOrganizationsByKardexResponse(organizations, ServiceStatus.Success, string.Empty);
            }
            catch (Exception ex)
            {
                _log.Error(request, ex);
                return new GetOrganizationsByKardexResponse(null, ServiceStatus.ServiceError, ex.Message);
            }
            finally
            {
                _log.Info($"Exit {GetType().FullName}.{MethodBase.GetCurrentMethod().Name}");
            }
        }
    }
}
