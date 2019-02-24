using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
	public float explosionForce = 50f;/* 300000f;*/
	public float explosionRadius = 5f;

	private void OnCollisionEnter(Collision col)
	{
		CreateExplosion();
	}

	private void CreateExplosion()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, 5f);

		foreach (Collider hit in colliders)
		{
			if (hit && hit.GetComponent<Rigidbody>())
			{
				hit.GetComponent<Rigidbody>().isKinematic = false;
				hit.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, .3f);
			}
		}

		//TODO: use Object pooling here
		Destroy(gameObject);
	}
}
