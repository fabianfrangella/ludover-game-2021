using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hud
{
    public class Hints : MonoBehaviour
    {
        public Image image;
        private void Start()
        {
            image.enabled = false;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                image.enabled = !image.isActiveAndEnabled;
            }
        }
    }
}