using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{ 
    [SerializeField]
    private GameObject bullet;
    private float moveSpeed = 2.0f;
    private float bullet_count_add_time = 0.0f;
    private Vector3 moveDirection = Vector3.zero;
    private Rigidbody2D rigid2D;
    public static int bulletcount = 3;
    private SpriteRenderer spriteRenderer;
    private bool is_bullet_appeared = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        bullet_count_add_time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        bullet_count_add_time += Time.deltaTime;

        if(bullet_count_add_time > 5.0f) {
            ++bulletcount;
            bullet_count_add_time = 0.0f;
        }

        if((Input.GetKeyDown("l") || Input.GetKeyDown("i") || Input.GetKeyDown("k") || Input.GetKeyDown("j")) && !is_bullet_appeared && bulletcount > 0){
            GameObject clone = null;
            --bulletcount;
            is_bullet_appeared = true;

            if (Input.GetKeyDown("l")){
                clone = Instantiate(bullet, transform.position + new Vector3(1,0,0), Quaternion.identity);
                clone.GetComponent<BulletMovement>().setDirection(0);
            }
            if (Input.GetKeyDown("i")){
                clone = Instantiate(bullet, transform.position + new Vector3(0,1,0), Quaternion.identity);
                clone.GetComponent<BulletMovement>().setDirection(1);
            }
            if (Input.GetKeyDown("k")){
                clone = Instantiate(bullet, transform.position + new Vector3(0,-1,0), Quaternion.identity);
                clone.GetComponent<BulletMovement>().setDirection(2);
            }
            if (Input.GetKeyDown("j")){
                clone = Instantiate(bullet, transform.position + new Vector3(-1,0,0), Quaternion.identity);
                clone.GetComponent<BulletMovement>().setDirection(3);
            }

            if(clone != null) {
                Destroy(clone, 3.0f);
            }
            Invoke("bulletDisappear", 3.0f);
        }
        
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        if(x < 0){
            spriteRenderer.flipX = true;
        }
        else if(x > 0){
            spriteRenderer.flipX = false;
        }

        rigid2D.velocity = new Vector3(x,y,0) * moveSpeed;
    }

    void bulletDisappear() {
        is_bullet_appeared = false;
    }
}
