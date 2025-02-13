using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class LoginHandles : MonoBehaviour
{
    public GameObject mainMenu;
    public List<GameObject> listButon;
    private MainChar mainChar;
    void Start()
    {
        Init();
    }
    private void Init()
    {
        string path = Application.persistentDataPath + "/myInfo.json";
        if(!File.Exists(path))
        {
            listButon[1].SetActive(false);
            return;
        }
        else
        {
            mainChar = null;
            try
            {
                mainChar = JsonUtility.FromJson<MainChar>(File.ReadAllText(path));
            }
            catch(Exception ex)
            {
                listButon[1].SetActive(false);
                Debug.Log(ex);
            }
        }
    }
    public void BtnChoiMoi()
    {
        SceneManager.LoadScene(MainChar.listLevel[mainChar.currentLevel]);
    }
    public void BtnContinue()
    {
        SceneManager.LoadScene(MainChar.listLevel[mainChar.currentLevel]);
    }
}
