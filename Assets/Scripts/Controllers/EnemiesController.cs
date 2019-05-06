using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Engine.Common.Singleton;
using Engine.Common.Profiles;

public class EnemiesController : SceneSingleton<EnemiesController> {

    [SerializeField]
    private List<GameObject> _spawnerPrefabs;
    [SerializeField]
    private Transform _LevelObjectTransform;

    private List<GameObject> _spawnedObjects;

    protected override void Init() {
        _spawnedObjects = new List<GameObject>();
    }

    private void Start() {
        var spawnerCount = 10 + 2 * ProfileManager.Instance.Profile.CurrentLevel;
        while (spawnerCount > 0) {
            _spawnedObjects.Add(Instantiate(_spawnerPrefabs[Random.Range(0, _spawnerPrefabs.Count)], GetPosition(), Quaternion.identity, _LevelObjectTransform));
            spawnerCount--;
        }
    }

    private Vector3 GetPosition() {
        Vector3 randomPosition = new Vector3();
        for (int i = 0; i < 50; i++) {
            randomPosition = GetRandomPosition();
            if (IsValid(randomPosition)) {
                break;
            }
            else {
                continue;
            }
        }
        return randomPosition;
    }

    private bool IsValid(Vector3 candidate) {
        foreach (var obj in _spawnedObjects) {
            if (Vector3.Distance(candidate, obj.transform.position) > 15) {
                continue;
            }
            return false;
        }
        return true;
    }

    private Vector3 GetRandomPosition() {
        return new Vector3(Random.Range(-5, 5), Random.Range(-36, 45), 0);
    }
}
