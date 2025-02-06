using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainChar : MonoBehaviour
{
    public float moveSpeed = 2f;
    public static MainChar instance;
    public static string[] listLevel = new string[] { "Menu", "Level_1", "Level_2", "Level_3" };
    public int currentLevel = 1;
    public Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator amn;
    public float jumpForce = 10f; 
    public int jumpCount = 0;
    public bool isDie = false;
    private bool isGrounded;
    public bool isLadder;
    public CameraManager cameramn;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        amn = GetComponent<Animator>();
        cameramn = GetComponentInChildren<CameraManager>();

    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    private void Move()
    {
        float movesize = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(movesize * moveSpeed, rb.velocity.y);
        amn.SetBool("isMove", movesize != 0 && isGrounded);
        amn.SetBool("isJump", !isGrounded && !isLadder);
        if(movesize > 0)
        {
            sr.flipX = false;
        }else if(movesize < 0)
        {
            sr.flipX = true;
        }
    }
    private void Row()
    {

        float movesize = Input.GetAxisRaw("Vertical");
        amn.SetBool("isRow", movesize != 0);
        if (!isLadder)
            return;
        rb.velocity = new Vector2(rb.velocity.x, movesize * moveSpeed);
    }
    private  void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 1)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //foreach (ContactPoint2D contact in collision.contacts)
        //{
        //    Chỉ xét va chạm từ phía dưới
        //    if (contact.normal.y > 0.5f)
        //    {
        //        if (collision.gameObject.CompareTag("Ground"))
        //        {
        //            isGrounded = true;
        //            jumpCount = 0;
        //            Debug.Log("Chạm đất!");
        //        }
        //    }
        //}
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;// Debug.Log("Chạm đất!");
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    void FixedUpdate()
    {
        Row();
        Move();
        Jump();
    }
}
