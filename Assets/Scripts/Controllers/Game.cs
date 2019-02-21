using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoSingleton<Game>
{
	public Transform canvasTr;
	public GameObject Player => _player;

	public GameObject playerCameraGO;

	[SerializeField] private GameObject _player; 
}
