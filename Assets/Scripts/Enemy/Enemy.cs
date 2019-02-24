using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public static event Action<Enemy> OnDie; // send event to Score system and to Spawn system (remove from the active enemy collection)

	[SerializeField] private Transform _markPoint;
	[SerializeField] private Transform _healthbarPoint;

	public PointOfInterest Poi { get; private set; }

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

		//TODO: move it back to Start and use coroutine into EnemySpawn.Execute operation for WaitUntil Poi!= null
		SpawnPOI();
	}

	private void Start()
	{
		//SpawnPOI();
	}

	private void Update()
	{
		if (health.IsAlive) return;

		//TODO: use Object pooling here
		DestroyAllEnemyStuff();

		OnDie?.Invoke(this);
	}

	public void DestroyAllEnemyStuff()
	{
		Destroy(gameObject);
		Destroy(_poiGo);
		Destroy(healthBar.gameObject);
	}

	private void SpawnPOI()
	{
		_poiGo = Instantiate(_enemyPoiPrefab, Game.Instance.canvasTr);
		Poi = _poiGo.GetComponent<PointOfInterest>();
		Poi.Init(_markPoint);

		healthBar = Instantiate(_enemyHealthBarPref, Game.Instance.canvasTr).GetComponent<EnemyHealthBar>();
		healthBar.Init(_healthbarPoint);
	}
}