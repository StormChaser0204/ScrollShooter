﻿using UnityEngine;

namespace Engine.Common.Singleton {
    public abstract class ApplicationSingleton<T> : SceneSingleton<T> where T : ApplicationSingleton<T> {

        protected override void Awake() {
            base.Awake();
            if (Instance == this && Application.isPlaying) {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}