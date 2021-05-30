using TMPro;
using UnityEngine;

namespace Hud
{
    public class ItemUI : MonoBehaviour
    {
        public TextMeshProUGUI text;
        public ItemEnum item;
        private PlayerInventory playerInventory;
        private void Start()
        {
            playerInventory = FindObjectOfType<PlayerInventory>();
        }

        private void Update()
        {
            text.text = playerInventory.FindItemQuantity(item).ToString();
        }
    }
}