using UnityEngine;

namespace The25thStudio.Util.Numbers
{
    [CreateAssetMenu(fileName = "Float 1", menuName = "The 25th Studio/Numbers/Float", order = 1)]
    public class FloatVariable : ScriptableObject
    {
        #region Properties
        [Multiline]
        [SerializeField]
        private string description;
        
        [SerializeField]
        private float floatValue;

        #endregion

        #region Setters and getters
        public float Value { get => floatValue; set => floatValue = value; }

        public float Increase(float value = 0.0f)
        {
            floatValue += value;
            return floatValue;
        }

        public float Decrease(float value = 0.0f)
        {
            floatValue -= value;
            return floatValue;
        }
        
        public static implicit operator float(FloatVariable i) => i.Value;
        #endregion  
    }
}