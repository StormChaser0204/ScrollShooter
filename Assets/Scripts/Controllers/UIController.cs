using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Engine.Common.Events;
using Engine.Common.Singleton;
using Engine.Components;
using System;
using Engine.Common.Profiles;
using UnityEngine.SceneManagement;

public class UIController : SceneSingleton<UIController> {

    [SerializeField]
    private Text _killCounterText;
    [SerializeField]
    private Text _distanceCounterText;
    [SerializeField]
    private Text _healthCount;
    [SerializeField]
    private GameObject _loseWindow;
    [SerializeField]
    private GameObject _winWindow;



    private int _killCounter;
    private Coroutine _checkDistanceCoroutine;

    protected override void Init() {
        _killCounter = 0;
        _killCounterText.text = string.Format("Kill: {0}", _killCounter);
        EventManager.Instance.AddEventListener<LevelObjectEvent>(OnLevelObjectEvent);
        EventManager.Instance.AddEventListener<HealthComponentEvent>(OnHealthComponentEvent);
        _checkDistanceCoroutine = StartCoroutine(CheckDistance());
    }

    private void OnHealthComponentEvent(HealthComponentEvent healthComponentEvent) {
        if (healthComponentEvent.Target.LevelObject.ObjectType != LevelObjectType.Player) return;
        _healthCount.text = string.Format("Health count: {0}", healthComponentEvent.Target.CurrentHealth.ToString());
    }

    public void ShowLoseWindow() {
        _loseWindow.SetActive(true);
    }

    public void ShowWinWindow() {
        if (ProfileManager.Instance.Profile.CurrentLevel == ProfileManager.Instance.Profile.LastPassedLevel + 1)
            ProfileManager.Instance.Profile.LastPassedLevel++;
        ProfileManager.Instance.Save();
        _winWindow.SetActive(true);
    }

    private void OnDisable() {
        EventManager.Instance?.RemoveEventListener<LevelObjectEvent>(OnLevelObjectEvent);
        EventManager.Instance?.RemoveEventListener<HealthComponentEvent>(OnHealthComponentEvent);
        StopCoroutine(_checkDistanceCoroutine);
    }

    public void LoadMenu() {
        SceneManager.LoadScene("Menu");
    }

    private IEnumerator CheckDistance() {
        var wfs = new WaitForSeconds(1);
        Transform player = LevelController.Instance.PlayerTransform;
        Transform finish = LevelController.Instance.FinishTransform;
        int distance;
        while (true) {
            distance = (int)Vector3.Distance(finish.position, player.position);
            _distanceCounterText.text = string.Format("Distance to the end: {0}", distance - 10);
            yield return wfs;
        }
    }

    private void OnLevelObjectEvent(LevelObjectEvent levelObjectEvent) {
        if (levelObjectEvent.Target.ObjectType != LevelObjectType.Enemy) return;
        if (levelObjectEvent.EventSubtype != LevelObjectEventType.Dead) return;
        _killCounter++;
        _killCounterText.text = string.Format("Kill: {0}", _killCounter);
    }
}
