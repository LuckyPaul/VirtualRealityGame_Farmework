using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuckyPual.UI;
using UnityEngine.UI;

public class LodingPage : LPUIPage {

    public LodingPage() : base(UIType.Fixed, UIMode.HideOther, UICollider.WithBg)
    {
        uiPath = "Prefabs/Loding";
    }

    public override void Active()
    {
        this.transform.GetChild(0).GetComponent<Slider>().value =ScenesMgr.Instance.asyncOperation.progress;
        this.transform.GetChild(1).GetComponent<Text>().text = "页面载入进度" + ScenesMgr.Instance.asyncOperation.progress * 100 + "%";

        if (ScenesMgr.Instance.asyncOperation.progress > 0.89f)
        {
            ScenesMgr.Instance.asyncOperation.allowSceneActivation = true;
        }
    }

}
