using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{

	public float speed;



	private void Awake()
	{
		
	}

	void Start()
    {
		//GetComponent<Rigidbody>().velocity = transform.forward * speed;
	}

}
