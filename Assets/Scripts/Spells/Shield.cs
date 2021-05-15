using UnityEngine;

public class Shield : MonoBehaviour
{
    private PlayerHealthManager playerHealthManager;
    private PlayerSpellManager playerSpellManager;
    private float duration;
    private float absorption;
    void Start()
    {
        duration = 10;
        absorption = 20;
        playerSpellManager = FindObjectOfType<PlayerSpellManager>();
        playerHealthManager = FindObjectOfType<PlayerHealthManager>();
        playerHealthManager.AddDamageAbsorption(absorption);
    }

    private void OnDestroy()
    {
        playerHealthManager.SubstractDamageAbsorption(absorption);
        playerSpellManager.SetBuffActive(false);
    }

    void Update()
    {
        //transform.position = playerHealthManager.transform.position;
        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            Destroy(gameObject);
        }
    }

}
