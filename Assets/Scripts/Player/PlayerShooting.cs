using UnityEngine;

[RequireComponent(typeof(WeaponController))]
public class PlayerShooting : MonoBehaviour
{
	[SerializeField] private float _distanceMax = 30f;
	[SerializeField] private float _timeBetweenShots = 0.5f;

	private float _nextShotTime;
	private WeaponController _weaponController;
	private Ray _shootRay;
	private RaycastHit _raycastHit;
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
			_shootRay = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(_shootRay, out _raycastHit, Mathf.Infinity, _combineLayerAttack))
			{
				if (Vector3.Distance(transform.position, _raycastHit.point) > _distanceMax)
				{
					return;
				}

				_nextShotTime = Time.time + _timeBetweenShots;
				_weaponController.Fire(_raycastHit.point);
			}
		}
	}
}