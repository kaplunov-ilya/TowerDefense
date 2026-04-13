using Core.Logger.Domain;

namespace Core.Logger.Contract
{
    public interface ILoggerCore
    { 
        void Log(string message, ELogLevels level = ELogLevels.Debug);
        void Warning(string message, ELogLevels level = ELogLevels.Debug);
        void Error(string message, ELogLevels level = ELogLevels.Debug);
    }
}