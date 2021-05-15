using UnityEngine;

public class Shield : MonoBehaviour
{
    private PlayerHealthManager playerHealthManager;
    private PlayerSpellManager playerSpellManager;
    private float duration;
    private float absorption;
    void Awake()
    {
        duration = 30;
        absorption = 20;
        playerSpellManager = FindObjectOfType<PlayerSpellManager>();
        playerHealthManager = FindObjectOfType<PlayerHealthManager>();
        playerHealthManager.OnPlayerDeath += HandleOnDeath;
    }

    void Update()
    {
        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            playerHealthManager.SubstractDamageAbsorption(absorption);
            playerSpellManager.SetBuffActive(false);
            Destroy(gameObject);
        }
    }

    public void SetAbsorption()
    {
        playerHealthManager.AddDamageAbsorption(absorption);
    }

    private void HandleOnDeath()
    {
        playerHealthManager.SubstractDamageAbsorption(absorption);
        playerSpellManager.SetBuffActive(false);
        playerHealthManager.OnPlayerDeath -= HandleOnDeath;
        Destroy(gameObject);
    }
}
