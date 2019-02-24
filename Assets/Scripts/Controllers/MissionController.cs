using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionController : MonoBehaviour
{
	public float countdown = 3;

	int missionNumber;

	bool gameStart;

	public EnemySpawn enemySpawn;

	private void Awake()
	{
		Game.OnStart += Game_OnStart;
		Game.OnStop += Game_OnStop;
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
			countdown -= Time.deltaTime;

			if (countdown < 0)
			{
				enemySpawn.Execute();

				countdown = 3;
				gameStart = false;
			}
		}
	}
}
