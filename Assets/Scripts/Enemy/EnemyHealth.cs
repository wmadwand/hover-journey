using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	public int value = 100;

	public void GetDamage(int value)
	{
		Remove(value);
	}

	//TODO: move to Enemy, add event
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Missile")
		{
			Remove(10);
		}
	}

	public void Add(int value)
	{
		this.value += value;
	}

	public void Remove(int value)
	{
		if (value <= 0)
		{
			return;
		}

		this.value -= value;
	}

	private void Update()
	{
		if (value <= 0)
		{
			Destroy(gameObject);
		}
	}
}