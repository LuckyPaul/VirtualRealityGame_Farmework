using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class AutoSaveScene : MonoBehaviour {

    // 此函数在脚本启动时调用
    private void Awake() {
        timer = Time.realtimeSinceStartup;
    }

    private float timer;
    private void OnGUI() {

        if ((Time.realtimeSinceStartup - timer) >= 20) {
            timer = Time.realtimeSinceStartup;
            Scene _scene = EditorSceneManager.GetActiveScene();
            if (_scene.isDirty) {
                EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
            }
        }
    }

}
