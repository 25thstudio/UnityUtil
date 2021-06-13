using UnityEngine;

namespace The25thStudio.Util.Numbers
{
    [CreateAssetMenu(fileName = "Integer 1", menuName = "The 25th Studio/Numbers/Integer", order = 0)]
    public class IntVariable : ScriptableObject
    {
        #region Properties
        [Multiline]
        [SerializeField]
        private string description;

        [SerializeField]
        private int intValue;

        #endregion

        #region Setters and getters
        public int Value { get => intValue; set => intValue = value; }

        public int Increase(int value = 0)
        {
            intValue += value;
            return intValue;
        }

        public int Decrease(int value = 0)
        {
            intValue -= value;
            return intValue;
        }

        public static implicit operator int(IntVariable i) => i.Value;

        #endregion
    }
}