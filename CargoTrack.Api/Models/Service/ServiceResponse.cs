using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoTrack.Api.Models.Service
{
    /// <summary>
    /// Base class for all service responses
    /// </summary>
    public class ServiceResponse
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="serviceStatus"></param>
        /// <param name="errorMessage"></param>
        public ServiceResponse(ServiceStatus serviceStatus, string errorMessage)
        {
            ServiceStatus = serviceStatus;
            ErrorMessage = errorMessage;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The status of the service operation
        /// </summary>
        public ServiceStatus ServiceStatus { get; set; }

        /// <summary>
        /// A description of the error if the operation was not successfull
        /// </summary>
        public string ErrorMessage { get; set; }

        #endregion
    }
}