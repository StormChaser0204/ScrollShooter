using Engine.Common.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Components {
    public class ShellComponent : BaseComponent {

        private RunnerComponent _runnerComponent;

        protected override void Init() {
            _runnerComponent = GetComponent<RunnerComponent>();
            _runnerComponent.SetDirection(transform.up, gameObject.transform.localEulerAngles.z);
            EventManager.Instance.AddEventListener<DamageDealerComponentEvent>(OnDamageDealerComponent);
        }

        private void OnDamageDealerComponent(DamageDealerComponentEvent damageDealerComponentEvent) {
            if (damageDealerComponentEvent.Target.LevelObject != this.LevelObject) return;
            Destroy(gameObject);
        }
    }
}