using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoSingleton<Game>
{
	public static event Action OnStart;
	public static event Action OnStop;

	public Transform canvasTr;
	public GameObject Player => _player;

	public GameObject playerCameraGO;

	[SerializeField] private GameObject _player;
}
