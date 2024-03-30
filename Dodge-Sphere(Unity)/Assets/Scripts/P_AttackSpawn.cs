using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_AttackSpawn : MonoBehaviour
{
    private MonsterMap monsterMap;

    // �� ����
    public GameObject f_SpawnPoint;
    public Vector3 f_BoxSize;

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
    }

    void Start()
    {
        spawned = false;
        currentAttack = null;
    }

    void Update()
    {
        if (monsterMap.fireMoved || monsterMap.cactusMoved)
        {
            spawnPoint = f_SpawnPoint;
            boxSize = f_BoxSize;
            spawned = true;
        }

        if (spawned)
        {
            if(currentAttack == null)
            {
                SpawnAttack();
            }
        }
    }
    void SpawnAttack()
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
