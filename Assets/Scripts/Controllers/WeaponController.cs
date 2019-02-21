using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
	public int ownerID;

	public GameObject _projectileGO;
	public Transform _shotSpawn;


	GameObject _theProjectileGO;
	bool isInfiniteAmmo;

	Transform myTransform;

	int myLayer;

	Projectile theProjectileController;

	public int ammo = 100;
	public int maxAmmo = 100;

	public Collider parentCollider;

	[System.NonSerialized]
	public Transform theProjectile;

	public Vector3 spawnPosOffset;
	public float forwardOffset = 1.5f;
	public float reloadTime = 0.2f;
	public float projectileSpeed = 10f;

	private void Start()
	{
		Init();
	}

	public virtual void Init()
	{
		myTransform = transform;
		myLayer = gameObject.layer;
		// load the weapon
		//Reloaded();

		SetCollider(GetComponent<Collider>());
	}

	public virtual void Fire(Vector3 aDirection/*, int ownerID*/)
	{
		//if (!canFire)
		//	return;
		//if (!isLoaded)
		//	return;
		if (ammo <= 0 && !isInfiniteAmmo)
			return;
		ammo--;

		FireProjectile(aDirection, this.ownerID);
		//isLoaded = false;

		//CancelInvoke("Reloaded");
		//Invoke("Reloaded", reloadTime);
	}

	public virtual void FireProjectile(Vector3 fireDirection, int ownerID)
	{
		theProjectile = MakeProjectile(this.ownerID);
		theProjectile.LookAt(theProjectile.position + fireDirection);

		theProjectile.GetComponent<Rigidbody>().velocity = fireDirection * projectileSpeed;
	}

	public virtual void SetCollider(Collider aCollider)
	{
		parentCollider = aCollider;
	}

	public virtual Transform MakeProjectile(int ownerID)
	{
		theProjectile = Instantiate(_projectileGO, _shotSpawn.position, /*myTransform.position + spawnPosOffset + (myTransform.forward * forwardOffset)*/ myTransform.rotation).transform;
		_theProjectileGO = theProjectile.gameObject;
		_theProjectileGO.layer = myLayer;

		theProjectileController = theProjectile.GetComponent<Projectile>();
		theProjectileController.SetOwnerType(ownerID);

		Physics.IgnoreLayerCollision(myTransform.gameObject.layer, myLayer);

		if (parentCollider != null)
		{
			Physics.IgnoreCollision(theProjectile.GetComponent<Collider>(), parentCollider);
		}

		return theProjectile;
	}
}