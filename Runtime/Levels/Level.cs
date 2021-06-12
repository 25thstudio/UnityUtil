using System;
using UnityEngine;

namespace The25thStudio.Util.Levels
{
    public abstract class Level : ScriptableObject
    {
        [field: NonSerialized] public bool Completed { get; set; }

        public virtual LevelSaveState SaveState()
        {
            return new LevelSaveState(Completed);
        }

        public virtual void LoadState(LevelSaveState state)
        {
            Completed = state.Completed;
        }
    }
}