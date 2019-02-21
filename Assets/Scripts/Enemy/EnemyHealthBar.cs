using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
	public Transform indicatorPivotPoint;
	public Image green;
	public GameObject bar;

	public float healthValue;

	//--------------------------------------------------------

	public void Init(Transform indicatorPivotPoint)
	{
		this.indicatorPivotPoint = indicatorPivotPoint;
	}

	void Start()
	{
		bar.SetActive(false);
	}

	//--------------------------------------------------------

	public void SetVisible(bool value)
	{
		bar.SetActive(value);
	}

	void ChangeHealthAmount()
	{
		green.fillAmount = Mathf.Lerp(green.fillAmount, healthValue / 100, Time.deltaTime);
	}

	void Update()
	{
		ChangeHealthAmount();

		UpdateHealthBarPosition();
	}

	void UpdateHealthBarPosition()
	{
		Vector3 vec2 = Camera.main.WorldToScreenPoint(indicatorPivotPoint.position);
		bar.transform.position = vec2;
	}
}