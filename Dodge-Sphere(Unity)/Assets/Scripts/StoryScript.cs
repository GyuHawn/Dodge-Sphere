using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryScript : MonoBehaviour
{
    private AudioManager audioManager;

    private GameStartScript gameStartScript;

    public GameObject story; // ���丮 UI����

    public int page; // ���� ������ ��
    public GameObject[] storyImages; // ���丮 �̹���
    public bool onStory; // ���丮�� �� �� �ΰ�
    public Toggle storyToggle;

    void Awake()
    {
        gameStartScript = GameObject.Find("Manager").GetComponent<GameStartScript>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void CheckGameStory()
    {
        page = 0;
        if (storyToggle.isOn && !onStory)
        {
            onStory = true;
            PlayerPrefs.SetInt("Story", onStory ? 1 : 0);
        }
        else if (!storyToggle.isOn && onStory)
        {
            onStory = false;
            PlayerPrefs.SetInt("Story", onStory ? 1 : 0);
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
            if (page <= 2)
            {
                page++;
                storyImages[page].SetActive(true);
            }
            if (page > 2)
            {
                audioManager.b_Story.Stop();
                story.SetActive(false);
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
