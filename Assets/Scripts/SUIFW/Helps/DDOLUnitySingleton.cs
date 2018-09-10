/***
 * 
 *    Title: "SUIFW" UI框架项目
 *           主题： unity单例模块，全局唯一永不消毁     
 *    Description: 
 *           功能： 本功能是提供了单例模块，所有全局唯一永不消毁的集成Mono的单例都继承这个类。
 *                  本脚本适用于
 *    Date: 2017
 *    Version: 0.1版本
 *    Modify Recoder: 
 *    
 *   
 */

using UnityEngine;

namespace SUIFW {
    public abstract class DDOLUnitySingleton<T> : MonoBehaviour where T : Component {

        static private T _instance = null;
        static private Object _mutex = new Object();
        static public T Instance {
            get {
                if (null == _instance) {
                    _instance = HelperAboutUnity.FindObjByType<T>();
                    if (null == _instance) {
                        lock (_mutex) {
                            if (null == _instance) {
                                _instance = HelperAboutUnity.FindObjByType<T>();
                                if (null == _instance) {
                                    GameObject obj = new GameObject(typeof(T).Name);
                                    _instance = obj.AddComponent<T>();

                                    GameObject _Manager = GetOrCreateUnitySingletonScriptsRoot();
                                    obj.transform.SetParent(_Manager.transform);

                                }

                            }
                        }
                    }

                }
                return _instance;
            }
        }

        /// <summary>
        /// 获取挂载unity单例脚本的节点
        /// </summary>
        private static GameObject GetOrCreateUnitySingletonScriptsRoot() {
            GameObject _Manager = HelperAboutUnity.FindObjByTag(SysDefine.UNITY_SINGLETON_SCRIPTS_ROOT);
            if (null == _Manager) {
                _Manager = new GameObject("UnitySingletonScriptsRoot");
                _Manager.tag = SysDefine.UNITY_SINGLETON_SCRIPTS_ROOT;
            }

            return _Manager;
        }


        protected virtual void Awake() {
            DontDestroyOnLoad(GetOrCreateUnitySingletonScriptsRoot());
            if (_instance == null) {
                _instance = this as T;
            }
            else {
                DestroyImmediate((this as T));
            }
        }

    }
}
