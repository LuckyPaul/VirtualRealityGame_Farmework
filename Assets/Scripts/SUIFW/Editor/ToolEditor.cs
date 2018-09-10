using System.IO;
using UnityEditor;
using UnityEngine;

namespace SUIFW {
    public class ToolEditor : MonoBehaviour {
        #region 标签     
        [MenuItem("MyTool/给子物体添加标签(和选中的物体一样)")]
        static void AddTagForChild() {

            Transform[] trans = Selection.GetTransforms(SelectionMode.DeepAssets);
            for (int i = 0; i < trans.Length; i++) {
                //    trans[i].gameObject.AddComponent<MeshCollider> ();
                AddTag(trans[i]);
            }
        }

        private static void AddTag(Transform tf) {
            string _tag = tf.tag;
            for (int i = 0; i < tf.childCount; i++) {

                tf.GetChild(i).tag = _tag;
                AddTag(tf.GetChild(i));
            }
        }
        #endregion


        #region 碰撞器      
        [MenuItem("MyTool/添加碰撞器/添加BoxCollider(不包括子物体)")]
        static void AddBoxColliderNotChild() {

            Transform[] trans = Selection.GetTransforms(SelectionMode.DeepAssets);
            for (int i = 0; i < trans.Length; i++) {
                if (trans[i].GetComponent<BoxCollider>() == null) {
                    trans[i].gameObject.AddComponent<BoxCollider>();
                }
            }
        }


        [MenuItem("MyTool/添加碰撞器/添加BoxCollider(包括子物体)")]
        static void AddBoxCollider() {

            Transform[] trans = Selection.GetTransforms(SelectionMode.DeepAssets);
            for (int i = 0; i < trans.Length; i++) {
                //    trans[i].gameObject.AddComponent<MeshCollider> ();
                AddBC(trans[i]);
            }
        }
        private static void AddBC(Transform tf) {
            BoxCollider _bc = tf.GetComponent<BoxCollider>();
            if (_bc == null) {
                tf.gameObject.AddComponent<BoxCollider>();
            }
            for (int i = 0; i < tf.childCount; i++) {
                BoxCollider bc = tf.GetChild(i).GetComponent<BoxCollider>();
                if (bc == null) {
                    tf.GetChild(i).gameObject.AddComponent<BoxCollider>();
                }
                AddBC(tf.GetChild(i));
            }
        }


        [MenuItem("MyTool/添加碰撞器/添加meshcollider(不包括子物体)")]
        static void AddMeshColliderNotChild() {
            Transform[] trans = Selection.GetTransforms(SelectionMode.DeepAssets);
            for (int i = 0; i < trans.Length; i++) {
                if (trans[i].GetComponent<MeshCollider>() == null) {
                    trans[i].gameObject.AddComponent<MeshCollider>();
                }
            }
        }
        [MenuItem("MyTool/添加碰撞器/添加meshcollider(包括子物体)")]
        static void AddMeshCollider() {

            Transform[] trans = Selection.GetTransforms(SelectionMode.DeepAssets);
            for (int i = 0; i < trans.Length; i++) {
                //    trans[i].gameObject.AddComponent<MeshCollider> ();
                AddMC(trans[i]);
            }
        }
        private static void AddMC(Transform tf) {
            MeshCollider _c = tf.GetComponent<MeshCollider>();
            if (_c == null) {
                tf.gameObject.AddComponent<MeshCollider>();
            }
            if (tf.childCount <= 0) return;
            for (int i = 0; i < tf.childCount; i++) {
                MeshCollider meshCollider = tf.GetChild(i).GetComponent<MeshCollider>();
                if (meshCollider == null) {
                    tf.GetChild(i).gameObject.AddComponent<MeshCollider>();
                }
                AddMC(tf.GetChild(i));
            }
        }
        [MenuItem("MyTool/删除碰撞器/删除BoxCollider(不包括子物体)")]
        static void ClearBoxColiderNotChild() {

            foreach (Transform t in Selection.transforms) {
                BoxCollider _boxCollider = t.GetComponent<BoxCollider>();
                if (_boxCollider) DestroyImmediate(_boxCollider);
            }
        }


        [MenuItem("MyTool/删除碰撞器/删除BoxCollider(包括子物体)")]
        static void ClearBoxColider() {
            // Debug.Log(Selection.activeObject);

            foreach (Transform t in Selection.transforms) {
                DeleteBoxCollider(t);
            }
        }
        private static void DeleteBoxCollider(Transform tf) {
            BoxCollider _bc = tf.GetComponent<BoxCollider>();
            if (_bc)
                DestroyImmediate(_bc);
            if (tf.childCount <= 0) return;
            for (int i = 0; i < tf.childCount; i++) {
                _bc = tf.GetChild(i).GetComponent<BoxCollider>();
                if (_bc) {
                    DestroyImmediate(_bc);
                }
                DeleteBoxCollider(tf.GetChild(i));
            }
        }

