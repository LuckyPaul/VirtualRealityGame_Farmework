using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SUIFW {
    public class UIType  {

        public UIFormsType _UIFormsType = UIFormsType.Normal;
        public UIFormsShowType _UIFormsShowType = UIFormsShowType.Normal;
        public UIFormsLucencyType _UIFormsLucencyType = UIFormsLucencyType.Lucency;
        public bool isClearStack = false;//是否清空栈

        public UIType() {

        }

        public UIType(bool isClearStack) {
            this.isClearStack = isClearStack;
        }


        public UIType(UIFormsType _UIFormsType, UIFormsShowType _UIFormsShowType, UIFormsLucencyType _UIFormsLucencyType) {
            this._UIFormsType = _UIFormsType;
            this._UIFormsShowType = _UIFormsShowType;
            this._UIFormsLucencyType = _UIFormsLucencyType;
        }



        public UIType(UIFormsType _UIFormsType, UIFormsShowType _UIFormsShowType, UIFormsLucencyType _UIFormsLucencyType, bool isClearStack) {
            this._UIFormsType = _UIFormsType;
            this._UIFormsShowType = _UIFormsShowType;
            this._UIFormsLucencyType = _UIFormsLucencyType;
            this.isClearStack = isClearStack;
        }
    }
}
