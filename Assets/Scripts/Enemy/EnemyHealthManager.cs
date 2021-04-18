using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int health;
    public int maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool IsAlive()
    {
        return health <= 0;
    }

    public void OnDamageReceived(int damage)
    {
        
        health -= damage;
        if (health < 0)
        {
            Destroy(gameObject);
        }
        
    }

    public void OnHealing(int healing)
    {
        if (health + healing >= maxHealth)
        {
            health = maxHealth;
            return;
        }
        health += healing;
    }
}
