using System;

namespace SUIFW {
    /// <summary>
    ///  Template Class Of Not Inherit
    /// </summary>
    /// <typeparam name="T">Singleton Classs name</typeparam>
    public class Singleton<T> where T : new() {
        static private T _instance = default(T);
        static private object _Mutex = new object();

        static public T Instance {
            get {
                if (null == _instance) {
                    lock (_Mutex) {
                        if (null == _instance) {
                            _instance = new T();
                        }
                    }
                }
                return _instance;
            }
        }

        protected Singleton() {
            Init();
            if (_instance != null) {
                throw new SingletonException(GetType()+"This Singleton is already exist ! Please not new again !!!");
            }
        }

        protected virtual void Init() { }
    }
}
