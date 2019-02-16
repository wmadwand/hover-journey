using UnityEngine;

namespace DTools.ResourcesPool
{
	/// <summary>
	/// Base class for all objects are going to be used as pool controlled.
	/// Controlled object must have only one PoolObject's successor attached
	/// </summary>
	public abstract class PoolObject : MonoBehaviour
	{
		public virtual string Path => _pool.Path;

		private IPool _pool;
		protected bool inPool;

		public void SetPool(IPool pool)
		{
		    _pool = pool;
		}

		public void TakeFromPool()
		{
			if (!inPool)
				return;

			inPool = false;
			OnTakenFromPool();
		}

		public void ReturnToPool()
		{
			if (inPool || _pool == null) return;

			BeforeReturnToPool();
			inPool = true;
			gameObject.SetActive(false);
			_pool.AcceptObject(this);
		}

		/// <summary>
		/// This message will be sent when object was taken from pool repeatedly (not instantiated just now)
		/// </summary>
		protected virtual void OnTakenFromPool()
		{}

    	protected virtual void BeforeReturnToPool()
		{}

	    private void OnDestroy()
	    {
	        _pool?.OnPoolObjectDestroy(this);
	    }

		public virtual float GetStealPriority() => 0f;
	}
}
