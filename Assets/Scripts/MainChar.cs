using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainChar : MonoBehaviour
{
    public float moveSpeed = 2f;
    public static MainChar instance;
    public static string[] listLevel = new string[] { "LoginScr", "CreatChar","Level_1", "Level_2", "Level_3" };
    public int currentLevel = 1;
    public Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator amn;
    [SerializeField] private float jumpForce = 10f; 
    public float bulletSpeed = 10f; 
    public int jumpCount = 0;
    public bool isDie = false;
    private bool isGrounded;
    public bool isLadder;
    public long lastTimeTick;
    public int coins= 0;
    public CameraManager cameramn;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private GameObject bullets;

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
        {
            Destroy(gameObject);
            Debug.Log("Duplicate MainChar Destroyed!");
        }
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

    private void Jump()
    {
        if (isGrounded)
            jumpCount = 0;
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 1)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
        }
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject gameObjects = Instantiate(bullets, transform.position, Quaternion.identity);
            Rigidbody2D rb = gameObjects.GetComponent<Rigidbody2D>();
            float localScaleX = sr.flipX ? -1 : 1;
            rb.velocity = new Vector2(localScaleX * bulletSpeed, 0);
        }
    }
    public static long currentTimeMillis()
    {
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return (DateTime.UtcNow.Ticks - dateTime.Ticks) / 10000;
    }
    
    private void Update()
    {
        Attack();
        Jump();
        Move();
    }
    void FixedUpdate()
    {
        Row();
    }
}
