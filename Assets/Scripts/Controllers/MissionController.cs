using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ZombieMessage;

public interface IGameFeedSender
{
	void SendFeedMessage(ZombieMessageType type);
}

public class MissionController : MonoBehaviour, IGameFeedSender
{
	public float countdown = 3;
	float countdownCount;

	int missionNumber;

	bool gameStart;

	public EnemySpawn enemySpawn;

	ZombieMessageService _service;

	[Inject]
	private void Init(ZombieMessageService service)
	{
		_service = service;
	}

	private void Awake()
	{
		Game.OnStart += Game_OnStart;
		Game.OnStop += Game_OnStop;

		countdownCount = countdown;
	}

	private void Game_OnStop()
	{
		gameStart = false;
	}

	private void Game_OnStart()
	{
		gameStart = true;

		++missionNumber;
	}


	float previousPauseTime;

	// Update is called once per frame
	void Update()
	{
		if (gameStart)
		{
			countdownCount -= Time.deltaTime;

			SendFeedMessage(ZombieMessageType.NextWaveCountdown);

			if (countdownCount < 0)
			{
				countdownCount = countdown;
				gameStart = false;

				enemySpawn.Execute();
			}
		}
	}

	public void SendFeedMessage(ZombieMessageType type)
	{
		if (Mathf.Abs(previousPauseTime - countdownCount) >= 1)
		{
			previousPauseTime = Mathf.RoundToInt(countdownCount);

			object[] settings = { type, previousPauseTime };
			_service.OnGetNotified(settings);
		}
	}
}
