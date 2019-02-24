using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionController : MonoBehaviour
{
	public float countdown = 3;
	float countdownCount;

	int missionNumber;

	bool gameStart;

	public EnemySpawn enemySpawn;

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

	// Update is called once per frame
	void Update()
	{
		if (gameStart)
		{
			countdownCount -= Time.deltaTime;

			if (countdownCount < 0)
			{
				countdownCount = countdown;
				gameStart = false;

				enemySpawn.Execute();
			}
		}
	}
}
