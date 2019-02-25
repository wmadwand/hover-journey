using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public static event Action<Enemy> OnDie;

	public PointOfInterest Poi { get; private set; }
	public EnemyHealthBar healthBar;

	[SerializeField] private Transform _markPoint;
	[SerializeField] private Transform _healthbarPoint;
	[SerializeField] private GameObject _enemyPoiPrefab;
	[SerializeField] private GameObject _enemyHealthBarPref;

	private GameObject _poiGo;

	//--------------------------------------------------------

	public void UpdateHealthBar(int value)
	{
		healthBar.UpdateState(value);
	}

	//--------------------------------------------------------

	private void Awake()
	{
		//TODO: move it back to Start and use coroutine into EnemySpawn.Execute operation for WaitUntil Poi!= null
		SpawnPOI();
	}

	private void Start()
	{
		//SpawnPOI();
	}

	public void DestroyEnemy()
	{
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