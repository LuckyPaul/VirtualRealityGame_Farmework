/***
 * 
 *    Title: "SUIFW" UI框架项目
 *           主题： unity单例模块，当前场景唯一，跳转场景销毁     
 *    Description: 
 *           功能： 本功能是提供了单例模块，当前场景唯一继承Mono的单例都继承这个类。
 *                  本脚本适用于
 *    Date: 2017
 *    Version: 0.1版本
 *    Modify Recoder: 
 *    
 *   
 */

using UnityEngine;

namespace SUIFW {
    public abstract class UnitySingleton<T> : MonoBehaviour where T : Component {

        static private T _instance = null;
        static private Object _mutex = new Object();
        static public T Instance {
            get {
                if (null == _instance) {
                    _instance = GameObject.FindObjectOfType<T>();
                    if (null == _instance) {
                        lock (_mutex) {
                            if (null == _instance) {
                                _instance = GameObject.FindObjectOfType<T>();
                                if (null == _instance) {
                                    GameObject obj = new GameObject(typeof(T).Name);
                                    _instance = obj.AddComponent<T>();
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
            GameObject _Manager = GameObject.FindGameObjectWithTag(SysDefine.UNITY_SINGLETON_SCRIPTS_ROOT);
            if (null == _Manager) {
                _Manager = new GameObject("UnitySingletonScriptsRoot");
                _Manager.tag = SysDefine.UNITY_SINGLETON_SCRIPTS_ROOT;
            }

            return _Manager;
        }


        protected virtual void Awake() {
            if (_instance == null) {
                _instance = this as T;
            }
            else {
                DestroyImmediate((this as T));
            }
        }
    }
}
