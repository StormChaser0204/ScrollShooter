using System;
using Engine.Common.Events;
using UnityEngine;

namespace Engine.Components {
    public class RunnerComponent : BaseComponent {
        [SerializeField]
        private float _speed;

        private Transform _transform;
        private Vector3 _direction;
        private Quaternion _rotation;
        private float _currentSpeed;
        private bool _canMove;

        private void Update() {
            if (!_canMove) return;
            _transform.rotation = _rotation;
            _transform.position += _direction * _currentSpeed * Time.deltaTime;
        }

        public void SetPosition(Vector3 position) {
            _transform.position = new Vector3(position.x, position.y, 0);
        }

        public void SetDirection(Vector3 direction, float rotation) {
            _direction = direction;
            _rotation = Quaternion.Euler(new Vector3(0f, 0f, rotation));
        }

        protected override void Init() {
            _transform = GetComponent<Transform>();
            _currentSpeed = _speed;
            EventManager.Instance.AddEventListener<LevelObjectEvent>(OnLevelObjectEvent);
        }

        private void OnDisable() {
            EventManager.Instance?.RemoveEventListener<LevelObjectEvent>(OnLevelObjectEvent);
        }

        private void OnLevelObjectEvent(LevelObjectEvent levelObjectEvent) {
            if (levelObjectEvent.Target != LevelObject) return;
            if (levelObjectEvent.EventSubtype != LevelObjectEventType.StateChanged) return;
            _canMove = levelObjectEvent.Target.CurrentState.CanMove;
        }
    }
}