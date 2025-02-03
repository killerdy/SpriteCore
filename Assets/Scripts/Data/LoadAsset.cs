using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadAsset : MonoBehaviour
{


    public string equipConfigName = "EquipConfig";
    public string enemyConfigName = "EnemyConfig";
    public string humanConfigName = "HumanConfig";
    public List<EquipData> equipConfig;
    public List<EnemyData> enemyConfig;
    public List<HumanData> humanConfig;
    public static LoadAsset instance;

    private void Awake()
    {
        instance = this;
        LoadEquip();
        LoadEnemy();
        LoadHuman();
    }

    void Start()
    {
        
    }

    private void LoadEquip()
    {
        EquipConfig config = Resources.Load<EquipConfig>($"Data/{equipConfigName}");
        if (config != null)
        {
            equipConfig = config.list;
            if (equipConfig != null && equipConfig.Count > 0)
            {
                Debug.Log($"EquipConfig loaded successfully: {equipConfig[0].Name}");
            }
            else
            {
                Debug.LogError($"EquipConfig list is null or empty");
            }
        }
        else
        {
            Debug.LogError($"Failed to load EquipConfig with name: {equipConfigName}");
        }
    }

    private void LoadEnemy()
    {
        EnemyConfig config = Resources.Load<EnemyConfig>($"Data/{enemyConfigName}");
        if (config != null)
        {
            enemyConfig = config.list;
            if (enemyConfig != null && enemyConfig.Count > 0)
            {
                Debug.Log($"EnemyConfig loaded successfully: {enemyConfig[0].Name}");
            }
            else
            {
                Debug.LogError($"EnemyConfig list is null or empty");
            }
        }
        else
        {
            Debug.LogError($"Failed to load EnemyConfig with name: {enemyConfigName}");
        }
    }
    private void LoadHuman()
    {
        HumanConfig config = Resources.Load<HumanConfig>($"Data/{humanConfigName}");
        if (config != null)
        {
            humanConfig = config.list;
            if (humanConfig != null && humanConfig.Count > 0)
            {
                Debug.Log($"humanConfig loaded successfully: {humanConfig[0].Name} {humanConfig[2].Name}");
            }
            else
            {
                Debug.LogError($"EnemyConfig list is null or empty");
            }
        }
        else
        {
            Debug.LogError($"Failed to load EnemyConfig with name: {humanConfig}");
        }
    }
    //public string equipConfigAddress = "Assets/Data/EquipConfig.asset";
    //public string enemyConfigAddress = "Assets/Data/EnemyConfig.asset";
    //public string equipConfigAddress = "Data/EquipConfig";
    //public string enemyConfigAddress = "Data/EnemyConfig";
    //public List<EquipData> equipConfig;
    //public List<EnemyData> enemyConfig;
    //public static LoadAsset instance;
    //private void Awake()
    //{
    //    instance = this;
    //}
    //void Start()
    //{
    //    LoadEquip();
    //    LoadEnemy();
    //}
    //void LoadEquip()
    //{
    //    equipConfig = Resources.Load<EquipConfig>(equipConfigAddress).list;
    //}
    //void LoadEnemy()
    //{
    //    enemyConfig = Resources.Load<EnemyConfig>(enemyConfigAddress).list;
    //}
    //async private void LoadEquip()
    //{
    //    AsyncOperationHandle<EquipConfig> handle = Addressables.LoadAssetAsync<EquipConfig>(equipConfigAddress);
    //    await handle.Task;
    //    if(handle.Status==AsyncOperationStatus.Succeeded)
    //    {
    //        equipConfig = handle.Result.list;
    //        Debug.Log(equipConfig[0].Name);
    //        Addressables.Release(handle);
    //    }
    //}
    //async private void LoadEnemy()
    //{
    //    AsyncOperationHandle<EnemyConfig> handle = Addressables.LoadAssetAsync<EnemyConfig>(enemyConfigAddress);
    //    await handle.Task;
    //    if (handle.Status == AsyncOperationStatus.Succeeded)
    //    {
    //        enemyConfig = handle.Result.list;
    //        Debug.Log(enemyConfig[0].Name);
    //        Addressables.Release(handle);
    //    }
    //}
}
