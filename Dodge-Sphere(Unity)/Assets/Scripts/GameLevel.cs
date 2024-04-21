using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameLevel : MonoBehaviour
{
    // ���� 
    public int gameLevel;
    public TMP_Text gameLevelText;
    public TMP_Text gameExpText;
    public int maxExp;
    public int currentExp;

    // ĳ���� ��
    public GameObject playerLock;
    public GameObject[] cannonLocks;

    void Start()
    {
        currentExp = PlayerPrefs.GetInt("GameExp");
        maxExp = PlayerPrefs.GetInt("MaxExp", 100);
        gameLevel = PlayerPrefs.GetInt("GameLevel", 1);
    }

    
    void Update()
    {
        gameLevelText.text = gameLevel.ToString();
        gameExpText.text = currentExp.ToString() + " / " + maxExp.ToString(); 
        if (currentExp > maxExp)
        {
            gameLevel++;
            currentExp -= maxExp;

            PlayerPrefs.SetInt("GameLevel", gameLevel);

            SettingMaxExp();
        }

        Unlocked();
    }

    void Unlocked() // ������ ���� �������
    {
        if (gameLevel >= 2)
        {
            cannonLocks[0].SetActive(false);
        }
        if (gameLevel >= 3)
        {
            cannonLocks[1].SetActive(false);
            cannonLocks[2].SetActive(false);
        }
        if (gameLevel >= 5)
        {
            playerLock.SetActive(false);
        }
        if (gameLevel >= 6)
        {
            cannonLocks[3].SetActive(false);
        }
        if (gameLevel >= 8)
        {
            cannonLocks[4].SetActive(false);
        }
    }

    void SettingMaxExp()
    {
        maxExp = gameLevel * maxExp;
        PlayerPrefs.SetInt("MaxExp", maxExp);
    }
}
