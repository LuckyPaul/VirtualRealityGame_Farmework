using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SUIFW {
    public abstract class BaseUIModule {
        protected bool isAutoRegister = false;

        protected virtual void Init() { }
        protected virtual void Destroy() { }

        public BaseUIModule() {
            Init();

            if (isAutoRegister) {
                UIModuleMgr.Instance.Register(this);
            }
     
        }



         ~BaseUIModule() {
            Destroy();
            if (isAutoRegister) {
                UIModuleMgr.Instance.UnRegister(this);
            }
            
        }
    }
}
