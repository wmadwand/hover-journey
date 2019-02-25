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
	[SerializeField] private float _fieldView = .5f;
	[SerializeField] private float _letterViewDistance = 60f;

	private GameObject _mark;
	private Transform _playerTr;
	private Vector2 _resultVec;

	//--------------------------------------------------------

	public void Init(Transform targetPivotPoint)
	{
		_markPoint = targetPivotPoint;
		_playerTr = Game.Instance.playerCameraGO.transform;
	}

	public void InitView(string text, Color color)
	{
		SetText(text);
		SetColor(color);
	}

	//--------------------------------------------------------

	private void Start()
	{
		_mark = this.gameObject;
	}

	private void Update()
	{
		UpdateState();
	}

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
				_resultVec = new Vector2(1, markPosViewPort.y);
				Vector3 temp = Camera.main.ViewportToScreenPoint(_resultVec);
				_view.transform.position = new Vector3(temp.x - screenOffset, _view.transform.position.y);

				//_view.transform.DOMove(new Vector3(_view.transform.position.x - screenOffset, _view.transform.position.y), .2f);
			}
			else if (markPosViewPort.x <= 0)
			{
				_resultVec = new Vector2(0, markPosViewPort.y);

				Vector3 temp = Camera.main.ViewportToScreenPoint(_resultVec);
				_view.transform.position = new Vector3(temp.x + screenOffset, _view.transform.position.y);
			}

			if (markPosViewPort.y >= 1)
			{
				_resultVec = new Vector2(markPosViewPort.x, 1);

				Vector3 temp = Camera.main.ViewportToScreenPoint(_resultVec);
				_view.transform.position = new Vector3(_view.transform.position.x, temp.y - screenOffset);
			}
			else if (markPosViewPort.y <= 0)
			{
				_resultVec = new Vector2(markPosViewPort.x, 0);

				Vector3 temp = Camera.main.ViewportToScreenPoint(_resultVec);
				_view.transform.position = new Vector3(_view.transform.position.x, temp.y + screenOffset);
			}
		}

		Vector3 direction22 = _markPoint.position - _playerTr.position;
		float res = Vector3.Dot(_playerTr.forward.normalized, direction22.normalized);

		_view.SetActive(res > 0.1f ? true : false);

		Vector3 direction = _markPoint.transform.position - _playerTr.position;
		float dotValue = Vector3.Dot(_playerTr.forward, direction);

		_view.SetActive(dotValue > _fieldView ? true : false);

		float distance = Vector3.Distance(_markPoint.transform.position, _playerTr.position);
		_text.gameObject.SetActive(distance > _letterViewDistance ? false : true);
		_view.transform.localScale = distance > _letterViewDistance ? new Vector3(.5f, .5f, .5f) : Vector3.one;
	}

	private bool IsPointVisibleCameraView(Vector3 point)
	{
		Vector3 viewportPoint = Camera.main.WorldToViewportPoint(point);
		return (viewportPoint.z > 0 && (new Rect(0, 0, 1, 1)).Contains(viewportPoint));
	}

	private bool IsMeshObjectVisibleCameraView(Collider2D col)
	{
		var planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

		return GeometryUtility.TestPlanesAABB(planes, col.bounds);
	}

	private void SetText(string value)
	{
		_text.text = value;
	}

	private void SetColor(Color value)
	{
		_background.color = value;
		_text.color = value;
	}
}