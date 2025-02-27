using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

public class LoginHandles : MonoBehaviour
{
    public enum CurenInPut
    {
        UserName =0,
        Pass =1,
        Email = 2,
        Name =3,
        SDT= 4
    }
    public TextMeshProUGUI PanelTB;
    public static LoginHandles instance;
    public GameObject mainMenu;
    public List<GameObject> listButon;
    public List<TMP_InputField> listTextBox;
    public List<TMP_InputField> listTextBoxLogin;
    public TMP_Dropdown comboBoxType;
    public GameObject CreatCharScr;
    public GameObject ScreenLogin;
    public bool isSetSuccesField(CurenInPut index)
    {
        bool isFlag = true;
        switch(index)
        {
            case CurenInPut.UserName:
                isFlag = isUserNameSuccess(listTextBox[(int)index].text);
                break;
            case CurenInPut.Pass:
                isFlag = isPassWordSucces(listTextBox[(int)index].text);
                break;
            case CurenInPut.Email:
                isFlag = isEmailSucces(listTextBox[(int)index].text);
                break;
            case CurenInPut.SDT:
                isFlag = isSDTVNSucces(listTextBox[(int)index].text);
                break;
        }
        return isFlag;
    }
    public List<Username> listUsername = new List<Username>();
    public class Username
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Sdt { get; set; }
        public string Type { get; set; }
    }
    public bool isUserNameSuccess(string username) => Regex.IsMatch(username, "^[a-z0-9]{1,20}$");

    public bool isPassWordSucces(string password) => Regex.IsMatch(password, @"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[@#$%])[A-Za-z\d@#$%]{6,20}$");

    public bool isEmailSucces(string email) => Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

    public bool isCNameSucces(string name) => name.Length <= 15;

    public bool isSDTVNSucces(string phoneNumber) => Regex.IsMatch(phoneNumber, @"^(0[3|5|7|8|9])([0-9]{8})$");
    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        comboBoxType.AddOptions(new List<string>() { "IOS", "PC", "Java" });
        Username playername = null;
        try
        {
            playername = JsonConvert.DeserializeObject<Username>(PlayerPrefs.GetString("Name.txt"));
        }catch(Exception ex)
        {
            Debug.Log(ex);
            playername = null;
        }
        string path = GameManager.PathFile("listName.txt");
        if (File.Exists(path))
        {
            try
            {

                listUsername = JsonConvert.DeserializeObject<List<Username>>(File.ReadAllText(path));

            }catch(Exception ex)
            {
                Debug.Log(ex);
                listUsername = new List<Username>();
            }
        }
        else
            listUsername = new List<Username>();
        if (playername!=null)
        {
            GameManager.Username = playername;
        }
        
    }
    public void Start()
    {
         if(GameManager.Username != null)
        {
            listTextBoxLogin[0].text = GameManager.Username.UserName;
            listTextBoxLogin[1].text = GameManager.Username.PassWord;
        }
    }
    public void HandleLogin()
    {
        string acc = listTextBoxLogin[0].text;
        string pass = listTextBoxLogin[1].text;
        if(!string.IsNullOrEmpty(acc) && !string.IsNullOrEmpty(pass))
        {
            Username username = listUsername.FirstOrDefault(x => x.UserName.Equals(acc) && x.PassWord.Equals(pass));
            if(username ==null)
            {
                PanelTB.text = "Thông tin tài khoản hoặc mật khẩu không chính xác!";
                return;
            }
            PanelTB.text = $"Login Thành công Acc: {username.UserName}, pass {username.PassWord}, email: {username.Email}, Player name {username.Name}, Type {username.Type}";
            GameManager.Username = username;
            SaveTaiKhoan(username);
            StartCoroutine(SuccesLogin());
        }
    }
    IEnumerator SuccesLogin()
    {
        yield return 2f;
        SceneManager.LoadScene(GameManager.listLevel[0]);
    }
    public void LoadSceneMenuFromLoginScr()
    {
        PanelTB.text = "";
        if (GameManager.Username != null)
        {
            listTextBoxLogin[0].text = GameManager.Username.UserName;
            listTextBoxLogin[1].text = GameManager.Username.PassWord;
        }
        mainMenu.SetActive(true);
        CreatCharScr.SetActive(false);
        ScreenLogin.SetActive(false);
    }
    public void LoadSceneCreatCharFromLoginScr()
    {
        PanelTB.text = "";
        mainMenu.SetActive(false);
        CreatCharScr.SetActive(true);
        ScreenLogin.SetActive(false);
    }
    public void LoadSceneLoginCharFromLoginScr()
    {
        PanelTB.text = "";
        mainMenu.SetActive(false);
        CreatCharScr.SetActive(false);
        ScreenLogin.SetActive(true);
    }
    public void SaveTaiKhoan(object user)
    {
        PlayerPrefs.SetString("Name.txt", JsonConvert.SerializeObject(user));
    }
    public void CheckName() => StartCoroutine(CheckNames());
    IEnumerator CheckNames()
    {
        yield return new WaitForSeconds(1f);
        if (listUsername == null)
        {
            listUsername = new List<Username>();
        }
            bool flag = false;
        Username currentPlayer = null;
        for(int i =0; i< listTextBox.Count; i++)
        {
            CurenInPut num = (CurenInPut)i;
            if(!isSetSuccesField(num))
            {
                flag = true;
                listTextBox[i].text = string.Empty;
            }
        }
        if(!flag)
        {
            currentPlayer = new Username() { UserName = listTextBox[0].text, PassWord = listTextBox[1].text, Email = listTextBox[2].text, Name = listTextBox[3].text, Sdt = listTextBox[4].text, Type = comboBoxType.options[comboBoxType.value].text };
            if(listUsername.Any(x => (x.Name.Equals(currentPlayer.Name) || x.UserName.Equals(currentPlayer.UserName))))
            {
                PanelTB.text = "Tài khoản đã có người đăng ký vui lòng thử lại Tên NV hoặc tên Account";
            }
            else
            {
                PanelTB.text = $"Đăng ký thành công Acc: {currentPlayer.UserName}, pass {currentPlayer.PassWord}, email: {currentPlayer.Email}, Player name {currentPlayer.Name}, Type {currentPlayer.Type}";
               
                listUsername.Add(currentPlayer);
                File.WriteAllText(GameManager.PathFile("listName.txt"), JsonConvert.SerializeObject(listUsername));
                yield return new WaitForSeconds(5f);
                LoadSceneMenuFromLoginScr();
            }
        }
    }
}
