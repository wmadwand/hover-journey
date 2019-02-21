using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] private Transform _markPoint;
	[SerializeField] private Transform _healthbarPoint;
	private GameObject _poiGo;

	[SerializeField] private GameObject _enemyPoiPrefab;
	[SerializeField] private GameObject _enemyHealthBarPref;

	EnemyHealth health;
	public EnemyHealthBar healthBar;

	//--------------------------------------------------------

	public void GetDamage(int value)
	{
		health.GetDamage(value);
		healthBar.UpdateState(health.Value);
	}

	//--------------------------------------------------------

	private void Awake()
	{
		health = GetComponent<EnemyHealth>();
	}

	private void Start()
	{
		SpawnPOI();
	}

	private void Update()
	{
		if (health.IsAlive) return;

		//TODO: use Object pooling here
		Destroy(gameObject);
		Destroy(_poiGo);
		Destroy(healthBar.gameObject);
	}

	private void SpawnPOI()
	{
		_poiGo = Instantiate(_enemyPoiPrefab, Game.Instance.canvasTr);
		PointOfInterest poi = _poiGo.GetComponent<PointOfInterest>();
		poi.Init(_markPoint);

		healthBar = Instantiate(_enemyHealthBarPref, Game.Instance.canvasTr).GetComponent<EnemyHealthBar>();
		healthBar.Init(_healthbarPoint);
	}
}