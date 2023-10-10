using SimonSays.Managers.Config;

namespace SimonSays.Managers
{
    public interface ILevelManager
    {
        public void LoadCurrentLevel(cGameConfiguration gameConfiguration);
        public void RemoveCurrentLevel();
    }
}