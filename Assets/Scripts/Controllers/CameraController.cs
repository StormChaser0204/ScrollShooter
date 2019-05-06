using Engine.Common.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Engine.Components;

public class CameraController : MonoBehaviour {

    [SerializeField]
    private float _speed;

    void Update() {
        transform.position += new Vector3(0, _speed * Time.deltaTime, 0);
    }
}
