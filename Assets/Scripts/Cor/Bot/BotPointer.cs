using UnityEngine;

namespace BlueStellar.Cor
{
    public class BotPointer : MonoBehaviour
    {
        private void Start()
        {
            PointerController.Instance.AddToList(this);
        }

        public void Remove()
        {
            PointerController.Instance.RemoveFromList(this);
        }
    }
}
