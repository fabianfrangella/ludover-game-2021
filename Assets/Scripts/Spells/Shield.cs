using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    private PlayerHealthManager playerHealthManager;
    private float duration;
    private float absorption;
    void Start()
    {
        duration = 10;
        absorption = 1000;
        playerHealthManager = FindObjectOfType<PlayerHealthManager>();
        playerHealthManager.AddDamageAbsorption(absorption);
    }

    void Update()
    {
        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            playerHealthManager.SubstractDamageAbsorption(absorption);
            Destroy(gameObject);
        }
    }

}
