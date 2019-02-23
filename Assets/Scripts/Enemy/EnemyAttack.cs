using UnityEngine;

[RequireComponent(typeof(WeaponController))]
public class EnemyAttack : MonoBehaviour
{
	private bool _isPlayerInRange;
	private float _nextShotTime;
	public float _timeBetweenShots = 1.5f;

	private WeaponController _weaponController;

	const float ATTACK_DISTANCE = 10;
	const float FIELD_VIEW_VALUE = .9f;

	private GameObject _player;

	public GameObject tarrget;

	//--------------------------------------------------------

	#region Debug
#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, _player.transform.position);
	}
#endif
	#endregion

	private void Awake()
	{
		_weaponController = GetComponent<WeaponController>();
		_player = Game.Instance.Player;
	}

	private void FixedUpdate()
	{
		//if (Vector3.Distance(transform.position, _player.transform.position) <= ATTACK_DISTANCE)
		//{
		//	RotateToPlayer();
		//}
	}

	private void Update()
	{
		if (Vector3.Distance(transform.position, _player.transform.position) <= ATTACK_DISTANCE)
		{
			RotateToPlayer();
		}

		CheckForPlayerInRange();

		if (Time.time > _nextShotTime && _isPlayerInRange && _player.GetComponent<PlayerHealth>().IsAlive)
		{
			_nextShotTime = Time.time + _timeBetweenShots;

			_weaponController.Fire(_player.transform.position - transform.position);
		}
	}

	private void CheckForPlayerInRange()
	{
		_isPlayerInRange = false;

		//TODO: replace with Physics.OverlapSphere or just put a coliider
		Vector3 direction = _player.transform.position - transform.position;
		float isPlayerInRangeAngle = Vector3.Dot(transform.forward, direction);

		if (isPlayerInRangeAngle > FIELD_VIEW_VALUE)
		{
			//TODO: sqrMagnitude or collider
			if (Vector3.Distance(transform.position, _player.transform.position) <= ATTACK_DISTANCE)
			{
				_isPlayerInRange = true;
			}
		}
	}

	void RotateToPlayer()
	{
		Vector3 targetDir = _player.transform.position - transform.position;
		targetDir.y = 0f;
		Quaternion targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);

		Quaternion newRotation = Quaternion.Lerp(GetComponent<Rigidbody>().rotation, targetRotation, 2f * Time.deltaTime);
		GetComponent<Rigidbody>().MoveRotation(newRotation);
	}

	#region Rotation tricks
	void RotationTricks()
	{
		//Vector3 rot = Quaternion.LookRotation(target.position - transform.position).eulerAngles;
		//rot.x = rot.z = 0;
		//transform.rotation = Quaternion.Euler(rot);
		//// or
		//Vector3 newtarget = target.position;
		//newtarget.y = transform.position.y;
		//transform.LookAt(newtarget);
		//// or
		//Vector3 dir = target.position - transform.position;
		//dir.y = 0;
		//transform.rotation = Quaternion.LookRotation(dir);
	} 
	#endregion
}