//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;
//using UnityEditor.Build;
//using UnityEditor.Build.Reporting;
//public class PreBuildActions : IPreprocessBuildWithReport
//{
//    public List<GameObject> list = new List<GameObject>();
//    public int callbackOrder
//    {
//        get { return 0; }
//    }

//    public void OnPreprocessBuild(BuildReport report)
//    {

//        Debug.Log("开始预构建...");
//        list.Add(GameObject.Find("Canvas/MainPanel/Tips"));
//        list.Add(GameObject.Find("CanvasSky/Sky"));
//        foreach (GameObject go in list)
//        {
//            if (go != null)
//            {
//                go.SetActive(true);
//            }
//            else
//            {
//                Debug.Log("未找到"+go.name);
//            }
//        }
//        Debug.Log("预处理构建完成");

//    }
//}
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

class MyCustomBuildProcessor : IPreprocessBuildWithReport
{
    public int callbackOrder { get { return 0; } }
    public void OnPreprocessBuild(BuildReport report)
    {
        Debug.Log("MyCustomBuildProcessor.OnPreprocessBuild for target " + report.summary.platform + " at path " + report.summary.outputPath);
    }
}