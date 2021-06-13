using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace The25thStudio.Util.Levels
{
    [CreateAssetMenu(fileName = "Levels 1", menuName = "The 25th Studio/Levels", order = 0)]
    public class Levels : ScriptableObject, IEnumerable<Level>
    {
        [SerializeField] private string saveGameName = "game.dat";
        [SerializeField] private List<Level> levels;
        [field: NonSerialized] private int _index;


        private void OnEnable()
        {
            _index = 0;

            if (levels.Count > 0)
            {
                levels.First().Unlocked = true;
            }
            
        }

        public void CurrentLevelIndex(int index)
        {
            if (index > 0 && index < levels.Count)
            {
                _index = index;
            }
        }
        public bool HasMoreLevels => levels.Count > (_index + 1);

        public T CurrentLevel<T>() where T : Level => levels[_index] as T;

        public void NextLevel()
        {
            if (!HasMoreLevels) return;
            _index++;
            levels[_index].Unlocked = true;
            Save();
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

        public IEnumerator<Level> GetEnumerator()
        {
            return levels.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}