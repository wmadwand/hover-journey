using System;
using UnityEngine;
using Zenject;
using ZombieMessage;

public interface IGameFeedSender
{
	void SendFeedMessage(ZombieMessageType type);
}

public class MissionController : MonoBehaviour
{
	public static event Action<float> OnCountdown;

	public float countdown = 3;

	private float _countdownCount;
	private int _missionNumber;
	private bool _gameStart;
	private EnemySpawn _enemySpawn;
	private ZombieMessageService _service;

	[Inject]
	private void Init(ZombieMessageService service, EnemySpawn enemySpawn)
	{
		_service = service;
		_enemySpawn = enemySpawn;
	}

	private void Awake()
	{
		Game.OnStart += Game_OnStart;
		Game.OnStop += Game_OnStop;

		_countdownCount = countdown;
	}

	private void Game_OnStop()
	{
		_gameStart = false;
	}

	private void Game_OnStart()
	{
		_gameStart = true;

		++_missionNumber;
	}


	float previousPauseTime;

	// Update is called once per frame
	void Update()
	{
		if (_gameStart)
		{
			_countdownCount -= Time.deltaTime;

			if (Mathf.Abs(previousPauseTime - _countdownCount) >= 1)
			{
				previousPauseTime = Mathf.RoundToInt(_countdownCount);

				OnCountdown?.Invoke(previousPauseTime);
			}

			if (_countdownCount < 0)
			{
				_countdownCount = countdown;
				_gameStart = false;

				_enemySpawn.Execute();

			}
		}
	}
}