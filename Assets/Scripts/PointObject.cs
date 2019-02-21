using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{


	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		FindObjectOfType<EnemyHealthBar>().SetVisible(true);
		Debug.Log("Enter");
	}

	public void OnPointerExit(PointerEventData eventData)
	{

		FindObjectOfType<EnemyHealthBar>().SetVisible(false);

		Debug.Log("Exit");
	}
}
