using Engine.Common.Events;
using System;
using System.Collections;
using UnityEngine;

namespace Engine.Components {
    public class EnemyAI : UnitComponent {

        private RunnerComponent _runnerComponent;
        private Quaternion _rotation;
        private Transform _playerTransform;

        private Coroutine _checkDistance;

        public void SetDirection(Quaternion rotation) {
            _rotation = rotation;
        }

        protected override void Init() {
            _runnerComponent = GetComponent<RunnerComponent>();
            _checkDistance = StartCoroutine(CheckDistanceToCentr());
            EventManager.Instance.AddEventListener<LevelObjectEvent>(OnLevelObjectEvent);
        }

        private void OnLevelObjectEvent(LevelObjectEvent levelObjectEvent) {
            if (levelObjectEvent.EventSubtype != LevelObjectEventType.Dead) return;
            
        }

        private IEnumerator CheckDistanceToCentr() {
            var delay = new WaitForSeconds(1f);
            while (true) {
                if (Vector2.Distance(new Vector2(0, 0), transform.position) > 10) {
                    EventManager.Instance.DispatchEvent(new EnemyAIEvent(EnemyAIEventType.RunAway, this));
                }
                yield return delay;
            }
        }

        private void Update() {
            if (_playerTransform == null)
                _runnerComponent.SetDirection(transform.up, gameObject.transform.localEulerAngles.z);
            else {
                var angle = _playerTransform.position - transform.position;
                var angle_y = Mathf.Atan2(angle.x, angle.y) * Mathf.Rad2Deg;
                _runnerComponent.SetDirection(transform.up, -angle_y);
            }
        }

        private void OnTriggerEnter2D(Collider2D col) {
            var levelObject = col.GetComponent<LevelObject>();
            if (levelObject == null) return;
            if (levelObject.ObjectType == LevelObjectType.Player) {
                _playerTransform = col.transform;
            }
            if (LevelObject.ObjectType == LevelObjectType.Enemy) {
                ChangeDirection();
            }
        }

        private void ChangeDirection() {
            _rotation = _rotation * Quaternion.Euler(0, UnityEngine.Random.Range(-45, 45), 0);
        }
    }
}