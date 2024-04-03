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
        else if (monsterMap.chsetMoved)
        {
            spawnPoint = Stage2SpawnPoint;
            boxSize = Stage2BoxSize;
            spawned = true;
        }

        if (spawned)
        {
            if(currentAttack == null)
            {
                if(mapSetting.stage == 1)
                {
                    Stage1SpawnAttack();
                }
                else if (mapSetting.stage == 2)
                {
                    Stage2SpawnAttack();
                }
            }
        }
    }
    void Stage1SpawnAttack()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-boxSize.x / 2, boxSize.x / 2), 0, Random.Range(-boxSize.z / 2, boxSize.z / 2)) + spawnPoint.transform.position;
        currentAttack = Instantiate(p_AttackPrefab, spawnPosition, Quaternion.identity);
        currentAttack.name = "PlayerAttack";

        Destroy(currentAttack, 5f);
    }

    void Stage2SpawnAttack()
    {
        bool isValidPosition = false;
        Vector3 spawnPosition = Vector3.zero;

        // �ִ� �õ� Ƚ���� ���� ���� ������ ������ �ʵ��� �մϴ�.
        int maxAttempts = 100;
        int attempts = 0;

        while (!isValidPosition && attempts < maxAttempts)
        {
            attempts++;
            // Stage2BoxSize �ȿ��� ������ ��ġ ����
            spawnPosition = new Vector3(Random.Range(-boxSize.x / 2, boxSize.x / 2), 0, Random.Range(-boxSize.z / 2, boxSize.z / 2)) + spawnPoint.transform.position;

            // ������ ��ġ�� Stage2ExceptSize �ȿ� �ִ��� Ȯ��
            if (!IsInExclusionZone(spawnPosition))
            {
                isValidPosition = true;
            }
        }

        if (isValidPosition)
        {
            currentAttack = Instantiate(p_AttackPrefab, spawnPosition, Quaternion.identity);
            currentAttack.name = "PlayerAttack";

            Destroy(currentAttack, 5f); // 5�� �Ŀ� ���� ������Ʈ �ı�
        }
    }

    // ������ ��ġ�� ���� ���� �ȿ� �ִ��� �Ǻ��ϴ� �Լ�
    bool IsInExclusionZone(Vector3 position)
    {
        // Stage2ExceptSize�� �������� �߽����� ����մϴ�.
        Vector3 exclusionCenter = spawnPoint.transform.position + new Vector3(0, 0, Stage2ExceptSize.z / 2);

        // ���� ������ ��踦 ����մϴ�.
        float minX = exclusionCenter.x - Stage2ExceptSize.x / 2;
        float maxX = exclusionCenter.x + Stage2ExceptSize.x / 2;
        float minZ = exclusionCenter.z - Stage2ExceptSize.z / 2;
        float maxZ = exclusionCenter.z + Stage2ExceptSize.z / 2;

        // ��ġ�� ���� ���� �ȿ� �ִ��� Ȯ���մϴ�.
        return position.x >= minX && position.x <= maxX && position.z >= minZ && position.z <= maxZ;
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
