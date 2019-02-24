using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAttack : MonoBehaviour
{
	public GameObject _player;
	public GameObject _bullet;
	public GameObject _shotSpawn;
	public float projectileSpeed;

	bool _isPlayerInRange;
	const float FIELD_VIEW_VALUE = .9f;

	const float ATTACK_DISTANCE = 10;

	Transform theProjectile;

	private float _nextShotTime;
	public float _timeBetweenShots = 1.5f;

	bool canFire;

	// Start is called before the first frame update
	void Start()
    {
		//_shotSpawn = transform.position;

	}

    // Update is called once per frame
    void Update()
    {
		RotateToPlayer();
		CheckForPlayerInRange();

		if (_isPlayerInRange && Time.time > _nextShotTime)
		{
			if (isCastToPlayer())
			{
				FireProjectile(_player.transform.position - transform.position, 5);

			}
		}

	}



	bool isCastToPlayer()
	{
		RaycastHit hit;

		bool result = false;

		Ray ray = new Ray(transform.position, _player.transform.position - transform.position);

		Debug.DrawLine(transform.position, _player.transform.position, Color.red);

		if (Physics.SphereCast(ray, 0.55f, out hit, ATTACK_DISTANCE, 1 << LayerMask.NameToLayer("Shootable")))
		{
			//if(Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Shootable"))) { 
			if (hit.collider.gameObject.GetComponent<PlayerHealth>())
			{
				result = true;
			}
		}

		return result;
	}

	private void CheckForPlayerInRange()
	{
		_isPlayerInRange = false;

		//TODO: replace with Physics.OverlapSphere or just put a coliider
		Vector3 direction = _player.transform.position - transform.position;

		//Vector3 vect3 = 
		float isPlayerInRangeAngle = Vector3.Dot(/*transform.TransformDirection(Vector3.forward)*/transform.forward.normalized, direction.normalized);

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

	public virtual void FireProjectile(Vector3 fireDirection, int ownerID)
	{
		_nextShotTime = Time.time + _timeBetweenShots;

		theProjectile = Instantiate(_bullet, /*_shotSpawn.*/transform.position, Quaternion.identity).transform;
		theProjectile.LookAt(theProjectile.position + fireDirection);

		theProjectile.GetComponent<Rigidbody>().velocity = fireDirection * projectileSpeed;
	}
}
