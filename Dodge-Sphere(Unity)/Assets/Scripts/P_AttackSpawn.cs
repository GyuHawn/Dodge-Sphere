using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_AttackSpawn : MonoBehaviour
{
    private MonsterMap monsterMap;
    private MapSetting mapSetting;

    // Stage1
    public GameObject Stage1SpawnPoint;
    public Vector3 Stage1BoxSize;
    // Stage2
    public GameObject Stage2SpawnPoint;
    public Vector3 Stage2BoxSize;
    public Vector3 Stage2ExceptSize;

    // ���� ���� ��ġ
    public GameObject p_AttackPrefab; // �÷��̾� ���� ������
    public GameObject spawnPoint; // ���� ���� ��ġ
    public Vector3 boxSize; // ���� ����

    // ������ ���� ����
    public bool spawned;

    public GameObject currentAttack; // ���� ������ p_Attack�� ���۷���

    private void Awake()
    {
        monsterMap = GameObject.Find("Manager").GetComponent<MonsterMap>();
        mapSetting = GameObject.Find("Manager").GetComponent<MapSetting>();
    }

    void Start()
    {
        spawned = false;
        currentAttack = null;
    }

    void Update()
    {
        if (monsterMap.fireMoved || monsterMap.cactusMoved || monsterMap.mushMoved)
        {
            spawnPoint = Stage1SpawnPoint;
            boxSize = Stage1BoxSize;
            spawned = true;
        }
        else if (monsterMap.chsetMoved || monsterMap.beholderMoved || monsterMap.clownMoved)
        {
            spawnPoint = Stage2SpawnPoint;
            boxSize = Stage2BoxSize;
            spawned = true;
        }

        if (spawned)
        {
            if (currentAttack == null)
            {
                StageSpawnAttack();
            }
        }
    }
    void StageSpawnAttack()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-boxSize.x / 2, boxSize.x / 2), 0, Random.Range(-boxSize.z / 2, boxSize.z / 2)) + spawnPoint.transform.position;
        currentAttack = Instantiate(p_AttackPrefab, spawnPosition, Quaternion.identity);
        currentAttack.name = "PlayerAttack";

        Destroy(currentAttack, 5f);
    }

    /*private void OnDrawGizmos()
    {
        if(spawnPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(spawnPoint.transform.position, boxSize);
        }
    }*/
}
