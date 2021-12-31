namespace CatBreedsDetector.Web.Infrastructure.Helpers.Contracts
{
    using System;

    public interface ILogHelper
    {
        /// <summary>
        /// Use this method to log an <see cref="Exception"/> to a file on the file system.
        /// </summary>
        /// <param name="exception">The <see cref="Exception"/> that should be logged.</param>
        void SaveToLog(Exception exception);
    }
}
