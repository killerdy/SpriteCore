using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.U2D;
using UnityEngine.U2D.Animation;

public class WeaponManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static WeaponManager instance;
    void Start()
    {
        instance = this;
    }
    public void ChangeWeaponByImage(GameObject weapon, string weaponName)
    {
        string path = "Images/Weapons/" + weaponName;
        Sprite sprite = Resources.Load<Sprite>(path);
        GameObject go = new GameObject(weapon.name);
        go.AddComponent<SpriteRenderer>();
        go.GetComponent<SpriteRenderer>().sprite = sprite;
        go.GetComponent<SpriteRenderer>().sortingLayerName = "ItemFront";
        SpriteSkin newSkin= go.AddComponent<SpriteSkin>();
        SpriteSkin oldSkin = weapon.GetComponent<SpriteSkin>();
        //这也是通过反射机制
        var rootBoneProperty = typeof(SpriteSkin).GetProperty(nameof(SpriteSkin.rootBone));
        rootBoneProperty!.SetValue(newSkin, oldSkin.rootBone, BindingFlags.NonPublic | BindingFlags.Instance, null, null, CultureInfo.InvariantCulture);
        var boneTransformsProperty = typeof(SpriteSkin).GetProperty(nameof(SpriteSkin.boneTransforms));
        boneTransformsProperty!.SetValue(newSkin, oldSkin.boneTransforms, BindingFlags.NonPublic | BindingFlags.Instance, null, null, CultureInfo.InvariantCulture);
        Transform oldTransform = weapon.transform;
        go.transform.position = oldTransform.position;
        go.transform.rotation = oldTransform.rotation;
        go.transform.SetParent(oldTransform.parent);
        go.transform.localScale = oldTransform.localScale;
        Destroy(weapon);

    }
    public void ChangeWeaponByPrefab(GameObject weapon,string weaponName)
    {
        string path = "Prefabs/" + weaponName;
        GameObject go= Instantiate(Resources.Load<GameObject>(path));
        SpriteSkin oldSkin = weapon.GetComponent<SpriteSkin>();
        SpriteSkin newSkin = go.GetComponent<SpriteSkin>();
        //newSkin.rootBone = oldbone;

        //newSkin.boneTransforms = oldSkin.boneTransforms;
        //newSkin.rootBone=oldSkin.rootBone;
        //通过反射来解决
        var rootBoneProperty = typeof(SpriteSkin).GetProperty(nameof(SpriteSkin.rootBone));
        rootBoneProperty!.SetValue(newSkin, oldSkin.rootBone, BindingFlags.NonPublic | BindingFlags.Instance, null, null, CultureInfo.InvariantCulture);
        var boneTransformsProperty = typeof(SpriteSkin).GetProperty(nameof(SpriteSkin.boneTransforms));
        boneTransformsProperty!.SetValue(newSkin, oldSkin.boneTransforms, BindingFlags.NonPublic | BindingFlags.Instance, null, null, CultureInfo.InvariantCulture);
        Transform oldTransform = weapon.transform;
        go.transform.position = oldTransform.position;
        go.transform.rotation = oldTransform.rotation;
        go.transform.SetParent(oldTransform.parent);
        go.transform.localScale = oldTransform.localScale;
        Destroy(weapon);
        //skin.rootBone = old.GetComponent<SpriteSkin>().rootBone
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
