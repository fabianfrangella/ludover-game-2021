using UnityEngine;

namespace Spells
{
    public class NecromancerShield : MonoBehaviour
    {
        public Transform shieldPrefab;

        private Transform currentShield;
        public void DestroyShield()
        {
            Destroy(currentShield);
        }

        public void ActivateShield()
        {
            currentShield = Instantiate(shieldPrefab, transform.position, Quaternion.identity);
        }

    }
}