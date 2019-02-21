using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyOutline : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	Material material;

	public void OnPointerEnter(PointerEventData eventData)
	{
		//material.

		Debug.Log("Enter");
	}

	public void OnPointerExit(PointerEventData eventData)
	{



		Debug.Log("Exit");
	}
}