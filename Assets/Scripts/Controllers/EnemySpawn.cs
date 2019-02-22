using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
	[SerializeField] private Transform[] _spawnPoints;
	[SerializeField] private GameObject _enemyPrefab;

	private GameObject _theEnemy;

	public int enemiesCount;

	public void Execute()
	{
		for (int i = 0; i < enemiesCount; i++)
		{
			_theEnemy = Instantiate(_enemyPrefab, _spawnPoints[i].position, _spawnPoints[i].rotation);
		}

		//foreach (var item in _spawnPoints)
		//{
		//	_theEnemy = Instantiate(_enemyPrefab, item.position, item.rotation);

		//}

	}
}
