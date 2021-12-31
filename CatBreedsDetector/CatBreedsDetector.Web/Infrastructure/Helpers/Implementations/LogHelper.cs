namespace CatBreedsDetector.Web.Infrastructure.Helpers.Implementations
{
    using System;
    using CatBreedsDetector.Web.Infrastructure.Helpers.Contracts;
    using NLog;

    /// <summary>
    /// A custom implementation of the <see cref="ILogHelper"/> interface.
    /// </summary>
    public class LogHelper : ILogHelper
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogHelper"/> class.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> that should be used internally for logging.</param>
        public LogHelper(ILogger logger)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc />
        public void SaveToLog(Exception exception)
        {
            if (exception == null)
                return;

            this._logger.Error(exception);
        }
    }
}
