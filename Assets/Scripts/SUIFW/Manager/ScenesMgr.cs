/***
 * 
 *    Title: 特斯拉项目
 *           主题： 场景加载管理器     
 *    Description: 
 *           功能： 
 *                  
 *    Date: 9/10/2018
 *    Version: 0.1版本
 *    Modify Recoder: 
 *    Author: WSX
 *   
 */

using UnityEngine;
using System.Collections;
using SUIFW;

namespace Tesla {
	public class ScenesMgr : DDOLUnitySingleton<ScenesMgr>
    {

        public delegate void OnLoadSceneAsyncOverCallBack(AsyncOperation asyncOperation);   //场景异步加载完成回调

        /// <summary>
        /// 加载场景
        /// </summary>
        /// <param name="sceneName"></param>
        public void Load(string sceneName)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName) == null)
            {
                Debug.Log("Error:场景-" + sceneName + "不存在");
                return;
            }

            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }

        /// <summary>
        /// 加载场景_累加（之前关卡不销毁）
        /// </summary>
        /// <param name="sceneName"></param>
        public void LoadAdditive(string sceneName)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName) == null)
            {
                Debug.Log("Error:场景-" + sceneName + "不存在");
                return;
            }

            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive);

        }




        /// <summary>
        /// 异步加载场景
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="asyncOperation"></param>
        /// <param name="onLoadSceneAsyncOver"></param>
        /// <returns></returns>
       public IEnumerator LoadAsync(string sceneName, AsyncOperation asyncOperation, OnLoadSceneAsyncOverCallBack onLoadSceneAsyncOverCallBack)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName) == null)
            {
                Debug.Log("Error:场景-" + sceneName + "不存在");
                yield break;
            }
            yield return new WaitForEndOfFrame();
            asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
            asyncOperation.allowSceneActivation = false;
            yield return asyncOperation;

            onLoadSceneAsyncOverCallBack(asyncOperation);
        }

        /// <summary>
        /// 异步加载场景_叠加（之前关卡不销毁）
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="asyncOperation"></param>
        /// <param name="onLoadSceneAsyncOverCallBack"></param>
        /// <returns></returns>
        public IEnumerator LoadAdditiveAsync(string sceneName, AsyncOperation asyncOperation, OnLoadSceneAsyncOverCallBack onLoadSceneAsyncOverCallBack)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName) == null)
            {
                Debug.Log("Error:场景-" + sceneName + "不存在");
                yield break;
            }
            yield return new WaitForEndOfFrame();
            asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName,UnityEngine.SceneManagement.LoadSceneMode.Additive);
            asyncOperation.allowSceneActivation = false;
            yield return asyncOperation;
            onLoadSceneAsyncOverCallBack(asyncOperation);

        }



    }
}

