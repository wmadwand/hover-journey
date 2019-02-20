using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMovement : MonoBehaviour
{

	public float speed;



	private void Awake()
	{
		
	}

	void Start()
    {
		GetComponent<Rigidbody>().velocity = transform.forward * speed;
	}




}
