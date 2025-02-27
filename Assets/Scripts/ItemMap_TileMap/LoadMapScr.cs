using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMapScr : MonoBehaviour
{
    public static LoadMapScr instance;
    void Start()
    {
        StartCoroutine(HideAfterDelay());
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
    public void ShowLoading()
    {
        gameObject.SetActive(true);
        StartCoroutine(HideAfterDelay());
    }
    public IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(1f);

        gameObject.SetActive(false); 
        if (MainChar.instance != null)
        {
            MainChar.instance.jumpCount = 0;
            MainChar.instance.currentCoin = 0;
            MainChar.instance.cameramn.UpdateConfiner();
            MainChar.instance.transform.position = GameManager.instance.reponPossition();
            GameManager.instance.Intit();
        }
        GameManager.instance.HealthUpdate();
        MainChar.instance.isDie = false;
        SoundManages.instance.Play("checkpoint");


    }
    //public void WaitresetPoint() => StartCoroutine(HideAfterDelay2());
    //public IEnumerator HideAfterDelay2()
    //{
    //    yield return new WaitForSeconds(2f);
    //    MainChar.instance.transform.position = GameManager.instance.reponPossition();
    //}
}
