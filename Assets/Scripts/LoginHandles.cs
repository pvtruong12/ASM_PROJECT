using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginHandles : MonoBehaviour
{
    public GameObject mainMenu;
    public List<GameObject> listButon;
    private List<string> listName;
    public TMP_InputField inputField;  
    public Text resultText;
    public GameObject CreatCharScr;
    public void Awake()
    {
        string a = PlayerPrefs.GetString("cName.txt");
        if (File.Exists(GameManager.PathFile("listName.txt")))
            listName = File.ReadAllLines(GameManager.PathFile("listName.txt")).Where(x => !string.IsNullOrEmpty(x)).ToList();
        else
            listName = new List<string>();
        if (!string.IsNullOrEmpty(a))
        {
            GameManager.name = a;
        }
        
    }
    public void Start()
    {
        BtnChoiMoi.instance = this;
        if (GameManager.name == "")
        {
            listButon.ElementAt(1).SetActive(false);
        }
    }
    public void LoadCreatChar()
    {
        CreatCharScr.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void CheckName() => StartCoroutine(CheckNames());
    IEnumerator CheckNames()
    {
        yield return 1f;
        string name = inputField.text;
        if (listName.Count <= 0 || (listName.Last() != name && !string.IsNullOrEmpty(name) && !name.Contains(" ")))
        {
            PlayerPrefs.SetString("cName.txt", name);
            File.AppendAllText(GameManager.PathFile("listName.txt"), name+"\n");
            GameManager.name = name;
            SceneManager.LoadScene(GameManager.listLevel[0]);
        }
        else
        {
            inputField.text = "Vui lòng nhâp lại tên";
        }

    }
}
