using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class MapSetting : MonoBehaviour
{
    public int stage; // ���� ��������

    public GameObject[] tiles; // ������ Ÿ��
    public List<GameObject> empyTiles = new List<GameObject>(); // ������ �� Ÿ�� ������Ʈ
    public List<GameObject> restTiles = new List<GameObject>(); // ������ �޽� Ÿ�� ������Ʈ
    public List<GameObject> itemTiles = new List<GameObject>(); // ������ ������ Ÿ�� ������Ʈ
    public List<GameObject> eventTiles = new List<GameObject>(); // ������ �̺�Ʈ Ÿ�� ������Ʈ
    public List<GameObject> shopTiles = new List<GameObject>(); // ������ ���� Ÿ�� ������Ʈ
    public List<GameObject> monsterTiles = new List<GameObject>(); // ������ ���� Ÿ�� ������Ʈ

    public int tileNum; // Ÿ�� ����

    public GameObject[] randomFloorPos; // ���� Ÿ�� ��ġ
    public GameObject[] empyFloorPos; // ���� �� Ÿ�� ��ġ
    public GameObject[] restFloorPos; // ���� �޽� Ÿ�� ��ġ
    public GameObject bossFloorPos; // ���� Ÿ�� ��ġ

    public GameObject empyFloorPrefab;
    public GameObject restFloorPrefab; // �޽� Ÿ�� ������
    public GameObject itemFloorPrefab; // ������ Ÿ�� ������
    public GameObject eventFloorPrefab; // �̺�Ʈ Ÿ�� ������
    public GameObject shopFloorPrefab; // ���� Ÿ�� ������
    public GameObject boss1FloorPrefab; // 1�������� ���� Ÿ�� ������
    public GameObject boss2FloorPrefab; // 2�������� ���� Ÿ�� ������
    public GameObject boss3FloorPrefab; // 2�������� ���� Ÿ�� ������
    public GameObject[] monster1FloorPrefab; // 1�������� ���� Ÿ�� ������
    public GameObject[] monster2FloorPrefab; // 2�������� ���� Ÿ�� ������
    public GameObject[] monster3FloorPrefab; // 3�������� ���� Ÿ�� ������

    public int empyFloorNum; // �� Ÿ�� ����
    public int restFloorNum; // �޽� Ÿ�� ����
    public int itemFloorNum; // ������ Ÿ�� ����
    public int eventFloorNum; // �̺�Ʈ Ÿ�� ����
    public int shopFloorNum; // ���� Ÿ�� ����
    public int monsterFloorNum; // ���� Ÿ�� ����

    void Start()
    {
        StageMapSetting();
    }
     
    public void StageMapSetting()
    {
        // tileNum�� 0���� 4���� �������� ����
        tileNum = Random.Range(0, 5);

        // ���õ� tileNum�� ���� �� Ÿ�� ������ ������ ����
        SelectTile(tileNum);
      
        // ���� �� Ÿ�� ��ġ
        foreach (GameObject position in empyFloorPos)
        {
            Vector3 pos = new Vector3(position.transform.position.x, 1.2f, position.transform.position.z);
            GameObject empy = Instantiate(empyFloorPrefab, pos, Quaternion.identity, position.transform);
            empyTiles.Add(empy);
        }

        // ���� �޽� Ÿ�� ��ġ
        foreach (GameObject position in restFloorPos)
        {
            GameObject rest = Instantiate(restFloorPrefab, position.transform.position, Quaternion.Euler(0, 220, 0), position.transform);
            restTiles.Add(rest);
        }
       // ���� ��ġ�� �����ϴ� ����Ʈ
       List<int> usedIndexes = new List<int>();

       // �� Ÿ�� ��ġ
       for (int i = 0; i < empyFloorNum; i++)
       {
           int randomIndex = GetUniqueRandomIndex(usedIndexes);
           GameObject empy = Instantiate(empyFloorPrefab, randomFloorPos[randomIndex].transform.position, Quaternion.Euler(0, 180, 0), randomFloorPos[randomIndex].transform);
           empyTiles.Add(empy);
       }

       // ������ Ÿ�� ��ġ
       for (int i = 0; i < itemFloorNum; i++)
       {
           int randomIndex = GetUniqueRandomIndex(usedIndexes);
           GameObject item = Instantiate(itemFloorPrefab, randomFloorPos[randomIndex].transform.position, Quaternion.Euler(0, 180, 0), randomFloorPos[randomIndex].transform);
           itemTiles.Add(item);
       }

       // �޽� Ÿ�� ��ġ
       for (int i = 0; i < restFloorNum; i++)
       {
           int randomIndex = GetUniqueRandomIndex(usedIndexes);
           GameObject rest = Instantiate(restFloorPrefab, randomFloorPos[randomIndex].transform.position, Quaternion.Euler(0, 220, 0), randomFloorPos[randomIndex].transform);
           restTiles.Add(rest);
       }

       // �̺�Ʈ Ÿ�� ��ġ
       for (int i = 0; i < eventFloorNum; i++)
       {
           int randomIndex = GetUniqueRandomIndex(usedIndexes);
           Vector3 spawnPosition = randomFloorPos[randomIndex].transform.position + new Vector3(0, 0.5f, 0);
           GameObject events = Instantiate(eventFloorPrefab, spawnPosition, Quaternion.Euler(0, 180, 0), randomFloorPos[randomIndex].transform);
           shopTiles.Add(events);
        }

       // ���� Ÿ�� ��ġ
       for (int i = 0; i < shopFloorNum; i++)
       {
           int randomIndex = GetUniqueRandomIndex(usedIndexes);
           Vector3 spawnPosition = randomFloorPos[randomIndex].transform.position + new Vector3(0, 0.5f, 0);
           GameObject shop = Instantiate(shopFloorPrefab, spawnPosition, Quaternion.Euler(90, 180, 0), randomFloorPos[randomIndex].transform);
           shopTiles.Add(shop);
       }

        // ���� & ���� Ÿ�� ��ġ
        if (stage == 0)
        {
            Instantiate(boss1FloorPrefab, bossFloorPos.transform.position, Quaternion.Euler(0, 180, 0), bossFloorPos.transform);

            for (int i = 0; i < monsterFloorNum; i++)
            {
                int randomIndex = GetUniqueRandomIndex(usedIndexes);
                int randomMonster = Random.Range(0, monster1FloorPrefab.Length);
                GameObject monster = Instantiate(monster1FloorPrefab[randomMonster], randomFloorPos[randomIndex].transform.position, Quaternion.Euler(0, 180, 0), randomFloorPos[randomIndex].transform);
                monsterTiles.Add(monster);
            }
        }
        else if (stage == 1)
        {
            Instantiate(boss2FloorPrefab, bossFloorPos.transform.position, Quaternion.Euler(0, 180, 0), bossFloorPos.transform);

            for (int i = 0; i < monsterFloorNum; i++)
            {
                int randomIndex = GetUniqueRandomIndex(usedIndexes);
                int randomMonster = Random.Range(0, monster2FloorPrefab.Length);
                GameObject monster = Instantiate(monster2FloorPrefab[randomMonster], randomFloorPos[randomIndex].transform.position, Quaternion.Euler(0, 180, 0), randomFloorPos[randomIndex].transform);
                monsterTiles.Add(monster);
            }
        }
        else if (stage == 2)
        {
            Instantiate(boss3FloorPrefab, bossFloorPos.transform.position, Quaternion.Euler(0, 180, 0), bossFloorPos.transform);

            for (int i = 0; i < monsterFloorNum; i++)
            {
                int randomIndex = GetUniqueRandomIndex(usedIndexes);
                int randomMonster = Random.Range(0, monster3FloorPrefab.Length);
                GameObject monster = Instantiate(monster3FloorPrefab[randomMonster], randomFloorPos[randomIndex].transform.position, Quaternion.Euler(0, 180, 0), randomFloorPos[randomIndex].transform);
                monsterTiles.Add(monster);
            }
        }
    }

    public void MapReset()
    {
        foreach (GameObject empy in empyTiles)
        {
            Destroy(empy);
        }
        foreach (GameObject rest in restTiles)
        {
            Destroy(rest);
        }
        foreach (GameObject item in itemTiles)
        {
            Destroy(item);
        }
        foreach (GameObject shop in shopTiles)
        {
            Destroy(shop);
        }
        foreach (GameObject events in eventTiles)
        {
            Destroy(events);
        }
        foreach (GameObject monster in monsterTiles)
        {
            Destroy(monster);
        }

        foreach (GameObject tile in tiles)
        {
            Vector3 currentPos = tile.transform.position;

            tile.transform.position = new Vector3(currentPos.x, -10, currentPos.z);
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

    void SelectTile(int num) // Ÿ�� ����
    {
        switch (num) 
        {
            case 0: // �� 50��
                empyFloorNum = 10;
                restFloorNum = 5;
                itemFloorNum = 5;
                eventFloorNum = 8;
                shopFloorNum = 3;
                monsterFloorNum = 19;
                break;
            case 1:
                empyFloorNum = 15;
                restFloorNum = 4;
                itemFloorNum = 5;
                eventFloorNum = 6;
                shopFloorNum = 3;
                monsterFloorNum = 17;
                break;
            case 2:
                empyFloorNum = 10;
                restFloorNum = 6;
                itemFloorNum = 5;
                eventFloorNum = 9;
                shopFloorNum = 3;
                monsterFloorNum = 17;
                break;
            case 3:
                empyFloorNum = 12;
                restFloorNum = 4;
                itemFloorNum = 4;
                eventFloorNum = 10;
                shopFloorNum = 3;
                monsterFloorNum = 17;
                break;
            case 4:
                empyFloorNum = 10;
                restFloorNum = 7;
                itemFloorNum = 5;
                eventFloorNum = 7;
                shopFloorNum = 4;
                monsterFloorNum = 17;
                break;
            default:
                break;
        }
    }
}
