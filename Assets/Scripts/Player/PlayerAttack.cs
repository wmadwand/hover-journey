using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	public float nextShot;
	public float timeBetweenAttacks;
	bool enemyInRange = true;
	float enemyHealth = 10;

	WeaponController weaponController;

	private void Awake()
	{
		weaponController = GetComponent<WeaponController>();
	}

	private void Update()
	{
		if (Input.GetButton("Fire1") && Time.time > nextShot && enemyInRange && enemyHealth > 0)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Shootable")))
			{
				nextShot = Time.time + timeBetweenAttacks;
				weaponController.Fire(hit.point - transform.position);
			}
		}
	}
}