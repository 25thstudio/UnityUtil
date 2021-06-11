using UnityEngine;
using UnityEngine.Events;

namespace The25thStudio.Util.KeyBinding
{
    [System.Serializable]
    public class KeyBinding
    {
        [SerializeField] private KeyCode keyCode;
        [SerializeField] private UnityEvent action;
        
        public KeyCode KeyCode => keyCode;
        public UnityEvent Action => action;
    }
}