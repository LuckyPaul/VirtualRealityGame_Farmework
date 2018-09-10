using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class JT_Build  {
	enum BuilderTager{BuilHX_Android,BuilHX_IOS,BuilDTGL_Android,BuilDTGL_IOS,BuildPC_Tools};
	static float ver = 5.45f;
    static int versionCode = 1;

	static void  init_ver(string _ver)
	{
		ver = PlayerPrefs.GetFloat(_ver,ver);
        versionCode = PlayerPrefs.GetInt(_ver + "versionCode", versionCode);
        ver = 5.49f;
        versionCode = 4 ;
        PlayerPrefs.SetFloat(_ver,ver);
        PlayerPrefs.SetInt(_ver + "versionCode", versionCode);
        //WriteINI("versionCode", versionCode, Utils.GetStreamingFilePath("VersionCode.txt"));
    }

    public static void WriteINI(string key, int value, string path) {
        //INIParser ini = new INIParser();
        //ini.Open(path);
        //ini.WriteValue(key, key, value);

        //ini.Close();
    }

    [MenuItem ("JT/Build Android  for  互享")]
	static void BuilHX_Android ()
	{  	
		init_ver("BuilHX");
		BulidTarget(BuilderTager.BuilHX_Android,BuildTarget.Android);
	}
	
	[MenuItem ("JT/Build IOS  for  互享")]
	static void BuilHX_IOS ()
	{  
		init_ver("BuilHX");
		BulidTarget(BuilderTager.BuilHX_IOS,BuildTarget.iOS);
	}

    [MenuItem("JT/Build Android  for  地铁概论 正式版")]
    static void BuilDTGL_Android_Final() {
        init_ver("BuilDTGL");
        BulidTarget(BuilderTager.BuilDTGL_Android, BuildTarget.Android);
    }

    [MenuItem ("JT/Build Android  for  地铁概论 测试版")]
	static void BuilDTGL_Android_Beta ()
	{  
		//init_ver("BuilDTGL");
		BulidTarget(BuilderTager.BuilDTGL_Android,BuildTarget.Android);
	}
	
	[MenuItem ("JT/Build IOS  for  地铁概论")]
	static void BuilDTGL_IOS ()
	{  
		init_ver("BuilDTGL");
		BulidTarget(BuilderTager.BuilDTGL_IOS,BuildTarget.iOS);
	}
	
	[MenuItem ("JT/Build 管理工具")]
	static void BuilDPC_TOOLS ()
	{  
		init_ver("BuilDPC");
		BulidTarget(BuilderTager.BuildPC_Tools);
	}
	
	static void BulidTarget(BuilderTager _BuilderTager,BuildTarget buildTarget = BuildTarget.StandaloneWindows)
	{
		string applicationPath = string.Format("{0}/Setup/",Application.dataPath.Replace("/Assets",""));
		string iconPath = "Assets/Pic/";
		string target_name = "";
		BuildTargetGroup targetGroup = BuildTargetGroup.Standalone;
		string target_dir = "";
		
		if(buildTarget == BuildTarget.StandaloneWindows)
		{
			target_name = _BuilderTager.ToString() + ".exe";
			targetGroup = BuildTargetGroup.Standalone;
			target_dir = applicationPath+"PC";
		}

		
		
		if(buildTarget == BuildTarget.Android)
		{
			target_dir = applicationPath + "Android";
			target_name = _BuilderTager.ToString() + ".apk";
			targetGroup = BuildTargetGroup.Android;
		}
		
		if(buildTarget == BuildTarget.iOS)
		{
			target_dir = applicationPath + "IOS";
			target_name = _BuilderTager.ToString();
			targetGroup = BuildTargetGroup.iOS;
			buildTarget = BuildTarget.iOS;
		}
		
		if (target_name.Equals("")  ||  target_dir.Equals(""))
		{
			Debug.LogError("target_name  is  null  or   target_dir  is  null");
			return;
		}
		
		if(Directory.Exists(target_dir))
		{
			if (File.Exists(target_name))
			{
				File.Delete(target_name);
			}
		}else
		{
			Directory.CreateDirectory(target_dir);
		}
		
		
		string oldtargetGroup = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup);
		foreach (BuilderTager item in Enum.GetValues(typeof(BuilderTager)))
			oldtargetGroup = oldtargetGroup.Replace(string.Format("{0};",item.ToString()),"");
		

		switch(_BuilderTager)
		{
		case BuilderTager.BuilHX_Android://C+S
			iconPath = iconPath + "HX_Android";  
			PlayerSettings.applicationIdentifier = "com.jiteng.shapan";
			PlayerSettings.bundleVersion = ver.ToString("F2");

            PlayerSettings.productName = "互享";
			
			PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup,string.Format("{0};{1}",_BuilderTager.ToString(),oldtargetGroup));  
			PlayerSettings.runInBackground = true;
			break;
			
		case BuilderTager.BuilHX_IOS://C+S
			iconPath = iconPath + "HX_IOS";  
			PlayerSettings.applicationIdentifier = "com.jiteng.shapan";
			PlayerSettings.bundleVersion = ver.ToString("F2");
			PlayerSettings.productName = "互享";
			PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup,string.Format("{0};{1}",_BuilderTager.ToString(),oldtargetGroup));      
			PlayerSettings.runInBackground = true;
			PlayerSettings.forceSingleInstance = true;

            break;
			
		case BuilderTager.BuilDTGL_Android://C
			iconPath = iconPath + "DTGL_Android";  
			PlayerSettings.applicationIdentifier = "com.woling.CourseTrain";//Train
            PlayerSettings.bundleVersion = ver.ToString("F2");
			PlayerSettings.productName = "地铁概论";
			PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup,string.Format("{0};{1}",_BuilderTager.ToString(),oldtargetGroup));    
			PlayerSettings.runInBackground = true;
			PlayerSettings.forceSingleInstance = true;
            PlayerSettings.Android.bundleVersionCode = versionCode;
            break;

            case BuilderTager.BuilDTGL_IOS://C+S
			iconPath = iconPath + "DTGL_IOS";   
			PlayerSettings.applicationIdentifier = "com.woling.CourseTrain";
			PlayerSettings.bundleVersion = ver.ToString("F2");
			PlayerSettings.productName = "地铁概论";
			PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup,string.Format("{0};{1}",_BuilderTager.ToString(),oldtargetGroup));   
			PlayerSettings.runInBackground = true;
			PlayerSettings.forceSingleInstance = true;
            PlayerSettings.Android.bundleVersionCode = versionCode;
            break;
			
		case BuilderTager.BuildPC_Tools:
			iconPath = iconPath + "PCTOOLS";
			PlayerSettings.applicationIdentifier = "com.woling.CourseTrain";
			PlayerSettings.bundleVersion = ver.ToString("F2");
			PlayerSettings.productName = "管理工具";
			PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup,string.Format("{0};{1}",_BuilderTager.ToString(),oldtargetGroup));   
			PlayerSettings.runInBackground = true;
			PlayerSettings.forceSingleInstance = true;
			break;
		}
		Texture2D[] icon = PlayerSettings.GetIconsForTargetGroup(targetGroup);
		for(int i=0;i<icon.Length;i++)
		{  
			icon[i] = (Texture2D)AssetDatabase.LoadAssetAtPath(iconPath +  "/" +i +".png",typeof(Texture2D));  
		}
		PlayerSettings.SetIconsForTargetGroup(targetGroup,icon);  
		GenericBuild(FindEnabledEditorScenes(_BuilderTager), target_dir + "/" + target_name, buildTarget,BuildOptions.None,targetGroup,oldtargetGroup);
	}

    static string[] FindEnabledEditorScenes(BuilderTager _BuilderTager)
    {
	    List<string> EditorScenes = new List<string>();
	    switch(_BuilderTager)
	    {
	    case BuilderTager.BuilHX_Android:
	    case BuilderTager.BuilHX_IOS:
	    case BuilderTager.BuilDTGL_Android:
	    case BuilderTager.BuilDTGL_IOS:
		    EditorScenes.Add("Assets/Scenes/book/welcome.unity");
		    EditorScenes.Add("Assets/Scenes/book/ShowVideo.unity");
		    EditorScenes.Add("Assets/Scenes/book/video.unity");
		    EditorScenes.Add("Assets/Scenes/book/main.unity");
		    EditorScenes.Add("Assets/Scenes/book/ShowHtml.unity");
		    EditorScenes.Add("Assets/Scenes/book/directory.unity");
		    EditorScenes.Add("Assets/Scenes/book/directory_video.unity");
		    
            break;
		    
	    case BuilderTager.BuildPC_Tools:
		    EditorScenes.Add("Assets/Scenes/book/welcomeTools.unity");
		    EditorScenes.Add("Assets/Scenes/book/tools.unity");
		    break;
	    }
        return EditorScenes.ToArray();
    }
	
	static void GenericBuild(string[] scenes, string target_dir, BuildTarget build_target, BuildOptions build_options,BuildTargetGroup targetGroup,string oldtargetGroup )
	{  
		Debug.Log(target_dir);
		EditorUserBuildSettings.SwitchActiveBuildTarget(build_target);
		string res = BuildPipeline.BuildPlayer(scenes,target_dir,build_target,build_options);
		
		PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup,oldtargetGroup);
		if (res.Length > 0) {
			throw new Exception("BuildPlayer failure: " + res);
		}
	}
}
