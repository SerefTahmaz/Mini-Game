using SimonSays.Managers.GameManager;
using SimonSays.Managers.SaveManager;
using UnityEngine;
using Zenject;

namespace SimonSays.Managers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private cGameManagerStateMachine m_GameManagerStateMachine;
        [SerializeField] private cObjectPooler m_ObjectPooler;
        [SerializeField] private cUIManager m_UIManager;
        [SerializeField] private cSoundManager m_SoundManager;
        [SerializeField] private cInputManager m_InputManager;
        [SerializeField] private cLevelManager m_LevelManager;

        public override void InstallBindings()
        {
            Container.Bind<cGameManagerStateMachine>().FromInstance(m_GameManagerStateMachine);
            Container.Bind<cUIManager>().FromInstance(m_UIManager);
            Container.Bind<IObjectPooler>().To<cObjectPooler>().FromInstance(m_ObjectPooler);
            Container.Bind<ISoundManager>().To<cSoundManager>().FromInstance(m_SoundManager);
            Container.Bind<IInputManager>().To<cInputManager>().FromInstance(m_InputManager);
            Container.Bind<ILevelManager>().To<cLevelManager>().FromInstance(m_LevelManager);
            Container.Bind<ISaveManager>().To<cSaveManager>().AsSingle();
        }
    }
}