using UnityEngine;

namespace Pause
{
    public class QuitGameButton : PauseButton
    {

        public void Quit()
        {
            Application.Quit();
        }
    }
}