using System.Collections.Generic;
using UnityEngine;
namespace SUIFW {
    public class LanguageMgr:Singleton<LanguageMgr> {
        private Dictionary<string, string> _DicLanguageCache;

        protected override void Init() {
            _DicLanguageCache = new Dictionary<string, string>();
            InitAndAnalysisJsonData(SysDefine.SYS_PATH_JSON_LAUGUAGE);
        }

        public string GetLanguageByKey(string key) {
            if (string.IsNullOrEmpty(key))
                return null ;

            if (null == _DicLanguageCache || _DicLanguageCache.Count <= 0)
                return null ;

            return true == _DicLanguageCache.ContainsKey(key) ? _DicLanguageCache[key] : null;

        }

        private void InitAndAnalysisJsonData(string path) {  
            if (string.IsNullOrEmpty(path))
                return;

            ConfigMgrByJson _Config = new ConfigMgrByJson(path);
            if (null != _Config) {
                _DicLanguageCache = _Config.AppSetting;
            }
        }
    }
}