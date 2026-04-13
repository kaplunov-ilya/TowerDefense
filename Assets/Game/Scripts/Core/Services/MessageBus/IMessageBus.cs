using System;

namespace TowerDefence.Core.Services.MessageBus
{
    public interface IMessageBus
    {
        IObservable<TMessage> Subscribe<TMessage>() where TMessage : struct;
        void Publish<TMessage>(in TMessage message) where TMessage : struct;
    }
}