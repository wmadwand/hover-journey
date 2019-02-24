using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyList : MonoBehaviour
{
	public GameObject itemPref;
	public Transform parent;

	Dictionary<Enemy, GameObject> items = new Dictionary<Enemy, GameObject>();

	private void Awake()
	{
		EnemySpawn.OnEnemiesSpawned += EnemySpawn_OnEnemiesSpawned;
		Enemy.OnDie += Enemy_OnDie;
	}

	private void Enemy_OnDie(Enemy obj)
	{
		items[obj].GetComponentInChildren<TextMeshProUGUI>().text = $"<s>{items[obj].GetComponentInChildren<TextMeshProUGUI>().text}</s>";
	}

	private void EnemySpawn_OnEnemiesSpawned(Dictionary<SpawnPoint, Enemy> obj)
	{
		ClearAll();

		foreach (var item in obj)
		{
			GameObject go = Instantiate(itemPref, parent);

			go.GetComponentInChildren<TextMeshProUGUI>().text = $" Enemy {item.Key.name}";
			go.GetComponentInChildren<TextMeshProUGUI>().color = item.Key.color;

			items[item.Value] = go;

		}
	}

	public void ClearAll()
	{
		foreach (var item in items)
		{
			Destroy(item.Value);
		}

		items.Clear();
	}
}
