using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerHealthManager playerHealthManager;

    public event System.Action OnPlayerDeath;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        if (!playerHealthManager.IsAlive())
        {
            if (OnPlayerDeath != null)
            {
                OnPlayerDeath();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

 
}
