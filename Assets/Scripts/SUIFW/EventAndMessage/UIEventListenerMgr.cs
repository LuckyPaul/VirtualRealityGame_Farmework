/***
 * 
 *    Title: MOBA游戏项目
 *           主题：       
 *    Description: 
 *           功能： 
 *                  
 *    Date: 8/11/2018
 *    Version: 0.1版本
 *    Modify Recoder: 
 *    Author: 汪孝龙
 *   
 */

using UnityEngine;
namespace SUIFW {
    public class UIEventListenerMgr  {

        static public void OnClick(GameObject go ,UUIEventListener.VoidDelegate voidDelegate) {
            UUIEventListener.Get(go).onClick = voidDelegate;
        }

        static public void OnDoubleClick(GameObject go, UUIEventListener.VoidDelegate voidDelegate) {
            UUIEventListener.Get(go).onDoubleClick = voidDelegate;
        }

        static public void OnLongPress(GameObject go, UUIEventListener.VoidDelegate voidDelegate) {
            UUIEventListener.Get(go).onLongPressDown = voidDelegate;
        }

	}
}
