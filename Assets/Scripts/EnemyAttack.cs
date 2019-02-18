using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
	[SerializeField] private GameObject _missile;
	[SerializeField] private Transform _shotSpawn;
	[SerializeField] private float _shotWait;
	[SerializeField] private float _delay;

	private void OnEnable()
	{
		InvokeRepeating("Fire", Random.Range(1, _delay), Random.Range(1.5f, _shotWait));
	}

	private void OnDisable()
	{
		CancelInvoke("Fire");
	}

	void Fire()
	{
		Instantiate(_missile, _shotSpawn.position, _shotSpawn.rotation);		
	}
}
