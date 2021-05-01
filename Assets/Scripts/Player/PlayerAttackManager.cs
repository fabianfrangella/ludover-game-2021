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
        ChangeAttackMode();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerAttackState.HandleAttack();
        }
    }

    private void ChangeAttackMode()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAttackState = playerAttackState.GetNextState();
        }
    }

}
