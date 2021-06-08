using System;

namespace OpenIddict.Abstractions
{
    /// <summary>
    /// Exposes common exceptions thrown by OpenIddict.
    /// </summary>
    public static class OpenIddictExceptions
    {
        /// <summary>
        /// Represents an OpenIddict concurrency exception.
        /// </summary>
        public class ConcurrencyException : Exception
        {
            /// <summary>
            /// Creates a new <see cref="ConcurrencyException"/>.
            /// </summary>
            /// <param name="message">The exception message.</param>
            public ConcurrencyException(string message)
                : this(message, exception: null)
            {
            }

            /// <summary>
            /// Creates a new <see cref="ConcurrencyException"/>.
            /// </summary>
            /// <param name="message">The exception message.</param>
            /// <param name="exception">The inner exception.</param>
            public ConcurrencyException(string message, Exception exception)
                : base(message, exception)
            {
            }
        }
    }
}