        [MenuItem("MyTool/删除碰撞器/删除MeshCollider(不包括子物体)")]
        static void ClearMeshColiderNotChild() {

            foreach (Transform t in Selection.transforms) {
                MeshCollider _meshCollider = t.GetComponent<MeshCollider>();
                if (_meshCollider) DestroyImmediate(_meshCollider);
            }
        }


        [MenuItem("MyTool/删除碰撞器/删除MeshCollider(包括子物体)")]
        static void ClearMeshColider() {
            foreach (Transform t in Selection.transforms) {
                DeleteMeshCollider(t);
            }
        }
        private static void DeleteMeshCollider(Transform tf) {
            MeshCollider _bc = tf.GetComponent<MeshCollider>();
            if (_bc)
                DestroyImmediate(_bc);
            if (tf.childCount <= 0) return;
            for (int i = 0; i < tf.childCount; i++) {
                _bc = tf.GetChild(i).GetComponent<MeshCollider>();
                if (_bc) {
                    DestroyImmediate(_bc);
                }
                DeleteMeshCollider(tf.GetChild(i));
            }
        }



        #endregion

        #region SetFileBundleName
        [MenuItem("MyTool/SetFileBundleName")]
        static void SetBundleName() {

            #region 设置资源的AssetBundle的名称和文件扩展名
            UnityEngine.Object[] selects = Selection.objects;
            foreach (UnityEngine.Object selected in selects) {
                string path = AssetDatabase.GetAssetPath(selected);
                AssetImporter asset = AssetImporter.GetAtPath(path);
                asset.assetBundleName = selected.name; //设置Bundle文件的名称
                asset.assetBundleVariant = "unity3d";//设置Bundle文件的扩展名
                asset.SaveAndReimport();

            }
            AssetDatabase.Refresh();
            #endregion
        }
        #endregion

        [MenuItem("MyTool/得到物体的位置和旋转")]
        public static void GetPos() {
            Transform[] trans = Selection.GetTransforms(SelectionMode.DeepAssets);
            string path = Application.dataPath + "/Script/wangxiaolong/pos.text";
            for (int i = 0; i < trans.Length; i++) {
                using (StreamWriter sw = new StreamWriter(path, true)) {
                    string str;
                    str = trans[i].name;
                    sw.WriteLine(str);
                    Vector3 v = trans[i].localPosition;
                    str = "Position:" + "new Vector3" + "(" + v.x.ToString() + "f" + "," + v.y.ToString() + "f" + "," + v.z.ToString() + "f" + ")";
                    sw.WriteLine(str);
                    Vector3 rotate = trans[i].localEulerAngles;
                    str = "EulerAngles:" + "Quaternion.Euler(new Vector3" + "(" + rotate.x.ToString() + "f" + "," + rotate.y.ToString() + "f" + "," + rotate.z.ToString() + "f" + "))";
                    sw.WriteLine(str);

                }
            }
        }


        #region 锚点适配
        [MenuItem("MyTool/FitToSelf")]
        static void FitToSelf() {
            foreach (Transform t in Selection.transforms) {
                RectTransform rct = t.GetComponent<RectTransform>();
                if (rct == null) return;
                RectTransform parentRct = t.parent.GetComponent<RectTransform>();

                rct.anchorMin = new Vector2(rct.offsetMin.x / parentRct.rect.width + rct.anchorMin.x, rct.offsetMin.y / parentRct.rect.height + rct.anchorMin.y);
                rct.offsetMin = new Vector2(0, 0);

                rct.anchorMax = new Vector2(rct.rect.width / parentRct.rect.width + rct.anchorMin.x, rct.rect.height / parentRct.rect.height + rct.anchorMin.y);
                rct.offsetMax = new Vector2(0, 0);

            }
        }

        [MenuItem("MyTool/FitToAnchor")]
        static void FitToAnchor() {
            foreach (Transform t in Selection.transforms) {
                RectTransform rct = t.GetComponent<RectTransform>();
                if (rct == null) return;
                // RectTransform parentRct = t.parent.GetComponent<RectTransform>();

                // rct.anchorMin = new Vector2(rct.offsetMin.x / parentRct.rect.width + rct.anchorMin.x, rct.offsetMin.y / parentRct.rect.height + rct.anchorMin.y);
                rct.offsetMin = new Vector2(0, 0);

                //  rct.anchorMax = new Vector2(rct.rect.width / parentRct.rect.width + rct.anchorMin.x, rct.rect.height / parentRct.rect.height + rct.anchorMin.y);
                rct.offsetMax = new Vector2(0, 0);

            }
        }

