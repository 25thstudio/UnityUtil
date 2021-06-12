using System;

namespace The25thStudio.Util.Levels
{
    [Serializable]
    public class LevelSaveState
    {
        public LevelSaveState(bool pCompleted)
        {
            Completed = pCompleted;
        }

        public bool Completed { get; }
    }
}