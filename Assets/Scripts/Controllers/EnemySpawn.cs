using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[Serializable]
public struct SpawnPoint
{
	public int id;
	public string name;
	public Transform transform;
	public float weight;
	public Color color;
}

public class EnemySpawn : MonoBehaviour
{
	public static event Action OnAllEnemiesDestroy;
	public static event Action<Dictionary<SpawnPoint, Enemy>> OnEnemiesSpawned;

	//[SerializeField] private Transform[] _spawnPointss;
	[SerializeField] private GameObject _enemyPrefab;

	public List<SpawnPoint> _spawnPointsOrigin = new List<SpawnPoint>();

	private Dictionary<SpawnPoint, Enemy> activeEnemies = new Dictionary<SpawnPoint, Enemy>();

	private GameObject _theEnemy;

	public int enemiesCount;

	bool gameOver;

	private void Awake()
	{
		Enemy.OnDie += Enemy_OnDie;
	}

	private void Update()
	{
		if (gameOver)
		{
			Debug.Log("Game over");

			OnAllEnemiesDestroy?.Invoke();

			gameOver = false;
		}
	}

	private void Enemy_OnDie(Enemy obj)
	{
		var key = activeEnemies.FirstOrDefault(item => item.Value == obj).Key;
		activeEnemies.Remove(key);

		if (activeEnemies.Count <= 0)
		{
			gameOver = true;
		}
	}

	void ResetSpawn()
	{
		activeEnemies.Values.ToList().ForEach(item => item.DestroyAllEnemyStuff());

		activeEnemies.Clear();
	}

	private void OnDestroy()
	{
		Enemy.OnDie -= Enemy_OnDie;
	}

	public void Execute()
	{
		gameOver = false;

		ResetSpawn();

		int enCount = Mathf.Min(enemiesCount, _spawnPointsOrigin.Count);

		for (int i = 0; i < enCount; i++)
		{
			_theEnemy = Instantiate(_enemyPrefab);


			SpawnPoint point = GetFreeSpawnPoint();

			//_theEnemy.GetComponent<Enemy>().Poi.InitView(point.name, point.color);

			Enemy enemy = _theEnemy.GetComponent<Enemy>();
			enemy.Poi.InitView(point.name, point.color);

			_theEnemy.transform.SetPositionAndRotation(point.transform.position, point.transform.rotation);

			activeEnemies[point] = _theEnemy.GetComponent<Enemy>();

			
		}

		OnEnemiesSpawned?.Invoke(activeEnemies);

	}

	SpawnPoint GetFreeSpawnPoint()
	{
		while (true)
		{
			int pointIndex = UnityEngine.Random.Range(0, _spawnPointsOrigin.Count);

			if (!activeEnemies.ContainsKey(_spawnPointsOrigin[pointIndex]))
			{
				return _spawnPointsOrigin[pointIndex];
			}
		}
	}
}
