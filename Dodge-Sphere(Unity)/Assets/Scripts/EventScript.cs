using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class EventScript : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private GetItem getItem;
    private ClearInfor clearInfor;

    public int eventNum;
    public GameObject eventUI;
    public GameObject[] events;

    public bool onEvent; // �̺�Ʈ ���� ����

    public GameObject player;

    private void Awake()
    {
        getItem = GameObject.Find("Manager").GetComponent<GetItem>();
        clearInfor = GameObject.Find("Manager").GetComponent<ClearInfor>();
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
            playerMovement = player.GetComponent<PlayerMovement>();
        }
        
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
        clearInfor.useEvent++;

        playerMovement.currentHealth += 2;
        events[eventNum].SetActive(false);
        eventUI.SetActive(false);
        playerMovement.moveNum = 1;
        playerMovement.isEvent = false;
    }
    public void LakeEvent2()
    {
        clearInfor.useEvent++;

        if (Random.Range(0, 100) < 30)
        {
            playerMovement.money += 300;
            clearInfor.getMoney += 300;
        }
        events[eventNum].SetActive(false);
        eventUI.SetActive(false);
        playerMovement.moveNum = 1;
        playerMovement.isEvent = false;
    }

    // �� �̺�Ʈ
    public void HouseEvent1()
    {
        clearInfor.useEvent++;

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
        playerMovement.isEvent = false;
    }
    public void HouseEvent2()
    {
        clearInfor.useEvent++;

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
        playerMovement.isEvent = false;
    }

    // ���� �̺�Ʈ
    public void CaveEvent1()
    {
        clearInfor.useEvent++;

        playerMovement.currentHealth += 3;
        events[eventNum].SetActive(false);
        eventUI.SetActive(false);
        playerMovement.moveNum = 1;
        playerMovement.isEvent = false;
    }
    public void CaveEvent2()
    {
        clearInfor.useEvent++;

        if (Random.Range(0, 100) < 50)
        {
            playerMovement.money += 300;
            clearInfor.getMoney += 300;
        }
        else
        {
            playerMovement.currentHealth -= 2;
        }
        events[eventNum].SetActive(false);
        eventUI.SetActive(false);
        playerMovement.moveNum = 1;
        playerMovement.isEvent = false;
    }

    // ���� �̺�Ʈ
    public void ItemEvent1()
    {
        clearInfor.useEvent++;

        playerMovement.currentHealth -= 2;

        if (Random.Range(0, 100) < 50)
        {
            playerMovement.money += 100;
            clearInfor.getMoney += 100;
        }
        else
        {
            getItem.SelectItem();
        }
        events[eventNum].SetActive(false);
        eventUI.SetActive(false);
        playerMovement.moveNum = 1;
        playerMovement.isEvent = false;
    }
    public void ItemEvent2()
    {
        clearInfor.useEvent++;

        if (Random.Range(0, 100) < 30)
        {
            getItem.SelectItem();
        }
        events[eventNum].SetActive(false);
        eventUI.SetActive(false);
        playerMovement.moveNum = 1;
        playerMovement.isEvent = false;
    }

    // ���ư���
    public void PassEvent()
    {
        clearInfor.useEvent++;

        events[eventNum].SetActive(false);
        eventUI.SetActive(false);
        playerMovement.moveNum = 1;
        playerMovement.isEvent = false;
    }
}
