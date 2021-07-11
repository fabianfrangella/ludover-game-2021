using Persistence;

namespace Pause
{
    public class SaveGameButton : PauseButton
    {
        public PlayerStatsDAO dao;

        public void Save()
        {
            dao.SaveIntoJson();
        }
        
    }
}