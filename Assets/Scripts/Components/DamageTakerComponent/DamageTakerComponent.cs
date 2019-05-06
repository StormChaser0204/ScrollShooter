using Engine.Common.Events;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Components {
    public class DamageTakerComponent : BaseComponent {
        [SerializeField]
        private List<LevelObjectType> _ignoredTypes;

        public int TakedDamage { get; private set; }

        private bool _canTakeDamage;

        public void OnCollisionEnter2D(Collision2D col) {
            if (!_canTakeDamage) return;
            var levelObject = col.gameObject.GetComponent<LevelObject>();
            if (levelObject == null) return;
            if (_ignoredTypes.Contains(levelObject.ObjectType)) return;
            var damageDealerComponent = levelObject.GetComponent<DamageDealerComponent>();
            if (damageDealerComponent == null) return;
            TakedDamage = damageDealerComponent.Damage;
            EventManager.Instance.DispatchEvent(new DamageTakerComponentEvent(DamageTakerComponentEventType.TakeDamage, this));
            EventManager.Instance.DispatchEvent(new DamageDealerComponentEvent(DamageDealerComponentEventType.DealDamage, damageDealerComponent));
        }

        protected override void Init() {
            EventManager.Instance.AddEventListener<LevelObjectEvent>(OnLevelObjectEvent);
        }

        private void OnDisable() {
            EventManager.Instance?.RemoveEventListener<LevelObjectEvent>(OnLevelObjectEvent);
        }

        private void OnLevelObjectEvent(LevelObjectEvent levelObjectEvent) {
            if (levelObjectEvent.Target != LevelObject) return;
            if (levelObjectEvent.EventSubtype != LevelObjectEventType.StateChanged) return;
            _canTakeDamage = levelObjectEvent.Target.CurrentState.CanTakeDamage;
        }
    }
}