        #endregion

        #region 删除animator
        [MenuItem("MyTool/删除animator（包括子物体）")]
        static void DeleteAnimator() {
            Transform[] obj = Selection.GetTransforms(SelectionMode.DeepAssets);
            for (int i = 0; i < obj.Length; i++) {
                DeleteAnimator(obj[i]);
            }
        }
        private static void DeleteAnimator(Transform trans) {
            Animator _animator = trans.GetComponent<Animator>();
            if (_animator != null) DestroyImmediate(_animator);
            if (trans.childCount > 0) {
                for (int i = 0; i < trans.childCount; i++) {
                    DeleteAnimator(trans.GetChild(i));
                    if (trans.GetChild(i).childCount > 0)
                        DeleteAnimator(trans.GetChild(i));
                }
            }
        }

        /* 这里需要做一个误点击处理
        [MenuItem("ToolEditor/删除场景没用的MeshCollider和Animation")]
        static public void Remove()
        {
            //获取当前场景里的所有游戏对象
            GameObject[] rootObjects = (GameObject[])UnityEngine.Object.FindObjectsOfType(typeof(GameObject));
            //遍历游戏对象
            foreach (GameObject go in rootObjects)
            {
                //如果发现Render的shader是Diffuse并且颜色是白色，那么将它的shader修改成Mobile/Diffuse
                if (go != null && go.transform.parent != null)
                {
                    Renderer render = go.GetComponent<Renderer>();
                    if (render != null && render.sharedMaterial != null && render.sharedMaterial.shader.name == "Diffuse" && render.sharedMaterial.color == Color.white)
                    {
                        render.sharedMaterial.shader = Shader.Find("Mobile/Diffuse");
                    }
                }

                //删除所有的MeshCollider
                foreach (MeshCollider collider in UnityEngine.Object.FindObjectsOfType(typeof(MeshCollider)))
                {
                    DestroyImmediate(collider);
                }

                //删除没有用的动画组件
                foreach (Animation animation in UnityEngine.Object.FindObjectsOfType(typeof(Animation)))
                {
                    if (animation.clip == null)
                        DestroyImmediate(animation);
                }

                //应该没有人用Animator吧？ 避免美术弄错我都全部删除了。
                foreach (Animator animator in UnityEngine.Object.FindObjectsOfType(typeof(Animator)))
                {
                    DestroyImmediate(animator);
                }
            }
            //保存
            AssetDatabase.SaveAssets();
        }
        */

        #endregion


        #region 一键创建一些基本的必须的文件

        [MenuItem("MyTool/CreateBasicFolder #&_b")]
        private static void CreateBasicFolder() {
            //    GenerateFolder();
            Debug.Log("Folders Created");
        }

        [MenuItem("MyTool/CreateALLFolder")]
        private static void CreateAllFolder() {
            //    GenerateFolder(1);
            Debug.Log("Folders Created");
        }


        private static void GenerateFolder(int flag = 0) {
            // 文件路径
            string prjPath = Application.dataPath + "/";
            if (!File.Exists(prjPath + "Audio"))
                Directory.CreateDirectory(prjPath + "Audio");
            if (!File.Exists(prjPath + "Prefabs"))
                Directory.CreateDirectory(prjPath + "Prefabs");
            if (!File.Exists(prjPath + "Materials"))
                Directory.CreateDirectory(prjPath + "Materials");
            if (!File.Exists(prjPath + "Resources"))
                Directory.CreateDirectory(prjPath + "Resources");
            if (!File.Exists(prjPath + "Scripts"))
                Directory.CreateDirectory(prjPath + "Scripts");
            if (!File.Exists(prjPath + "Textures"))
                Directory.CreateDirectory(prjPath + "Textures");
            if (!File.Exists(prjPath + "Scenes"))
                Directory.CreateDirectory(prjPath + "Scenes");

            if (1 == flag) {
                if (!File.Exists(prjPath + "Meshes"))
                    Directory.CreateDirectory(prjPath + "Meshes");
                if (!File.Exists(prjPath + "Shaders"))
                    Directory.CreateDirectory(prjPath + "Shaders");
                if (!File.Exists(prjPath + "GUI"))
                    Directory.CreateDirectory(prjPath + "GUI");
            }


            AssetDatabase.Refresh();
        }

        //创建文件
        [MenuItem("MyTool/CreateMyFolder #%_T")]

        public static void CreaeFileCurSeletion() {
            var select = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets);
            foreach (var item in select) {
                string path = AssetDatabase.GetAssetPath(item);
                Directory.CreateDirectory(path + "/MyFile");
            }
            AssetDatabase.Refresh();
        }
        #endregion


    }
}