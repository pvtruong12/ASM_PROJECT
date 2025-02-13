using UnityEngine;

[CreateAssetMenu(fileName = "MainCharData", menuName = "GameData/MainCharData")]
public class MainCharDataSO : ScriptableObject
{
    public string cName = "";
    public Vector3 currentPostion;
    public int currentLevel = 1;
    public float moveSpeed = 2f;
    public float bulletSpeed = 10f; // has
    public int coins = 0;
}
