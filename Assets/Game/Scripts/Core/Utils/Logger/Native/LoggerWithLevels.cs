using System.Collections.Generic;
using VContainer;
using UnityEngine;
using Core.Logger.Contract;
using Core.Logger.Domain;

namespace Core.Logger.Native
{
    public sealed class LoggerWithLevels : ILoggerCore
    {
        [Inject] private readonly ELogLevels _logLevel;

        private readonly Dictionary<ELogLevels, int> _levels = new()
        {
            { ELogLevels.Production, 1 },
            { ELogLevels.Debug, 0 },
        };

        public void Log(string message, ELogLevels level = ELogLevels.Debug)
        {
            if(!CheckNeedLog(level))
                return;
            
            Debug.Log(message);
        }
        
        public void Warning(string message, ELogLevels level = ELogLevels.Debug)
        {
            if(!CheckNeedLog(level))
                return;
            
            Debug.LogWarning(message);
        }
        
        public void Error(string message, ELogLevels level = ELogLevels.Debug)
        {
            if(!CheckNeedLog(level))
                return;
            
            Debug.LogError(message);
        }

        private bool CheckNeedLog(ELogLevels eLogLevels)
        {
            _levels.TryGetValue(eLogLevels, out var levelLog);
            _levels.TryGetValue(_logLevel, out var projectLevel);

            return levelLog >= projectLevel;
        }
    }
}