using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectOutline : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	private Material _material;

	private void Awake()
	{
		_material = GetComponentInChildren<MeshRenderer>().material;

	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		_material.SetFloat("_OutlineVal", .2f);

		GetComponent<Enemy>().healthBar.SetVisible(true);

		Debug.Log("Enter");
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		_material.SetFloat("_OutlineVal", 0);

		GetComponent<Enemy>().healthBar.SetVisible(false);

		Debug.Log("Exit");
	}
}