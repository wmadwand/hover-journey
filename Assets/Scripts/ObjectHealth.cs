using UnityEngine;

public class ObjectHealth : MonoBehaviour
{
	public int Value => _value;

	[SerializeField] private int _value = 100;

	public bool IsAlive => _value > 0;

	bool isDead;

	//--------------------------------------------------------

	public void GetDamage(int value)
	{
		Remove(value);

		OnGetDamage();
	}

	public virtual void OnGetDamage()
	{

	}

	//--------------------------------------------------------

	private void Add(int value)
	{
		_value += value;
	}

	private void Remove(int value)
	{
		if (value <= 0)
		{
			return;
		}

		_value -= value;
	}
}