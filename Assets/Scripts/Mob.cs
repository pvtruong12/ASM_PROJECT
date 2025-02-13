using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 localPossion;
    public float sizeMove= 5f;
    public float moveSpeed= 3f;
    public float speedVienDan= 10f;
    private bool isMoveRight = true;
    private long lastTimeAttack;
    public GameObject prefabsVenDan2;
    private bool isCanAttack = false;
    void Start()
    {
        localPossion = transform.position;
    }

    void Update()
    {

        Attack();
        MoveMob();

    }
    void Attack()
    {
        Vector3 vectorChar = MainChar.instance.gameObject.transform.position;
        Vector3 mPosion = transform.position;
        float size = Vector3.Distance(vectorChar, mPosion);
        if(size <= 5f && mPosion.y  <= vectorChar.y)
        {
            isCanAttack = true;
            if (MainChar.currentTimeMillis() - lastTimeAttack >=1500)
            {
                lastTimeAttack = MainChar.currentTimeMillis();
                if (vectorChar.x > transform.position.x && transform.localScale.x < 0)
                {
                    FlipX();
                }
                else if (vectorChar.x < transform.position.x && transform.localScale.x > 0)
                {
                    FlipX(); 
                }
                Vector3 vector = (vectorChar - transform.position).normalized;
                GameObject viendans = Instantiate(prefabsVenDan2, transform.position, Quaternion.identity);
                viendans.GetComponent<SpriteRenderer>().color = Color.blue;
                Rigidbody2D rb = viendans.GetComponent<Rigidbody2D>();
                rb.velocity =vector * speedVienDan;
            }
            isCanAttack = true;
            return;
        }
        isCanAttack = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullets"))
        {
            Destroy(gameObject);
        }
    }
    void MoveMob()
    {
        if (isCanAttack)
            return;
        float maxX = localPossion.x + sizeMove;
        float minX = localPossion.x - sizeMove;
        if (isMoveRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            if (transform.position.x >= maxX)
            {
                isMoveRight = false; FlipX();
            }
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            if (transform.position.x <= minX)
            {
                isMoveRight = true; FlipX();
            }
        }
    }
    void FlipX()
    {
        Vector3 vector = transform.localScale;
        vector.x *= -1;
        transform.localScale = vector;
    }
}
