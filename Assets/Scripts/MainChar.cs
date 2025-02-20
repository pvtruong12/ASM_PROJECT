using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class MainChar : MonoBehaviour
{
    public static MainChar instance;
    public Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator amn;
    [SerializeField] private float jumpForce = 10f; 
    public int jumpCount = 0;
    private bool isGrounded;
    public bool isLadder;
    public long lastTimeTick;
    public int currentCoin = 0;
    public bool isDie = false;
    public CameraManager cameramn;
    private TextMeshProUGUI textName;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;

    void Start()
    {
        textName = GetComponentInChildren<TextMeshProUGUI>();
        textName.text = GameManager.name;
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
        rb.velocity = new Vector2(movesize * GameManager.instance.moveSpeed, rb.velocity.y);
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
    public void Row()
    {

        float movesize = Input.GetAxisRaw("Vertical");
        amn.SetBool("isRow", movesize != 0);
        if (!isLadder)
            return;
        rb.velocity = new Vector2(rb.velocity.x, movesize * GameManager.instance.moveSpeed);
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
            amn.SetTrigger("isAttack");
            GameObject gameObjects = BulletManagers.instance.GetBullets();
            gameObjects.GetComponent<Bullets>().who = "Char";
            gameObjects.transform.position = transform.position;
            gameObjects.GetComponent<SpriteRenderer>().color = Color.red;
            Rigidbody2D rb = gameObjects.GetComponent<Rigidbody2D>();
            float localScaleX = sr.flipX ? -1 : 1;
            rb.velocity = new Vector2(localScaleX * GameManager.instance.bulletSpeed, 0);
        }
    }
    public static long currentTimeMillis()
    {
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return (DateTime.UtcNow.Ticks - dateTime.Ticks) / 10000;
    }
    
    public void update()
    {
        Attack();
        Jump();
        Move();
    }
}
