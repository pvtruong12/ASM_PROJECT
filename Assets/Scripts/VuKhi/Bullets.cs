using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public string who;
    public Bullets instance;

    void Start()
    {
        instance = this;
        StartCoroutine(DisableAfterTime(5f));
    }

    IEnumerator DisableAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        BulletManagers.instance.ReturnBullet(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            BulletManagers.instance.ReturnBullet(gameObject);
        }
        else if (collision.CompareTag("Mobs") && who =="Char")
        {
            GameObject game2 = collision.gameObject;
            Mob mob = game2.GetComponent<Mob>();
            mob.TakeDameMob(1);
            BulletManagers.instance.ReturnBullet(gameObject);
        }
        else if (collision.CompareTag("myCharz") && who == "Mob")
        {
            MainChar.instance.isDie = true;
            GameManager.instance.health--;
            if (GameManager.instance.health <= 0)
            {
                GameManager.instance.SetPanelWinOrLose(false);
                return;
            }
            GameManager.instance.MeDead();
            BulletManagers.instance.ReturnBullet(gameObject);
        }

    }
}
