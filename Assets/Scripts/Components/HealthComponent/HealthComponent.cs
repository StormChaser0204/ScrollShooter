using System;
using Engine.Common.Events;
using UnityEngine;

namespace Engine.Components {
    public class HealthComponent : BaseComponent {

        [SerializeField]
        private int _health;

        public int CurrentHealth {
            get {
                return _health;
            }
            set {
                _health = value;
            }
        }

        public void ChangeHealth(int value) {
            _health += value;
        }

        protected override void Init() {
            EventManager.Instance.DispatchEvent(new HealthComponentEvent(HealthComponentEventType.HealthChanged, this));
            EventManager.Instance.AddEventListener<DamageTakerComponentEvent>(OnDamageTaked);
        }

        private void OnDisable() {
            EventManager.Instance?.RemoveEventListener<DamageTakerComponentEvent>(OnDamageTaked);
        }

        private void OnDamageTaked(DamageTakerComponentEvent damageTakerComponentEvent) {
            if (damageTakerComponentEvent.Target.LevelObject != LevelObject) return;
            var takedDamage = damageTakerComponentEvent.Target.TakedDamage;
            ChangeHealth(-takedDamage);
            EventManager.Instance.DispatchEvent(new HealthComponentEvent(HealthComponentEventType.HealthChanged, this));
        }
    }
}