using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SUIFW {
    public class UIModuleMgr : Singleton<UIModuleMgr> {
        private Dictionary<string, BaseUIModule> _DicModules;

        protected override void Init() {
            _DicModules = new Dictionary<string, BaseUIModule>();
        }

        /// <summary>
        /// Register a UIModule 
        /// </summary>
        /// <param name="moduleName">Module Name</param>
        /// <param name="_UIModule">This is a Type Of BaseUIModule</param>
        public void Register(BaseUIModule _UIModule) {
            if ( null == _DicModules) {
                return;
            }
            if(null ==_UIModule){
                Debug.Log("BaseUIModule is null ,This is not allow!!!");
                return ;
            }
            Type _T = _UIModule.GetType();
            if (_DicModules.ContainsKey(_T.ToString())) {
                Debug.Log("BaseUIModule is already register !!!");
                return;
            }

            _DicModules.Add(_T.ToString(), _UIModule);
        }

        /// <summary>
        /// Remove a BaseUIModule form _DicModules
        /// </summary>
        /// <param name="moduleName">Module Name</param>
        public void UnRegister(BaseUIModule _UIModule) {
            if (null == _DicModules) {
                return;
            }
            Type _T = _UIModule.GetType();
            if (_DicModules.ContainsKey(_T.ToString())) {
                _DicModules.Remove(_T.ToString());
            }
            else {
                Debug.Log("The BaseUIModule is not register!!!");
            }
        }

        /// <summary>
        /// Remove all BaseUIModule form _DicModules
        /// </summary>
        public void AllUnRegister() {
            _DicModules.Clear();
        }

        public T GetUIModule<T>() where T : BaseUIModule {
            if (null == _DicModules) {
                return null;
            }
            
            Type _type = typeof(T);
            if (_DicModules.ContainsKey(_type.ToString())) {
                return _DicModules[_type.ToString()] as T;
            }
            return null;

        }
    }
}
