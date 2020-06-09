namespace CatBreedsDetector.Web.Infrastructure.Helpers.Contracts
{
    using System;

    public interface ILogHelper
    {
        void SaveToLog(Exception exception);
    }
}
