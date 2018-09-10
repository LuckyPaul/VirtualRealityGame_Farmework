/***
 * 
 *    Title: "SUIFW" UI框架项目
 *           主题： 资源加载管理器      
 *    Description: 
 *           功能： 本功能是在Unity的Resources类的基础之上，增加了“缓存”的处理。
 *                  本脚本适用于
 *    Date: 2017
 *    Version: 0.1版本
 *    Modify Recoder: 
 *    
 *   
 */
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;


namespace SUIFW {
    public class ResourcesMgr : DDOLUnitySingleton<ResourcesMgr> {
        /* 字段 */
        private Hashtable ht = null;                        //容器键值对集合


        public delegate void OnLoadAsyncOverCallBack(UnityEngine.Object TResource);         //异步加载资源回调



        protected override void Awake() {
            base.Awake();
            ht = new Hashtable();
        }

        /// <summary>
        /// 调用资源（带对象缓冲技术）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="isCatch"></param>
        /// <returns></returns>
        public T LoadResource<T>(string path, bool isCatch) where T : UnityEngine.Object {
            if (ht.Contains(path)) {
                return ht[path] as T;
            }

            T TResource = Resources.Load<T>(path);
            if (TResource == null) {
                Debug.LogError(GetType() + "/GetInstance()/TResource 提取的资源找不到，请检查。 path=" + path);
            }
            else if (isCatch) {
                ht.Add(path, TResource);
            }

            return TResource;
        }





        /// <summary>
        /// 调用资源_异步（带对象缓冲技术）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="isCatch"></param>
        /// <param name="onLoadAsyncOverCallBack">加载完成回调</param>
        /// <returns></returns>
        public IEnumerator LoadResourceAsync<T>(string path, bool isCatch,OnLoadAsyncOverCallBack onLoadAsyncOverCallBack) where T : UnityEngine.Object
        {
            if (ht.Contains(path))
            {
                onLoadAsyncOverCallBack(ht[path] as T);
            }

            ResourceRequest rt = Resources.LoadAsync<T>(path);
            yield return rt;
            T TResource = rt.asset as T;
            if (TResource == null)
            {
                Debug.LogError(GetType() + "/GetInstance()/TResource 提取的资源找不到，请检查。 path=" + path);
            }
            else if (isCatch)
            {
                ht.Add(path, TResource);
            }
            onLoadAsyncOverCallBack(TResource);
        }







        /// <summary>
        /// 调用资源（带对象缓冲技术）
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isCatch"></param>
        /// <returns></returns>
        public GameObject LoadAsset(string path, bool isCatch) {
            GameObject goObj = LoadResource<GameObject>(path, isCatch);
            if (null == goObj) {
                Debug.LogError(GetType() + "/LoadAsset()/加载资源不成功，请检查。 path=" + path);
                return null;
            }
            GameObject goObjClone = GameObject.Instantiate<GameObject>(goObj);
            if (goObjClone == null) {
                Debug.LogError(GetType() + "/LoadAsset()/克隆资源不成功，请检查。 path=" + path);
            }
            //goObj = null;//??????????
            return goObjClone;
        }

        /// <summary>
        /// 加载路径下的所有资源（不带对象缓冲技术）
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isCatch"></param>
        /// <returns></returns>
        public T[] LoadAllAsset<T>(string path) where T:UnityEngine.Object{
            if (string.IsNullOrEmpty(path)) {
                return null;
            }


           T[] _T = Resources.LoadAll<T>(path);
           return _T;
        }

    }//Class_end
}