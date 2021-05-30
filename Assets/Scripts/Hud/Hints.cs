using TMPro;
using UnityEngine;

namespace Hud
{
    public class Hints : MonoBehaviour
    {
        public TextMeshProUGUI text;

        private void Start()
        {
            text.enabled = false;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                text.enabled = !text.isActiveAndEnabled;
            }
        }
    }
}