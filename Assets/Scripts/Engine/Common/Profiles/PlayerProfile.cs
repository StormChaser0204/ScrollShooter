namespace Engine.Common.Profiles {
    [System.Serializable]
    public class PlayerProfile {

        public PlayerProfile() {
            _lastPassedLevel = 0;
        }

        private int _lastPassedLevel;
        private int _currentLevel;

        public int LastPassedLevel {
            get { return _lastPassedLevel; }
            set { _lastPassedLevel = value; }
        }

        public int CurrentLevel {
            get { return _currentLevel; }
            set { _currentLevel = value; }
        }
    }
}