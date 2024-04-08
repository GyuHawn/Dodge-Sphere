using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryScript : MonoBehaviour
{
    private GameStartScript gameStartScript;

    public GameObject story; // ���丮 UI����

    public int page; // ���� ������ ��
    public GameObject[] storyImages; // ���丮 �̹���
    public bool onStory; // ���丮�� �� �� �ΰ�
    public Toggle storyToggle;

    void Awake()
    {
        gameStartScript = GameObject.Find("Manager").GetComponent<GameStartScript>();
    }

    public void CheckGameStory()
    {
        if (storyToggle.isOn && !onStory)
        {
            onStory = true;
        }
        else if (!storyToggle.isOn && onStory)
        {
            onStory = false;
        }
    }

    public void StartStory() // ��ü ���丮 UI Ȱ��ȭ
    {
        story.SetActive(true);
    }

    public void NextStory() // �� �������� ���丮 ����
    {
        storyImages[page].SetActive(false);

        if (onStory)
        {
            if (page <= 3)
            {
                page++;
                storyImages[page].SetActive(true);
            }
            else if (page > 3)
            {
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
                page = 0;
                storyImages[page].SetActive(true);
                story.SetActive(false);
            }
        }
    }

    public void AllStory()
    {
        onStory = false;
        storyToggle.isOn = false;
        story.SetActive(true);
    }
}
