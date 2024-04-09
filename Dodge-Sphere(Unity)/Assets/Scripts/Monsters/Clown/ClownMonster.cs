using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class ClownMonster : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private MonsterMap monsterMap;
    private P_AttackSpawn p_AttackSpawn;
    private CameraMovement cameraMovement;
    private GetMoney getMoney;
    private MapSetting mapSetting;

    // �⺻ ����
    public int maxHealth;
    public int currentHealth;
    public int money;

    public GameObject baseAttackPrefab; // �Ѿ� ������

    // �б� ����
    public GameObject[] pushPos1;
    public GameObject[] pushPos2;
    public float p_AttackSpd; // �Ѿ� �ӵ�
    public int p_AttackNum; // ���� ��

    // �߻� ����
    public float s_AttackSpd; // �Ѿ� �ӵ�
    public int s_AttackNum; // �߻� ��

    // �� ����
    public GameObject[] dancePos;
    public float d_AttackSpd; // �Ѿ� �ӵ�
    public int d_AttackNum; // ���� ��
    public int d_BulletNum; // ���� ��
    public float d_AttackAngles; // �߻� ����

    // �ı� ����
    public GameObject breakAttackPrefab; // ���� Ư�� �Ѿ�
    public float b_AttackSpd; // �Ѿ� �ӵ�
    public int b_AttackNum; // �߻� ��

    // ��ȯ ����
    public List<GameObject> summonMonsters = new List<GameObject>();
    public GameObject defenceEffect;
    public int summonNum;
    public bool isSummon; // �Ա� ���� ��
    public GameObject[] summonPos; // ���� ���� ��ġ
    public GameObject[] summonPrefabs;

    public List<GameObject> playerCannons = new List<GameObject>();

    private Animator anim;

    private void Awake()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        monsterMap = GameObject.Find("Manager").GetComponent<MonsterMap>();
        p_AttackSpawn = GameObject.Find("Manager").GetComponent<P_AttackSpawn>();
        cameraMovement = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
        getMoney = GameObject.Find("Manager").GetComponent<GetMoney>();
        mapSetting = GameObject.Find("Manager").GetComponent<MapSetting>();
    }

    void Start()
    {
        anim = GetComponent<Animator>();

        maxHealth = 1;
        //maxHealth = 10;
        currentHealth = maxHealth;
        money = 300;

        p_AttackSpd = 10f;
        p_AttackNum = 10;

        s_AttackSpd = 20f;
        s_AttackNum = 5;

        d_AttackSpd = 12f;
        d_AttackNum = 15;
        d_BulletNum = 10;

        FindCannons();

        // InvokeRepeating("StartPattern", 1f, 7f); // ���� ���� ����
        //InvokeRepeating("StartBreakAttack", 3f, 15f); // Ư�� ���� 1����
        InvokeRepeating("SummonMonster", 1f, 30f); // Ư�� ���� 2����
        //InvokeRepeating("StartDanceAttack", 1f, 20f);
    }

    public void FindCannons() // ��� ���� ã��
    {
        GameObject[] Cannons = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject cannon in Cannons)
        {
            if (cannon.name == "PlayerCannon")
            {
                playerCannons.Add(cannon);
            }
        }
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        anim.SetTrigger("Die");
        yield return new WaitForSeconds(1.5f);

        getMoney.getMoney = money;
        getMoney.PickUpMoney();

        playerMovement.OnTile();
        playerMovement.moveNum = 1;
        playerMovement.currentTile = 0;
        playerMovement.PostionReset(); // �÷��̾� ��ġ �ʱ�ȭ

        monsterMap.fireMoved = false;
        p_AttackSpawn.spawned = false;

        cameraMovement.fix = true;

        monsterMap.DeleteCannon();

        mapSetting.MapReset(); // ���� Ŭ����� �� �ʱ�ȭ
        mapSetting.StageMapSetting(); // �� ����

        Destroy(gameObject);
    }

    void StartPattern() // ���� ���� ����
    {
        int randomPattern = Random.Range(0, 3);
        switch (randomPattern)
        {
            case 0:
                StartPushAttack();
                break;
            case 1:
                StartShotAttack();
                break;
            case 2:
                StartDanceAttack();
                break;
            case 3:
                SummonMonster();
                break;
        }
    }

    private void StartPushAttack()
    {
        StartCoroutine(PushAttacks());
    }

    IEnumerator PushAttacks()
    {
        int pushNum = 0;
        Vector3 direction = Quaternion.Euler(0, 180, 0) * Vector3.forward; // ������ ���� ���� ���

        for (int i = 0; i < p_AttackNum; i++)
        {
            anim.SetTrigger("Push");
            if (pushNum == 0)
            {
                pushNum++;
                for (int j = 0; j < pushPos1.Length; j++)
                {
                    Vector3 bulletPos = new Vector3(pushPos1[j].transform.position.x, 2f, pushPos1[j].transform.position.z); // �Ѿ� ��ġ ����
                    GameObject bullet = Instantiate(baseAttackPrefab, bulletPos, Quaternion.identity); // �Ѿ� ����
                    bullet.name = "PushAttack"; // �Ѿ� �̸� ����         
                    bullet.GetComponent<Rigidbody>().velocity = direction * p_AttackSpd; // źȯ ���� ����
                    Destroy(bullet, 3f);
                }
            }
            else if (pushNum == 1)
            {
                pushNum--;
                for (int j = 0; j < pushPos2.Length; j++)
                {
                    Vector3 bulletPos = new Vector3(pushPos2[j].transform.position.x, 2f, pushPos2[j].transform.position.z); // �Ѿ� ��ġ ����
                    GameObject bullet = Instantiate(baseAttackPrefab, bulletPos, Quaternion.identity); // �Ѿ� ����
                    bullet.name = "PushAttack"; // �Ѿ� �̸� ����         
                    bullet.GetComponent<Rigidbody>().velocity = direction * p_AttackSpd; // źȯ ���� ����
                    Destroy(bullet, 3f);
                }
            }

            yield return new WaitForSeconds(0.7f);
        }
    }

    private void StartShotAttack()
    {
        StartCoroutine(ShotAttacks());
    }

    IEnumerator ShotAttacks()
    {
        for (int i = 0; i < s_AttackNum; i++)
        {
            anim.SetTrigger("Shot");
            yield return new WaitForSeconds(0.5f);

            GameObject player = GameObject.Find("Player");
            Vector3 playerPosition = player.transform.position;

            // �÷��̾�ο� 5��ŭ �Ÿ��� ���� ��ġ�� �Ѿ� ����
            Vector3 randomDirection = Random.insideUnitSphere.normalized * 5; 
            randomDirection.y = 0; // ���� �״��
            Vector3 bulletPos = playerPosition + randomDirection;
            GameObject bullet = Instantiate(baseAttackPrefab, bulletPos, Quaternion.identity);
            bullet.name = "ShotAttack";

            // �Ѿ��� 1�� �� �÷��̾� �������� �߻�
            yield return new WaitForSeconds(1f); 
            Vector3 direction = (playerPosition - bullet.transform.position).normalized;
            bullet.GetComponent<Rigidbody>().velocity = direction * s_AttackSpd;

            Destroy(bullet, 5f); // 5�� �� �Ѿ� ����
        }
    }

    private void StartBreakAttack() // ���� ����
    {
        if(playerCannons.Count > 1) 
        {
            Vector3 bulletPos = new Vector3(transform.position.x, 3f, transform.position.z);
            GameObject bullet = Instantiate(breakAttackPrefab, bulletPos, Quaternion.identity); // �Ѿ� ����
            bullet.name = "BreakAttack"; // �Ѿ� �̸� ����
        }
        else if(playerCannons.Count <= 1)
        {
            CancelInvoke("StartBreakAttack");
        }
    }

    public void StartDanceAttack()
    {
        StartCoroutine(DanceAttack());
    }

    IEnumerator DanceAttack()
    {
        anim.SetBool("Dance", true);
        int num = Random.Range(0, 2);

        for (int i = 0; i < d_AttackNum; i++)
        {
            if (num == 0)
            {
                for (int j = 0; j < d_BulletNum; j++)
                {
                    float angle = 130 + j * (90f / d_BulletNum); // ������ �κ�
                    Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward;
                    Vector3 bulletPos = new Vector3(dancePos[0].transform.position.x, 2f, dancePos[0].transform.position.z);
                    GameObject bullet = Instantiate(baseAttackPrefab, bulletPos, Quaternion.identity);
                    bullet.name = "DanceAttack";
                    bullet.GetComponent<Rigidbody>().velocity = direction * d_AttackSpd;

                    Destroy(bullet, 2.5f);
                }
                num = 1;
            }
            else if (num == 1)
            {
                for (int j = 0; j < d_BulletNum; j++)
                {
                    float angle = 135 + j * (90f / d_BulletNum);
                    Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward;
                    Vector3 bulletPos = new Vector3(dancePos[1].transform.position.x, 2f, dancePos[1].transform.position.z);
                    GameObject bullet = Instantiate(baseAttackPrefab, bulletPos, Quaternion.identity);
                    bullet.name = "DanceAttack";
                    bullet.GetComponent<Rigidbody>().velocity = direction * d_AttackSpd;

                    Destroy(bullet, 2.5f);
                }
                num = 0;
            }
            yield return new WaitForSeconds(0.3f);
        }
        anim.SetBool("Dance", false);
    }


    void SummonMonster()
    {
        if (!isSummon)
        {
            defenceEffect.SetActive(true);
            isSummon = true;

            summonNum = Random.Range(0, 2);

            // ù ��° ���� ��ȯ
            GameObject firstMonster = Instantiate(summonPrefabs[summonNum], summonPos[0].transform.position, Quaternion.Euler(0, 90, 0));
            summonMonsters.Add(firstMonster);
            firstMonster.name = "SummonMonster";

            // 0�̸� 1, 1�̸� 0�� ���
            GameObject secondMonster = Instantiate(summonPrefabs[1 - summonNum], summonPos[1].transform.position, Quaternion.Euler(0, -90, 0));
            summonMonsters.Add(secondMonster);
            secondMonster.name = "SummonMonster";
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CannonBullet"))
        {
            if (!isSummon)
            {
                Bullet bulletComponent = collision.gameObject.GetComponent<Bullet>();
                if (bulletComponent != null)
                {
                    currentHealth -= bulletComponent.damage;
                    anim.SetTrigger("Hit");
                }
                Destroy(collision.gameObject);
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }
    }

}
