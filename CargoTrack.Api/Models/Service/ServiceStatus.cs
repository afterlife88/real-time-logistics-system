using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoTrack.Api.Models.Service
{
    /// <summary>
    /// The status of a service operation
    /// </summary>
    public enum ServiceStatus
    {
        /// <summary>
        /// Operation was successfull
        /// </summary>
        Success = 0,
        /// <summary>
        /// Not authorized to access the operation
        /// </summary>
        NotAuthorized = 1,
        /// <summary>
        /// An error occured in the operation
        /// </summary>
        ServiceError = 2,
        /// <summary>
        /// One or more parameters passed to the call is invalid
        /// </summary>
        InvalidParameters = 3
    }
}