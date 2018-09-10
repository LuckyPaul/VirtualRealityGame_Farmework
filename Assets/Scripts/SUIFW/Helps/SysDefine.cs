using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SUIFW {

    public enum UIFormsType { 
        Normal   =0,
        Fixed = 1,
        Popup = 2,
    }

    public enum UIFormsShowType { 
        Normal  =0 ,
        ReverseChange =1,
        HidingOther =2,
    }

    public enum UIFormsLucencyType { 
        /// <summary>
        /// 完全透明，不可点击
        /// </summary>
        Lucency = 0,
        /// <summary>
        /// 半透明，不可点击
        /// </summary>
        Translucent =1,
        /// <summary>
        /// 低透明，不可点击
        /// </summary>
        LowTranslucent =2,
        /// <summary>
        /// 可点击
        /// </summary>
        Transpaent =3,
    }


    /// <summary>
    /// 颜色管理
    /// </summary>
    public class MyColorMgr {

        static public Color LUCENCY = new Color(1, 1, 1, 0);

        static public Color TRANSLUCENT = new Color(0.196f, 0.196f, 0.196f, 0.784f);

        static public Color LOWTRANSLUCENT = new Color(0.863f, 0.863f, 0.863f, 0.196f);

    

    }



    public class SysDefine : MonoBehaviour {
        /*标签管理*/
        public const string UNITY_SINGLETON_SCRIPTS_ROOT = "UnitySingletonScriptsRoot";
        //public const string UIROOT = "UIRoot";

        //END


        /*Path Mgr*/
        //Resources Asset path
        public const string SYS_PATH_UIROOT = "UIPrefabs/UIRoot";

        //Config File Path 
        public const string SYS_PATH_JSON_UIFORMS = "Config/UIFormsPathJsonData";
        public const string SYS_PATH_JSON_LOG = "Config/SysConfigInfo";
        public const string SYS_PATH_JSON_LAUGUAGE = "Config/LauguageJSONConfig";
                  

        //Find Path Mgr
        public const string SYS_PATH_NORMALROOT = "NormalRoot";
        public const string SYS_PATH_FIXEDROOT = "FixedRoot";
        public const string SYS_PATH_POPUPROOT = "PopupRoot";
        public const string SYS_PATH_UISCRIPTSROOT = "_UIScriptsRoot";

        //END 
    }


}
