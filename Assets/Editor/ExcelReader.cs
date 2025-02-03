using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using OfficeOpenXml;
using UnityEditor;
using UnityEditor.AddressableAssets.HostingServices;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets;
using Unity.VisualScripting;

[InitializeOnLoad]
public class ExcelReader
{
    //static string excelName = "enemy.xlsx";
    static List<string> excelName = new List<string>();
    //static List<string> assetPath=new List<string>();
    static List<string> assetName = new List<string>();
    static string assetPath = "Assets/Resources/Data";
    //static string assetName = "EnemyConfig";
    static ExcelReader()
    {

        excelName.Add("enemy.xlsx");
        excelName.Add("equip.xlsx");
        excelName.Add("human.xlsx");
        assetName.Add("EnemyConfig");
        assetName.Add("EquipConfig");
        assetName.Add("HumanConfig");
        
        LoadEnemyData(0);
        LoadEquipData(1);
        LoadHumanData(2);
    }
    static void LoadHumanData(int id)
    {

        string filePath = Path.Combine(Application.streamingAssetsPath, excelName[id]);
        if (File.Exists(filePath))
        {
            FileInfo fileInfo = new FileInfo(filePath);
            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int row = worksheet.Dimension.Rows;
                int col = worksheet.Dimension.Columns;
                if (row < 2)
                {
                    Debug.LogWarning("Excel file has no data ");
                    return;
                }
                HumanConfig config = ScriptableObject.CreateInstance<HumanConfig>();
                for (int i = 2; i <= row; i++)
                {
                    config.list.Add(new HumanData
                    {
                        ID = ParseInt(worksheet.Cells[i, 1].Value?.ToString()),
                        Name = worksheet.Cells[i, 2].Value?.ToString(),
                        Level = ParseInt(worksheet.Cells[i, 3].Value?.ToString()),
                        HPMax = ParseInt(worksheet.Cells[i, 5].Value?.ToString()),
                        HP = ParseInt(worksheet.Cells[i, 6].Value?.ToString()),
                        MPMax = ParseInt(worksheet.Cells[i, 7].Value?.ToString()),
                        MP = ParseInt(worksheet.Cells[i, 8].Value?.ToString()),
                        Str = ParseInt(worksheet.Cells[i, 9].Value?.ToString()),
                        Mag = ParseInt(worksheet.Cells[i, 10].Value?.ToString()),
                        Def = ParseInt(worksheet.Cells[i, 11].Value?.ToString()),
                        Cir = ParseFloat(worksheet.Cells[i, 12].Value?.ToString()),
                        CirEEfect = ParseFloat(worksheet.Cells[i, 13].Value?.ToString()),
                        ExpGen = ParseInt(worksheet.Cells[i, 14].Value?.ToString()),
                        ExpMax = ParseInt(worksheet.Cells[i, 15].Value?.ToString()),
                        MaxSpeed = ParseInt(worksheet.Cells[i, 16].Value?.ToString()),
                        PrefabPath = worksheet.Cells[i, 20].Value?.ToString(),
                    });
                }
                if (!Directory.Exists(assetPath))
                {
                    Directory.CreateDirectory(assetPath);
                }
                string assetFullPath = $"{assetPath}/{assetName[id]}.asset";
                AssetDatabase.CreateAsset(config, assetFullPath);


                AssetDatabase.SaveAssets();
                //MakeAddressable(assetFullPath);
            }
        }
        else
        {
            Debug.LogError("enemyconfig not found");
        }
    }
    static void LoadEnemyData(int id)
    {

        string filePath = Path.Combine(Application.streamingAssetsPath, excelName[id]);   
        if(File.Exists(filePath) )
        {
           FileInfo fileInfo = new FileInfo(filePath);
            using (ExcelPackage package=new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int row = worksheet.Dimension.Rows;
                int col = worksheet.Dimension.Columns;
                if (row < 2)
                {
                    Debug.LogWarning("Excel file has no data ");
                    return;
                }
                EnemyConfig config = ScriptableObject.CreateInstance<EnemyConfig>();
                for(int i = 2; i <= row; i++) {
                    config.list.Add(new EnemyData
                    {
                        ID = ParseInt(worksheet.Cells[i, 1].Value?.ToString()),
                        Name = worksheet.Cells[i, 2].Value?.ToString(),
                        Level = ParseInt(worksheet.Cells[i, 3].Value?.ToString()),
                        HPMax = ParseInt(worksheet.Cells[i, 5].Value?.ToString()),
                        HP = ParseInt(worksheet.Cells[i, 6].Value?.ToString()),
                        MPMax = ParseInt(worksheet.Cells[i, 7].Value?.ToString()),
                        MP = ParseInt(worksheet.Cells[i, 8].Value?.ToString()),
                        Str = ParseInt(worksheet.Cells[i, 9].Value?.ToString()),
                        Mag = ParseInt(worksheet.Cells[i, 10].Value?.ToString()),
                        Def = ParseInt(worksheet.Cells[i, 11].Value?.ToString()),
                        Cir = ParseFloat(worksheet.Cells[i, 12].Value?.ToString()),
                        CirEEfect = ParseFloat(worksheet.Cells[i, 13].Value?.ToString()),
                        ExpGen = ParseInt(worksheet.Cells[i, 14].Value?.ToString()),
                        ExpMax = ParseInt(worksheet.Cells[i, 15].Value?.ToString()),
                        MaxSpeed = ParseInt(worksheet.Cells[i, 16].Value?.ToString()),
                        PrefabPath = worksheet.Cells[i, 20].Value?.ToString(),
                    });
                }
                if (!Directory.Exists(assetPath))
                {
                    Directory.CreateDirectory(assetPath);
                }
                string assetFullPath = $"{assetPath}/{assetName[id]}.asset";
                AssetDatabase.CreateAsset(config, assetFullPath);


                AssetDatabase.SaveAssets();
                //MakeAddressable(assetFullPath);
            }
        }
        else
        {
            Debug.LogError("enemyconfig not found");
        }
    }
    static void LoadEquipData(int id)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, excelName[id]);
        if (File.Exists(filePath))
        {

            FileInfo fileInfo = new FileInfo(filePath);
            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int row = worksheet.Dimension.Rows;
                int col = worksheet.Dimension.Columns;
                if (row < 2)
                {
                    Debug.LogWarning("Excel file has no data ");
                    return;
                }
                EquipConfig config = ScriptableObject.CreateInstance<EquipConfig>();
                for (int i = 2; i <= row; i++)
                {
                    config.list.Add(new EquipData
                    {
                        ID = ParseInt(worksheet.Cells[i, 1].Value?.ToString()),
                        Name = worksheet.Cells[i, 2].Value?.ToString(),
                        Level = ParseInt(worksheet.Cells[i, 3].Value?.ToString()),
                        HPMax = ParseInt(worksheet.Cells[i, 5].Value?.ToString()),
                        HP = ParseInt(worksheet.Cells[i, 6].Value?.ToString()),
                        MPMax = ParseInt(worksheet.Cells[i, 7].Value?.ToString()),
                        MP = ParseInt(worksheet.Cells[i, 8].Value?.ToString()),
                        Str = ParseInt(worksheet.Cells[i, 9].Value?.ToString()),
                        Mag = ParseInt(worksheet.Cells[i, 10].Value?.ToString()),
                        Def = ParseInt(worksheet.Cells[i, 11].Value?.ToString()),
                        Cir = ParseFloat(worksheet.Cells[i, 12].Value?.ToString()),
                        CirEEfect = ParseFloat(worksheet.Cells[i, 13].Value?.ToString()),
                        ExpGen = ParseInt(worksheet.Cells[i, 14].Value?.ToString()),
                        ExpMax = ParseInt(worksheet.Cells[i, 15].Value?.ToString()),
                        MaxSpeed = ParseInt(worksheet.Cells[i, 16].Value?.ToString()),
                        PrefabPath = worksheet.Cells[i, 20].Value?.ToString(),
                        ImagePath = worksheet.Cells[i, 21].Value?.ToString(),
                        Description = worksheet.Cells[i, 22].Value?.ToString(), 
                    });
                }
                Debug.Log(config.list);
                if (!Directory.Exists(assetPath))
                {
                    Directory.CreateDirectory(assetPath);
                }
                string assetFullPath = $"{assetPath}/{assetName[id]}.asset";
                AssetDatabase.CreateAsset(config, assetFullPath);
                Debug.Log(assetFullPath);
                AssetDatabase.SaveAssets();
                //MakeAddressable(assetFullPath);
            }
        }
        else
        {
            Debug.LogError("equipconfig not found");
        }
    }
    //static void MakeAddressable(string assetPath)
    //{
    //    AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;
    //    AddressableAssetGroup group=settings.FindGroup("Default");
    //    if(group == null)
    //    {
    //        group = settings.CreateGroup("Default", false, false, false, new List<AddressableAssetGroupSchema>());
    //    }

    //    string guid=AssetDatabase.AssetPathToGUID(assetPath);
    //    AddressableAssetEntry entry = settings.CreateOrMoveEntry(guid, group, false, false);
    //    if (entry != null)
    //    {
    //        Debug.Log("Addressable entry created");
    //    }
    //    else
    //    {
    //        Debug.Log("Fail to created");
    //    }
    //}
    static int ParseInt(string str)
    {
        if (str == null) return 0;
        return int.Parse(str);
    }
    static float ParseFloat(string str)
    {
        if (str == null) return 0f;
        return float.Parse(str);
    }
}