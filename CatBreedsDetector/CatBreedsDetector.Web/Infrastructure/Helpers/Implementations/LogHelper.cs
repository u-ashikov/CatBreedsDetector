namespace CatBreedsDetector.Web.Infrastructure.Helpers.Implementations
{
    using Contracts;
    using NLog;
    using System;

    public class LogHelper : ILogHelper
    {
        private readonly ILogger logger;

        public LogHelper(ILogger logger)
        {
            this.logger = logger;
        }

        public void SaveToLog(Exception exception)
        {
            if (exception == null)
            {
                return;
            }

            this.logger.Error(exception);
        }
    }
}
