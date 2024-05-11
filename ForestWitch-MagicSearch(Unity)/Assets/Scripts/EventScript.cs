using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EventScript : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private GetItem getItem;
    private ClearInfor clearInfor;
    private Ability ability;
    private AudioManager audioManager;

    public int eventNum;
    public GameObject eventUI;
    public GameObject[] events;

    public bool onEvent; // �̺�Ʈ ���� ����

    public GameObject player;

    private void Awake()
    {
        getItem = GameObject.Find("Manager").GetComponent<GetItem>();
        clearInfor = GameObject.Find("Manager").GetComponent<ClearInfor>();
        ability = GameObject.Find("Manager").GetComponent<Ability>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Update()
    {
        FindPlayer(); // �÷��̾� ã��

        if (onEvent)
        {
            onEvent = false;
            playerMovement.currentTile = 0;
            StartEvent();
        }
    }

    void FindPlayer()
    {
        if (player == null) // �÷��̾� ã��
        {
            player = GameObject.Find("Player");
            playerMovement = player.GetComponent<PlayerMovement>();
        }
    }


    void StartEvent()
    {
        eventNum = Random.Range(0, events.Length);
  
        eventUI.SetActive(true);
        events[eventNum].SetActive(true);
    }

    // ȣ�� �̺�Ʈ
    public void LakeEvent1()
    {
        clearInfor.useEvent++;

        audioManager.TileMapAudio();

        playerMovement.currentHealth += 2;
        events[eventNum].SetActive(false);
        eventUI.SetActive(false);
        playerMovement.moveNum = 1;
        playerMovement.isEvent = false;
    }

    public void LakeEvent2()
    {
        clearInfor.useEvent++;

        audioManager.TileMapAudio();

        if (Random.Range(0, 100) < 50)
        {
            int money = 300; // �⺻�� 300������ ȹ��

            // �ɷ� 3-1�� Ȱ��ȭ
            if (ability.ability3Num == 1)
            {
                money = ability.GamblingCoin(money); // �ɷ� 3-1�� ���� ���� ȹ���� ����
            }
            // �ɷ� 3-2�� Ȱ��ȭ
            else if (ability.ability3Num == 2)
            {
                money = ability.PlusCoin(money); // �ɷ� 3-2�� ���� ���� ȹ���� ����
            }

            playerMovement.money += money;
            clearInfor.getMoney += money;
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

        audioManager.TileMapAudio();

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

        audioManager.TileMapAudio();

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

        audioManager.TileMapAudio();

        playerMovement.currentHealth += 3;
        events[eventNum].SetActive(false);
        eventUI.SetActive(false);
        playerMovement.moveNum = 1;
        playerMovement.isEvent = false;
    }
    public void CaveEvent2()
    {
        clearInfor.useEvent++;

        audioManager.TileMapAudio();

        if (Random.Range(0, 100) < 50)
        {
            int money = 300; // �⺻�� 300������ ȹ��

            // �ɷ� 3-1�� Ȱ��ȭ
            if (ability.ability3Num == 1)
            {
                money = ability.GamblingCoin(money); // �ɷ� 3-1�� ���� ���� ȹ���� ����
            }
            // �ɷ� 3-2�� Ȱ��ȭ
            else if (ability.ability3Num == 2)
            {
                money = ability.PlusCoin(money); // �ɷ� 3-2�� ���� ���� ȹ���� ����
            }

            playerMovement.money += money;
            clearInfor.getMoney += money;
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

        audioManager.TileMapAudio();

        playerMovement.currentHealth -= 2;

        if (Random.Range(0, 100) < 50)
        {
            int money = 100; // �⺻�� 300������ ȹ��

            // �ɷ� 3-1�� Ȱ��ȭ
            if (ability.ability3Num == 1)
            {
                money = ability.GamblingCoin(money); // �ɷ� 3-1�� ���� ���� ȹ���� ����
            }
            // �ɷ� 3-2�� Ȱ��ȭ
            else if (ability.ability3Num == 2)
            {
                money = ability.PlusCoin(money); // �ɷ� 3-2�� ���� ���� ȹ���� ����
            }

            playerMovement.money += money;
            clearInfor.getMoney += money;
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

        audioManager.TileMapAudio();

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

        audioManager.TileMapAudio();

        events[eventNum].SetActive(false);
        eventUI.SetActive(false);
        playerMovement.moveNum = 1;
        playerMovement.isEvent = false;
    }
}