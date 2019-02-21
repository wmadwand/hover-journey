using UnityEngine;

public class WeaponController : MonoBehaviour
{
	public int ownerID;

	public GameObject _projectileGO;
	public Transform _shotSpawn;


	private GameObject _theProjectileGO;
	private bool isInfiniteAmmo;

	private Transform myTransform;

	private int myLayer;

	private Projectile theProjectileController;

	public int ammo = 100;
	public int maxAmmo = 100;

	private Collider parentCollider;

	[System.NonSerialized]
	public Transform theProjectile;

	public float projectileSpeed = 10f;

	//--------------------------------------------------------	

	public virtual void Init()
	{
		myTransform = transform;
		myLayer = gameObject.layer;

		//Reloaded();

		SetCollider(GetComponent<Collider>());
	}

	public virtual void Fire(Vector3 direction/*, int ownerID*/)
	{
		if (ammo <= 0 && !isInfiniteAmmo)
		{
			return;
		}

		ammo--;
		FireProjectile(direction, this.ownerID);
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
		theProjectile = Instantiate(_projectileGO, _shotSpawn.position, myTransform.rotation).transform;
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

	//--------------------------------------------------------

	private void Start()
	{
		Init();
	}
}