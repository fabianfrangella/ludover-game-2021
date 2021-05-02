public interface PlayerAttackState 
{
    void HandleAttack();
    PlayerAttackState GetNextState();
}
