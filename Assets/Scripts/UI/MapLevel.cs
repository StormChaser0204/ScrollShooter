using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapLevel : MonoBehaviour {
    [SerializeField]
    private int _targetLevel;

    public int TargetLevel { get { return _targetLevel; } }

    private Button _button;

    public void Init(bool levelOpened) {
        _button = GetComponent<Button>();
        _button.interactable = levelOpened;
        _button.onClick.AddListener(() => MapController.Instance.OpenLevel(_targetLevel));
    }


}
