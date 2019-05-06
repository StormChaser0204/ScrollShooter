using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnElement {
    [SerializeField]
    private GameObject _prefab;
    [SerializeField, Range(0, 1)]
    private float _chanceOnSpawn;

    public GameObject Prefab { get { return _prefab; } }
    public float ChanceOnSpawn { get { return _chanceOnSpawn; } }
}
