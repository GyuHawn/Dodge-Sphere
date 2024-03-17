using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMap : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public GameObject player;
    public bool fireMoved;
    public int monsterNum;

    public GameObject playerFireMapSpawnPos; // �� ���� �� �÷��̾� ���� ����Ʈ 
    public GameObject fireMonsterPrefab; // �� ���� ������
    public GameObject fireMonsterSpawnPos; // �� ���� ���� ����Ʈ

    public GameObject[] cannonPoints; // ���� ��ġ ��ġ

    private void Awake()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void Start()
    {
        if(player == null)
        {
            player = GameObject.Find("Player");
        }

        fireMoved = false;
        monsterNum = 1;
    }

    
    void Update()
    {
        // ���� �� �̵� �غ�
        if(playerMovement.currentTile == 5.1f)
        {
            fireMoved = true;
        }

        // �� �̵� & ���� ��ȯ
        if (fireMoved && monsterNum > 0)
        {
            playerMovement.tile = false;
            playerMovement.game = true;
            StartCoroutine(MoveFireMonsterMap());
            InstallationCannons();
        }
    }

    IEnumerator MoveFireMonsterMap()
    {
        monsterNum = -1;
        yield return new WaitForSeconds(2f);

        player.transform.position = playerFireMapSpawnPos.transform.position;

        Vector3 monsterPos = new Vector3(fireMonsterSpawnPos.transform.position.x, -1.35f, fireMonsterSpawnPos.transform.position.z);
        GameObject monster = Instantiate(fireMonsterPrefab, monsterPos, Quaternion.Euler(0, 180, 0));
        monster.name = "FireMonster";
    }

    void InstallationCannons()
    {
        int cannonIndex = 0;
        int cannonY = 0;
        foreach (GameObject cannon in playerMovement.cannons)
        {
            Vector3 cannonPos = new Vector3(cannonPoints[cannonIndex].transform.position.x, 2.5f, cannonPoints[0].transform.position.z);
            if(cannonIndex == 0)
            {
                cannonY = 45; 
            }
            else if(cannonIndex == 1)
            {
                cannonY = 135; 
            }
            GameObject p_cannon = Instantiate(cannon, cannonPos, Quaternion.Euler(0, cannonY, 0));
            p_cannon.name = "PlayerCannon";
            cannonIndex++;
        }       
    }
}
