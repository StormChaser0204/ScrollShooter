using UnityEngine;

namespace Engine.Components.State {
    [System.Serializable]
    public class LevelObjectState {
        [SerializeField]
        private string _name;
        [SerializeField]
        private string _stateAnimationName;
        [SerializeField]
        private bool _canMove;
        [SerializeField]
        private bool _canTakeDamage;
        [SerializeField]
        private float _delayBeforeNextState;
        [SerializeField]
        private string _nextStateName;

        public string Name => _name;
        public string StateAnimationName => _stateAnimationName;
        public bool CanMove => _canMove;
        public bool CanTakeDamage => _canTakeDamage;
        public float DelayBeforeChangeState => _delayBeforeNextState;
        public string NextStateName => _nextStateName;
    }
}