using UnityEngine;

public class ObjectHealth : MonoBehaviour, IObjectHealth
{
	public int Value => _value;
	public bool IsAlive => _value > 0;

	[SerializeField] private int _value = 100;

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