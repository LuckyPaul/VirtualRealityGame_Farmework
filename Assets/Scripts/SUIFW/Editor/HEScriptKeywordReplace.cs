/* 
 描 述：修改创建出来的脚本里的key值，比如改创建时间
 作 者：wxl 
 创建时间：#CreateTime#
 版 本：v 1.0
*/

using UnityEngine;

using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;

/// <summary>
/// 用unity自带的方法创建C#脚本
/// 并修改了C#模板后
/// 需要这个修改创建出来的脚本里的key值，即修改创建时间
/// </summary>
namespace SUIFW {
    public class HEScriptKeywordReplace : UnityEditor.AssetModificationProcessor {

        #region Public My_Method

        public static void OnWillCreateAsset(string path) {
            path = path.Replace(".meta", "");
            int index = path.LastIndexOf(".");
            string file = path.Substring(index);
            if (file != ".cs" && file != ".js" && file != ".boo") return;
            //string fileExtension = file;

            index = Application.dataPath.LastIndexOf("Assets");
            path = Application.dataPath.Substring(0, index) + path;
            file = System.IO.File.ReadAllText(path);

            file = file.Replace("#CreateTime#", System.DateTime.Now.ToString("d"));
            file = file.Replace("#ProjectName#", "特斯拉项目");
            file = file.Replace("#NAMESPACE#", "Tesla");
            file = file.Replace("#Author#", "WXL");


            System.IO.File.WriteAllText(path, file);
            AssetDatabase.Refresh();
        }
        #endregion

        #region 用工具的方法创建C#脚本，并附加快捷键


        [MenuItem("Assets/Create/MyC# Script #%_U", false, 80)]
        public static void CreatNewLua() {
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
            ScriptableObject.CreateInstance<MyDoCreateScriptAsset>(),
            GetSelectedPathOrFallback() + "/CSharpScript.cs",
            null,
            Application.dataPath + "/Scripts/SUIFW/Editor/MyCharpTemplate.txt");
            //"E:/GameEngine/unity/unity2017.4.3/Editor/Data/Resources/ScriptTemplates/82-C# Script-NewBehaviourScript.cs.txt");
        }

        public static string GetSelectedPathOrFallback() {
            string path = "Assets";
            foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets)) {
                path = AssetDatabase.GetAssetPath(obj);
                if (!string.IsNullOrEmpty(path) && File.Exists(path)) {
                    path = Path.GetDirectoryName(path);
                    break;
                }
            }
            return path;
        }

        #endregion

    }

    class MyDoCreateScriptAsset : EndNameEditAction {
        public override void Action(int instanceId, string pathName, string resourceFile) {
            UnityEngine.Object o = CreateScriptAssetFromTemplate(pathName, resourceFile);
            ProjectWindowUtil.ShowCreatedAsset(o);
        }

        internal static UnityEngine.Object CreateScriptAssetFromTemplate(string pathName, string resourceFile) {
            string fullPath = Path.GetFullPath(pathName);
            StreamReader streamReader = new StreamReader(resourceFile);
            string text = streamReader.ReadToEnd();
            streamReader.Close();
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(pathName);
            text = Regex.Replace(text, "#SCRIPTNAME#", fileNameWithoutExtension);

            bool encoderShouldEmitUTF8Identifier = true;
            bool throwOnInvalidBytes = false;
            UTF8Encoding encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier, throwOnInvalidBytes);
            bool append = false;
            StreamWriter streamWriter = new StreamWriter(fullPath, append, encoding);
            streamWriter.Write(text);
            streamWriter.Close();
            AssetDatabase.ImportAsset(pathName);
            return AssetDatabase.LoadAssetAtPath(pathName, typeof(UnityEngine.Object));
        }
    }
}