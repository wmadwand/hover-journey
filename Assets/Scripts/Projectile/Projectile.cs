using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileOwner
{
	Player,
	Enemy
}

public class Projectile : MonoBehaviour
{
	int ownerID;

    public void Init()
	{

	}

	public void SetOwnerType(int value)
	{
		ownerID = value;
	}
}
