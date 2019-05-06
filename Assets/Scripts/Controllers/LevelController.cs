using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Engine.Common.Singleton;
using Engine.Common.Events;
using System;

public class LevelController : SceneSingleton<LevelController> {
    [SerializeField]
    private Transform _playerTransform;
    [SerializeField]
    private Transform _finishTransform;

    public Transform PlayerTransform { get { return _playerTransform; } }
    public Transform FinishTransform { get { return _finishTransform; } }

    private Coroutine _checkDistanceCoroutine;

    protected override void Init() {
        Time.timeScale = 1;
        EventManager.Instance.AddEventListener<LevelObjectEvent>(OnLevelObjectEvent);
        _checkDistanceCoroutine = StartCoroutine(CheckDistance());

    }

    private void OnDisable() {
        StopCoroutine(_checkDistanceCoroutine);
    }

    private void OnLevelObjectEvent(LevelObjectEvent levelObjectEvent) {
        if (levelObjectEvent.Target.ObjectType != Engine.Components.LevelObjectType.Player) return;
        if (levelObjectEvent.EventSubtype != LevelObjectEventType.Dead) return;
        StartCoroutine(LoseGame());
    }

    private IEnumerator CheckDistance() {
        var wfs = new WaitForSeconds(1);
        Transform player = PlayerTransform;
        Transform finish = FinishTransform;
        int distance;
        while (true) {
            distance = (int)Vector3.Distance(finish.position, player.position);
            if (distance <= 10) StartCoroutine(WinGame());
            yield return wfs;
        }
    }

    private IEnumerator LoseGame() {
        yield return new WaitForSeconds(1);
        Time.timeScale = 0;
        UIController.Instance.ShowLoseWindow();
        Cursor.visible = true;
    }


    private IEnumerator WinGame() {
        StopCoroutine(_checkDistanceCoroutine);
        yield return new WaitForSeconds(1);
        Time.timeScale = 0;
        UIController.Instance.ShowWinWindow();
        Cursor.visible = true;
    }
}
