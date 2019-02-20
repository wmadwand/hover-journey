﻿using System.Collections;
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

	private void Update()
	{
		if (Time.time > nextShotTime && playerInRange && enemyHealth > 0)
		{
			nextShotTime = Time.time + timeBetweenShots;

			Fire();
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
		float isPlayerInRangeAngle = Vector3.Dot(direction, transform.forward);

		if (Vector3.Dot(direction, transform.forward) > .25f)
		{
			if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) <= 10)
			{
				playerInRange = true;
			}
		}
		else
		{
			playerInRange = false;
		}
	}

	//private void OnTriggerEnter(Collider other)
	//{
	//	if (other.gameObject.tag == "Player")
	//	{
	//		playerInRange = true;
	//	}
	//}

	//private void OnTriggerExit(Collider other)
	//{
	//	if (other.gameObject.tag == "Player")
	//	{
	//		playerInRange = false;
	//	}

	//}

	void Fire()
	{

		Instantiate(_projectile, _shotSpawn.position, Quaternion.identity);

		//TODO: set fireDirection vector3, projectileSpeed

		//theProjectile.LookAt(theProjectile.position + fireDirection);
		//theProjectile.rigidbody.velocity = fireDirection * projectileSpeed;
	}
}
