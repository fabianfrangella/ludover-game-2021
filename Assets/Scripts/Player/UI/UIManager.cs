using System.Collections.Generic;
using Menu;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player.UI
{
    public class UIManager : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag("Dungeon"))
            {
                SceneManager.LoadScene("LoadingScreen");
                var buttons = new List<string>() { "DungeonButton", "SafeZoneButton" };
                SceneLoader.instance.SetButtons(buttons);
            }

            if (other.collider.CompareTag("Portal"))
            {
                SceneManager.LoadScene("LoadingScreen");
                var buttons = new List<string>() { "OpenLandsButton", "SafeZoneButton" };
                SceneLoader.instance.SetButtons(buttons);
            }
        }
    }
}