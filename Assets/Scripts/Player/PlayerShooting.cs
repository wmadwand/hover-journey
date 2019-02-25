using UnityEngine;

[RequireComponent(typeof(WeaponController))]
public class PlayerShooting : MonoBehaviour
{
	[SerializeField] private float _distanceMax = 30f;
	[SerializeField] private float _timeBetweenShots = 0.5f;

	private float _nextShotTime;
	private WeaponController _weaponController;
	private Ray _ray;
	private RaycastHit _hit;
	private int _combineLayerAttack;

	//--------------------------------------------------------

	private void Awake()
	{
		_weaponController = GetComponent<WeaponController>();
		_combineLayerAttack = 1 << LayerMask.NameToLayer("Static") | 1 << LayerMask.NameToLayer("Shootable");
	}

	private void Update()
	{
		if (Input.GetButton("Fire1") && Time.time > _nextShotTime)
		{
			_ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, _combineLayerAttack))
			{
				if (Vector3.Distance(transform.position, _hit.point) > _distanceMax)
				{
					return;
				}

				_nextShotTime = Time.time + _timeBetweenShots;
				_weaponController.Fire(_hit.point);
			}
		}
	}
}