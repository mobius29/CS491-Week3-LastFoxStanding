using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelMovement : MonoBehaviour
{
    private int option = 0;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void setOption(int op){
        option = op;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        switch(option){
            case 0:
                if (time<1){
                    transform.position = transform.position + Vector3.up * 2 * Time.deltaTime;
                }
                else if (time>1 && time<2){
                    transform.position = transform.position + Vector3.right * 2 * Time.deltaTime; 
                }
                else if (time<3){
                    transform.position = transform.position + Vector3.down * 2 * Time.deltaTime;
                }
                break;
            case 1:
                if (time<1){
                    transform.position = transform.position + new Vector3(1,1,0) * 2 * Time.deltaTime;
                }
                else if (time>1 && time<2){
                    transform.position = transform.position + new Vector3(1,-1,0) * 2 * Time.deltaTime; 
                }
                else if (time<3){
                    transform.position = transform.position + Vector3.left * 2 * Time.deltaTime;
                }
                break;
            case 2:
                if (time<0.5){
                    transform.position = transform.position + new Vector3(-1,1,0) * 2 * Time.deltaTime;
                }
                else if (time>0.5 && time<1){
                    transform.position = transform.position + new Vector3(1,1,0) * 2 * Time.deltaTime; 
                }
                else if (time>1.0 && time<1.5){
                    transform.position = transform.position + new Vector3(1,0,0) * 2 * Time.deltaTime;
                }
                else if (time>1.5 && time<2.0){
                    transform.position = transform.position + new Vector3(1,-1,0) * 2 * Time.deltaTime;
                }
                else if (time>2.0 && time< 2.5){
                    transform.position = transform.position + new Vector3(-1,-1,0) * 2 * Time.deltaTime;
                }
                else if (time>2.5 && time<3.0){
                    transform.position = transform.position + new Vector3(-1,0,0) * 2 * Time.deltaTime;
                }
                break;
        }
    }
}
