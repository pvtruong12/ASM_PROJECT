using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharCollisson : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Mobs") || collision.CompareTag("Traps")  || collision.CompareTag("Bullets")) && !MainChar.instance.isDie)
        {
            MainChar.instance.isDie = true;
            HealthManager.health--;
        }
        else if (collision.CompareTag("ladder"))
        {
            MainChar.instance.isLadder = true;
            MainChar.instance.rb.gravityScale = 0;
        }
        else if(collision.CompareTag("Coin"))
        {
            GameManager.instance.AddCoins(1);
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ladder"))
        {
            MainChar.instance.isLadder = false;
            MainChar.instance.rb.gravityScale = 3;
        }
    }
}
