using UnityEngine;
using Zenject;

public class LevelSelectInstaller : MonoInstaller
{
    [SerializeField] private cLevelSelectButton m_LevelSelectButton;
    public override void InstallBindings()
    {
        Container.BindFactory<cLevelSelectButton, LevelSelectButtonFactory>().FromComponentInNewPrefab(m_LevelSelectButton);
    }
}

public class LevelSelectButtonFactory : PlaceholderFactory<cLevelSelectButton>
{
    
}