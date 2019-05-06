using UnityEngine;

namespace Engine.Components {
    public abstract class BaseComponent : MonoBehaviour {

        public LevelObject LevelObject { get; private set; }

        protected void Awake() {
            LevelObject = GetComponent<LevelObject>();
            Init();
        }

        protected abstract void Init();
    }
}