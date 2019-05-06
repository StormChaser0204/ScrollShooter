using Engine.Components;

namespace Engine.Common.Events {
    public enum HealthComponentEventType {
        HealthChanged = 0,
        LoseHealth = 1,
        GetHealth = 2
    }

    public class HealthComponentEvent : BaseEvent<HealthComponentEventType, HealthComponent> {
        public HealthComponentEvent(HealthComponentEventType eventSubtype, HealthComponent target) {
            EventSubtype = eventSubtype;
            Target = target;
        }
    }
}