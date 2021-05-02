using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{

    private PlayerAttackState playerAttackState;

    // Start is called before the first frame update
    void Start()
    {
        playerAttackState = GetComponent<PlayerMeleeAttackManager>();
    }

    private void Update()
    {
        HandleAttack();
        HandleChangeAttackMode();
    }

    private void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerAttackState.HandleAttack();
        }
    }

    private void HandleChangeAttackMode()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAttackState = playerAttackState.GetNextState();
        }
    }

}
