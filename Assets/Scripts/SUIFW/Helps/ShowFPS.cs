using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SUIFW {
    public class ShowFPS : DDOLUnitySingleton<ShowFPS> {
        private float time_delay = 0.5f;
        private float prev_time = 0;
        private int i_frame = 0;
        private float fps = 0;

        private GUIStyle style;
        private bool isShow = true;
        
        void Start() {
            this.prev_time = Time.realtimeSinceStartup;

            style = new GUIStyle();
            style.fontSize = 30;
            style.normal.textColor = Color.red;
        }

        public void OnGUI() {
            GUI.Label(new Rect(20, Screen.height - 40, 200, 200), "fps: " + this.fps.ToString("f2"), style);
        }

        void Update() {
            if (!isShow)
                return;
            this.i_frame++;
            if (Time.realtimeSinceStartup > (this.prev_time + time_delay)) {
                this.fps = (float)this.i_frame / time_delay;
                this.prev_time = Time.realtimeSinceStartup;
                this.i_frame = 0;
            }
        }


        public void IsShowFPS(bool isShow) {
            GameObject obj = GameObject.FindGameObjectWithTag(SysDefine.UNITY_SINGLETON_SCRIPTS_ROOT);
            GameObject fps = HelperAboutUnity.FindChildNode(obj, typeof(ShowFPS).Name);
         
            if (isShow) {
                if (null == fps) {
                    obj.AddComponent<ShowFPS>(); 
                }

            }
            else {
                if (null != fps)
                    Destroy(fps);
            }
        }
    }
}
