using Engine.Components;
namespace Engine.Common.Events {

    public enum DamageDealerComponentEventType {
        DealDamage = 0
    }

    public class DamageDealerComponentEvent : BaseEvent<DamageDealerComponentEventType, DamageDealerComponent> {
        public DamageDealerComponentEvent(DamageDealerComponentEventType eventSubtype, DamageDealerComponent target) {
            EventSubtype = eventSubtype;
            Target = target;
        }
    }
}