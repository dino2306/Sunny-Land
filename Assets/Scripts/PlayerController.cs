using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    //Khai báo biến
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;

    //FSM
    private enum State {idle, running, jumping, falling } // các hành động của nhân vâtj
    private State state = State.idle;

    

    // Kiểm tra biến
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private int cherries = 0;
    [SerializeField] private Text cherryText;   


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    private void Update()
    {
        MoveManager();
        VelocityState(); 
        anim.SetInteger("state", (int)state); //đặt animation dựa trên trạng thái của Enumerator
    }

    private void MoveManager()
    {
        float hDirection = Input.GetAxis("Horizontal");


        // đi sang phải
        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);


        }

        // đi sang trái

        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);

        }

        else
        {

        }
        // nhảy bằng phím space or chạm vào màn hình
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            state = State.jumping;
        }   
    }

    private void VelocityState()
    {
        if(state == State.jumping) 
        { 
            if(rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }

        else if(state == State.falling)
        {
            if(coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        // trạng thái của nhân vật
        else if(Mathf.Abs(rb.velocity.x) > 2f)
        {
            //Di chuyển
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Collectable")
        {
            Destroy(collision.gameObject);
            cherries += 1;
            cherryText.text = cherries.ToString();
        }    
    }


}
