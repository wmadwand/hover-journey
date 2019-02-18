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
		Vector3 vec = Camera.main.WorldToScreenPoint(target.position);
		mark.transform.position = vec;

		Vector3 direction = target.transform.position - playerTr.position;

		if (Vector3.Dot(playerTr.transform.forward, direction) > .5f)
		{
			mark.transform.Find("Image").gameObject.SetActive(true);
		}
		else
		{
			mark.transform.Find("Image").gameObject.SetActive(false);
		}

		//TODO: sqrtMagnitude instead
		if (Vector3.Distance(target.transform.position, playerTr.position) > dist)
		{
			mark.GetComponent<CanvasGroup>().alpha = .3f;
		}
		else
		{
			mark.GetComponent<CanvasGroup>().alpha = 1f;
		}

	}
}
