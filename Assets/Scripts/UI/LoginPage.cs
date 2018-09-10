using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuckyPual.UI;
using UnityEngine.UI;

public class LoginPage : LPUIPage {
    public LoginPage() : base(UIType.Normal, UIMode.HideOther, UICollider.None)
    {
        uiPath = "Prefabs/Login";
    }

    public override void Awake(GameObject go)
    {
        this.transform.Find("LoginWindows/SignIn").GetComponent<Button>().onClick.AddListener(()=>
        {
            LPUIPage.ShowPage<LodingPage>();
            LPUIPage.ClosePage<LoginPage>();
        });
    }
    public override void Active()
    {
        base.Active();
    }
    public override void Refresh()
    {
        base.Refresh();
    }
    public override void Hide()
    {
        base.Hide();
    }


}
