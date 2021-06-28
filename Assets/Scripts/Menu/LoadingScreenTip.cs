using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Menu
{
    public class LoadingScreenTip : MonoBehaviour
    {
        public TextMeshProUGUI tmp;
        private float tipTime = 5f;
        private float timeElapsed = 0;
        [SerializeField]
        public string[] tips;

        private void Start()
        {
            tmp.text = tips[Random.Range(0, tips.Length)];
        }

        private void Update()
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed > tipTime)
            {
                tmp.text = tips[Random.Range(0, tips.Length)];
                timeElapsed = 0;
            }
        }
        
    }
}