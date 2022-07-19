using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystemScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject mynpcPrefab;
    [SerializeField]
    private GameObject othernpcPrefab;
    [SerializeField]
    private GameObject otherplayerPrefab;
    [SerializeField]
    private GameObject wallPrefab;
    [SerializeField]
    private GameObject myplayer;

    public static List<GameObject> npc_arr;
    public static List<GameObject> user_arr;

    public static int id;
    private int num_of_player;

    private int move_mode;
    public float timer; 
    public int newtarget;

    void Start()
    {
        npc_arr = new List<GameObject>();
        user_arr = new List<GameObject>();

        num_of_player = 3;
        move_mode = 0;
        timer = 0f;  
       
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 5.0f){
            switch(move_mode){
                case 0:
                    ChangeTargetPos();
                    break;
                case 1:
                    ChangeDirection();
                    break;
            }
            move_mode = Random.Range(0, 2);
            Debug.Log("move_mode : " +  move_mode);
            timer = 0.0f;
        }
    }
    
    
    void ChangeDirection(){
        
        foreach (GameObject npc in npc_arr)
        {
            if (npc == null) return;
            if(npc.name.Split('_')[0] == id +""){
                int dx = Random.Range(-1, 2);
                int dy = Random.Range(-1, 2);
            
                npc.GetComponent<Movement2D>().SetDirection(new Vector3(dx, dy, 0));
            }      
        }
    }

    void ChangeTargetPos(){
        foreach (GameObject npc in npc_arr)
        {
            if (npc == null) return;
            if(npc.name.Split('_')[0] == id + ""){
                npc.GetComponent<Movement2D>().SetTargetPos();
            }
        }
    }

    public void InitGame(int id){
        GameSystemScript.id = id;
        for(int i = 0; i < num_of_player; i++){
            if(i != id){
                GameObject other_player = Instantiate(otherplayerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                other_player.name = "" + i;
                user_arr.Add(other_player);
            }
            else{
                user_arr.Add(myplayer);
            }
            for(int j = 0; j < 40; j++){
                int x = Random.Range(-9, 9);
                int y = Random.Range(-4, 4);
                if(i == id){
                    GameObject npc = Instantiate(mynpcPrefab, new Vector3(x, y, 0), Quaternion.identity);
                    npc.name = "" + id + "_" + j;
                    npc_arr.Add(npc);
                }
                else{
                    GameObject npc = Instantiate(othernpcPrefab, new Vector3(x, y, 0), Quaternion.identity);
                    npc.name = "" + i  + "_" + j;
                    npc_arr.Add(npc);
                }
            }
        }
    }
}
