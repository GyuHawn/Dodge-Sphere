using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryScript : MonoBehaviour
{
    private AudioManager audioManager;
    private ClearInfor clearInfor;
    private GameStartScript gameStartScript;
    private GameDatas gameDatas;


    public GameObject story; // ���丮 UI����

    public int page; // ���� ������ ��
    public GameObject[] storyImages; // ���丮 �̹���
    public bool onStory; // ���丮�� �� �� �ΰ�
    public Toggle storyToggle;

    void Awake()
    {
        clearInfor = GameObject.Find("Manager").GetComponent<ClearInfor>();
        gameStartScript = GameObject.Find("Manager").GetComponent<GameStartScript>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        gameDatas = GameObject.Find("Manager").GetComponent<GameDatas>();
    }

    void Start()
    {

        gameDatas.LoadFieldData<bool>("onStory", value => {
            onStory = value;
        }, () => {
            onStory = false;
        });
    }

    public void CheckGameStory()
    {
        page = 0;
        if (storyToggle.isOn && !onStory)
        {
            onStory = true;
            gameDatas.SaveFieldData("onStory", onStory);
        }
        else if (!storyToggle.isOn && onStory)
        {
            onStory = false;
            gameDatas.SaveFieldData("onStory", onStory);
        }
    }

    public void StartStory() // ��ü ���丮 UI Ȱ��ȭ
    {
        story.SetActive(true);
        audioManager.b_MainMenu.Stop();
        audioManager.StoryAudio();
    }

    public void NextStory() // �� �������� ���丮 ����
    {
        storyImages[page].SetActive(false);
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            if (onStory)
            {
                if (page <= 3)
                {
                    page++;
                    storyImages[page].SetActive(true);
                }
                else if (page > 3)
                {
                    audioManager.b_Story.Stop();
                    audioManager.MainAudio();

                    page = 0;
                    storyImages[page].SetActive(true);
                    story.SetActive(false);
                    gameStartScript.selectWindow.SetActive(true);
                }

            }
            else 
            {
                if (page <= 7)
                {
                    page++;
                    storyImages[page].SetActive(true);
                }
                else if (page > 7)
                {
                    audioManager.b_Story.Stop();
                    audioManager.MainAudio();

                    page = 0;
                    storyImages[page].SetActive(true);
                    story.SetActive(false);
                }
            }
        }
        else if(SceneManager.GetActiveScene().name == "Game")
        {
            if (clearInfor.clear && clearInfor.onStory)
            {
                if (page <= 3)
                {
                    page++;
                    if (page < 4)
                    {
                        storyImages[page].SetActive(true);
                    }
                }
                if (page >= 4)
                {
                    story.SetActive(false);
                    audioManager.TileMapAudio();
                }
            }
        }
    }

    public void AllStory()
    {
        audioManager.ButtonAudio();

        onStory = false;
        storyToggle.isOn = false;

        story.SetActive(true);
        audioManager.b_MainMenu.Stop();
        audioManager.StoryAudio();
    }
}