using System.Runtime.Serialization;

namespace CargoTrack.TransactionService.Contracts.Models.Service
{
    /// <summary>
    /// Base class for all service responses
    /// </summary>
    [DataContract]
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
        [DataMember]
        public ServiceStatus ServiceStatus { get; set; }

        /// <summary>
        /// A description of the error if the operation was not successfull
        /// </summary>
        [DataMember]
        public string ErrorMessage { get; set; }

        #endregion
    }
}
