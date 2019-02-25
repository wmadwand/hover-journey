public interface IObjectHealth
{
	bool IsAlive { get; }
	int Value { get; }

	void GetDamage(int value);
	void OnGetDamage();
}