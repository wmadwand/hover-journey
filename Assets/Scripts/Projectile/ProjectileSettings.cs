using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSettings : ScriptableObject
{
	public float speed;

	[SerializeField] private float _speed;
	public float Speed => _speed;
}
