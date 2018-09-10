using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuckyPual.UI;

public class LodingPage : LPUIPage {

    public LodingPage() : base(UIType.Fixed, UIMode.HideOther, UICollider.WithBg)
    {
        uiPath = "Prefabs/Loding";
    }
}
