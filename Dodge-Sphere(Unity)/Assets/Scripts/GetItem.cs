using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerItem playerItem;

    public int itemNum; // ȹ�� ������ ��
    public bool onItem; // ȹ�� ���� ����

    public GameObject[] items; // ������
    public GameObject getItemUI; // ������ ȹ�� UI
    public Transform getPos; // ������ ǥ�� ��ġ

    // ������ Ȯ��
    private Dictionary<GameObject, int> itemProbabilities = new Dictionary<GameObject, int>();

    private void Awake()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerItem = GameObject.Find("Manager").GetComponent<PlayerItem>();

        // �����۰� Ȯ���� ��ųʸ��� �߰�
        itemProbabilities.Add(items[0], 1); // crown 
        itemProbabilities.Add(items[1], 2); // pick 
        itemProbabilities.Add(items[2], 3); // jewel 
        itemProbabilities.Add(items[3], 3); // fish 
        itemProbabilities.Add(items[4], 3); // necklace 
        itemProbabilities.Add(items[5], 4); // bag 
        itemProbabilities.Add(items[6], 4); // mushroom 
        itemProbabilities.Add(items[7], 4); // shield 
        itemProbabilities.Add(items[8], 4); // sowrd 
        itemProbabilities.Add(items[9], 5); // bow 
        itemProbabilities.Add(items[10], 5); // dagger 
        itemProbabilities.Add(items[11], 5); // gold 
        itemProbabilities.Add(items[12], 5); // grow 
        itemProbabilities.Add(items[13], 5); // skull 
        itemProbabilities.Add(items[14], 7); // bone 
        itemProbabilities.Add(items[15], 7); // book 
        itemProbabilities.Add(items[16], 7); // hood 
        itemProbabilities.Add(items[17], 8); // ring 
        itemProbabilities.Add(items[18], 9); // arrow 
        itemProbabilities.Add(items[19], 9); // coin        
    }

    void Update()
    {
        if (playerMovement.currentTile == 2f)
        {
            onItem = true;
            itemNum = 1;
        }

        if (onItem && itemNum > 0)
        {
            SelectItem();
            onItem = false;
            itemNum--;
        }
    }

    public void SelectItem()
    {
        getItemUI.SetActive(true);

        // Ȯ���� ���� ������ ����
        GameObject selectedItem = ChooseItem();

        if (selectedItem != null)
        {
            // �������� null�� �ƴ� ��쿡�� �������� �����մϴ�.
            GameObject newItem = Instantiate(selectedItem, getPos.position, Quaternion.identity);

            // �ߺ��� ������ ����
            RemoveItem(selectedItem);
        }
        else
        {
            Debug.LogError("Selected item is null."); // ���� ���� ����
        }
    }

    private GameObject ChooseItem()
    {
        // �� Ȯ�� �� ���
        int totalProb = 0;
        foreach (var pair in itemProbabilities)
        {
            totalProb += pair.Value;
        }

        // ������ ���� �������� ������ ����
        int randomValue = Random.Range(1, totalProb + 1);
        int cumulativeProb = 0;
        foreach (var pair in itemProbabilities)
        {
            cumulativeProb += pair.Value;
            if (randomValue <= cumulativeProb)
            {
                return pair.Key;
            }
        }

        // �������� �������� ���� ��� null�� ��ȯ�մϴ�.
        return null;
    }

    private void RemoveItem(GameObject item)
    {
        // ���õ� �������� Ȯ������ ����
        itemProbabilities.Remove(item);

        // Ȯ���� ��迭
        ResetPercent();
    }

    public void ResetPercent()
    {
        // ��ųʸ� Ű�� ���纻�� ���� ��ųʸ��� �ݺ��ϴ� ���� ������ �����մϴ�.
        List<GameObject> keys = new List<GameObject>(itemProbabilities.Keys);

        // Ȯ���� �����ϱ� ���� ���ذ��� �����մϴ�.
        int baseProbability = 0;

        // �������� Ȯ���� ���ذ��� ���ذ��鼭 �����մϴ�.
        foreach (var key in keys)
        {
            baseProbability += itemProbabilities[key];
        }

        // �� �����ۿ� ���ο� Ȯ���� �����մϴ�.
        foreach (var key in keys)
        {
            itemProbabilities[key] = baseProbability / itemProbabilities[key];
        }
    }

}
