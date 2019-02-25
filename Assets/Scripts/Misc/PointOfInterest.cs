using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PointOfInterest : MonoBehaviour
{
	public string Letter => _text.text;

	public /*[SerializeField] private*/ Transform _markPoint;
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

	public void InitView(string text, Color color)
	{
		SetText(text);
		SetColor(color);
	}

	bool IsObjectVisible2(Collider2D col)
	{
		var planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
		if (GeometryUtility.TestPlanesAABB(planes, col.bounds))
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public void SetText(string value)
	{
		_text.text = value;
	}

	public void SetColor(Color value)
	{
		_background.color = value;
		_text.color = value;
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

	Vector2 resultVec;

	private void UpdateState()
	{
		float screenOffset = _view.GetComponent<Image>().rectTransform.rect.size.x / 2 + 5;

		Vector3 vec = Camera.main.WorldToScreenPoint(_markPoint.position);
		_view.transform.position = vec;

		if (!IsPointVisibleCameraView(_markPoint.position))
		{
			Vector3 markPosViewPort = Camera.main.ScreenToViewportPoint(_view.transform.position);

			if (markPosViewPort.x >= 1)
			{
				resultVec = new Vector2(1, markPosViewPort.y);
				Vector3 temp = Camera.main.ViewportToScreenPoint(resultVec);
				_view.transform.position = new Vector3(temp.x - screenOffset, _view.transform.position.y);

				//_view.transform.DOMove(new Vector3(_view.transform.position.x - screenOffset, _view.transform.position.y), .2f);
			}
			else if (markPosViewPort.x <= 0)
			{
				resultVec = new Vector2(0, markPosViewPort.y);

				Vector3 temp = Camera.main.ViewportToScreenPoint(resultVec);
				_view.transform.position = new Vector3(temp.x + screenOffset, _view.transform.position.y);
			}

			if (markPosViewPort.y >= 1)
			{
				resultVec = new Vector2(markPosViewPort.x, 1);

				Vector3 temp = Camera.main.ViewportToScreenPoint(resultVec);
				_view.transform.position = new Vector3(_view.transform.position.x, temp.y - screenOffset);
			}
			else if (markPosViewPort.y <= 0)
			{
				resultVec = new Vector2(markPosViewPort.x, 0);

				Vector3 temp = Camera.main.ViewportToScreenPoint(resultVec);
				_view.transform.position = new Vector3(_view.transform.position.x, temp.y + screenOffset);
			}

		}

		Vector3 direction22 = _markPoint.position - playerTr.position;
		float res = Vector3.Dot(playerTr.forward.normalized, direction22.normalized);

		_view.SetActive(res > 0.1f ? true:false);


		Vector3 direction = _markPoint.transform.position - playerTr.position;
		float dotValue = Vector3.Dot(playerTr.forward, direction);

		_view.SetActive(dotValue > FIELD_VIEW_VALUE ? true : false);

		//TODO: sqrtMagnitude instead
		float distance = Vector3.Distance(_markPoint.transform.position, playerTr.position);
		//mark.GetComponent<CanvasGroup>().alpha = distance > dist ? .3f : 1f;
		_text.gameObject.SetActive(distance > dist ? false : true);
		_view.transform.localScale = distance > dist ? new Vector3(.5f, .5f, .5f) : Vector3.one;

	}

	bool IsPointVisibleCameraView(Vector3 point)
	{
		Vector3 viewportPoint = Camera.main.WorldToViewportPoint(point);
		return (viewportPoint.z > 0 && (new Rect(0, 0, 1, 1)).Contains(viewportPoint));
	}
}