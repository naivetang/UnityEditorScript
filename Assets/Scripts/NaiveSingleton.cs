using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naive
{
    /// <summary>
    /// 线程安全的C#单例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class NaiveSingleton<T> where T : class, new()
    {

        private static T m_instance;

        private static object m_lockObject = new object();

        protected NaiveSingleton()
        {
        }


        public static T Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_lockObject)
                    {
                        if (m_instance == null)
                        {
                            m_instance = new T();
                            (m_instance as NaiveSingleton<T>).Init();
                        }

                    }
                }

                return m_instance;
            }
        }

        public static void DestroyInstance()
        {
            if (m_instance != null)
            {
                (m_instance as NaiveSingleton<T>).UnInit();

                m_instance = null;
            }
        }

        /// <summary>
        /// 创建单例 -- Init
        /// </summary>
        public virtual void Init()
        {
        }

        /// <summary>
        /// 销毁单例 -- UnInit
        /// </summary>
        public virtual void UnInit()
        {
        }
    }
}
