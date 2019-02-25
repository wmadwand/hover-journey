using UnityEngine;
using Zenject;
using ZombieMessage;

public class ZenjectMainInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
		Container.Bind<ZombieMessageService>().FromNewComponentOnNewGameObject().AsSingle();
    }
}