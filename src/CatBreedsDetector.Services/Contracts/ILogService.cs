namespace CatBreedsDetector.Services.Contracts
{
    using System;

    /// <summary>
    /// An interface defining the structure of a component responsible for logging.
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// Use this method to log an <see cref="Exception"/> to a file on the file system.
        /// </summary>
        /// <param name="exception">The <see cref="Exception"/> that should be logged.</param>
        void SaveToLog(Exception exception);
    }
}
