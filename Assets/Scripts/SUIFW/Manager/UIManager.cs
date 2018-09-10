using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SUIFW {
    public class UIManager : UnitySingleton<UIManager> {

        private Dictionary<string, string> _DicUIFormsPaths = null;
        private Dictionary<string, BaseUIForms> _DicUIFormsCache = null;
        private Dictionary<string, BaseUIForms> _DicCurShowUIForms = null;
        private Stack<BaseUIForms> _StaCacheUIForms = null;

        private GameObject _UIRoot;
        private Transform _NormalRoot;
        private Transform _FixedRoot;
        private Transform _PopupRoot;
        private Transform _UIScriptsRoot;

        public GameObject UIRoot {
            get {
                return _UIRoot;
            }
        }

        public Transform UIScriptsRoot {
            get {
                return _UIScriptsRoot;
            }
        }

        private void InitDic() {
            _DicUIFormsPaths = new Dictionary<string, string>();
            _DicUIFormsCache = new Dictionary<string, BaseUIForms>();
            _DicCurShowUIForms = new Dictionary<string, BaseUIForms>();
            _StaCacheUIForms = new Stack<BaseUIForms>();

            InitUIFormsPaths();
        }

        /// <summary>
        /// 初始化字段
        /// </summary>
        private void InitField() {

            _UIRoot = ResourcesMgr.Instance.LoadAsset(SysDefine.SYS_PATH_UIROOT, false);

            _NormalRoot = HelperAboutUnity.FindChildNode(_UIRoot.transform, SysDefine.SYS_PATH_NORMALROOT);
            _FixedRoot = HelperAboutUnity.FindChildNode(_UIRoot.transform, SysDefine.SYS_PATH_FIXEDROOT);
            _PopupRoot = HelperAboutUnity.FindChildNode(_UIRoot.transform, SysDefine.SYS_PATH_POPUPROOT);
            _UIScriptsRoot = HelperAboutUnity.FindChildNode(_UIRoot.transform, SysDefine.SYS_PATH_UISCRIPTSROOT);

            this.transform.SetParent(_UIScriptsRoot,false);
            DontDestroyOnLoad(_UIRoot);
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitUIFormsPaths() {
            ConfigMgrByJson _ConfigMgrByJson = new ConfigMgrByJson(SysDefine.SYS_PATH_JSON_UIFORMS);
            if (null != _ConfigMgrByJson)
                _DicUIFormsPaths = _ConfigMgrByJson.AppSetting;

        }

        protected override void Awake() {
            base.Awake();

            InitDic();
            InitField();


        }

        public void OpenUIForms(string UIFormsName) {
            if (string.IsNullOrEmpty(UIFormsName)) {
                Debug.LogError("参数不合法！！！");
                return;
            }

            BaseUIForms _baseUIForms = null;

            //缓存字典里没有
            if (!_DicUIFormsCache.TryGetValue(UIFormsName, out _baseUIForms)) {
                _baseUIForms = LoadUIForms(UIFormsName);
                if (null == _baseUIForms)
                    return;

                _baseUIForms.gameObject.SetActive(false);
                _DicUIFormsCache.Add(UIFormsName, _baseUIForms);
            }

            switch (_baseUIForms.CurrentUIType._UIFormsType) {
                case UIFormsType.Normal:
                    _baseUIForms.transform.SetParent(_NormalRoot, false);
                    break;
                case UIFormsType.Fixed:
                    _baseUIForms.transform.SetParent(_FixedRoot, false);
                    break;
                case UIFormsType.Popup:
                    _baseUIForms.transform.SetParent(_PopupRoot, false);
                    break;
                default:
                    break;
            }


            switch (_baseUIForms.CurrentUIType._UIFormsShowType) {
                case UIFormsShowType.Normal:
                    AddUIFormsToShowUIFormsDic(UIFormsName, _baseUIForms);
                    break;
                case UIFormsShowType.ReverseChange:
                    AddUIFormsToShowUIFormsSta(_baseUIForms);
                    break;
                case UIFormsShowType.HidingOther:
                    OpenUIFormsAndHidingOther(UIFormsName, _baseUIForms);
                    break;
                default:
                    break;
            }

        }

        public void CloseUIForms(string UIFormsName) {
            if (string.IsNullOrEmpty(UIFormsName)) {
                Debug.LogError("参数不合法！！！");
                return;
            }

            BaseUIForms _baseUIForms = null;

            //缓存字典里没有
            if (!_DicUIFormsCache.TryGetValue(UIFormsName, out _baseUIForms)) {
                Debug.LogError("要关闭的窗体不存在！！！UIFormsName： " + UIFormsName);
                return;
            }


            switch (_baseUIForms.CurrentUIType._UIFormsShowType) {
                case UIFormsShowType.Normal:
                    CloseUIFormsByNormalType(UIFormsName);
                    break;
                case UIFormsShowType.ReverseChange:
                    CloseUIFormsByReverseChangeType();
                    break;
                case UIFormsShowType.HidingOther:
                    CloseUIFormsByHidingOtherType(UIFormsName);
                    break;
                default:
                    break;
            }

        }


        private void CloseUIFormsByNormalType(string UIFormsName) {
            if (string.IsNullOrEmpty(UIFormsName) ) {
                return;
            }

            BaseUIForms _baseUIFormsAtDic = null;
            if (!_DicCurShowUIForms.TryGetValue(UIFormsName, out _baseUIFormsAtDic)) {
                Debug.LogError("要关闭的窗体不存在！！！UIFormsName： " + UIFormsName);
                return;
            }

            _baseUIFormsAtDic.Hiding();
            _DicCurShowUIForms.Remove(UIFormsName);
            _baseUIFormsAtDic = null;

        }


        private void CloseUIFormsByReverseChangeType() {
            if (_StaCacheUIForms.Count > 1) {
                _StaCacheUIForms.Pop().Hiding();
                _StaCacheUIForms.Peek().Redisplay();
            }
            else if(_StaCacheUIForms.Count >0){
                _StaCacheUIForms.Pop().Hiding();
            }

        }

        private void CloseUIFormsByHidingOtherType(string UIFormsName) {
            if (string.IsNullOrEmpty(UIFormsName)) {
                return;
            }
            BaseUIForms _baseUIFormsAtDic = null ;
            if (!_DicCurShowUIForms.TryGetValue(UIFormsName, out _baseUIFormsAtDic)) {
                Debug.LogError("要关闭的窗体不存在！！！UIFormsName： " + UIFormsName);
                return;
            }

            _baseUIFormsAtDic.Hiding();
            _DicCurShowUIForms.Remove(UIFormsName);
            _baseUIFormsAtDic = null;

            foreach (BaseUIForms item in _DicCurShowUIForms.Values) {
                item.Redisplay();
            }

            foreach (BaseUIForms item in _StaCacheUIForms) {
                item.Redisplay();
            }


        }




        private void ClearStack() {
            if (_StaCacheUIForms != null || _StaCacheUIForms.Count > 0) {
                _StaCacheUIForms.Clear();
            }
        }

        private void AddUIFormsToShowUIFormsDic(string UIFormsName, BaseUIForms _baseUIForms) {
            if (string.IsNullOrEmpty( UIFormsName )|| null == _baseUIForms) {
                return;
            }
            BaseUIForms _baseUIFormsCurShowDic = null ;
            if (!_DicCurShowUIForms.TryGetValue(UIFormsName, out _baseUIFormsCurShowDic)) {
                _DicCurShowUIForms.Add(UIFormsName, _baseUIForms);
                _baseUIForms.Display();
            }
        }

        private void AddUIFormsToShowUIFormsSta(BaseUIForms _baseUIForms) {
            if ( null == _baseUIForms) {
                return;
            }

            if (_StaCacheUIForms.Contains(_baseUIForms)) {
                return;
            }

            if (_baseUIForms.CurrentUIType.isClearStack) {
                ClearStack();
            }

            if (_StaCacheUIForms.Count > 0) {
                _StaCacheUIForms.Peek().Freeze();
            }
            _StaCacheUIForms.Push(_baseUIForms);
            _baseUIForms.Display();
        }

        private void OpenUIFormsAndHidingOther(string UIFormsName, BaseUIForms _baseUIForms) {
            if (string.IsNullOrEmpty(UIFormsName) || null == _baseUIForms) {
                return;
            }

            BaseUIForms _baseUIFormsAtDic = null;
            if (_DicCurShowUIForms.TryGetValue(UIFormsName,out _baseUIFormsAtDic)) {
                _baseUIFormsAtDic = null;
                return;
            }

            foreach (BaseUIForms _baseUIFormsItem in _DicCurShowUIForms.Values) {
                _baseUIFormsItem.Hiding();
            }

            foreach (BaseUIForms _baseUIFormsItem in _StaCacheUIForms) {
                _baseUIFormsItem.Hiding();
            }
            _DicCurShowUIForms.Add(UIFormsName, _baseUIForms);
            _baseUIForms.Display();

        }

        /// <summary>
        /// 根据ui窗体的名字加载对应的Ui窗体
        /// </summary>
        /// <param name="UIFormsName">窗体的名字</param>
        /// <returns>返回窗体上挂载的脚本</returns>
        private BaseUIForms LoadUIForms(string UIFormsName){
            if (string.IsNullOrEmpty(UIFormsName)) {
                return null;
            }

            BaseUIForms _baseUIForms = null;
            string _UIFormsPath = null;
            if (!_DicUIFormsPaths.TryGetValue(UIFormsName, out _UIFormsPath)) {
                Debug.LogError("窗体路径不存在，请检查！！！，UIFormsName： " + UIFormsName);
                return null ;
            }

            GameObject _UIForms = ResourcesMgr.Instance.LoadAsset(_UIFormsPath,false);
            if (null == _UIForms) {
                Debug.LogError("加载窗体失败，请检查路径！！！，UIFormsPath： " + _UIFormsPath);
            }

            _baseUIForms = _UIForms.GetComponent<BaseUIForms>();
            if (null == _baseUIForms) {
                Debug.LogError("窗体上未挂载对应的脚本，请检查！！！，UIFormsName： " + UIFormsName);
            }
            return _baseUIForms;
        }

        
    }
}