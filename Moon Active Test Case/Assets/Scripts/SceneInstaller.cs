using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private cGameLogicManager m_GameLogicManager;
    [SerializeField] private cObjectPooler m_ObjectPooler;
    [SerializeField] private cUIManager m_UIManager;
    [SerializeField] private cSoundManager m_SoundManager;
    [SerializeField] private cInputManager m_InputManager;
    [SerializeField] private cLevelManager m_LevelManager;
    [SerializeField] private cCameraManager m_CameraManager;

    public override void InstallBindings()
    {
        Container.Bind<cGameLogicManager>().FromInstance(m_GameLogicManager);
        Container.Bind<cObjectPooler>().FromInstance(m_ObjectPooler);
        Container.Bind<cUIManager>().FromInstance(m_UIManager);
        Container.Bind<ISoundManager>().To<cSoundManager>().FromInstance(m_SoundManager).NonLazy();
        Container.Bind<IInputManager>().To<cInputManager>().FromInstance(m_InputManager);
        Container.Bind<cLevelManager>().FromInstance(m_LevelManager);
        Container.Bind<cCameraManager>().FromInstance(m_CameraManager);
    }
}