using Engine.Common.Profiles;
using Engine.Common.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapController : SceneSingleton<MapController> {
    [SerializeField]
    private List<MapLevel> _levels;

    public void OpenLevel(int targetLevel) {
        ProfileManager.Instance.Profile.CurrentLevel = targetLevel;
        SceneManager.LoadScene("Game");
    }


    protected override void Init() {
        var lastPassedLevel = ProfileManager.Instance.Profile.LastPassedLevel;
        foreach (var level in _levels) {
            level.Init(lastPassedLevel + 1 >= level.TargetLevel);
        }
    }
}
