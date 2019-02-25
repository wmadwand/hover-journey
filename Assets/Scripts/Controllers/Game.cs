using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoSingleton<Game>
{
	public static event Action OnStart;
	public static event Action OnStop;

	public Transform canvasTr;
	public GameObject Player => _player;

	public GameObject playerCameraGO;

	[SerializeField] private GameObject _player;

	public void Restart()
	{
		SceneManager.LoadScene("Main", LoadSceneMode.Single);
	}

	private void Awake()
	{
		EnemySpawn.OnAllEnemiesDestroy += EnemySpawn_OnAllEnemiesDestroy;

		DontDestroyOnLoad(this.gameObject);
	}

	private void EnemySpawn_OnAllEnemiesDestroy()
	{
		OnStart?.Invoke();
	}

	private void Start()
	{
		OnStart?.Invoke();
	}
}
