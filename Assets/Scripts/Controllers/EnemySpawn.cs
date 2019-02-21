﻿using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
	[SerializeField] private Transform[] _spawnPoints;
	[SerializeField] private GameObject _enemyPrefab;
	[SerializeField] private GameObject _enemyPoiPrefab;

	private GameObject _theEnemy;

	public void Execute()
	{
		foreach (var item in _spawnPoints)
		{
			_theEnemy = Instantiate(_enemyPrefab, item.position, item.rotation);
			
		}

	}
}
