using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	[SerializeField] private float _fieldView = .95f;

	private Vector3 _targetRotateDir;
	private Quaternion _targetRotation;
	private Quaternion _newRotation;
	private Rigidbody _rigidbody;

	private Transform _player;
	private Vector3 rotationDir;
	private float _isPlayerInRangeAngle;

	//--------------------------------------------------------

	public bool IsFaceToObject(Vector3 target)
	{
		rotationDir = target - transform.position;
		_isPlayerInRangeAngle = Vector3.Dot(transform.forward.normalized, rotationDir.normalized);

		return _isPlayerInRangeAngle > _fieldView;
	}

	public void RotateToObject(Vector3 position)
	{
		_targetRotateDir = position - transform.position;
		_targetRotateDir.y = 0f;
		_targetRotation = Quaternion.LookRotation(_targetRotateDir, Vector3.up);

		_newRotation = Quaternion.Lerp(_rigidbody.rotation, _targetRotation, 2f * Time.deltaTime);
		_rigidbody.MoveRotation(_newRotation);
	}

	//--------------------------------------------------------

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_player = Game.Instance.Player.transform;
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