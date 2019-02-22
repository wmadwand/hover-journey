using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PointOfInterest : MonoBehaviour
{
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

	bool IsObjectVisible(Transform tr)
	{
		Vector3 objPos = Camera.main.ScreenToViewportPoint(tr.localPosition);

		return (/*objPos.z > 0 && */(objPos.y < 0 || objPos.y > 1) || (objPos.x < 0 || objPos.x > 1)) ? false : true;
	}

	bool IsObjectVisible2(Collider2D col)
	{
		//var planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
		//var image = GetComponent<Image>();
		//var bounds = new Bounds(image.transform.localPosition, image.rectTransform.rect.size);

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



	//return GeometryUtility.TestPlanesAABB(planes, bounds);

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

		//if (!IsObjectVisible(_view.transform))
		//{
		//	Debug.Log("Out!");
		//}
		//else
		//{
		//	Debug.Log("In!");
		//}

		if (IsObjectVisible2(GetComponent<Collider2D>()))
		{
			Debug.Log("Out!");
		}
		else
		{
			Debug.Log("In!");
		}

		UpdateState();
	}

	Vector2 resultVec;

	private void UpdateState()
	{
		float screenOffset = 5;

		Vector3 vec = Camera.main.WorldToScreenPoint(_markPoint.position);
		_view.transform.position = vec;

		if (!IsVisible444GOOD(_markPoint.position))
		{
			//if (resultVec == Vector2.one || resultVec == Vector2.zero || resultVec == Vector2.up || resultVec == Vector2.right)
			//{
			//	return;
			//}

			Vector3 markPosViewPort = Camera.main.ScreenToViewportPoint(_view.transform.position); /*Camera.main.WorldToViewportPoint(_markPoint.position);*/

			if (markPosViewPort.x >= 1)
			{
				resultVec = new Vector2(1, markPosViewPort.y);
				_view.transform.position = Camera.main.ViewportToScreenPoint(resultVec);
				_view.transform.position = new Vector3(_view.transform.position.x - screenOffset, _view.transform.position.y);
			}
			else if (markPosViewPort.x <= 0)
			{
				resultVec = new Vector2(0, markPosViewPort.y);
				_view.transform.position = Camera.main.ViewportToScreenPoint(resultVec);
				_view.transform.position = new Vector3(_view.transform.position.x + screenOffset, _view.transform.position.y);
			}

			if (markPosViewPort.y >= 1)
			{
				resultVec = new Vector2(markPosViewPort.x, 1);
				_view.transform.position = Camera.main.ViewportToScreenPoint(resultVec);
				_view.transform.position = new Vector3(_view.transform.position.x, _view.transform.position.y - screenOffset);
			}
			else if (markPosViewPort.y <= 0)
			{
				resultVec = new Vector2(markPosViewPort.x, 0);
				_view.transform.position = Camera.main.ViewportToScreenPoint(resultVec);
				_view.transform.position = new Vector3(_view.transform.position.x, _view.transform.position.y + screenOffset);
			}



		}

		Vector3 direction22 = _markPoint.position - Camera.main.transform.position;
		float res = Vector3.Dot(Camera.main.transform.forward.normalized, direction22.normalized);

		if (res < 0.3)
		{
			_view.SetActive(false);
		}
		else
		{
			_view.SetActive(true);
		}


		//Vector3 direction = _markPoint.transform.position - playerTr.position;
		//float dotValue = Vector3.Dot(playerTr.forward, direction);

		//_view.SetActive(dotValue > FIELD_VIEW_VALUE ? true : false);

		//TODO: sqrtMagnitude instead
		float distance = Vector3.Distance(_markPoint.transform.position, Camera.main.transform.position/* playerTr.position*/);
		//mark.GetComponent<CanvasGroup>().alpha = distance > dist ? .3f : 1f;
		_text.gameObject.SetActive(distance > dist ? false : true);
		_view.transform.localScale = distance > dist ? new Vector3(.5f,.5f,.5f) : Vector3.one;

	}

	bool IsVisible444GOOD(Vector3 point)
	{
		Vector3 viewportPoint = Camera.main.WorldToViewportPoint(point);
		return (viewportPoint.z > 0 && (new Rect(0, 0, 1, 1)).Contains(viewportPoint));
	}
}