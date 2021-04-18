using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{

    public int damage;
    float timeSinceLastAttack;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<PlayerController>().OnAttacking += SetAttack;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeSinceLastAttack= timeSinceLastAttack + Time.deltaTime;
        if (timeSinceLastAttack > 0.5)
        {
            gameObject.SetActive(false);
        }
    }

    private void SetAttack()
    {
        gameObject.SetActive(true);
        timeSinceLastAttack = 0;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealthManager>().OnDamageReceived(damage);
        }
    }
}
