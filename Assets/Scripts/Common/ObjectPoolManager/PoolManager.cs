using UnityEngine;
using System.Collections.Generic;

namespace DTools.ResourcesPool
{
    public class PoolManager : MonoBehaviour
	{
		private struct PoolWaiting
        {
            public PoolWaiting(IPool pool, System.Action<IPool> onAssetLoaded)
            {
                this.pool = pool;
                this.onAssetLoaded = onAssetLoaded;
            }

            public readonly IPool pool;
            public readonly System.Action<IPool> onAssetLoaded;
        }

		//------------------------------------------------------------------------------

		public static PoolManager Instance
		{
			get { return instance ?? (instance = GetManagerInstance()); }
			private set { instance = value; }
		}

		//------------------------------------------------------------------------------

		private static PoolManager instance;
		private readonly Dictionary<string, IPool> pools = new Dictionary<string, IPool>(4);
        private readonly List<PoolWaiting> waitedPools = new List<PoolWaiting>();
        private readonly List<PoolWaiting> waitedPoolsToDelete = new List<PoolWaiting>();

		//------------------------------------------------------------------------------

		private void Awake()
		{
			Instance = this;
		}

	    private void Update()
	    {
	        if (waitedPools.Count == 0)
	            return;

            for (var i = 0; i < waitedPools.Count; ++i)
            {
                if (waitedPools[i].pool.IsReady)
                {
                    waitedPools[i].onAssetLoaded(waitedPools[i].pool);
                    waitedPoolsToDelete.Add(waitedPools[i]);
                }
            }

	        for (var i = 0; i < waitedPoolsToDelete.Count; ++i)
                waitedPools.Remove(waitedPoolsToDelete[i]);

            waitedPoolsToDelete.Clear();
	    }
		
		private void OnDestroy()
		{
            instance = null;
		}

		//------------------------------------------------------------------------------

		public static void GetObject<T>(string path, out T result, Vector3 position, Quaternion rotation,
			Transform parent = null) where T : PoolObject
		{
			result = GetObject<T>(path, position, rotation, parent);
		}

		public static T GetObject<T>(string path, Vector3 position, Quaternion rotation, Transform parent = null) where T : PoolObject
		{
            var pool = GetPool<T>(path);
			var obj = pool.GetObject(position, rotation, parent);
            if (obj == null)
            {
                Debug.LogError("Trying to get object from pool before complete its creation (asynchronous)");
                return null;
            }

            obj.transform.position = position;
		    obj.transform.rotation = rotation;

            if (obj.transform.parent == null)
            {
                obj.transform.SetParent(Instance.transform);
            }

            return obj;
		}

		// TODO: Callback must get not pool but specific object
		public static void GetPoolAsync<T>(string path, int maxSize = -1, System.Action<IPool> onAssetLoaded = null) where T : PoolObject
        {
            if (string.IsNullOrEmpty(path))
            {
                Debug.LogError("PoolManager: Trying to create pool by null or empty path");
                return;
            }

            IPool somePool;
            if (!Instance.pools.TryGetValue(path, out somePool) || !somePool.IsReady)
            {
				var pool = new Pool<T>(path, maxSize, true);
                Instance.pools[path] = pool;
                if (onAssetLoaded != null)
                {
                    Instance.waitedPools.Add(new PoolWaiting(pool, onAssetLoaded));
                }
                return;
            }

            onAssetLoaded?.Invoke((Pool<T>)somePool);
        }

		public static Pool<T> GetPool<T>(string path, int maxSize = -1) where T : PoolObject
	    {
            if (string.IsNullOrEmpty(path))
            {
                Debug.LogError("PoolManager: Trying to create pool by null or empty path");
                return null;
            }

            IPool somePool;
            if (Instance.pools.TryGetValue(path, out somePool))
            {
                return (Pool<T>) somePool;
            }

            var pool = new Pool<T>(path, maxSize);
            Instance.pools[path] = pool;
            return pool;
	    }

	    public static void MarkPoolAsCustom(string path, object customer)
	    {
            if (string.IsNullOrEmpty(path))
            {
                Debug.LogError("PoolManager: Trying to mark pool as necessary by null or empty path");
                return;
            }

	        IPool pool;
            if (!Instance.pools.TryGetValue(path, out pool))
            {
                Debug.LogErrorFormat("PoolManager: Trying to mark unexisted pool ({0}) as necessary", path);
                return;
            }

            pool.MarkAsNecessary(customer.GetHashCode());
	    }

        public static void ReleasePool(string path, object customer)
        {
            if (customer != null)
            {
                ReleasePool(path, customer.GetHashCode());
            }
        }

        public static void ReleasePool(string path, int customerHash)
	    {
            if (string.IsNullOrEmpty(path))
            {
                Debug.LogError("PoolManager: Trying to release pool by null or empty path");
                return;
            }

            IPool pool;
            if (!Instance.pools.TryGetValue(path, out pool))
                return;

	        if (pool.Release(customerHash))
	            Instance.pools.Remove(path);
	    }

		//------------------------------------------------------------------------------

		private static PoolManager GetManagerInstance()
	    {
		    var poolManager = FindObjectOfType<PoolManager>() ?? new GameObject("PoolManager").AddComponent<PoolManager>();
		    return poolManager;
	    }
	}
}