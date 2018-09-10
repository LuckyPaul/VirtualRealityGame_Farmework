/***
 * 
 *    Title: 特斯拉项目
 *           主题：       
 *    Description: 
 *           功能： 
 *                  
 *    Date: 9/6/2018
 *    Version: 0.1版本
 *    Modify Recoder: 
 *    Author: WXL
 *   
 */

using System.Collections;
using System.Collections.Generic;

namespace SUIFW {
	public class Message : IEnumerable<KeyValuePair<string, object>> {

        private Dictionary<string, object> transmitDatas = null;

        public string Name { get; private set; }
        public object Sender { get; private set; }

        public object Content { get; private set; }

        public object this[string key] {
            get {
                if (null == transmitDatas)
                    return null;
                if (!transmitDatas.ContainsKey(key))
                    return null;
                return transmitDatas[key];
            }
            set {
                if (null == transmitDatas)
                    transmitDatas = new Dictionary<string, object>();
                if (transmitDatas.ContainsKey(key)) {
                    transmitDatas[key] = value;
                    return;
                }
                transmitDatas.Add(key,value);

            }

        }
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() {
            if (null == transmitDatas)
                yield break;
            foreach (KeyValuePair<string,object> kvp in transmitDatas) {
                yield return kvp;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return transmitDatas.GetEnumerator();
        }

        public Message(Message msg) {
            this.Name = msg.Name;
            this.Sender = msg.Sender;
            this.Content = msg.Content;
            if (null == msg.transmitDatas)
                return;
            foreach (KeyValuePair<string ,object> kvp in msg.transmitDatas) {
                this[kvp.Key] = kvp.Value;
            }
        }

        public Message(string msgName ,object sender) {
            this.Name = msgName;
            this.Sender = sender;
            this.Content = null;
        }

        public Message(string msgName, object sender,object content) {
            this.Name = msgName;
            this.Sender = sender;
            this.Content = content;
        }

        public Message(string msgName, object sender, object content,params object[] _params) {
            this.Name = msgName;
            this.Sender = sender;
            this.Content = content;

            if (_params.GetType() == typeof(Dictionary<string, object>)) {
                foreach (object param in _params) {
                    foreach (KeyValuePair<string,object> kvp in param as Dictionary<string,object>) {
                        this[kvp.Key] = kvp.Value;
                    }        
                }
            }
        }


        public void Add(string key ,object value) {
            this[key] = value;
        }

        public void Remove(string key ) {
            if (null == transmitDatas || transmitDatas.ContainsKey(key)) {
                return;
            }
            transmitDatas.Remove(key);

        }


        public void SendMsg() {
            MessageCenter.Instance.SendMessage(this);
        }

    }
}

