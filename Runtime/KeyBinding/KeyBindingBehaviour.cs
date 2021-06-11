using UnityEngine;

namespace The25thStudio.Util.KeyBinding
{
    public class KeyBindingBehaviour : MonoBehaviour
    {
        [SerializeField] private KeyBinding[] keyBindings;


        private void Update()
        {
            foreach (var binding in keyBindings)
            {
                if (Input.GetKeyDown(binding.KeyCode))
                {
                    binding.Action.Invoke();
                }
            }
        }
    }
}