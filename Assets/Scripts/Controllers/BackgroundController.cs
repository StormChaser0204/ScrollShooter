using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {

    [SerializeField]
    private Material _backgroundMaterial;
    [SerializeField]
    private Gradient _emissionGradient;

    private Coroutine _updateColor;

    private void Start() {
        _updateColor = StartCoroutine(UpdateColor());
    }
    private void OnDisable() {
        StopCoroutine(_updateColor);
    }

    private IEnumerator UpdateColor() {
        var delayTime = 0.001f;
        var wfs = new WaitForSeconds(delayTime);
        var colors = _emissionGradient.colorKeys;
        float time = 0;
        while (true) {
            _backgroundMaterial.SetColor("_EmissionColor", _emissionGradient.Evaluate(time));
            time += delayTime;
            if (time >= 1) time = 0;
            yield return wfs;
        }
    }
}
