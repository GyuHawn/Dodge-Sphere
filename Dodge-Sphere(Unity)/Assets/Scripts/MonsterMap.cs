using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMap : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private GameSetting gameSetting;
    private MapSetting mapSetting;
    private AudioManager audioManager;

    public GameObject player;
    public bool fireMoved; // �� ���� ����
    public bool cactusMoved; // ������ ���� ����
    public bool mushMoved; // ���� ���� ����
    public bool clownMoved; // ���� ���� ����
    public bool chestMoved; // ���� ���� ����
    public bool beholderMoved; // �ֽ��� ���� ����
    public int monsterNum; // ���� ��

    public GameObject monster; // ���� ����

    public GameObject[] playerMapSpawnPos; // ���� �� �÷��̾� ���� ����Ʈ / ��������[1, 2]

    // �� & ������ ���� (�� ����)
    public GameObject[] nomalMonsterSpawnPos; // ���� ���� ����Ʈ / ��������[1, 2]

    // ���� ���� (�� ����)
    public GameObject[] TwinsMonsterSpawnPos; // ���� ���� ����Ʈ

    public GameObject fireMonsterPrefab; // �� ���� ������
    public GameObject cactusMonsterPrefab; // ������ ���� ������
    public GameObject[] mushMonsterPrefab; // ���� ���� ������
    public GameObject clownMonsterPrefab; // ���� ���� ������
    public GameObject chestMonsterPrefab; // ���� ���� ������
    public GameObject beholderMonsterPrefab; // �ֽ��� ���� ������

    // ����
    public GameObject[] cannonPoints; // ���� ��ġ ��ġ / ��������[1, 1, 2, 2]
    public List<GameObject> cannons;


    private void Awake()
    {
        gameSetting = GameObject.Find("Manager").GetComponent<GameSetting>();
        mapSetting = GameObject.Find("Manager").GetComponent<MapSetting>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Start()
    {     
        fireMoved = false;
        cactusMoved = false;
        mushMoved = false;
        clownMoved = false;
        chestMoved = false;
        beholderMoved = false;
        monsterNum = 1;
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
            playerMovement = player.GetComponent<PlayerMovement>();
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
        else if (playerMovement.currentTile == 5.6f) // ���� ����
        {
            clownMoved = true;
        }
        else if (playerMovement.currentTile == 5.4f) // ���� ����
        {
            chestMoved = true;
        }
        else if (playerMovement.currentTile == 5.5f) // �ֽ��� ����
        {
            beholderMoved = true;
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
        else if (clownMoved && monsterNum > 0) // ���� ����
        {
            playerMovement.OnGame();
            StartCoroutine(MoveClownMonsterMap());
            InstallationCannons();
        }
        else if (chestMoved && monsterNum > 0) // ���� ����
        {
            playerMovement.OnGame();
            StartCoroutine(MoveChestMonsterMap());
            InstallationCannons();
        }
        else if (beholderMoved && monsterNum > 0) // �ֽ��� ����
        {
            playerMovement.OnGame();
            StartCoroutine(MoveBeholderMonsterMap());
            InstallationCannons();
        }
    }

    // 1�������� ����
    IEnumerator MoveFireMonsterMap()
    {
        monsterNum--;
        yield return new WaitForSeconds(2f);

        audioManager.BossAudio();

        player.transform.position = playerMapSpawnPos[0].transform.position;

        Vector3 monsterPos = new Vector3(nomalMonsterSpawnPos[0].transform.position.x, 0f, nomalMonsterSpawnPos[0].transform.position.z);
        monster = Instantiate(fireMonsterPrefab, monsterPos, Quaternion.Euler(0, 180, 0));
        monster.name = "FireMonster";
        audioManager.SpwanAudio();
    }
    IEnumerator MoveCactusMonsterMap()
    {
        monsterNum--;
        yield return new WaitForSeconds(2f);

        audioManager.MonsterAudio();

        player.transform.position = playerMapSpawnPos[0].transform.position;

        Vector3 monsterPos = new Vector3(nomalMonsterSpawnPos[0].transform.position.x, 1f, nomalMonsterSpawnPos[0].transform.position.z);
        monster = Instantiate(cactusMonsterPrefab, monsterPos, Quaternion.Euler(0, 180, 0));
        monster.name = "CactusMonster";
        audioManager.SpwanAudio();
    }
    IEnumerator MoveMushMonsterMap()
    {
        monsterNum--;
        yield return new WaitForSeconds(2f);

        audioManager.MonsterAudio();

        player.transform.position = playerMapSpawnPos[0].transform.position;

        for(int i = 0;  i < mushMonsterPrefab.Length; i++)
        {
            if(i == 0)
            {
                Vector3 monsterPos = new Vector3(TwinsMonsterSpawnPos[i].transform.position.x, 1f, TwinsMonsterSpawnPos[i].transform.position.z);
                monster = Instantiate(mushMonsterPrefab[i], monsterPos, Quaternion.Euler(0, 150, 0));
                monster.name = "AngryMonster";
            }
            else if(i == 1)
            {
                Vector3 monsterPos = new Vector3(TwinsMonsterSpawnPos[i].transform.position.x, 1f, TwinsMonsterSpawnPos[i].transform.position.z);
                monster = Instantiate(mushMonsterPrefab[i], monsterPos, Quaternion.Euler(0, 210, 0));
                monster.name = "SmileMonster";
            }
            audioManager.SpwanAudio();
        }
    }

    // 2�������� ����
    IEnumerator MoveClownMonsterMap()
    {
        monsterNum--;
        yield return new WaitForSeconds(2f);

        audioManager.BossAudio();

        player.transform.position = playerMapSpawnPos[1].transform.position;

        Vector3 monsterPos = new Vector3(nomalMonsterSpawnPos[1].transform.position.x, 1f, nomalMonsterSpawnPos[1].transform.position.z);
        monster = Instantiate(clownMonsterPrefab, monsterPos, Quaternion.Euler(0, 180, 0));
        monster.name = "ClownMonster";
        audioManager.SpwanAudio();
    }
    IEnumerator MoveChestMonsterMap()
    {
        monsterNum--;
        yield return new WaitForSeconds(2f);

        audioManager.MonsterAudio();

        player.transform.position = playerMapSpawnPos[1].transform.position;

        Vector3 monsterPos = new Vector3(nomalMonsterSpawnPos[1].transform.position.x, 1f, nomalMonsterSpawnPos[1].transform.position.z);
        monster = Instantiate(chestMonsterPrefab, monsterPos, Quaternion.Euler(0, 180, 0));
        monster.name = "ChestMonster";
        audioManager.SpwanAudio();
    }
    IEnumerator MoveBeholderMonsterMap()
    {
        monsterNum--;
        yield return new WaitForSeconds(2f);

        audioManager.MonsterAudio();

        player.transform.position = playerMapSpawnPos[1].transform.position;

        Vector3 monsterPos = new Vector3(nomalMonsterSpawnPos[1].transform.position.x, -1.5f, nomalMonsterSpawnPos[1].transform.position.z);
        monster = Instantiate(beholderMonsterPrefab, monsterPos, Quaternion.Euler(0, 180, 0));
        monster.name = "BeholderMonster";
        audioManager.SpwanAudio();
    }

    void InstallationCannons()
    {
        int cannonIndex = 0;
        int cannonY = 0;
        if (mapSetting.stage == 1)
        {
            cannonIndex = 0;
        }
        else if(mapSetting.stage == 2)
        {
            cannonIndex = 2;
        }

        foreach (GameObject cannon in gameSetting.cannons)
        {
            Vector3 cannonPos = new Vector3(cannonPoints[cannonIndex].transform.position.x, 2f, cannonPoints[cannonIndex].transform.position.z);
            if(cannonIndex == 0)
            {
                cannonY = 65; 
            }
            else if(cannonIndex == 1)
            {
                cannonY = 115; 
            }
            else if (cannonIndex == 2)
            {
                cannonY = 55;
            }
            else if (cannonIndex == 3)
            {
                cannonY = 125;
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
