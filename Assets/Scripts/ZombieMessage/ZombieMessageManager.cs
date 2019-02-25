using UnityEngine;
using Zenject;

namespace ZombieMessage
{
	public class ZombieMessageManager : MonoBehaviour
	{
		[Inject] private ZombieMessageService _messageService;

		private void Awake()
		{
			Enemy.OnDie += Enemy_OnDie;
			MissionController.OnCountdown += MissionController_OnCountdown;
			PlayerHealth.OnDie += PlayerHealth_OnDie;
		}

		private void PlayerHealth_OnDie()
		{
			object[] settings = { ZombieMessageType.Generic, "Player is dead" };
			_messageService.OnGetNotified(settings);
		}

		private void MissionController_OnCountdown(float obj)
		{
			object[] settings = { ZombieMessageType.NextWaveCountdown, obj };
			_messageService.OnGetNotified(settings);
		}

		private void Enemy_OnDie(Enemy obj)
		{
			object[] settings = { ZombieMessageType.TeamPlayerDead, obj.Poi.Letter };
			_messageService.OnGetNotified(settings);
		}

		private void OnDestroy()
		{
			Enemy.OnDie -= Enemy_OnDie;
		}
	}
}