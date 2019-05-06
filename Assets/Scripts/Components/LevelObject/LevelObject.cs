using Engine.Common.Events;
using Engine.Components.State;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Components {
    public class LevelObject : MonoBehaviour {
        [SerializeField]
        private LevelObjectType _levelObjectType;
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private string _currentStateName;
        [SerializeField]
        private List<LevelObjectState> _states;

        private string _animatorStateName;
        private Coroutine _delayEnableCoroutine;
        private Coroutine _delayChangeState;
        private Coroutine _death;

        public LevelObjectState CurrentState { get; private set; }
        public LevelObjectType ObjectType { get { return _levelObjectType; } }

        private void UpdateState() {
            var stateChanged = CurrentState == null || CurrentState.Name != _currentStateName;
            foreach (var state in _states) {
                if (state.Name != _currentStateName) continue;
                CurrentState = state;
                if (string.IsNullOrEmpty(CurrentState.NextStateName)) break;
                _delayChangeState = StartCoroutine(DelayChangeState(CurrentState.NextStateName, CurrentState.DelayBeforeChangeState));
            }

            if (stateChanged)
                EventManager.Instance.DispatchEvent(new LevelObjectEvent(LevelObjectEventType.StateChanged, this));
            if (string.IsNullOrEmpty(CurrentState.StateAnimationName)) return;
            _animator.Play(CurrentState.StateAnimationName);
        }

        private IEnumerator DelayChangeState(string nextStateName, float delay) {
            yield return new WaitForSeconds(delay);
            _currentStateName = nextStateName;
            UpdateState();
        }

        private void OnEnable() {
            _delayEnableCoroutine = StartCoroutine(DelayedEnable());
            EventManager.Instance.DispatchEvent(new LevelObjectEvent(LevelObjectEventType.Enabled, this));
        }

        private void OnDisable() {
            if (_delayEnableCoroutine != null)
                StopCoroutine(_delayEnableCoroutine);
            EventManager.Instance?.RemoveEventListener<HealthComponentEvent>(OnHealthComponentEvent);
            EventManager.Instance?.RemoveEventListener<DamageDealerComponentEvent>(OnDamageDealerComponentEvent);
            EventManager.Instance?.DispatchEvent(new LevelObjectEvent(LevelObjectEventType.Disabled, this));
        }

        private IEnumerator DelayedEnable() {
            yield return new WaitForEndOfFrame();
            EventManager.Instance.DispatchEvent(new LevelObjectEvent(LevelObjectEventType.Enabled, this));
            EventManager.Instance.AddEventListener<HealthComponentEvent>(OnHealthComponentEvent);
            EventManager.Instance.AddEventListener<DamageDealerComponentEvent>(OnDamageDealerComponentEvent);
            UpdateState();
        }

        private void OnDamageDealerComponentEvent(DamageDealerComponentEvent damageDealerComponentEvent) {
            if (damageDealerComponentEvent.Target.LevelObject != this) return;
            _currentStateName = "DoDamage";
            UpdateState();
        }

        private void OnHealthComponentEvent(HealthComponentEvent healthComponentEvent) {
            if (healthComponentEvent.Target.LevelObject != this) return;
            if (healthComponentEvent.Target.CurrentHealth > 0) {
                _currentStateName = "TakeDamage";
                UpdateState();
                return;
            }
            _currentStateName = "Death";
            UpdateState();
            _death = StartCoroutine(Death());
            EventManager.Instance.DispatchEvent(new LevelObjectEvent(LevelObjectEventType.Dead, this));
        }

        private IEnumerator Death() {
            var wait = new WaitForEndOfFrame();
            while (!_animator.GetCurrentAnimatorStateInfo(0).IsName(CurrentState.StateAnimationName)) {
                yield return wait;
            }

            var currentClipInfo = _animator.GetCurrentAnimatorClipInfo(0);

            if (currentClipInfo.Length == 0 || currentClipInfo[0].clip.isLooping) {
                yield break;
            }

            yield return new WaitForSeconds(currentClipInfo[0].clip.length);
            gameObject.SetActive(false);
        }
    }

    public enum LevelObjectType {
        Enemy = 0,
        Player = 1,
        Shell = 2,
        Spawner = 3
    }
}