using System;

namespace CargoTrack.Common.Exceptions
{
    /// <summary>
    /// Generic service exception
    /// </summary>
    public class ServiceErrorException : Exception
    {
        public ServiceErrorException(string message, string serviceName) : base($"{message} in {serviceName}")
        { }
    }
}
