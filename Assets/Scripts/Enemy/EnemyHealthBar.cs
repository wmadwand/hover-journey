using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyHealthBar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public Transform indicatorPivotPoint;
	public GameObject aimPic;

	void Start()
	{
		aimPic.SetActive(false);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		aimPic.SetActive(true);

		Debug.Log("Enter");
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		aimPic.SetActive(false);


		Debug.Log("Exit");
	}


	// Update is called once per frame
	void Update()
	{
		UpdateHealthBarPosition();
	}

	void UpdateHealthBarPosition()
	{
		Vector3 vec2 = Camera.main.WorldToScreenPoint(indicatorPivotPoint.position);
		aimPic.transform.position = vec2;
	}
}
