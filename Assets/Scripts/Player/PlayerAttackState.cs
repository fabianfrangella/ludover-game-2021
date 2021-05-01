using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerAttackState 
{
    void HandleAttack();
    PlayerAttackState GetNextState();
}
