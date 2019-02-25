using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
	private Text _text;

	private void Awake()
	{
		_text = GetComponentInChildren<Text>();
	}

	// Update is called once per frame
	void Update()
	{
		_text.text = $"Health: { Game.Instance.Player.GetComponent<PlayerHealth>().Value}";
	}
}
