using UnityEngine;
using System.Collections;
using System.Text;
using System.Security.Cryptography;
namespace SUIFW {
    public class HelperAboutUnity : MonoBehaviour {

        /// <summary>
        /// Find T Of Type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        static public T FindObjByType<T>()where T :UnityEngine.Object {
            return GameObject.FindObjectOfType<T>();
        }

        /// <summary>
        /// Find GameObject Of Tag
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        static public GameObject FindObjByTag(string tag) {
            return GameObject.FindGameObjectWithTag(tag);
        }


        #region 查找子物体
        static public Transform FindChildNode(Transform _parent, string childName) {
            if (null == _parent || string.IsNullOrEmpty(childName))
                return null;
            Transform childNode = _parent.Find(childName);
            if (null == childNode) {
                foreach (Transform child in _parent) {
                    childNode = FindChildNode(child, childName);
                    if (null != childNode) {
                        return childNode;
                    }
                }
            }
            return childNode;
        }

        static public GameObject FindChildNode(GameObject _parent, string childName) {
            Transform childNode = FindChildNode(_parent.transform, childName);
            return null == childNode ? null : childNode.gameObject;
        }
        #endregion

        #region 查找组件
        static public T FindComponent<T>(GameObject go) where T : Component {
            T _T = null;
            _T = FindComponent<T>(go.transform);
            return null == _T ? null : _T;
        }

        static public T FindComponent<T>(Transform go) where T : Component {
            T _T = null;
            _T = go.GetComponent<T>();
            if (null == _T) {
                foreach (Transform item in go) {
                    _T = item.GetComponent<T>();
                    if (null != _T) {
                        return _T;
                    }
                }
            }

            return _T;
        }

        #endregion

        #region 设置父物体
        static public void SetParent(Transform parent, Transform target) {
            if (null == parent || null == target)
                return;

            target.SetParent(parent);

        }
        #endregion

        #region 根据长度随机产生一个字符串
        static public string Random_Str(int len) {
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            System.Random r = new System.Random(System.BitConverter.ToInt32(b, 0));

            string str = null;
            str += "0123456789";
            str += "abcdefghijklmnopqrstuvwxyz";
            str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            string s = null;

            for (int i = 0; i < len; i++) {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }
            return s;
        }
        #endregion

        static public  string StrToMD5(string str) {
            string cl = str;
            StringBuilder md5_builder = new StringBuilder();
            MD5 md5 = MD5.Create();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++) {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符
                md5_builder.Append(s[i].ToString("X2"));
                //pwd = pwd + s[i].ToString("X");

            }
            return md5_builder.ToString();
        }


    }
}
