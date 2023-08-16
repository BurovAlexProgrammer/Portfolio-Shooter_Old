using System;
using Main.Extension;
using UnityEngine;

namespace Main.Wrappers
{
    public abstract class BasePoolItem : MonoBehaviour
    {
        private static int _idGenerator;
        private static int NewId => _idGenerator++;
        
        private int _id = -1;
        
        public Action<BasePoolItem> Returned;
        
        public int Id
        {
            get
            {
                _id = _id < 0 ? NewId : _id;
                return _id;
            }
        }
        
        public void ReturnToPool()
        {
            if (gameObject.IsDestroyed()) return;
            
            gameObject.SetActive(false);
            Returned?.Invoke(this);
        }
    }
}