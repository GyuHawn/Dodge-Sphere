using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class EventScript : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private GetItem getItem;

    public int eventNum;
    public GameObject eventUI;
    public GameObject[] events;

    public bool onEvent; // �̺�Ʈ ���� ����

    private void Awake()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        getItem = GameObject.Find("Manager").GetComponent<GetItem>();
    }

    void Start()
    {
    }
    
    void Update()
    {
        if (onEvent)
        {
            onEvent = false;
            playerMovement.currentTile = 0;
            StartEvent();
        }
    }

    void StartEvent()
    {
        eventNum = Random.Range(0, events.Length);
        Debug.Log(eventNum);

        eventUI.SetActive(true);
        events[eventNum].SetActive(true);
    }

    // ȣ�� �̺�Ʈ
    public void LakeEvent1()
    {
        playerMovement.currentHealth += 2;
        events[eventNum].SetActive(false);
        eventUI.SetActive(false);
        playerMovement.moveNum = 1;
    }
    public void LakeEvent2()
    {
        if (Random.Range(0, 100) < 30)
        {
            playerMovement.money += 300;
        }
        events[eventNum].SetActive(false);
        eventUI.SetActive(false);
        playerMovement.moveNum = 1;
    }

    // �� �̺�Ʈ
    public void HouseEvent1()
    {
        if (Random.Range(0, 100) < 50)
        {
            playerMovement.currentHealth += 2;
        }
        else
        {
            playerMovement.currentHealth -= 2;
        }
        events[eventNum].SetActive(false);
        eventUI.SetActive(false);
        playerMovement.moveNum = 1;
    }
    public void HouseEvent2()
    {
        if (Random.Range(0, 100) < 50)
        {
            playerMovement.currentHealth += 4;
        }
        else
        {
            playerMovement.currentHealth -= 4;
        }
        events[eventNum].SetActive(false);
        eventUI.SetActive(false);
        playerMovement.moveNum = 1;
    }

    // ���� �̺�Ʈ
    public void CaveEvent1()
    {
        playerMovement.currentHealth += 3;
        events[eventNum].SetActive(false);
        eventUI.SetActive(false);
        playerMovement.moveNum = 1;
    }
    public void CaveEvent2()
    {
        if (Random.Range(0, 100) < 50)
        {
            playerMovement.money += 300;
        }
        else
        {
            playerMovement.currentHealth -= 2;
        }
        events[eventNum].SetActive(false);
        eventUI.SetActive(false);
        playerMovement.moveNum = 1;
    }

    // ���� �̺�Ʈ
    public void ItemEvent1()
    {
        playerMovement.currentHealth -= 2;

        if (Random.Range(0, 100) < 50)
        {
            playerMovement.money += 100;
        }
        else
        {
            getItem.SelectItem();
        }
        events[eventNum].SetActive(false);
        eventUI.SetActive(false);
        playerMovement.moveNum = 1;
    }
    public void ItemEvent2()
    {
        if (Random.Range(0, 100) < 30)
        {
            getItem.SelectItem();
        }
        events[eventNum].SetActive(false);
        eventUI.SetActive(false);
        playerMovement.moveNum = 1;
    }

    // ���ư���
    public void PassEvent()
    {
        events[eventNum].SetActive(false);
        eventUI.SetActive(false);
        playerMovement.moveNum = 1;
    }
}
