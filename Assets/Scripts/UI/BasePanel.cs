using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BasePanel : MonoBehaviour {
    public UIFormType CurrentUIFormType = UIFormType._Normal;
    public UIFormShowMode CurrentUIFormShowMode = UIFormShowMode._Normal;

    /// <summary>
    /// 打开窗口
    /// </summary>
    public virtual void OnEnter() { }

    /// <summary>
    /// 冻结窗口--其他窗口弹出或叠加
    /// </summary>
    public virtual void OnPuse() { }

    /// <summary>
    /// 解冻窗口--其他窗口关闭
    /// </summary>
    public virtual void OnResume() { }

    /// <summary>
    /// 关闭窗口
    /// </summary>
    public virtual void OnExit() { }


}
