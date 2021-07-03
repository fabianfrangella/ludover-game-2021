using System.Collections.Generic;
using Menu;
using UnityEngine;

namespace Player.UI
{
    public class UIManager : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag("Dungeon"))
            {
                SceneLoader.instance.LoadScene("LoadingScreen");
                var buttons = new List<string>() { "DungeonButton", "SafeZoneButton" };
                SceneLoader.instance.SetButtons(buttons);
            }

            if (other.collider.CompareTag("Portal"))
            {
                SceneLoader.instance.LoadScene("LoadingScreen");
                var buttons = new List<string>() { "OpenLandsButton", "SafeZoneButton" };
                SceneLoader.instance.SetButtons(buttons);
            }
        }
    }
}