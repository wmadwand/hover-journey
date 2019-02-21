using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoSingleton<Game>
{
	public Transform canvasTr;
	public GameObject Player => _player;

	[SerializeField] private GameObject _player; 
}
