using System;

namespace Engine.Common.Events {
    public abstract class BaseEvent {
    }

    public abstract class BaseEvent<TEventType, TEventTarget> : BaseEvent where TEventType : struct, IConvertible {
        public TEventType EventSubtype { get; protected set; }
        public TEventTarget Target { get; protected set; }
    }
}