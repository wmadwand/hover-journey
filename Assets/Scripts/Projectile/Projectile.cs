using UnityEngine;

public enum ProjectileOwner
{
	Player,
	Enemy
}

public class Projectile : MonoBehaviour
{
	private int _ownerID;
	private Rigidbody playerRb;

	private Vector3 direction;

	public float explosionForce = 50f;/* 300000f;*/
	public float explosionRadius = 5f;

	public int valueDamage = 10;

	//--------------------------------------------------------

	public void SetOwnerType(int value)
	{
		_ownerID = value;
	}

	//--------------------------------------------------------

	private void OnTriggerEnter(Collider other)
	{
		//if (hit.rigidbody != null && !hit.rigidbody.isKinematic)
		//{

		//if (other.GetComponent<WeaponController>().ownerID == _ownerID)
		//{
		//	return;
		//}


		MakeDamage(other);


		CreateExplosion();

	}

	private void MakeDamage(Collider other)
	{
		if (other.GetComponent<Enemy>())
		{
			other.GetComponent<Enemy>().GetDamage(valueDamage);
		}
		else if (other.GetComponent<IObjectHealth>() != null)
		{
			other.GetComponent<IObjectHealth>().GetDamage(valueDamage);
		}
	}

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