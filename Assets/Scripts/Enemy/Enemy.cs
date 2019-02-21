using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] private Transform _targetPivotPoint;
	[SerializeField] private Transform _indicatorPivotPoint;
	[SerializeField] private GameObject _poiGo;

	[SerializeField] private GameObject _enemyPoiPrefab;
	[SerializeField] private GameObject _enemyHealthBarPref;

	EnemyHealth health;
	EnemyHealthBar healthBar;

	private void Awake()
	{
		health = GetComponent<EnemyHealth>();
	}

	void Start()
	{
		SpawnPOI();
	}

	private void Update()
	{
		if (health.IsAlive) return;

		Destroy(gameObject);
		Destroy(_poiGo);
	}

	void SpawnPOI()
	{
		_poiGo = Instantiate(_enemyPoiPrefab, Game.Instance.canvasTr);
		PointOfInterest poi = _poiGo.GetComponent<PointOfInterest>();
		poi.Init(_targetPivotPoint);

		//healthBar = Instantiate(_enemyHealthBarPref, Game.Instance.canvasTr);
		//PointOfInterest poi = _poiGo.GetComponent<PointOfInterest>();
		//poi.Init(_targetPivotPoint, _indicatorPivotPoint);

	}
}
