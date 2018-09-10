using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SUIFW {
    public class MessageCenter : Singleton<MessageCenter> {
        //public delegate void DelMsgDelivery(DelParameterData _DelParam);
        public delegate void DelMsgDelivery(Message msg);
        private static Dictionary<string,DelMsgDelivery> _DicDelMsgDelivery;

        public void AddMsgListener(string _MsgType, DelMsgDelivery _DelParam) {
            if (string.IsNullOrEmpty(_MsgType) || null == _DelParam)
                return;
            if (null == _DicDelMsgDelivery) {
                _DicDelMsgDelivery = new Dictionary<string, DelMsgDelivery>();
            }
            if (!_DicDelMsgDelivery.ContainsKey(_MsgType)) {
                _DicDelMsgDelivery.Add(_MsgType, null);
            }
            _DicDelMsgDelivery[_MsgType] += _DelParam;
        }

        public void RemoveMsgListener(string _MsgType, DelMsgDelivery _DelParam) {
            if (string.IsNullOrEmpty(_MsgType) || null == _DicDelMsgDelivery)
                return;

            if (_DicDelMsgDelivery.ContainsKey(_MsgType)) {
                if (null != _DicDelMsgDelivery[_MsgType]) {
                    _DicDelMsgDelivery[_MsgType] -= _DelParam;
                }
                if (null == _DicDelMsgDelivery[_MsgType]) {
                    _DicDelMsgDelivery.Remove(_MsgType);
                }

                if (_DicDelMsgDelivery.Count <= 0) {
                    _DicDelMsgDelivery = null;
                }
            }
        }

        public void RemoveAllMsgListener() {

            if (null == _DicDelMsgDelivery) {
                return;
            }
            _DicDelMsgDelivery.Clear();
            _DicDelMsgDelivery = null;
        }


        public void SendMessage(Message msg) {
            if (string.IsNullOrEmpty(msg.Name) || null == _DicDelMsgDelivery)
                return;
            if (!_DicDelMsgDelivery.ContainsKey(msg.Name) || null == _DicDelMsgDelivery[msg.Name]) {
                return;
            }

            _DicDelMsgDelivery[msg.Name](msg);
        }
    }

    public class DelParameterData {
        private string key;
        private object value;

        public string Key {
            get {
                return key;
            }

            set {
                key = value;
            }
        }

        public object Value {
            get {
                return value;
            }

            set {
                this.value = value;
            }
        }


        public DelParameterData(string key, object value) {
            this.key = key;
            this.value = value;
        }
    }
}
