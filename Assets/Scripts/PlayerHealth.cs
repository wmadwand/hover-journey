using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	public int value = 100;

	public void GetDamage(int value)
	{
		Remove(value);
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

		Debug.Log($"Player health {this.value}");
	}
}