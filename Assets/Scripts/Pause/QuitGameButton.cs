using Audio;
using UnityEngine;

namespace Pause
{
    public class QuitGameButton : PauseButton
    {
        public void Quit()
        {
            audioManager.Play("Press");
            Application.Quit();
        }
    }
}