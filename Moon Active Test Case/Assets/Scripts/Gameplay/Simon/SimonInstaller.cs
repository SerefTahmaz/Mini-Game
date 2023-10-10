using System;
using UnityEngine;
using Zenject;

namespace SimonSays.Gameplay
{
    public class SimonInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<cSimon3DInputHandler>().AsSingle();
        }
    }
}