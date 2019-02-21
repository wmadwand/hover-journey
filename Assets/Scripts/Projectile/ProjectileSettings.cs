using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSettings : ScriptableObject
{
	public float speed;
	public float Speed => _speed;
	public float Damage => _damage;

	[SerializeField] private float _speed;
	[SerializeField] private float _damage;
}
