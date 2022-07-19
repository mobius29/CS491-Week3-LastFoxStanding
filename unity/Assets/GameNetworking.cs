using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeWebSocket;
using System.Text;
using UnityEngine.SceneManagement;

public class GameNetworking : MonoBehaviour
{
    [SerializeField]
    private GameObject myplayer;

    public static WebSocket websocket;
    private int id;
    private int user_cnt = 3;
    // Start is called before the first frame update
    async void Start()
    {
        websocket = new WebSocket("ws://192.249.18.176:443");

        websocket.OnOpen += () =>
        {
        Debug.Log("Connection open!");
        };

        websocket.OnError += (e) =>
        {
        Debug.Log("Error! " + e);
        };

        websocket.OnClose += (e) =>
        {
        Debug.Log("Connection closed!");
        };

        websocket.OnMessage += (bytes) =>
        {
            var byteStr = System.Text.Encoding.UTF8.GetString(bytes);

            //Debug.Log("byteStr : " + byteStr);
            

            var event_name = byteStr.Split('!')[0];
            var message = byteStr.Split('!')[1];
            
            
            switch(event_name){
                case "id_set":
                    id = int.Parse(message);
                    Debug.Log("ID_SET : " + id);
                    myplayer.name = id+"";
                    GetComponent<GameSystemScript>().InitGame(id);
                    break;
                case "count_down":
                    Debug.Log("COUNT_DOWN");
                    InvokeRepeating("SendUnitPosition", 3.0f, 0.01f);
                    break;
                case "all_position":
                    List<GameObject> npc_arr = GameSystemScript.npc_arr;
                    List<GameObject> user_arr = GameSystemScript.user_arr;
                    string[] pos_arr_all = message.Split('/');
                    foreach(string pos_arr_str in pos_arr_all){
                        int tag = int.Parse(pos_arr_str.Split(',')[0]);
                        string[] pos_arr = pos_arr_str.Split(';');
                        if(tag != id){
                            float user_x = float.Parse(pos_arr[0].Split(',')[1]);
                            float user_y = float.Parse(pos_arr[0].Split(',')[2]);
                            if (user_arr[tag] != null){
                                user_arr[tag].transform.position = new Vector3(user_x, user_y, 0);
                            }
                            for(int i = 0; i < 40; i++){
                                float x = float.Parse(pos_arr[i+1].Split(',')[1]);
                                float y = float.Parse(pos_arr[i+1].Split(',')[2]);
                                if (npc_arr[tag*40 + i] != null){
                                    npc_arr[tag*40 + i].transform.position = new Vector3(x, y, 0);
                                }
                            }
                        }
                    }
                    break;
                case "die":
                    Debug.Log("die came");
                    List<GameObject> npc_arr1 = GameSystemScript.npc_arr;
                    List<GameObject> user_arr1 = GameSystemScript.user_arr;
                    if (message.Contains("_")){
                        for (int i = 0; i< npc_arr1.Count; i++){
                            if (npc_arr1[i] != null){
                                if (npc_arr1[i].name == message){
                                    Destroy(npc_arr1[i]);
                                    npc_arr1[i] = null;
                                }
                            } 
                        }
                    }
                    else {
                        if (id == int.Parse(message)) {
                             for (int i = 0; i < user_arr1.Count; i++){
                                 if (user_arr1[i] == null) continue; 
                                 if (user_arr1[i].name == ("" + id)){
                                    //Destroy(user_arr1[i].GetComponent<Rigidbody>());
                                    user_arr1[i].GetComponent<Animator>().SetBool("is_die", true);
                                    user_cnt--;
                                    if(user_cnt == 1) SceneManager.LoadScene("GameOverScene");                                    
                                 }
                             }
                        }
                        else{
                            for (int i = 0; i< user_arr1.Count; i++){
                                if (user_arr1[i] != null){
                                    if (user_arr1[i].name == message){
                                        Destroy(user_arr1[i]);
                                        user_arr1[i] = null;
                                        user_cnt--;
                                        if(user_cnt == 1) SceneManager.LoadScene("GameWin");
                                    }
                                }  
                        }
                        }
                        
                    }
                    break;
                case "random_move":
                    string[] temp = message.Split("_");
                    string time =temp[0];
                    Debug.Log("Time: " + temp[0]);
                    string choice = temp[1];
                    Debug.Log("Choice: " + temp[1]);
                    FollowPlayer.setterm(float.Parse(time), int.Parse(choice));
                    break;
                    

            }

            /*
            string[] xyz = message.Split(',');

            if(float.Parse(xyz[0]) == 1.0f){
                OtherPlayer.transform.position = new Vector3(float.Parse(xyz[1]), float.Parse(xyz[2]), float.Parse(xyz[3]));
            }

            if(float.Parse(xyz[0]) == 2.0f){
                MyPlayer.transform.position = new Vector3(float.Parse(xyz[1]), float.Parse(xyz[2]), float.Parse(xyz[3]));
            }*/

            // getting the message as a string
            // var message = System.Text.Encoding.UTF8.GetString(bytes);
            // Debug.Log("OnMessage! " + message);
        };

        // Keep sending messages at every 0.3s
        //InvokeRepeating("SendWebSocketMessage", 0.0f, 0.02f);

        // waiting for messages
        await websocket.Connect();
    }

    // Update is called once per frame
    void Update()
    {
        #if !UNITY_WEBGL || UNITY_EDITOR
            websocket.DispatchMessageQueue();
        #endif
    }

    // async void SendWebSocketMessage()
    // {
    //     if (websocket.State == WebSocketState.Open)
    //     {
    //         // Sending bytes
    //         // Sending plain text
    //         await websocket.SendText("2,"+ myplayer.transform.position.x + "," +myplayer.transform.position.y + "," + "asdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdaasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdsdasdasdasdasdasdasdaaaaaaaaaaaasdasdasdasdasaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaadasdasdasdasdasdasdasdasdasdasdasdasdasdasdasd" );
    //     }
    // }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }

    private async void SendUnitPosition(){
        //Debug.Log("SendUnitPosition");
        string send_str = "unit_position!" + id + "," + myplayer.transform.position.x + "," + myplayer.transform.position.y + ";" ;
        foreach (GameObject npc in GameSystemScript.npc_arr)
        {
            if (npc == null){
               send_str += id + "," + "0.0" + "," + "0.0" + ";"; 
            }
            else if(npc.name.Split('_')[0] == id + ""){
                send_str += id + "," + npc.transform.position.x + "," + npc.transform.position.y + ";";
            }
        }
        await websocket.SendText(send_str);
    }
}