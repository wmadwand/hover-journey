using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoSingleton<ScoreManager>
{
	public int Score { get; private set; }

	[SerializeField] private Text _scoreText;

	//---------------------------------------------------------

	private void Awake()
	{
		Game.OnStart += GameController_OnGameStart;

		Enemy.OnDie += Enemy_OnDie;
	}

	private void Enemy_OnDie(Enemy obj)
	{
		Score += 50/*scoreValue*/;
		UpdateText();
	}

	private void OnDestroy()
	{
		Game.OnStart -= GameController_OnGameStart;
		Enemy.OnDie -= Enemy_OnDie;
	}

	private void GameController_OnGameStart()
	{
		ResetScore();
	}

	private void ResetScore()
	{
		Score = 0;
		UpdateText();
	}

	private void UpdateText()
	{
		_scoreText.text = $"Score: {Score}";
	}
}