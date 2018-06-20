using System;

namespace CargoTrack.Common.Utilities
{
    /// <summary>
    /// Defencive programming guard class
    /// </summary>
    public static class Guard
    {
        #region Metoder

        /// <summary>
        /// Guard against null values
        /// </summary>
        /// <param name="obj"></param>
        public static void IsNotNull(object obj)
        {
            if (obj == null) throw new ArgumentNullException($"The object {nameof(obj)} can not be null");
        }

        /// <summary>
        /// Guard against null values or empty if it is a string
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        public static void IsNotNullOrEmpty(object obj, string name)
        {
            if ((obj == null) || (obj is string && string.IsNullOrEmpty((string)obj)))
            {
                throw new ArgumentNullException(name, $"The object {name} can not be null or empty");
            }
        }

        /// <summary>
        /// Guard against a logic expression
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="message"></param>
        public static void IsTrue(bool expression, string message)
        {
            if (!expression)
            {
                throw new ArgumentException(message);
            }
        }

        #endregion
    }
}