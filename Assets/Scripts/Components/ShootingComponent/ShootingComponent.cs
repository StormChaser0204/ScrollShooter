using System;
using System.Collections;
using System.Collections.Generic;
using Engine.Common.Events;
using UnityEngine;

namespace Engine.Components {
    public class ShootingComponent : BaseComponent {
        [SerializeField]
        private GameObject _shellPrefab;
        [SerializeField]
        private float _cooldownTime;
        [SerializeField]
        private List<Transform> _shootTransforms;

        private bool _canShoot;
        private Vector3 _shootOffset;
        private Coroutine _shooting;

        protected override void Init() {
            _canShoot = true;
            _shooting = StartCoroutine(Shooting());
        }

        private void OnDisable() {
            StopCoroutine(_shooting);
        }

        private IEnumerator Shooting() {
            var wfs = new WaitForSeconds(_cooldownTime);
            while (_canShoot) {
                GameObject spawnedShell;
                foreach (var shootTransform in _shootTransforms) {
                    spawnedShell = Instantiate(_shellPrefab, shootTransform.position, gameObject.transform.rotation);
                    spawnedShell.GetComponent<RunnerComponent>().SetDirection(Vector3.up, 0);
                }
                yield return wfs;
            }
        }
    }
}