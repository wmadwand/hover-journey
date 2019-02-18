using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class PointOfInterest : MonoBehaviour
{	
	public Transform targetPivotPoint;
	public GameObject mark;
	public float dist;
	Transform playerTr;

	void Start()
	{
		playerTr = GameObject.FindWithTag("Player").transform; /*FindObjectOfType<RigidbodyFirstPersonController>().transform;*/

	}

	void Update()
	{

		Rot044();
	}

	void Rot044()
	{
		Vector3 vec = Camera.main.WorldToScreenPoint(targetPivotPoint.position);
		mark.transform.position = vec;

		Vector3 direction = targetPivotPoint.transform.position - playerTr.position;

		if (Vector3.Dot(playerTr.transform.forward, direction) > .5f)
		{
			mark.transform.Find("Image").gameObject.SetActive(true);
		}
		else
		{
			mark.transform.Find("Image").gameObject.SetActive(false);
		}

		//TODO: sqrtMagnitude instead
		if (Vector3.Distance(targetPivotPoint.transform.position, playerTr.position) > dist)
		{
			mark.GetComponent<CanvasGroup>().alpha = .3f;
		}
		else
		{
			mark.GetComponent<CanvasGroup>().alpha = 1f;
		}

	}
}
