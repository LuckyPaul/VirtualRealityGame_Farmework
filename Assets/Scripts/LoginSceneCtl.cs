using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuckyPual.UI;

public class LoginSceneCtl : MonoBehaviour {

	// Use this for initialization
	void Start () {
        LPUIPage.ShowPage<LoginPage>();
        StartCoroutine(ScenesMgr.Instance.LoadAsync("MainScene", LoadAsyncCallBack));

	}
	
	// Update is called once per frame
	void Update () {
		if(ScenesMgr.Instance.asyncOperation != null)
        {
            Debug.Log(ScenesMgr.Instance.asyncOperation.progress);
        }
	}



    public void LoadAsyncCallBack(AsyncOperation async)
    {
            Debug.Log("加载完成");
    }
}
