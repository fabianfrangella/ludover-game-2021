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
        if (Input.GetMouseButtonDown(0))
        {
            playerAttackState.HandleAttack();
        }
        ChangeAttackMode();
    }

    private void ChangeAttackMode()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAttackState = playerAttackState.GetNextState();
        }
    }

}
