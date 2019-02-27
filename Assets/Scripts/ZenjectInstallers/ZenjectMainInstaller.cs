using UnityEngine;
using Zenject;
using ZombieMessage;

public class ZenjectMainInstaller : MonoInstaller
{
	[SerializeField] private EnemySpawn _enemySpawn;

    public override void InstallBindings()
    {
		Container.Bind<ZombieMessageService>().FromNewComponentOnNewGameObject().AsSingle();
		Container.Bind<EnemySpawn>().FromInstance(_enemySpawn);
    }
}