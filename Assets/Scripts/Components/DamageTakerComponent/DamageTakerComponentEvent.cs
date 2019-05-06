using Engine.Components;

namespace Engine.Common.Events {

    public enum DamageTakerComponentEventType {
        TakeDamage = 0
    }

    public class DamageTakerComponentEvent : BaseEvent<DamageTakerComponentEventType, DamageTakerComponent> {
        public DamageTakerComponentEvent(DamageTakerComponentEventType eventSubtype, DamageTakerComponent target) {
            EventSubtype = eventSubtype;
            Target = target;
        }
    }
}