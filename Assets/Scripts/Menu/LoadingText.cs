using System;
using TMPro;
using UnityEngine;

namespace Menu
{
    public class LoadingText : MonoBehaviour
    {
        public TextMeshProUGUI tmp;
        private float timeElapsed = 0f;
        private int dots = 5;

        private void Start()
        {
            tmp.text = "Loading";
        }
        private void Update()
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed > 0.5f)
            {
                if (dots > 4)
                {
                    tmp.text = "Loading";
                    dots = 1;
                }
                tmp.text += ".";
                dots++;
                timeElapsed = 0;

            }
        }
    }
}