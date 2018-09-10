using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SUIFW {
    public abstract class BaseUIForms : MonoBehaviour {
        #region Field Or Attribute
        private UIType _CurrentUIType = new UIType();

        public UIType CurrentUIType {
            get { return _CurrentUIType; }
            set { _CurrentUIType = value; }
        }

        #endregion

        public BaseUIForms(UIType _UIType)
        {
            this.CurrentUIType = _UIType;
        }



        #region 窗体的四种状态
        public virtual void Display() {
            this.gameObject.SetActive(true);

            if (this.CurrentUIType._UIFormsType == UIFormsType.Popup) {
                UIMaskMgr.Instance.SetUIFormsMask(this.gameObject, this.CurrentUIType._UIFormsLucencyType);
            }
        }

        public virtual void Hiding() {
            this.gameObject.SetActive(false);
            if (this.CurrentUIType._UIFormsType == UIFormsType.Popup) {
                UIMaskMgr.Instance.CancelUIFormsMask();
            }
        }

        public virtual void Redisplay() {
            this.gameObject.SetActive(true);
            if (this.CurrentUIType._UIFormsType == UIFormsType.Popup) {
                UIMaskMgr.Instance.SetUIFormsMask(this.gameObject, this.CurrentUIType._UIFormsLucencyType);
            }
        }

        public virtual void Freeze() {
            this.gameObject.SetActive(true);
        }
        #endregion

        #region 打开或者关闭UI窗体
        protected void OpenUIForms(string _UIFormsName) {
            UIManager.Instance.OpenUIForms(_UIFormsName);
        }

        protected void CloseUIForms(string _UIFormsName) {
            UIManager.Instance.CloseUIForms(_UIFormsName);
        }
        protected void CloseUIForms() {
            string selfClassName = GetType().Name;
            int index = -1;
            index = selfClassName.LastIndexOf(".");
            if (index != -1) {
                selfClassName = selfClassName.Substring(index + 1);
            }

            UIManager.Instance.CloseUIForms(selfClassName);
        }

        #endregion

        #region 注册按钮点击事件
        protected void RegisterClickEvent(UnityEngine.UI.Button _btn, UUIEventListener.VoidDelegate clickEvent) {
            _btn.onClick.AddListener(() => { clickEvent(_btn.gameObject); });
        }
        protected bool RegisterClickEvent(string btnName, UUIEventListener.VoidDelegate clickEvent) {
            GameObject btn = HelperAboutUnity.FindChildNode(this.gameObject, btnName);
            if (null != btn) {
                UUIEventListener.Get(btn).onClick = clickEvent;
                return true;
            }
            return false;
        }

        #endregion

        #region Receive Or Dispatcher Msg
        /// <summary>
        /// Listener Msg 
        /// </summary>
        /// <param name="_MsgType">Msg Type</param>
        /// <param name="_DelMsg">Delegate Method</param>
        protected void ReceiveMsg(string _MsgType, MessageCenter.DelMsgDelivery _DelMsg) {
            MessageCenter.Instance.AddMsgListener(_MsgType, _DelMsg);
        }

        /// <summary>
        /// Send Msg 
        /// </summary>
        /// <param name="_MsgType">Msg Type</param>
        /// <param name="_MsgName">Msg Name</param>
        /// <param name="_MsgContent">Msg Content</param>
        protected void DispatcherMsg(Message msg) {
            MessageCenter.Instance.SendMessage(msg);
        }


        /// <summary>
        /// Remove Message From MessageCenter
        /// </summary>
        /// <param name="_MsgType"> Message Type </param>
        /// <param name="_DelMsg"> Delegate </param>
        protected void RemoveMsg(string _MsgType, MessageCenter.DelMsgDelivery _DelMsg) {
            MessageCenter.Instance.RemoveMsgListener(_MsgType, _DelMsg);
        }

        #endregion

        /// <summary>
        /// Get Lauguage By Key Of The  Config File
        /// </summary>
        /// <param name="key"> Key Of The  Config File </param>
        /// <returns>Value Of The  Config File</returns>
        protected string GetLanguage(string key) {
            return LanguageMgr.Instance.GetLanguageByKey(key);
        }

        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="_clip"></param>
        protected void PlayEffect(AudioClip _clip) {
            AudioMgr.Instance.play_effect(_clip);
        }

        /// <summary>
        /// 播放音乐(如背景)
        /// </summary>
        /// <param name="_clip"></param>
        protected void PlayMusic(AudioClip _clip,bool isLoop = true) {
            AudioMgr.Instance.play_music(_clip,isLoop);
        }

    }
}