using System.Collections;
using System.Collections.Generic;
using System;
namespace SUIFW {
    public interface IConfigInfo  {
        Dictionary<string, string> AppSetting{get;}

        int GetAppSettingMaxNum();
    
    }

    [Serializable]
    internal class ConfigInfoByJson {
        public List<JsonNodeData> JsonConfigInfo = null;
    }

    [Serializable]
    internal class JsonNodeData {
        public string Key = null;
        public string Value = null;
    }
}
