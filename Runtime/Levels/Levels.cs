using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace The25thStudio.Util.Levels
{
    [CreateAssetMenu(fileName = "Levels 1", menuName = "The 25h Studio/Levels", order = 0)]
    public class Levels : ScriptableObject
    {
        [SerializeField] private string saveGameName = "game.dat";
        [SerializeField] private List<Level> levels;
        [field: NonSerialized] private int _index;


        private void Awake()
        {
            _index = 0;
        }

        public bool HasMoreLevels => levels.Count > (_index + 1);

        public T CurrentLevel<T>() where T : Level => levels[_index] as T;

        public void NextLevel()
        {
            if (!HasMoreLevels) return;
            _index++;
        }

        public void Save()
        {
            var formatter = new BinaryFormatter();

            var stream = new FileStream(Path(), FileMode.Create);

            var data = new List<LevelSaveState>();
            levels.ForEach(l => data.Add(l.SaveState()));


            formatter.Serialize(stream, data);
            stream.Close();
        }

        public void Load()
        {
            if (!File.Exists(Path())) return;

            var formatter = new BinaryFormatter();
            var stream = new FileStream(Path(), FileMode.Open);

            if (formatter.Deserialize(stream) is List<LevelSaveState> data)
            {
                for (var i = 0; i < data.Count; i++)
                {
                    if (i >= levels.Count) return;

                    levels[i].LoadState(data[i]);
                }
            }

            stream.Close();
        }

        private string Path()
        {
            return $"{Application.persistentDataPath}/{saveGameName}";
        }
    }
}