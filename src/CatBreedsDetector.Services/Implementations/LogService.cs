using CatBreedsDetector.Common;
using CatBreedsDetector.Common.Execution;

namespace CatBreedsDetector.Services.Implementations
{
    using System;
    using CatBreedsDetector.Services.Contracts;
    using NLog;

    /// <summary>
    /// A custom implementation of the <see cref="ILogService"/> interface.
    /// </summary>
    public class LogService : ILogService
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogService"/> class.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> that should be used internally for logging.</param>
        public LogService(ILogger logger)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc />
        public void SaveToLog(Exception exception)
        {
            ArgumentNullException.ThrowIfNull(exception);

            this._logger.Error(exception);
        }
    }
}
