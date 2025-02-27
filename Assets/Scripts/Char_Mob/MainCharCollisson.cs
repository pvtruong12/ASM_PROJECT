using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharCollisson : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Mobs") || collision.CompareTag("Traps")) && !MainChar.instance.isDie)
        {
            MainChar.instance.isDie = true;
            GameManager.instance.health--;
            if(GameManager.instance.health <= 0)
            {
                GameManager.instance.SetPanelWinOrLose(false);
                return;
            }
            GameManager.instance.MeDead();
        }
        else if (collision.CompareTag("ladder"))
        {
            MainChar.instance.isLadder = true;
            MainChar.instance.rb.gravityScale = 0;
        }
        else if(collision.CompareTag("Coin"))
        {
            if (MainChar.instance.isDie)
                return;
            GameManager.instance.AddCoins(1);
            ItemMapManager.instance.ReturnCoin(collision.gameObject);
            SoundManages.instance.Play("pick");
        }
        else if(collision.CompareTag("hearth"))
        {
            if (MainChar.instance.isDie)
                return;
            GameManager.instance.AddHearth(1);
            Destroy(collision.gameObject);
            SoundManages.instance.Play("pick");
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
