using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClearInfor : MonoBehaviour
{
    private AudioManager audioManager;
    private TimeManager timeManager;
    private StoryScript storyScript;
    private PlayerMovement playerMovement;

    // �� �ð�
    public float totalTime;
    public TMP_Text totalTimeText;

    // ���� ���� ��
    public int killedMonster;
    public TMP_Text killedMonsterText;

    // ���� ���� ��
    public int killedBoss;
    public TMP_Text killedBossText;

    // ���� �̿� Ƚ��
    public int useShop;
    public TMP_Text useShopText;

    // �߻��� �̺�Ʈ Ƚ��
    public int useEvent;
    public TMP_Text useEventText;

    // ȹ���� ������ ��
    public int getItem;
    public TMP_Text getItemText;

    // ��� Ƚ��
    public int useRest;
    public TMP_Text useRestText;

    // �� ȹ���� ����
    public int getMoney;
    public TMP_Text getMoneyText;

    // ����� ���� ��
    public int usePotion;
    public TMP_Text usePotionText;

    // ����
    public int totalScore;
    public TMP_Text totalScoreText;

    public GameObject resultUI; // ���â 
    public bool result; // ���â ǥ�� ����
    public TMP_Text clearStateText; // Ŭ���� or ����� ���� �̸� �ؽ�Ʈ

    public bool onStory;
    public bool clear; // Ŭ���� ����

    private void Awake()
    {
        timeManager = GameObject.Find("Manager").GetComponent<TimeManager>();
        storyScript = GameObject.Find("Manager").GetComponent<StoryScript>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Start()
    {
        onStory = PlayerPrefs.GetInt("Story") == 1 ? true : false;
    }

    
    void Update()
    {       
        if (onStory && result)
        {
            if (!clear)
            {
                audioManager.DefeatAudio();
                ClearUI();
            }
            else
            {               
                if (storyScript.page == 0)
                {
                    storyScript.story.SetActive(true);
                    audioManager.StoryAudio();
                }
                else if (storyScript.page >= 4)
                {
                    audioManager.ClearAudio();
                    ClearUI();
                }
            }
            
        }
        else if (!onStory && result) // ���â ǥ�� ��
        {
            if (!clear)
            {
                audioManager.DefeatAudio();
                ClearUI();
            }
            else
            {
                audioManager.ClearAudio();
                ClearUI();
            }     
        }
    }

    void ClearUI()
    {      
        resultUI.SetActive(true);

        TotalResults();

        // �ѽð� ǥ��
        totalTime = timeManager.currentTime;
        int minutes = Mathf.FloorToInt(totalTime / 60f);
        int seconds = Mathf.FloorToInt(totalTime % 60f);
        totalTimeText.text = timeManager.currnetTimerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);

        // ���� ���� �� ǥ�� (���� ������ ����)
        killedMonsterText.text = killedMonster.ToString();

        // ���� ���� �� ǥ�� (���� ������ ����)
        killedBossText.text = killedBoss.ToString();

        // ���� �̿� Ƚ�� ǥ�� (������ ���Ž� ����)
        useShopText.text = useShop.ToString();

        // �߻��� �̺�Ʈ Ƚ�� ǥ�� (�̺�Ʈ ���ý� ����)
        useEventText.text = useEvent.ToString();

        // ȹ���� ������ �� ǥ�� (������ ȹ��� ����)
        getItemText.text = getItem.ToString();

        // ��� Ƚ�� ǥ�� (�޽� �̺�Ʈ ���ý� ����)
        useRestText.text = useRest.ToString();

        // �� ȹ���� ���� �� ǥ�� (���� ȹ�� ���� ����)
        getMoneyText.text = getMoney.ToString();

        // ����� ���� �� ǥ�� (���� ���� ����)
        usePotionText.text = usePotion.ToString();

        // ���� ǥ��
        totalScoreText.text = totalScore.ToString();

        PlayerPrefs.SetInt("GameExp", totalScore);

        result = false;
    }

    void TotalResults()
    {
        totalScore = (killedMonster * 3) + (killedBoss * 10) + (useShop * 2) + (useEvent * 2) + (getItem * 2)
                    + (useRest * 2) + (getItem / 200) + (usePotion * 5);
    }

    public void GameClear()
    {
        LoadingController.LoadNextScene("MainMenu");
    }
}
