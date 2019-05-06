using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Components {
    public class SpawnerComponent : BaseComponent { //mb add Editor extension with spawnChange fixation and draw non distributed value
        [SerializeField]
        private List<SpawnElement> _spawnedElements;
        [SerializeField]
        private float _spawnDelay;

        private List<SpawnElement> _sortedSpawnedElements;
        private Coroutine _spawn;

        protected override void Init() {
            _sortedSpawnedElements = new List<SpawnElement>();

            _spawn = StartCoroutine(Spawn());
        }

        private IEnumerator Spawn() {
            var wfs = new WaitForSeconds(_spawnDelay);
            while (true) {
                var rand = Random.Range(0, 1);
                foreach (var element in _spawnedElements) {
                    if (rand <= element.ChanceOnSpawn)
                        SpawnObject(element.Prefab);
                }
                yield return wfs;
            }
        }

        private void SpawnObject(GameObject objectForSpawn) {
            Instantiate(objectForSpawn, transform.position, Quaternion.Euler(new Vector3(0, 0, -180 + Random.Range(-45, 45))));
        }

        private List<SpawnElement> SortArray(List<SpawnElement> array) {
            //Try use Sorting Algorithm
            var sortedArray = new List<SpawnElement>();
            sortedArray = array;
            sortedArray.Sort();
            return sortedArray;
        }
    }
}
