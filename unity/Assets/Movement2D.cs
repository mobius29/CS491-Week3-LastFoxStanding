using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    private float moveSpeed = 2.0f;

    private int move_mode = 1;

    private Vector3 target_pos;
    private Vector3 moveDirection;

    private SpriteRenderer spriteRenderer;
    private int randomtime = 0;
    private float time = 0.0f;
    private int option = 0;
    void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetTargetPos(){
        move_mode = 0;
        
        float xPos = Random.Range(transform.position.x - 10, transform.position.x + 10);
        float yPos = Random.Range(transform.position.y - 10, transform.position.y + 10);

        target_pos = new Vector3(xPos, yPos, 0);
    }

    public void SetDirection(Vector3 direction){
        move_mode = 1;
        moveDirection = direction;
    }
    public void randomTime(int mode){
        randomtime = 1;
        time = 0.0f;
        option = mode;
    }

    void Update(){
        time += Time.deltaTime;

        if (randomtime == 0){
            switch(move_mode){
                case 0:
                    if((target_pos - transform.position).x < 0){
                        spriteRenderer.flipX = true;
                    }
                    else{
                        spriteRenderer.flipX = false;
                    }
                    transform.position = Vector3.MoveTowards(transform.position, target_pos, moveSpeed * Time.deltaTime);
                    break;
                case 1:
                    if(moveDirection.x < 0){
                        spriteRenderer.flipX = true;
                    }
                    else{
                        spriteRenderer.flipX = false;
                    }
                    transform.position += moveDirection * moveSpeed * Time.deltaTime;
                    break;
            }
        }
        else{
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
                    else{
                        randomtime = 0;
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
                    else{
                        randomtime = 0;
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
                    else{
                        randomtime = 0;
                    }
                    break;
        }
        }
        
    }
}
