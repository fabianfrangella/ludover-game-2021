namespace Persistence
{
    [System.Serializable]
    public class PlayerStatsData
    {
        public float health;
        public float mana;
        public float stamina;

        public float maxHealth;
        public float maxMana;
        public float maxStamina;

        public float meleeDamage;
        public float spellDamage;

        public float currentExperience;
        public float nextLevelExperience;
        
        public int level;
        public int maxLevel;

        public string scene;

        public Position position;

    }

    [System.Serializable]
    public class Position
    {
        public float x;
        public float y;

        public Position(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }
}