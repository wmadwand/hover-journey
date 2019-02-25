
using System;

public class PlayerHealth : ObjectHealth
{
	public static event Action OnDie;

	public override void OnGetDamage()
	{
		if (!IsAlive)
		{
			OnDie?.Invoke();
		}
	}
}