using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;

public class IniTools : MonoBehaviour {

    [DllImport("kernel32")]
    private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
    [DllImport("kernel32")]
    private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

    /// <summary>
    /// 读取INI值
    /// </summary>
    /// <param name="section">段</param>
    /// <param name="key">键</param>
    /// <param name="def">异常缺省值</param>
    /// <param name="filePath">路径</param>
    /// <returns></returns>
    public static string ReadFromIni(string section, string key, string def, string filePath)
    {
        StringBuilder retVal = new StringBuilder();
        GetPrivateProfileString(section, key, def, retVal, 500, filePath);
        return retVal.ToString();
    }


    /// <summary>
    /// 值写入INI
    /// </summary>
    /// <param name="section"></param>
    /// <param name="key"></param>
    /// <param name="val"></param>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static bool WriteToINI(string section,string key,string val, string filePath)
    {
        bool result = false;
        try
        {
            if (!File.Exists(filePath))
                File.Create(filePath);
            WritePrivateProfileString(section, key, val, filePath);

        }
        catch
        {
            Debug.Log("写入异常");
            result = false;
        }

        return result;
    }




}
