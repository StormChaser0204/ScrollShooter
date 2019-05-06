using Engine.Components;

namespace Engine.Common.Events {
    public enum LevelObjectEventType {
        Enabled = 0,
        Disabled = 1,
        StateChanged = 2,
        Dead = 3
    }

    public class LevelObjectEvent : BaseEvent<LevelObjectEventType, LevelObject> {
        public LevelObjectEvent(LevelObjectEventType eventSubtype, LevelObject target) {
            EventSubtype = eventSubtype;
            Target = target;
        }
    }
}