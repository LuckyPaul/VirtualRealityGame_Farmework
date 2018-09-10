using System;
using System.Collections.Generic;
using UnityEngine;

namespace SUIFW {
    public class ConfigMgrByJson:IConfigInfo {
        private Dictionary<string, string> _appSetting;

        public Dictionary<string, string> AppSetting {
            get {
                return _appSetting; 
            }
        }

        public int GetAppSettingMaxNum() {
            return null == _appSetting ? 0 : _appSetting.Count ;
        }


        public ConfigMgrByJson(string _jsonPath) {
            _appSetting = new Dictionary<string, string>();

            InitAndAnalysisJsonData(_jsonPath);
        }

        private void InitAndAnalysisJsonData(string _jsonPath) {
            if (string.IsNullOrEmpty(_jsonPath))
                return;

            TextAsset jsonAsset = ResourcesMgr.Instance.LoadResource<TextAsset>(_jsonPath, false);
            ConfigInfoByJson _ConfigInfoByJson = null;
            if (null == jsonAsset) {
                Debug.LogError("加载配置文件失败！！！_jsonPath：" + _jsonPath);
                return;
            }

            try {
                _ConfigInfoByJson = JsonUtility.FromJson<ConfigInfoByJson>(jsonAsset.text);
    
            }
            catch (Exception e) {
                throw new JsonAnalysisException(GetType()+"/"+ e.ToString() + "/解析json失败！！！_jsonPath： " + _jsonPath);
            }

            foreach (JsonNodeData item in _ConfigInfoByJson.JsonConfigInfo) {
                _appSetting.Add(item.Key, item.Value);
            }

        }
    }
}
