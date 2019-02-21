using UnityEngine;
using UnityEngine.UI;

public class PointOfInterest : MonoBehaviour
{
	[SerializeField] private Transform _markPoint;
	[SerializeField] private Image _background;
	[SerializeField] private Text _text;

	[SerializeField] private GameObject _view;

	private GameObject mark;
	public float dist;

	const float FIELD_VIEW_VALUE = .5f;

	Transform playerTr;

	//--------------------------------------------------------

	public void Init(Transform targetPivotPoint)
	{
		this._markPoint = targetPivotPoint;

		playerTr = Game.Instance.playerCameraGO.transform;
		//SetText();
		//SetColor();
	}

	public void SetText(string value)
	{
		_text.text = value;
	}

	public void SetColor(Color value)
	{
		_background.color = value;
	}

	//--------------------------------------------------------

	private void Start()
	{
		mark = this.gameObject;
	}

	private void Update()
	{

		UpdateState();
	}

	private void UpdateState()
	{
		Vector3 vec = Camera.main.WorldToScreenPoint(_markPoint.position);
		_view.transform.position = vec;

		Vector3 direction = _markPoint.transform.position - playerTr.position;
		float dotValue = Vector3.Dot(playerTr.forward, direction);

		_view.SetActive(dotValue > FIELD_VIEW_VALUE ? true : false);

		//TODO: sqrtMagnitude instead
		float distance = Vector3.Distance(_markPoint.transform.position, playerTr.position);
		mark.GetComponent<CanvasGroup>().alpha = distance > dist ? .3f : 1f;
	}
}