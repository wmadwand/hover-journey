using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyAttack : MonoBehaviour
{
	[SerializeField] private GameObject _projectile;
	[SerializeField] private Transform _shotSpawn;
	[SerializeField] private float _shotWait;
	[SerializeField] private float _delay;

	bool playerInRange;

	float nextShotTime;
	float timeBetweenShots = 3f;

	float enemyHealth = 100;

	WeaponController weaponController;

	private void Awake()
	{
		weaponController = GetComponent<WeaponController>();
	}

	private void Update()
	{
		CheckForPlayer();

		if (Time.time > nextShotTime && playerInRange && enemyHealth > 0)
		{
			nextShotTime = Time.time + timeBetweenShots;

			weaponController.Fire(transform.forward);
		}

	}

#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
	}
#endif

	void CheckForPlayer()
	{
		Vector3 direction = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
		float isPlayerInRangeAngle = Vector3.Dot(transform.forward, direction);

		if (isPlayerInRangeAngle > .25f)
		{
			if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) <= 10)
			{
				playerInRange = true;
			}
			else
			{
				playerInRange = false;
			}
		}
		else
		{
			playerInRange = false;
		}
	}
}
