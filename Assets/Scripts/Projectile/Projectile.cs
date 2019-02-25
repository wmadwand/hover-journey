using UnityEngine;

public enum ProjectileOwner
{
	Player,
	Enemy
}

public class Projectile : MonoBehaviour
{
	public int damage = 10;

	//TODO: move to another class Explosion
	[SerializeField] private float _explosionForce = 40000f;
	[SerializeField] private float _explosionRadius = 5f;
	[SerializeField] private float _explosionUpwardsRate = .3f;

	private int _ownerID;

	//--------------------------------------------------------

	public void SetOwnerType(int value)
	{
		_ownerID = value;
	}

	//--------------------------------------------------------

	private void OnTriggerEnter(Collider other)
	{
		MakeDamage(other);
		CreateExplosion();
	}

	private void MakeDamage(Collider other)
	{
		if (other.GetComponent<IObjectHealth>() == null)
		{
			return;
		}

		other.GetComponent<IObjectHealth>().GetDamage(damage);
	}

	//TODO: move to another class Explosion
	private void CreateExplosion()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

		foreach (Collider hit in colliders)
		{
			if (hit && hit.GetComponent<Rigidbody>())
			{
				hit.GetComponent<Rigidbody>().isKinematic = false;
				hit.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, transform.position, _explosionRadius, _explosionUpwardsRate);
			}
		}

		//TODO: use Object pooling here
		Destroy(gameObject);
	}
}