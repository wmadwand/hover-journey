namespace DTools.ResourcesPool
{
	public interface IPool
	{
		string Path { get; }
		void AcceptObject(PoolObject poolObject);
		void OnPoolObjectDestroy(PoolObject poolObject);
		bool IsReady { get; }
		void MarkAsNecessary(int customerHash);
		bool Release(int customerHash);
	}
}