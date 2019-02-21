using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EnemyHighlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	public PointOfInterest pointOfInterest;

	public void OnPointerClick(PointerEventData eventData)
	{
		Debug.Log("Click");
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		pointOfInterest.aimPic.SetActive(true);

		Debug.Log("Enter");
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		pointOfInterest.aimPic.SetActive(false);


		Debug.Log("Exit");
	}	
	

	
}
