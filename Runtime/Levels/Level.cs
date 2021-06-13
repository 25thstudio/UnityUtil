using System;
using UnityEngine;

namespace The25thStudio.Util.Levels
{
    public abstract class Level : ScriptableObject
    {
        [field: NonSerialized] public bool Completed { get; set; }
        [field: NonSerialized] public bool Unlocked { get; set; }

        public virtual LevelSaveState SaveState()
        {
            return new LevelSaveState(this);
        }

        public virtual void LoadState(LevelSaveState state)
        {
            Completed = state.Completed;
            Unlocked = state.Unlocked;
        }
    }
}