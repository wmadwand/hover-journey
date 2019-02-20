using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
	Rigidbody playerRb;

	bool flag;
	Vector3 direction;

	public int valueDamage = 10;

	private void Start()
	{
		
	}

	private void OnTriggerEnter(Collider other)
	{
		//if (hit.rigidbody != null && !hit.rigidbody.isKinematic)
		//{

		if (other.GetComponent<PlayerHealth>())
		{
			//TODO: hit.rigidbody
			//other.GetComponent<Rigidbody>().AddForceAtPosition(((mRay.direction * impactForce) / Time.timeScale) / mAdjust, hit.point);
			playerRb = other.GetComponent<Rigidbody>();

			flag = true;

			direction = playerRb.transform.position - transform.position;
			other.GetComponent<Rigidbody>().AddForceAtPosition(-other.GetComponent<Rigidbody>().transform.up * 20f, other.GetComponent<Rigidbody>().transform.position, ForceMode.Acceleration);

			other.GetComponent<PlayerHealth>().GetDamage(valueDamage);

			//TODO: Instantiate the explosion here which will be the cause of addforce

			Destroy(gameObject);
		}
		else if (other.GetComponent<EnemyHealth>())
		{
			playerRb = other.GetComponent<Rigidbody>();

			flag = true;

			direction = playerRb.transform.position - transform.position;
			other.GetComponent<Rigidbody>().AddForceAtPosition(-other.GetComponent<Rigidbody>().transform.up * 20f, other.GetComponent<Rigidbody>().transform.position, ForceMode.Acceleration);

			other.GetComponent<EnemyHealth>().GetDamage(valueDamage);

			Destroy(gameObject);
		}
	}

	private void FixedUpdate()
	{
		//if (flag)
		//{
		//	playerRb.AddForceAtPosition(direction.normalized, transform.position + Vector3.one, ForceMode.Acceleration);
		//}
	}

	//IEnumerator AttackRoutine()
	//{

	//}

	//TODO: replace with checking via ray
}
