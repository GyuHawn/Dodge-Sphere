using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class MapSetting : MonoBehaviour
{
    public int tileNum; // Ÿ�� ����

    public GameObject[] randomFloorPos; // ���� Ÿ�� ��ġ
    public GameObject[] restFloorPos; // ���� �޽� Ÿ�� ��ġ
    public GameObject bossFloorPos; // ���� Ÿ�� ��ġ

    public GameObject restFloorPrefab; // �޽� Ÿ�� ������
    public GameObject itemFloorPrefab; // ������ Ÿ�� ������
    public GameObject eventFloorPrefab; // �̺�Ʈ Ÿ�� ������
    public GameObject shopFloorPrefab; // ���� Ÿ�� ������
    public GameObject[] bossFloorPrefab; // ���� Ÿ�� ������
    public GameObject[] monsterFloorPrefab; // ���� Ÿ�� ������

    public int empyFloorNum; // �� Ÿ�� ����
    public int restFloorNum; // �޽� Ÿ�� ����
    public int itemFloorNum; // ������ Ÿ�� ����
    public int eventFloorNum; // �̺�Ʈ Ÿ�� ����
    public int shopFloorNum; // ���� Ÿ�� ����
    public int monsterFloorNum; // ���� Ÿ�� ����

    void Start()
    {
        // tileNum�� 0���� 4���� �������� ����
        tileNum = Random.Range(0, 5);

        // ���õ� tileNum�� ���� �� Ÿ�� ������ ������ ����
        SelectTile(tileNum);

        // ���� Ÿ�� ��ġ
        Instantiate(bossFloorPrefab[0], bossFloorPos.transform.position, Quaternion.Euler(0, 180, 0), bossFloorPos.transform);

        // ���� �޽� Ÿ�� ��ġ
        foreach (GameObject position in restFloorPos)
        {
            Instantiate(restFloorPrefab, position.transform.position, Quaternion.Euler(0, 220, 0), position.transform);
        }

        // ���� ��ġ�� �����ϴ� ����Ʈ
        List<int> usedIndexes = new List<int>();

        // ������ Ÿ�� ��ġ
        for (int i = 0; i < itemFloorNum; i++)
        {
            int randomIndex = GetUniqueRandomIndex(usedIndexes);
            Instantiate(itemFloorPrefab, randomFloorPos[randomIndex].transform.position, Quaternion.Euler(0, 180, 0), randomFloorPos[randomIndex].transform);
        }

        // �޽� Ÿ�� ��ġ
        for (int i = 0; i < restFloorNum; i++)
        {
            int randomIndex = GetUniqueRandomIndex(usedIndexes);
            Instantiate(restFloorPrefab, randomFloorPos[randomIndex].transform.position, Quaternion.Euler(0, 220, 0), randomFloorPos[randomIndex].transform);
        }

        // �̺�Ʈ Ÿ�� ��ġ
        for (int i = 0; i < eventFloorNum; i++)
        {
            int randomIndex = GetUniqueRandomIndex(usedIndexes);
            Vector3 spawnPosition = randomFloorPos[randomIndex].transform.position + new Vector3(0, 0.5f, 0);
            Instantiate(eventFloorPrefab, spawnPosition, Quaternion.Euler(0, 180, 0), randomFloorPos[randomIndex].transform);
        }

        // ���� Ÿ�� ��ġ
        for (int i = 0; i < shopFloorNum; i++)
        {
            int randomIndex = GetUniqueRandomIndex(usedIndexes);
            Vector3 spawnPosition = randomFloorPos[randomIndex].transform.position + new Vector3(0, 0.5f, 0);
            Instantiate(shopFloorPrefab, spawnPosition, Quaternion.Euler(90, 180, 0), randomFloorPos[randomIndex].transform);
        }

        // ���� Ÿ�� ��ġ
        for (int i = 0; i < monsterFloorNum; i++)
        {
            int randomIndex = GetUniqueRandomIndex(usedIndexes);
            Instantiate(monsterFloorPrefab[0], randomFloorPos[randomIndex].transform.position, Quaternion.Euler(0, 180, 0), randomFloorPos[randomIndex].transform);
        }
    }

    // �ߺ����� �ʴ� ���� �ε��� ��������
    int GetUniqueRandomIndex(List<int> usedIndexes)
    {
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, randomFloorPos.Length);
        } while (usedIndexes.Contains(randomIndex));

        usedIndexes.Add(randomIndex);
        return randomIndex;
    }

    void Update()
    {
        
    }

    void SelectTile(int num) // Ÿ�� ����
    {
        switch (num) 
        {
            case 0: // �� 50��
                // empyFloorNum = 15;
                restFloorNum = 5;
                itemFloorNum = 5;
                eventFloorNum = 8;
                shopFloorNum = 3;
                monsterFloorNum = 14;
                break;
            case 1:
                // empyFloorNum = 20;
                restFloorNum = 4;
                itemFloorNum = 5;
                eventFloorNum = 6;
                shopFloorNum = 3;
                monsterFloorNum = 12;
                break;
            case 2:
                 // empyFloorNum = 10;
                restFloorNum = 6;
                itemFloorNum = 5;
                eventFloorNum = 9;
                shopFloorNum = 3;
                monsterFloorNum = 17;
                break;
            case 3:
                // empyFloorNum = 12;
                restFloorNum = 4;
                itemFloorNum = 4;
                eventFloorNum = 15;
                shopFloorNum = 3;
                monsterFloorNum = 12;
                break;
            case 4:
                // empyFloorNum = 12;
                restFloorNum = 7;
                itemFloorNum = 5;
                eventFloorNum = 10;
                shopFloorNum = 4;
                monsterFloorNum = 12;
                break;
            default:
                break;
        }
    }
}
