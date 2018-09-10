using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace SUIFW {
    public class UIMaskMgr : UnitySingleton<UIMaskMgr> {

        private Transform _UIRoot;

        private GameObject _UIMaskObj;
        private Transform _UIMaskPanel;
        private Image _UIMaskImage;

        private Camera _UICamera;

        private float _OriginCameraDpeth = 0;

        protected override void Awake() {
            base.Awake();
            _UIRoot = UIManager.Instance.UIRoot.transform;

            _UIMaskPanel = HelperAboutUnity.FindChildNode(_UIRoot, "_UIMaskPanel");
            if (null != _UIMaskPanel)
                _UIMaskObj = _UIMaskPanel.gameObject;
                _UIMaskImage = _UIMaskPanel.GetComponent<Image>();

            _UICamera = HelperAboutUnity.FindComponent<Camera>(_UIRoot);
            if (null != _UICamera) {
                _OriginCameraDpeth = _UICamera.depth;
            }

        }

        public void SetUIFormsMask(GameObject _UIForms ,UIFormsLucencyType lucency) {
            _UIRoot.SetAsLastSibling();
            _UIMaskPanel.SetAsLastSibling();
            _UIForms.transform.SetAsLastSibling();

            switch (lucency) {
                case UIFormsLucencyType.Lucency:
                    _UIMaskImage.color = MyColorMgr.LUCENCY;
                    ChangeUIMaskPanelStata(true);
                    break;
                case UIFormsLucencyType.Translucent:
                    _UIMaskImage.color = MyColorMgr.TRANSLUCENT;
                    ChangeUIMaskPanelStata(true);
                    break;
                case UIFormsLucencyType.LowTranslucent:
                    _UIMaskImage.color = MyColorMgr.LOWTRANSLUCENT;
                    ChangeUIMaskPanelStata( true);
                    break;
                case UIFormsLucencyType.Transpaent:
                    ChangeUIMaskPanelStata(false);
                    break;
                default:
                    break;
            }

            _UICamera.depth = _OriginCameraDpeth + 100;

        }


        public void CancelUIFormsMask() {
            _UIRoot.SetAsFirstSibling();
            _UIMaskPanel.SetAsFirstSibling();
            _UICamera.depth = _OriginCameraDpeth;

            if (_UIMaskObj.activeInHierarchy)
                _UIMaskObj.SetActive(false);

        }


        public void RegisterClickMask(UUIEventListener.VoidDelegate _VoidDel) {
            UIEventListenerMgr.OnClick(_UIMaskObj, _VoidDel);
        }

        private void ChangeUIMaskPanelStata(bool isActive) {

            if (null == _UIMaskPanel)
                return;

            if (_UIMaskObj.activeInHierarchy != isActive)
                _UIMaskObj.SetActive(isActive);
        }
    }
}
