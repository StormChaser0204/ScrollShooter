using Engine.Common.Events;
using UnityEngine;

namespace Engine.Components {
    public class InputComponent : BaseComponent {
        private RunnerComponent _runnerComponent;

        protected override void Init() {
            _runnerComponent = GetComponent<RunnerComponent>();
            Cursor.visible = false;
        }

        private void Update() {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _runnerComponent.SetPosition(mousePosition);

            if (Input.GetMouseButtonDown(0)) {
                EventManager.Instance.DispatchEvent(new InputComponentEvent(InputComponentEventType.Shoot, this));
            }
        }
    }
}
