using System;
using AutoMapper;
using CargoTrack.Api.Configuration;
using CargoTrack.Api.Models.Service;
using CargoTrack.Api.Models.Service.Organization;
using CargoTrack.Common.Utilities;
using CargoTrack.OrganizationService.Contracts;
using log4net;
using Microsoft.AspNet.SignalR;

namespace CargoTrack.Api.Hubs
{
    /// <summary>
    /// SignalR hub for organization related methods
    /// </summary>
    public class OrganizationHub : Hub
    {
        // Variables
        private readonly IOrganizationService _organizationService;
        private readonly ILog _log = LogManager.GetLogger(typeof(OrganizationHub));

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="organizationService"></param>
        public OrganizationHub(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
            AutomapperConfiguration.Load();
        }

        /// <summary>
        /// Find organizations by its Kardex
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public GetOrganizationsByKardexResponse GetOrganizationsByKardex(GetOrganizationsByKardexRequest request)
        {
            try
            {
                Guard.IsNotNull(request);
                var response =
                    _organizationService
                    .GetOrganizationsByKardex(Mapper.
                    Map<OrganizationService.Contracts.Models.Service.Organization.GetOrganizationsByKardexRequest>(request));

                if (response.ServiceStatus != OrganizationService.Contracts.Models.Service.ServiceStatus.Success)
                    return new GetOrganizationsByKardexResponse(null, ServiceStatus.ServiceError, response.ErrorMessage);

                return Mapper.Map<GetOrganizationsByKardexResponse>(response);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                return new GetOrganizationsByKardexResponse(null, ServiceStatus.ServiceError, ex.Message);
            }
        }
    }
}