using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMap : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private GameSetting gameSetting;

    public GameObject player;
    public bool fireMoved; // �� ���� ����
    public bool cactusMoved; // ������ ���� ����
    public bool mushMoved; // ���� ���� ����
    public int monsterNum; // ���� ��

    public GameObject monster; // ���� ����

    // �� & ������ ����
    public GameObject playerNomalMapSpawnPos; // ���� �� �÷��̾� ���� ����Ʈ 
    public GameObject nomalMonsterSpawnPos; // ���� ���� ����Ʈ

    public GameObject fireMonsterPrefab; // �� ���� ������
    public GameObject cactusMonsterPrefab; // ������ ���� ������
    public GameObject[] mushMonsterPrefab; // ���� ���� ������


    // ����
    public GameObject[] cannonPoints; // ���� ��ġ ��ġ
    public List<GameObject> cannons;

    private void Awake()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        gameSetting = GameObject.Find("Manager").GetComponent<GameSetting>();
    }

    void Start()
    {     
        fireMoved = false;
        cactusMoved = false;
        mushMoved = false;
        monsterNum = 1;
    }
  
    void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }

        // ���� �� �̵� �غ�
        if (playerMovement.currentTile == 5.1f) // �� ����
        {
            fireMoved = true;
        }
        else if (playerMovement.currentTile == 5.2f) // ������ ����
        {
            cactusMoved = true;
        }
        else if (playerMovement.currentTile == 5.3f) // ���� ����
        {
            mushMoved = true;
        }

        // �� �̵� & ���� ��ȯ
        if (fireMoved && monsterNum > 0) // �� ����
        {
            playerMovement.OnGame();
            StartCoroutine(MoveFireMonsterMap());
            InstallationCannons();   
        }
        else if (cactusMoved && monsterNum > 0) // ������ ����
        {
            playerMovement.OnGame();
            StartCoroutine(MoveCactusMonsterMap());
            InstallationCannons();
        }
        else if (mushMoved && monsterNum > 0) // ���� ����
        {
            playerMovement.OnGame();
            StartCoroutine(MoveMushMonsterMap());
            InstallationCannons();
        }
    }

    IEnumerator MoveFireMonsterMap()
    {
        monsterNum--;
        yield return new WaitForSeconds(2f);

        player.transform.position = playerNomalMapSpawnPos.transform.position;

        Vector3 monsterPos = new Vector3(nomalMonsterSpawnPos.transform.position.x, 0f, nomalMonsterSpawnPos.transform.position.z);
        monster = Instantiate(fireMonsterPrefab, monsterPos, Quaternion.Euler(0, 180, 0));
        monster.name = "FireMonster";
    }
    IEnumerator MoveCactusMonsterMap()
    {
        monsterNum--;
        yield return new WaitForSeconds(2f);

        player.transform.position = playerNomalMapSpawnPos.transform.position;

        Vector3 monsterPos = new Vector3(nomalMonsterSpawnPos.transform.position.x, 0f, nomalMonsterSpawnPos.transform.position.z);
        monster = Instantiate(cactusMonsterPrefab, monsterPos, Quaternion.Euler(0, 180, 0));
        monster.name = "CactusMonster";
    }
    IEnumerator MoveMushMonsterMap()
    {
        monsterNum--;
        yield return new WaitForSeconds(2f);

        player.transform.position = playerNomalMapSpawnPos.transform.position;

        Vector3 monsterPos = new Vector3(nomalMonsterSpawnPos.transform.position.x, 0f, nomalMonsterSpawnPos.transform.position.z);
        monster = Instantiate(fireMonsterPrefab, monsterPos, Quaternion.Euler(0, 180, 0));
        monster.name = "FireMonster";
    }


    void InstallationCannons()
    {
        int cannonIndex = 0;
        int cannonY = 0;
        foreach (GameObject cannon in gameSetting.cannons)
        {
            Vector3 cannonPos = new Vector3(cannonPoints[cannonIndex].transform.position.x, 2.2f, cannonPoints[0].transform.position.z);
            if(cannonIndex == 0)
            {
                cannonY = 65; 
            }
            else if(cannonIndex == 1)
            {
                cannonY = 115; 
            }
            GameObject p_cannon = Instantiate(cannon, cannonPos, Quaternion.Euler(0, cannonY, 0));
            cannons.Add(p_cannon);
            p_cannon.name = "PlayerCannon";
            cannonIndex++;
        }       
    }

    public void DeleteCannon()
    {
        foreach(GameObject cannon in cannons)
        {
            Destroy(cannon);
        }
    }
}
