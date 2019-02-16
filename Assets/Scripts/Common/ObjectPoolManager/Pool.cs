using System;
using System.Collections.Generic;
using DTools.ResourcesPool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DTools.ResourcesPool
{
    public class Pool<T> : IPool where T : PoolObject
    {
	    public bool IsReady => _asyncRequest == null || _asyncRequest.isDone;
	    public string Path => _path;

	    //------------------------------------------------------------------------------

		private HashSet<int> _customersHashes;
		private readonly string _path;
        private T _asset;
        private readonly Queue<T> _innerObjects;
        private ResourceRequest _asyncRequest;
        private readonly int _maxSize;
        private readonly List<PoolObject> _connectedObjects = new List<PoolObject>();
        private bool _released;

		//------------------------------------------------------------------------------

		public Pool(string path, int maxSize = -1, bool asyncResLoading = false)
        {
            this._path = path;
            this._maxSize = maxSize;
            _innerObjects = new Queue<T>(4);
            LoadAsset(asyncResLoading);
        }

		/// <summary>
		/// Mark pool as necessary for another customer.
		/// Experimental feature. Allow to spy for pool and destroy if it is no longer necessary for any customer
		/// </summary>
		/// <param name="customerHash"></param>
		public void MarkAsNecessary(int customerHash)
        {
            if (_customersHashes == null)
            {
                _customersHashes = new HashSet<int>();
            }

            _customersHashes.Add(customerHash);
        }

	    public void Prewarm(int objectsCount)
	    {
		    for (int i = 0; i < objectsCount; ++i)
		    {
			    T newObject = AddObject();
				newObject.ReturnToPool();
		    }
	    }

		/// <summary>
		/// Show that some customer no longer needs this pool.
		/// Experimental feature. Allow to spy for pool and destroy if it is no longer necessary for any customer
		/// </summary>
		/// <param name="customerHash">Object that no more needs pool (hash)</param>
		/// <returns>TRUE if pool is not necessary anymore</returns>
		public bool Release(int customerHash)
        {
            if (_customersHashes == null || !_customersHashes.Remove(customerHash))
            {
                Debug.LogError("Object tries to release pool without prerequisite marking as necessary");
                return false;
            }

            if (_customersHashes.Count == 0)
            {
                Clear();
                _released = true;
                return true;
            }

            return false;
        }

	    public void AcceptObject(PoolObject obj)
	    {
		    if (!_released)
		    {
			    var managerTr = PoolManager.Instance.transform;
			    _innerObjects.Enqueue((T)obj);
			    if (obj.transform.parent != managerTr)
			    {
				    obj.transform.SetParent(managerTr);
			    }
			    return;
		    }

		    Object.Destroy(obj.gameObject);
	    }

	    public override string ToString()
	    {
		    return $"DPool ({_path})";
	    }

	    public void OnPoolObjectDestroy(PoolObject poolObject)
	    {
		    _connectedObjects.Remove(poolObject);
	    }

	    //------------------------------------------------------------------------------

		private void LoadAsset(bool asynchronously)
		{
            if (!asynchronously)
            {
                _asset = (T)Resources.Load<PoolObject>(_path);
            }
            else
            {
                _asyncRequest = Resources.LoadAsync<PoolObject>(_path);
            }
        }

        private void Clear()
        {
            _asset = null;

            while (_innerObjects.Count > 0)
            {
                PoolObject obj = _innerObjects.Dequeue();
                Object.Destroy(obj.gameObject);
            }
        }

        public T GetObject(Vector3 position, Quaternion rotation, Transform parent = null)
        {
            T obj = null;
            if (_innerObjects.Count == 0)
            {
                if (_maxSize > 0 && _connectedObjects.Count == _maxSize)
                {
                    StealObject();
                }
                else
                {
                    obj = AddObject();
                }
            }

	        if (obj == null)
	        {
		        obj = _innerObjects.Dequeue();
		        obj.TakeFromPool();
	        }

	        obj.transform.position = position;
            obj.transform.rotation = rotation;

            if (parent != null)
            {
                obj.transform.SetParent(parent);
            }

            return obj;
        }

        private T AddObject()
        {
            if (_asyncRequest != null)
            {
                if (_asyncRequest.isDone)
                {
                    _asset = (T)_asyncRequest.asset;
                    _asyncRequest = null;
                }
                else
                {
                    return null;
                }
            }

            var obj = Object.Instantiate(_asset);
            obj.SetPool(this);
            if (_maxSize > 0)
            {
                _connectedObjects.Add(obj);
            }

	        return obj;
        }

        private void StealObject()
        {
            var poolObject = _connectedObjects[0];
            var maxPriority = poolObject.GetStealPriority();

            for (var i = 1; i < _connectedObjects.Count; i++)
            {
                var priority = _connectedObjects[i].GetStealPriority();
                if (_connectedObjects[i].GetStealPriority() > maxPriority)
                {
                    maxPriority = priority;
                    poolObject = _connectedObjects[i];
                }
            }

            poolObject.ReturnToPool();
        }
    }
}