using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
	private Rigidbody _rigidbody;

	public void SetVelocity(Vector3 value)
	{
		_rigidbody.velocity = value;
	}

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}
}