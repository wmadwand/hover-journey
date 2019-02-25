
public class EnemyHealth : ObjectHealth
{
	private Enemy _enemy;

	private void Awake()
	{
		_enemy = GetComponent<Enemy>();
	}

	public override void OnGetDamage()
	{
		_enemy.UpdateHealthBar(Value);

		if (!IsAlive)
		{
			_enemy.DestroyEnemy();
		}
	}
}