using UnityEngine;

namespace Enemy
{
    public class NecromancerAnimationManager : EnemyAnimationManager
    {
        public float timeToDisappearBody = 5;
        private bool isDead = false;
        private float timeSinceDeath = 0;
        
        private void Start()
        {
            animator = gameObject.GetComponent<Animator>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (isDead)
            {
                HandleDeath();
            }
        }
        public override void StartDieAnimation(float health)
        {
            isDead = health <= 0;
            if (isDead)
            {
                animator.SetTrigger("Die");
            }
        }
        
        public override void PlayAttackAnimation()
        {
            animator.SetTrigger("Cast");
        }
        
        private void HandleDeath()
        {
            if (timeSinceDeath < timeToDisappearBody)
            {
                timeSinceDeath += Time.deltaTime;
            }
            if (timeSinceDeath > timeToDisappearBody)
            {
                gameObject.SetActive(false);
            }
        }
    }
    
}