using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public Camera maincamera;
    public ActItem player;
    public Animator animator;
    public Vector2 targetPosion;
    public HashSet<int> players = new HashSet<int>();   
    public List<int> playerList= new List<int>();
    int playerId = -1;
    //public List<ActItem > 
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        maincamera = Camera.main;
    
        ChangePlayer(GameObject.Find("Girl").GetComponent<ActItem>());
        //NextPlayer();
        targetPosion = player.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        Attack();
        run();
    }
    public void NextPlayer()
    {
        
        playerId=(++playerId)%playerList.Count;
        //Debug.Log(playerId);
        Vector3 oldPosion = player.transform.position;
        player.gameObject.SetActive(false);
        player = ActItemManager.instance.humans[playerList[playerId]].GetComponent<ActItem>();
        player.gameObject.SetActive(true);
        //player.transform.position = targetPosion;
        player.transform.position = oldPosion;
        player.GetComponent<Animator>().Rebind();

        HeadUpManager.instance.ChangePlayer();
        CharacterPanel.instance.ClearEquip();
        CharacterPanel.instance.OnEnable();
        BagPanel.instance.OnEnable();
    }
    public void PrePlayer()
    {
        playerId = (--playerId+ playerList.Count) % playerList.Count;
        Vector3 oldPosion = player.transform.position;
        player.gameObject.SetActive(false);
        player = ActItemManager.instance.humans[playerList[playerId]].GetComponent<ActItem>();
        player.gameObject.SetActive(true);
        //player.transform.position = targetPosion;
        player.transform.position = oldPosion;
        player.GetComponent<Animator>().Rebind();

        HeadUpManager.instance.ChangePlayer();
        CharacterPanel.instance.ClearEquip();
        CharacterPanel.instance.OnEnable();
        BagPanel.instance.OnEnable();
    }
    public void UpdatePlayer()
    {
        
    }
    public void UpdateEquipment()
    {
        
    }
    public void ChangePlayer(ActItem go)
    {
        player = go;
        animator = go.GetComponent<Animator>();

        StartCoroutine(Tool.instance.DelayedAction(() =>
        {
            HeadUpManager.instance.ChangePlayer();
        }));
        
    }
    public bool Attack()
    {
        if (Input.GetKeyUp(KeyCode.A)){

            player.AttackA();
            return true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {

            NextPlayer();
            Vector3 pos = CameraMove.instance.transform.position;
            CameraMove.instance.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, pos.z);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            player.ReadyE(); 
            //return true;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            player.AttackE();
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            player.AttackW();
        }
        if (Input.GetKeyDown(KeyCode.F)) {
            player.Aim();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            //WeaponManager.instance.ChangeWeaponByPrefab(player.transform.GetChild(0).Find("Weapon").gameObject,"BangBang");
            //WeaponManager.instance.ChangeWeaponByImage(player.transform.GetChild(0).Find("Weapon").gameObject, "BangBang");
            WeaponManager.instance.ChangeWeaponByImage(player.transform.GetChild(0).Find("Weapon").gameObject, DataManager.instance.GetNextWeapon());
        }
            
        return false;
    }
    public void run()
    {
        
        Vector2 moveDirection = (targetPosion - (Vector2)player.transform.position);
        float distance = moveDirection.magnitude;
        if (distance > 0.1f)
        {
            player.rb.velocity = moveDirection.normalized * player.maxSpeed;

        }
        else player.rb.velocity = Vector2.zero;
    }
}
