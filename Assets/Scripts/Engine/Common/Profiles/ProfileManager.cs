using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Engine.Common.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Common.Profiles {
    public class ProfileManager : ApplicationSingleton<ProfileManager> {

        public sealed class VersionDeserializationBinder : SerializationBinder {
            public override Type BindToType(string assemblyName, string typeName) {
                if (string.IsNullOrEmpty(assemblyName) || string.IsNullOrEmpty(typeName)) return null;
                assemblyName = Assembly.GetExecutingAssembly().FullName;
                var typeToDeserialize = Type.GetType($"{typeName}, {assemblyName}");
                return typeToDeserialize;
            }
        }

        private PlayerProfile _profile;
        private string _storePath;

        public PlayerProfile Profile { get { return _profile; } }

        protected override void Init() {
            _storePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Path.Combine("ScrollShooter"));

            if (!Directory.Exists(_storePath)) {
                Directory.CreateDirectory(_storePath);
            }

            Load(_storePath);
        }

        public void Save() {
            try {
                var filePath = Path.Combine(_storePath, "player.profile");
                Stream stream = File.Open(filePath, FileMode.Create);
                var bformatter = new BinaryFormatter { Binder = new VersionDeserializationBinder() };
                bformatter.Serialize(stream, _profile);
                stream.Close();
            }
            catch (UnauthorizedAccessException) { }
        }

        public void Load(string storePath) {
            if (_profile != null) return;
            var filePath = Path.Combine(_storePath, "player.profile");
            if (File.Exists(filePath)) {
                try {
                    Stream stream = File.Open(filePath, FileMode.Open);
                    try {
                        var bformatter = new BinaryFormatter { Binder = new VersionDeserializationBinder() };
                        _profile = bformatter.Deserialize(stream) as PlayerProfile;
                        stream.Close();
                    }
                    catch (SerializationException) {
                        stream.Close();
                        File.Delete(filePath);
                    }
                }
                catch (IOException) {
                    Debug.LogError("Profile IO Error");
                }
            }
            if (_profile != null) return;
            if (!Directory.Exists(_storePath)) {
                Directory.CreateDirectory(_storePath);
            }
            _profile = new PlayerProfile();
            Save();
        }
    }
}