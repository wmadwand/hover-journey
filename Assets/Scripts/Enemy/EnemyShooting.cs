using UnityEngine;

[RequireComponent(typeof(WeaponController))]
[RequireComponent(typeof(EnemyMovement))]
public class EnemyShooting : MonoBehaviour
{
	[SerializeField] private GameObject _barrelPoint;
	[SerializeField] private float _distanceMax = 25f;

	private float _nextShotTime;
	public float _timeBetweenShots = 1.5f;
	private WeaponController _weaponController;
	private EnemyMovement _enemyMovement;
	private Transform _player;
	private Ray _shootRay;
	private RaycastHit _raycastHit;
	private int _combineLayerAttack;
	private bool _castToPlayerResult;

	//--------------------------------------------------------

	#region Debug
#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, _player.position);
	}
#endif
	#endregion

	private void Awake()
	{
		_weaponController = GetComponent<WeaponController>();
		_enemyMovement = GetComponent<EnemyMovement>();
		_player = Game.Instance.Player.transform;

		_combineLayerAttack = 1 << LayerMask.NameToLayer("Static") | 1 << LayerMask.NameToLayer("Player");
	}

	private void Update()
	{
		if (Vector3.Distance(transform.position, _player.position) > _distanceMax)
		{
			return;
		}

		_enemyMovement.RotateToObject(_player.position);

		if (Time.time > _nextShotTime && IsObjectInRange(_player.position) && _player.GetComponent<IObjectHealth>().IsAlive && IsCastToPlayer())
		{
			_nextShotTime = Time.time + _timeBetweenShots;
			_weaponController.Fire(_player.position);
		}
	}

	private bool IsCastToPlayer()
	{
		_castToPlayerResult = false;

		_shootRay = new Ray(_barrelPoint.transform.position, _player.position - _barrelPoint.transform.position);
		Debug.DrawLine(_barrelPoint.transform.position, _player.transform.position, Color.magenta);

		if (Physics.SphereCast(_shootRay, 0.55f, out _raycastHit, _distanceMax, _combineLayerAttack))
		{
			if (_raycastHit.collider.gameObject.GetComponent<IObjectHealth>() != null)
			{
				_castToPlayerResult = true;
			}
		}

		return _castToPlayerResult;
	}

	private bool IsObjectInRange(Vector3 position)
	{
		if (Vector3.Distance(transform.position, position) > _distanceMax || !_enemyMovement.IsFaceToObject(position))
		{
			return false;
		}

		return true;
	}
}