using System;

namespace The25thStudio.Util.Levels
{
    [Serializable]
    public class LevelSaveState
    {
        public LevelSaveState(Level level)
        {
            Completed = level.Completed;
            Unlocked = level.Unlocked;
        }

        public bool Completed { get; }
        public bool Unlocked { get; }
    }
}