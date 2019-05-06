using UnityEngine;

namespace Engine.Components {
    public class DamageDealerComponent : BaseComponent {

        [SerializeField]
        private int _damage;
        public int Damage {
            get {
                return _damage;
            }
        }

        protected override void Init() {
        }
    }
}