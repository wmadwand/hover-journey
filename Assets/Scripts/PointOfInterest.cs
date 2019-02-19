using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class PointOfInterest : MonoBehaviour
{	
	public Transform targetPivotPoint;
	public Transform indicatorPivotPoint;
	GameObject mark;
	public float dist;
	Transform playerTr;
	public Image bacground;
	public Text text;

	public GameObject aimPic;

	void Start()
	{
		playerTr = GameObject.FindWithTag("Player").transform; /*FindObjectOfType<RigidbodyFirstPersonController>().transform;*/
		mark = this.gameObject;

		aimPic.SetActive(false);
	}

	void Update()
	{

		Rot044();
	}

	void Init()
	{
		//SetText();
		//SetColor();
	}

	public void SetText(string value)
	{
		text.text = value;
	}

	public void SetColor(Color value)
	{
		bacground.color = value;
	}

	void Rot044()
	{
		Vector3 vec = Camera.main.WorldToScreenPoint(targetPivotPoint.position);
		mark.transform.Find("ImagePOI").transform.position = vec;

		Vector3 direction = targetPivotPoint.transform.position - playerTr.position;

		if (Vector3.Dot(playerTr.transform.forward, direction) > .5f)
		{
			mark.transform.Find("ImagePOI").gameObject.SetActive(true);
		}
		else
		{
			mark.transform.Find("ImagePOI").gameObject.SetActive(false);
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

		Vector3 vec2 = Camera.main.WorldToScreenPoint(indicatorPivotPoint.position);
		aimPic.transform.position = vec2;

	}
}
