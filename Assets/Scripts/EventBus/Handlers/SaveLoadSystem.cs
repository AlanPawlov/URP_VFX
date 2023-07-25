using UnityEngine;

namespace EventBus.Handlers
{
    public class SaveLoadSystem : MonoBehaviour, IQuickSaveLoadHandler
    {
        private void OnEnable()
        {
            EventBus.Subscribe(this);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe(this);
        }

        public void HandleQuickSave()
        {
            Debug.Log("Quick save");
        }

        public void HandleQuickLoad()
        {
            Debug.Log("Quick load");
        }
    }
}
