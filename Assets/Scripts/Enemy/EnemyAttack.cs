using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
	private bool _isPlayerInRange;
	private float _nextShotTime;
	private float _timeBetweenShots = 3f;

	private WeaponController _weaponController;

	const float ATTACK_DISTANCE = 10;
	const float FIELD_VIEW_VALUE = .25f;

	GameObject playerGo;

	//--------------------------------------------------------

	#region Debug
#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, playerGo.transform.position);
	}
#endif
	#endregion

	private void Awake()
	{
		_weaponController = GetComponent<WeaponController>();

		//TODO: get rid of it
		playerGo = GameObject.FindGameObjectWithTag("Player");
	}

	private void Update()
	{
		CheckForPlayerInRange();

		if (Time.time > _nextShotTime && _isPlayerInRange && playerGo.GetComponent<PlayerHealth>().IsAlive)
		{
			_nextShotTime = Time.time + _timeBetweenShots;

			_weaponController.Fire(transform.forward);
		}
	}

	private void CheckForPlayerInRange()
	{
		_isPlayerInRange = false;

		//TODO: replace with Physics.OverlapSphere or just put a coliider !!!
		Vector3 direction = playerGo.transform.position - transform.position;
		float isPlayerInRangeAngle = Vector3.Dot(transform.forward, direction);

		if (isPlayerInRangeAngle > FIELD_VIEW_VALUE)
		{
			//TODO: sqrMagnitude or collider
			if (Vector3.Distance(transform.position, playerGo.transform.position) <= ATTACK_DISTANCE)
			{
				_isPlayerInRange = true;
			}
		}
	}
}