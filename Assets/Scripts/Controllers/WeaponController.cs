using UnityEngine;

public class WeaponController : MonoBehaviour
{
	[SerializeField] private int _ownerID;
	[SerializeField] private GameObject _projectileGO;
	[SerializeField] private Transform _shotSpawn;
	[SerializeField] private float _projectileSpeed = 10f;
	[SerializeField] private int _ammo = 100;
	[SerializeField] private int _maxAmmo = 100;

	private bool _isInfiniteAmmo;
	private Transform _myTransform;
	private int _myLayer;
	private Projectile _theProjectileController;
	private Collider _parentCollider;
	private Vector3 _fireDirection;
	[System.NonSerialized] private Transform _theProjectile;

	//--------------------------------------------------------	

	public virtual void Init()
	{
		_myTransform = transform;
		_myLayer = /*1 << LayerMask.NameToLayer("Projectile");*/ gameObject.layer; //TODO: deal with it

		SetCollider(GetComponent<Collider>());
	}

	public virtual void Fire(Vector3 target/*, int ownerID*/)
	{
		if (_ammo <= 0 && !_isInfiniteAmmo)
		{
			return;
		}

		_ammo--;
		FireProjectile(target, this._ownerID);
	}

	public virtual void FireProjectile(Vector3 target, int ownerID)
	{
		_theProjectile = MakeProjectile(this._ownerID);
		_theProjectile.LookAt(_theProjectile.position + target);
		_fireDirection = target - _shotSpawn.position;
		_theProjectile.GetComponent<ProjectileMovement>().SetVelocity(_fireDirection * _projectileSpeed);
	}

	public virtual void SetCollider(Collider aCollider)
	{
		_parentCollider = aCollider;
	}

	public virtual Transform MakeProjectile(int ownerID)
	{
		_theProjectile = Instantiate(_projectileGO, _shotSpawn.position, Quaternion.identity).transform;
		_theProjectile.gameObject.layer = _myLayer;

		_theProjectileController = _theProjectile.GetComponent<Projectile>();
		_theProjectileController.SetOwnerType(ownerID);

		Physics.IgnoreLayerCollision(_myTransform.gameObject.layer, _myLayer);

		if (_parentCollider != null)
		{
			Physics.IgnoreCollision(_theProjectile.GetComponent<Collider>(), _parentCollider);
		}

		return _theProjectile;
	}

	//--------------------------------------------------------

	private void Start()
	{
		Init();
	}
}