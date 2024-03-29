using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopScript : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private GetItem getItem;

    public GameObject shopUI;
    public GameObject[] shopSolts; // �Ǹ� ������ ���� 
    public List<GameObject> shopItems = new List<GameObject>();
    public List<int> itemAmount = new List<int>(); // �Ǹ� �ݾ�
    public TMP_Text[] amountText;

    public bool reroll;
    public GameObject itemReroll;
    public int rerollNum;

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
        if (reroll && rerollNum == 1)
        {
            itemReroll.SetActive(true);
        }
        else
        {
            itemReroll.SetActive(false);
        }
    }

    public void ItemSetting()
    {
        List<int> selectedItems = new List<int>(); // ���õ� ��ȣ ���� ����Ʈ

        for (int i = 0; i < shopSolts.Length; i++)
        {
            int itemNum;
            do
            {
                itemNum = Random.Range(0, getItem.items.Count); // ������ ��ȣ ���� ����
            }
            while (selectedItems.Contains(itemNum)); // ���õ� ��ȣ ����Ʈ�� ������ �ٽ� ����

            selectedItems.Add(itemNum); // ���õ� ��ȣ�� ����Ʈ�� �߰�

            // ������ �ν��Ͻ�ȭ �� ����
            GameObject item = Instantiate(getItem.items[itemNum], Vector3.zero, Quaternion.identity);
            item.transform.SetParent(shopSolts[i].transform, false);
            item.name = getItem.items[itemNum].name;
            shopItems.Add(item);
        }

        AmountSetting();

        selectedItems.Clear();
    }

    void AmountSetting()
    {
        int min = 700 / 50;
        int max = 1500 / 50;

        for(int i  = 0; i < amountText.Length; i++)
        {
            int amount = Random.Range(min, max + 1) * 50; // 50������ �� ����
            itemAmount.Add(amount);
            amountText[i].text = itemAmount[i].ToString();
        }

    }

    public void BuySolt1()
    {
        shopSolts[0].SetActive(false);
        playerMovement.money -= itemAmount[0];
        getItem.OnItem(shopItems[0].name);
    }
    public void BuySolt2()
    {
        shopSolts[1].SetActive(false);
        playerMovement.money -= itemAmount[1];
        getItem.OnItem(shopItems[1].name);

    }
    public void BuySolt3()
    {
        shopSolts[2].SetActive(false);
        playerMovement.money -= itemAmount[2];
        getItem.OnItem(shopItems[2].name);
    }
    public void BuySolt4()
    {
        shopSolts[3].SetActive(false);
        playerMovement.money -= itemAmount[3];
        getItem.OnItem(shopItems[3].name);
    }
    public void BuySolt5()
    {
        shopSolts[4].SetActive(false);
        playerMovement.money -= itemAmount[4];
        getItem.OnItem(shopItems[4].name);
    }
    public void BuySolt6()
    {
        shopSolts[5].SetActive(false);
        playerMovement.money -= itemAmount[5];
        getItem.OnItem(shopItems[5].name);
    }


    public void Reroll()
    {
        rerollNum--;
        ResetSetting();
        ItemSetting();
    }

    void ResetSetting()
    {
        foreach (GameObject shopItem in shopItems)
        {
            Destroy(shopItem);
        }

        for (int i = 0; i < itemAmount.Count; i++)
        {
            itemAmount.Clear();
        }

        for (int j = 0 ; j < shopSolts.Length; j++)
        {
            shopSolts[j].SetActive(true);
        }
    }

    public void Exit()
    {
        ResetSetting();
        playerMovement.moveNum = 1;
        shopUI.SetActive(false);
    }
}