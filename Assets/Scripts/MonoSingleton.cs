using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Naive
{
    /// <summary>
    /// 线程安全的mono单例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T m_instance;

        private static object m_lockObject = new object();

        public static T Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_lockObject)
                    {
                        m_instance = GameObject.FindObjectOfType<T>();   

                        if (m_instance == null)
                        {
                            GameObject singletonGo = new GameObject {name = typeof(T).FullName};

                            m_instance = singletonGo.AddComponent<T>();
                        }

                        DontDestroyOnLoad(m_instance.gameObject);
                    }
                }

                return m_instance;
            }
        }

        public static void DestroyInstance()
        {
            if (m_instance != null)
            {
                Destroy(m_instance.gameObject);

                m_instance = null;
            }
        }

        public virtual void Awake()
        {
            if (m_instance == null)
            {
                m_instance = this as T;

                DontDestroyOnLoad(m_instance.gameObject);
            }
                
        }

        public virtual void OnDestroy()
        {

        }

    }
}
