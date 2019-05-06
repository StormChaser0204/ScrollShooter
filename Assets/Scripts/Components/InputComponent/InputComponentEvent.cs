using Engine.Components;

namespace Engine.Common.Events {

    public enum InputComponentEventType {
        Shoot = 0,
    }

    public class InputComponentEvent : BaseEvent<InputComponentEventType, InputComponent> {
        public InputComponentEvent(InputComponentEventType eventSubtype, InputComponent target) {
            EventSubtype = eventSubtype;
            Target = target;
        }
    }
}