using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Objects
{
    public abstract class PoolList<T> : MonoBehaviour where T : Component
    {
        protected List<T> _activities = new List<T>();
        protected Queue<T> _pools = new Queue<T>();

        protected T _prefab;

        protected bool _isParent = true;
        protected bool _isRename = true;
        protected bool _isControlActive = true;

        public virtual List<T> Activities => _activities;
        public virtual Queue<T> Pools => _pools;

        public virtual T Prefab
        {
            get => _prefab;
            set => _prefab = value;
        }

        public virtual bool IsParent
        {
            get => _isParent;
            set => _isParent = value;
        }

        public virtual bool IsRename
        {
            get => _isRename;
            set => _isRename = value;
        }

        public virtual bool IsControlActive
        {
            get => _isControlActive; 
            set => _isControlActive = value;
        }

        public virtual int Count => _activities.Count + _pools.Count;


        public virtual T Activate()
        {
            T getObject;
            if (_pools.Count <= 0) getObject = this.Create();
            else getObject = this.GetFromPool();

            _activities.Add(getObject);
            if (IsControlActive) getObject.gameObject.SetActive(true);

            return getObject;
        }

        public virtual bool Deactivate(T targetObject)
        {
            if(!_activities.Remove(targetObject)) return false;

            _pools.Enqueue(targetObject);
            if (IsControlActive) targetObject.gameObject.SetActive(false);

            return true;
        }

        public virtual bool Contains(T targetObject)
        {
            if(_activities.Contains(targetObject)) return true;
            if (_pools.Contains(targetObject)) return true;
            return false;
        }

        public virtual void Clear()
        {
            _activities.Clear();
            _pools.Clear();
        }

        protected virtual T Create()
        {
            if (Prefab == null) Debug.LogError("Prefab is null", this);

            T newObject = Instantiate(Prefab);

            if (IsParent) newObject.transform.SetParent(this.transform);
            if (IsRename) newObject.name = $"{Prefab.name} {this.Count}";
         
            return newObject;
        }

        protected virtual T GetFromPool()
        {
            if (_pools.Count <= 0) return null;
            T getObject = _pools.Dequeue();

            return getObject;
        }
    }
}