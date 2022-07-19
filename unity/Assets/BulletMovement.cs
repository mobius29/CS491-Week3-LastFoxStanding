using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeWebSocket;
public class BulletMovement : MonoBehaviour
{
    private int mode;
    private float time;
    [SerializeField]
    // Start is called before the first frame update\
    WebSocket websocket;
    public void setDirection(int direction){
        mode = direction;
    }
    void Start()
    {
        websocket = GameNetworking.websocket;
    }

    private async void OnCollisionEnter2D(Collision2D collision){
        Destroy(gameObject);
        await websocket.SendText("hit!"+ collision.transform.name);
    }


    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        switch(mode){
            case 0:
                transform.position += Vector3.right * 15 * Time.deltaTime;
                break;
            case 1:
                transform.position += Vector3.up * 15 * Time.deltaTime;
                break;
            case 2:
                transform.position += Vector3.down * 15 * Time.deltaTime;
                break;
            case 3:
                transform.position += Vector3.left * 15 * Time.deltaTime;
                break;
        }
        
    }
}
