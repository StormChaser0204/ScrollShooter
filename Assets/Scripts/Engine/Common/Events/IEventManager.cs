using System;

namespace Engine.Common.Events {
    public interface IEventManager {
        void AddEventListener<T>(Action<T> eventDelegate) where T : BaseEvent;
        void RemoveEventListener<T>(Action<T> eventDelegate) where T : BaseEvent;
        void DispatchEvent(BaseEvent baseEvent);
    }
}