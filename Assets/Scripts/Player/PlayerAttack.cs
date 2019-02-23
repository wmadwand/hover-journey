using UnityEngine;

[RequireComponent(typeof(WeaponController))]
public class PlayerAttack : MonoBehaviour
{
	[SerializeField] private float _timeBetweenAttacks;

	private float _nextShot;
	private WeaponController _weaponController;

	//TODO: bear in mind the attack range
	private bool _enemyInRange = true;
	private float _enemyHealth = 10;

	private Ray _ray;
	RaycastHit _hit;

	//--------------------------------------------------------

	private void Awake()
	{
		_weaponController = GetComponent<WeaponController>();
	}

	private void Update()
	{
		if (Input.GetButton("Fire1") && Time.time > _nextShot && _enemyInRange && _enemyHealth > 0)
		{
			_ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Shootable")))
			{
				_nextShot = Time.time + _timeBetweenAttacks;
				_weaponController.Fire(_hit.point);
			}
		}
	}
}