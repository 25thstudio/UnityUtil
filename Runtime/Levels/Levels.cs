using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Events;

namespace The25thStudio.Util.Levels
{
    [CreateAssetMenu(fileName = "Levels 1", menuName = "The 25th Studio/Levels", order = 0)]
    public class Levels : ScriptableObject, IEnumerable<Level>
    {
        [SerializeField] private string saveGameName = "game.dat";
        [SerializeField] private List<Level> levels;


        private void OnEnable()
        {
            LevelIndex = 0;

            if (levels != null && levels.Count > 0)
            {
                UnlockLevel(levels.First(), 0);
            }
            
        }

        [field: NonSerialized]
        public int LevelIndex { get; private set; }

        public void UpdateLevelIndex(int index)
        {
            if (index >= 0 && index < levels.Count)
            {
                LevelIndex = index;
            }
        }
        public bool HasMoreLevels => levels.Count > (LevelIndex + 1);

        public T CurrentLevel<T>() where T : Level => levels[LevelIndex] as T;

        public bool NextLevel()
        {
            if (!HasMoreLevels) return false;
            LevelIndex++;
            UnlockLevel(levels[LevelIndex], LevelIndex);
            Save();
            return true;
        }

        public void AddLevel(Level level)
        {
            levels ??= new List<Level>();
            levels.Add(level);
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

        private static void UnlockLevel(Level level, int index)
        {
            level.Unlocked = true;
        }
        private string Path()
        {
            return $"{Application.persistentDataPath}/{saveGameName}";
        }

        public int Count => levels.Count;
        
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