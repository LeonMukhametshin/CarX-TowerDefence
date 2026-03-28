using System;
using System.Collections.Generic;

namespace CarXTowerDefence.Utilities
{
    public class ObjectPool<T>
    {
        private readonly Func<T> m_preloadFunc;
        private readonly Action<T> m_getAction;
        private readonly Action<T> m_returnAction;

        private Queue<T> m_pool = new();
        private List<T> m_active = new();

        public ObjectPool(Func<T> preloadFunc, Action<T> getAction, Action<T> returnAction, int preloadCount)
        {
            m_preloadFunc = preloadFunc;
            m_getAction = getAction;
            m_returnAction = returnAction;
           
            if(preloadFunc is null)
            {
                throw new ArgumentNullException("PreloadFunc is null");
            }

            for (int i = 0; i < preloadCount; i++)
            {
                Return(preloadFunc());
            }
        }

        public T Get()
        {
            T item = m_pool.Count > 0 
                ? m_pool.Dequeue() 
                : m_preloadFunc();

            m_getAction?.Invoke(item);
            m_active.Add(item);

            return item;
        }

        public void Return(T item)
        {
            m_returnAction?.Invoke(item);
            m_pool.Enqueue(item);
            m_active.Remove(item);
        }

        public void ReturnAll()
        {
            foreach(T item in m_active.ToArray())
            {
                Return(item);
            }
        }
    }
}