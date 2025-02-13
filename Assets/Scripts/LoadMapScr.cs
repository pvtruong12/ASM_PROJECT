using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMapScr : MonoBehaviour
{
    public static LoadMapScr instance;
    void Start()
    {
        StartCoroutine(HideAfterDelay(2f));
    }
    private void Awake()
    {
        Debug.Log("Created LoadMap");
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
        StartCoroutine(HideAfterDelay(2f));
    }
    public IEnumerator HideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject bounds = GameObject.FindWithTag("repoint");
        if (bounds != null && MainChar.instance != null)
        {
            MainChar.instance.transform.position = bounds.transform.position;
            MainChar.instance.jumpCount = 0;
            MainChar.instance.cameramn.UpdateConfiner();
        }

        gameObject.SetActive(false);
    }
}
