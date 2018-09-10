using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuckyPual.UI;

public class LoginSceneCtl : MonoBehaviour {

    public ScenesMgr mgr;
	// Use this for initialization
	void Start () {
        LPUIPage.ShowPage<LoginPage>();
        mgr = new ScenesMgr();
        StartCoroutine(mgr.LoadAsync("MainScene", LoadAsyncCallBack));

	}
	
	// Update is called once per frame
	void Update () {
		if(mgr.asyncOperation != null)
        {
            Debug.Log(mgr.asyncOperation.progress);
        }
	}



    public void LoadAsyncCallBack(AsyncOperation async)
    {
        if (async.isDone)
        {
            Debug.Log("加载完成");
        }
    }
}
