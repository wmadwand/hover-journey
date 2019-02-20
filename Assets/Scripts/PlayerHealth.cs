using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	public static event Action<int> OnPlayerDamaged;

	public int value = 100;

	public void GetDamage(int valueDamage)
	{
		Remove(valueDamage);
		OnPlayerDamaged?.Invoke(valueDamage);
	}

	public void Add(int value)
	{
		this.value += value;
	}

	public void Remove(int valueDamage)
	{
		if (valueDamage <= 0)
		{
			return;
		}

		this.value -= valueDamage;

		Debug.Log($"Player health {this.value}");
	}
}