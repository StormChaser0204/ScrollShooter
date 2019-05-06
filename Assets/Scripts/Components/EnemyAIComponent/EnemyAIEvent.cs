using Engine.Components;

namespace Engine.Common.Events {
    public enum EnemyAIEventType {
        RunAway = 0
    }

    public class EnemyAIEvent : BaseEvent<EnemyAIEventType, EnemyAI> {
        public EnemyAIEvent(EnemyAIEventType eventSubtype, EnemyAI target) {
            EventSubtype = eventSubtype;
            Target = target;
        }
    }
}