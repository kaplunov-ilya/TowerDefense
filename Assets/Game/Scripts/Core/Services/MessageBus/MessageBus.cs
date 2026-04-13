using System;
using System.Collections.Generic;
using UniRx;

namespace TowerDefence.Core.Services.MessageBus
{
    public sealed class MessageBus
    {
        private readonly Dictionary<Type, object> _subjects = new();

        public IObservable<TMessage> Subscribe<TMessage>() where TMessage : struct
        {
            if (_subjects.TryGetValue(typeof(TMessage), out var obj))
                return (Subject<TMessage>)obj;
            
            obj = new Subject<TMessage>();
            _subjects[typeof(TMessage)] = obj;

            return (Subject<TMessage>)obj;
        }

        public void Publish<TMessage>(in TMessage message) where TMessage : struct
        {
            if (_subjects.TryGetValue(typeof(TMessage), out var obj))
            {
                ((Subject<TMessage>)obj).OnNext(message);
            }
        }
    }
}