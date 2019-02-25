
namespace ZombieMessage
{
	public class BaseUnderAttack : IZombieMessage
	{
		private bool _flag;

		public void Initialize(object[] settings)
		{
			_flag = (bool)settings[1];
		}

		public string GetText()
		{
			return $"Fort attacked!!!";
		}
	}
}