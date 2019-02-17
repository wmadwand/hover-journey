using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class TargetUI : MonoBehaviour
{
	public RectTransform labelTr;
	public Transform target;

	public GameObject mark;

	Transform playerTr;

	// Start is called before the first frame update
	void Start()
	{
		playerTr = FindObjectOfType<RigidbodyFirstPersonController>().transform;

	}

	// Update is called once per frame
	void Update()
	{
		//labelTr.LookAt(FindObjectOfType<RigidbodyFirstPersonController>().transform, Vector3.up);

		//Rot01();
		//Rot02();
		//Rot03();



		Rot044();
		//labelTr.rotation. = new Quaternion(,)


		//Vector3 targetPostition = new Vector3(playerTr.position.x,
		//							labelTr.transform.position.y,
		//							playerTr.position.z);
		//labelTr.transform.LookAt(targetPostition);

		//labelTr.rotation = new Quaternion(labelTr.rotation.x, playerTr.rotation.y, labelTr.rotation.z, labelTr.rotation.w);


	}

	void Rot01()
	{
		labelTr.LookAt(FindObjectOfType<RigidbodyFirstPersonController>().transform, Vector3.up);

	}

	void Rot02()
	{
		Vector3 targetPostition = new Vector3(playerTr.position.x, labelTr.transform.position.y, playerTr.position.z);
		labelTr.transform.LookAt(targetPostition);
	}

	void Rot03()
	{
		Vector3 posCam = playerTr.position - labelTr.transform.position;
		posCam.y = 0;
		Quaternion lastPos = Quaternion.LookRotation(-posCam);
		transform.rotation = Quaternion.Slerp(labelTr.transform.rotation, lastPos, Time.deltaTime * 1);
	}

	void Rot044()
	{
		Vector3 vec = Camera.main.WorldToScreenPoint(target.position);
		mark.transform.position = vec;



		//Vector3 forward = target.transform.TransformDirection(Vector3.forward);
		Vector3 direction = target.transform.position - playerTr.position;

		//var dot = Vector3.Dot(target.position, playerTr.position - target.position);

		if (Vector3.Dot(playerTr.transform.forward, direction) > .5f)
		{
			mark.GetComponent<RawImage>().enabled = true;
		}
		else
		{
			mark.GetComponent<RawImage>().enabled = false;
		}


		//Vector3 forward = transform.TransformDirection(Vector3.forward);
		//Vector3 toOther = other.position - transform.position;

		//if (Vector3.Dot(forward, toOther) < 0)
	}
}
