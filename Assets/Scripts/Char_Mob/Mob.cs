using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 localPossion;
    public float sizeMove= 5f;
    public float moveSpeed= 3f;
    public float speedVienDan= 3f;
    private bool isMoveRight = true;
    private long lastTimeAttack;
    private bool isCanAttack = false;
    private int [] levelMob = new int[3] {0, 1, 2 };
    public int mobHP =-1;
    void Start()
    {
        localPossion = transform.position;
    }

    public void update()
    {
        if(mobHP==-1)
        {
            mobHP = levelMob[GameManager.instance.currentLevel];
        }
        Attack();
        MoveMob();
    }

    public void TakeDameMob(int dame)
    {
        mobHP -= dame;
        if (mobHP == 0)
        {
            GameObject game = ItemMapManager.instance.GetCoin(transform.position);
            Destroy(gameObject);
        }
    }
    void Attack()
    {
        Vector3 vectorChar = MainChar.instance.gameObject.transform.position;
        Vector3 mPosion = transform.position;
        int capdo = levelMob[GameManager.instance.currentLevel];
        float size = Vector3.Distance(vectorChar, mPosion);
        if(size <= 5f* capdo && mPosion.y  <= vectorChar.y)
        { 
            if (MainChar.currentTimeMillis() - lastTimeAttack >=3000)
            {
                lastTimeAttack = MainChar.currentTimeMillis();
                if (vectorChar.x > transform.position.x && transform.localScale.x < 0)
                {
                    isMoveRight = true;
                    FlipX();
                }
                else if (vectorChar.x < transform.position.x && transform.localScale.x > 0)
                {
                    isMoveRight = false;
                    FlipX(); 
                }
                Vector3 vector = (vectorChar - transform.position).normalized;
                GameObject viendans = BulletManagers.instance.GetBullets();
                if (viendans == null)
                    return;

                viendans.transform.position = transform.position;
                viendans.GetComponent<Bullets>().who = "Mob";
                viendans.transform.position = transform.position;
                viendans.GetComponent<SpriteRenderer>().color = Color.blue;
                Rigidbody2D rb = viendans.GetComponent<Rigidbody2D>();
                rb.velocity =vector * speedVienDan;
                SoundManages.instance.Play("attack");
            }
            isCanAttack = true;
            return;
        }
        isCanAttack = false;
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
