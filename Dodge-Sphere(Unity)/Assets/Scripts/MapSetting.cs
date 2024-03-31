using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class MapSetting : MonoBehaviour
{
    public int stage; // 현재 스테이지

    public int tileNum; // 타일 선택

    public GameObject[] randomFloorPos; // 랜덤 타일 위치
    public GameObject[] empyFloorPos; // 고정 빈 타일 위치
    public GameObject[] restFloorPos; // 고정 휴식 타일 위치
    public GameObject bossFloorPos; // 보스 타일 위치

    public GameObject empyFloorPrefab;
    public GameObject restFloorPrefab; // 휴식 타일 프리팹
    public GameObject itemFloorPrefab; // 아이템 타일 프리팹
    public GameObject eventFloorPrefab; // 이벤트 타일 프리팹
    public GameObject shopFloorPrefab; // 상점 타일 프리팹
    public GameObject boss1FloorPrefab; // 1스테이지 보스 타일 프리팹
    public GameObject boss2FloorPrefab; // 2스테이지 보스 타일 프리팹
    public GameObject boss3FloorPrefab; // 2스테이지 보스 타일 프리팹
    public GameObject[] monster1FloorPrefab; // 1스테이지 몬스터 타일 프리팹
    public GameObject[] monster2FloorPrefab; // 2스테이지 몬스터 타일 프리팹
    public GameObject[] monster3FloorPrefab; // 3스테이지 몬스터 타일 프리팹

    public int empyFloorNum; // 빈 타일 개수
    public int restFloorNum; // 휴식 타일 개수
    public int itemFloorNum; // 아이템 타일 개수
    public int eventFloorNum; // 이벤트 타일 개수
    public int shopFloorNum; // 상점 타일 개수
    public int monsterFloorNum; // 몬스터 타일 개수

    void Start()
    {
        StageMapSetting();
    }
     
    public void StageMapSetting()
    {
        // tileNum을 0부터 4까지 랜덤으로 선택
        tileNum = Random.Range(0, 5);

        // 선택된 tileNum에 따라 각 타일 종류의 개수를 설정
        SelectTile(tileNum);

        // 고정 빈 타일 설치
        foreach (GameObject position in empyFloorPos)
        {
            Vector3 pos = new Vector3(position.transform.position.x, 1.2f, position.transform.position.z);
            Instantiate(empyFloorPrefab, pos, Quaternion.identity, position.transform);
        }

        // 고정 휴식 타일 설치
        foreach (GameObject position in restFloorPos)
        {
            Instantiate(restFloorPrefab, position.transform.position, Quaternion.Euler(0, 220, 0), position.transform);
        }

        // 사용된 위치를 저장하는 리스트
        List<int> usedIndexes = new List<int>();

        // 빈 타일 설치
        for (int i = 0; i < empyFloorNum; i++)
        {
            int randomIndex = GetUniqueRandomIndex(usedIndexes);
            Instantiate(empyFloorPrefab, randomFloorPos[randomIndex].transform.position, Quaternion.Euler(0, 180, 0), randomFloorPos[randomIndex].transform);
        }

        // 아이템 타일 설치
        for (int i = 0; i < itemFloorNum; i++)
        {
            int randomIndex = GetUniqueRandomIndex(usedIndexes);
            Instantiate(itemFloorPrefab, randomFloorPos[randomIndex].transform.position, Quaternion.Euler(0, 180, 0), randomFloorPos[randomIndex].transform);
        }

        // 휴식 타일 설치
        for (int i = 0; i < restFloorNum; i++)
        {
            int randomIndex = GetUniqueRandomIndex(usedIndexes);
            Instantiate(restFloorPrefab, randomFloorPos[randomIndex].transform.position, Quaternion.Euler(0, 220, 0), randomFloorPos[randomIndex].transform);
        }

        // 이벤트 타일 설치
        for (int i = 0; i < eventFloorNum; i++)
        {
            int randomIndex = GetUniqueRandomIndex(usedIndexes);
            Vector3 spawnPosition = randomFloorPos[randomIndex].transform.position + new Vector3(0, 0.5f, 0);
            Instantiate(eventFloorPrefab, spawnPosition, Quaternion.Euler(0, 180, 0), randomFloorPos[randomIndex].transform);
        }

        // 상점 타일 설치
        for (int i = 0; i < shopFloorNum; i++)
        {
            int randomIndex = GetUniqueRandomIndex(usedIndexes);
            Vector3 spawnPosition = randomFloorPos[randomIndex].transform.position + new Vector3(0, 0.5f, 0);
            Instantiate(shopFloorPrefab, spawnPosition, Quaternion.Euler(90, 180, 0), randomFloorPos[randomIndex].transform);
        }

        // 보스 & 몬스터 타일 설치
        if (stage == 0)
        {
            Instantiate(boss1FloorPrefab, bossFloorPos.transform.position, Quaternion.Euler(0, 180, 0), bossFloorPos.transform);

            for (int i = 0; i < monsterFloorNum; i++)
            {
                int randomIndex = GetUniqueRandomIndex(usedIndexes);
                int randomMonster = Random.Range(0, monster1FloorPrefab.Length);
                Instantiate(monster1FloorPrefab[randomMonster], randomFloorPos[randomIndex].transform.position, Quaternion.Euler(0, 180, 0), randomFloorPos[randomIndex].transform);
            }
        }
        else if (stage == 1)
        {
            Instantiate(boss2FloorPrefab, bossFloorPos.transform.position, Quaternion.Euler(0, 180, 0), bossFloorPos.transform);

            for (int i = 0; i < monsterFloorNum; i++)
            {
                int randomIndex = GetUniqueRandomIndex(usedIndexes);
                int randomMonster = Random.Range(0, monster2FloorPrefab.Length);
                Instantiate(monster2FloorPrefab[randomMonster], randomFloorPos[randomIndex].transform.position, Quaternion.Euler(0, 180, 0), randomFloorPos[randomIndex].transform);
            }
        }
        else if (stage == 2)
        {
            Instantiate(boss3FloorPrefab, bossFloorPos.transform.position, Quaternion.Euler(0, 180, 0), bossFloorPos.transform);

            for (int i = 0; i < monsterFloorNum; i++)
            {
                int randomIndex = GetUniqueRandomIndex(usedIndexes);
                int randomMonster = Random.Range(0, monster3FloorPrefab.Length);
                Instantiate(monster3FloorPrefab[randomMonster], randomFloorPos[randomIndex].transform.position, Quaternion.Euler(0, 180, 0), randomFloorPos[randomIndex].transform);
            }
        }
    }

    // 중복되지 않는 랜덤 인덱스 가져오기
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

    void SelectTile(int num) // 타일 선택
    {
        switch (num) 
        {
            case 0: // 총 50개
                empyFloorNum = 15;
                restFloorNum = 5;
                itemFloorNum = 5;
                eventFloorNum = 8;
                shopFloorNum = 3;
                monsterFloorNum = 14;
                break;
            case 1:
                empyFloorNum = 20;
                restFloorNum = 4;
                itemFloorNum = 5;
                eventFloorNum = 6;
                shopFloorNum = 3;
                monsterFloorNum = 12;
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
                eventFloorNum = 15;
                shopFloorNum = 3;
                monsterFloorNum = 12;
                break;
            case 4:
                empyFloorNum = 12;
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
