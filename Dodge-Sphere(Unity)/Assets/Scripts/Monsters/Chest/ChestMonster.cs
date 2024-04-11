using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ChestMonster : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private MonsterMap monsterMap;
    private P_AttackSpawn p_AttackSpawn;
    private CameraMovement cameraMovement;
    private GetMoney getMoney;
    private MapSetting mapSetting;
    private HpBarScript hpBarScript;

    // �⺻ ����
    public int maxHealth;
    public int currentHealth;
    public int money;

    public GameObject baseAttackPrefab; // �Ѿ� ������

    // ���� ����
    public GameObject[] b_AttackPrefab; // �Ѿ� ������
    public float b_AttackSpd; // �Ѿ� �ӵ�
    public int b_AttackNum; // �߻� ��
    public int b_AttackAngle; // �߻� ����

    // ��ġ�� ����
    public float bu_AttackSpd; // �Ѿ� �ӵ�
    public int bu_AttackNum; // �߻� ��
    public int bu_BulletNum; // �Ѿ� ��
    public Vector3 bu_AttackAngles; // �߻� ����

    // �Ա� ����
    public float e_AttackSpd = 20f; // �Ѿ� �ӵ�
    public int e_AttackNum = 10; // �߻� ��
    public int e_BulletNum = 10; // �Ѿ� ��
    public int EatingNum;
    public bool Eating; // �Ա� ���� ��
    public GameObject[] EatingPos; // ���� ���� ��ġ
    public GameObject EatingPrefab;

    private Animator anim;

    private void Awake()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        monsterMap = GameObject.Find("Manager").GetComponent<MonsterMap>();
        p_AttackSpawn = GameObject.Find("Manager").GetComponent<P_AttackSpawn>();
        cameraMovement = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
        getMoney = GameObject.Find("Manager").GetComponent<GetMoney>();
        mapSetting = GameObject.Find("Manager").GetComponent<MapSetting>();
        hpBarScript = GameObject.Find("MosterHP").GetComponent<HpBarScript>();
    }

    void Start()
    {
        anim = GetComponent<Animator>();

        maxHealth = 1;
        //maxHealth = 10;
        currentHealth = maxHealth;
        money = 300;

        b_AttackSpd = 10f;
        b_AttackNum = 50;

        bu_AttackSpd = 15f;
        bu_AttackNum = 4;
        bu_BulletNum = 10;

        e_AttackSpd = 10f;
        e_BulletNum = 6;

        InvokeRepeating("StartPattern", 1f, 7f); // ���� ���� ����

        hpBarScript.MoveToYStart(10, 0.5f);
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

        hpBarScript.MoveToYStart(150, 0.5f);
        hpBarScript.ResetHealthBar();

        GameObject monster = GameObject.Find("EatingMonster");
        Destroy(monster);

        getMoney.getMoney = money;
        getMoney.PickUpMoney();

        playerMovement.OnTile();
        playerMovement.MoveFinalPosition();
        playerMovement.moveNum = 1;
        playerMovement.currentTile = 0;

        monsterMap.chestMoved = false;
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
        if (!Eating)
        {
            switch (randomPattern)
            {
                case 0:
                    StartBiteAttack();
                    break;
                case 1:
                    StartButtAttack();
                    break;
                case 2:
                    StartEatingAttack();
                    break;
            }
        }
        else
        {
            switch (randomPattern)
            {
                case 0:
                    StartBiteAttack();
                    break;
                case 1:
                    StartButtAttack();
                    break;
                case 2:
                    StartCoroutine(EatingAttack());
                    break;
            }
        }
    }

    private void StartBiteAttack()
    {
        anim.SetTrigger("Bite");
        StartCoroutine(BiteAttacks());
    }

    IEnumerator BiteAttacks()
    {
        yield return new WaitForSeconds(0.3f);

        int num = 0;
        bool pos = true;

        for (int j = 0; j < b_AttackNum; j++)
        {
            num = pos ? 1 : 0;
            b_AttackAngle = Random.Range(135, 226); // źȯ�� ���� ���                                             
            Vector3 direction = Quaternion.Euler(0, b_AttackAngle, 0) * Vector3.forward; // ������ ���� ���� ���
            Vector3 bulletPos = new Vector3(transform.position.x, 2f, transform.position.z); // �Ѿ� ��ġ ����
            GameObject bullet = Instantiate(b_AttackPrefab[num], bulletPos, Quaternion.identity); // �Ѿ� ����
            bullet.name = "BiteAttack"; // �Ѿ� �̸� ����         
            bullet.GetComponent<Rigidbody>().velocity = direction * b_AttackSpd; // źȯ ���� ����
            pos = pos ? false : true;

            Destroy(bullet, 5f); // 2.5�� �� �Ѿ� ����

            yield return new WaitForSeconds(0.1f);
        }
    }

    private void StartButtAttack()
    {       
        StartCoroutine(ButtAttacks());
    }

    IEnumerator ButtAttacks()
    {
        for (int i = 0; i < bu_AttackNum; i++)
        {
            anim.SetTrigger("Butt");

            for (int j = 0; j < bu_BulletNum; j++)
            {
                GameObject player = GameObject.Find("Player");
                Vector3 playerPosition = player.transform.position;
                // �÷��̾� �ֺ� ������ ��ġ ����
                Vector3 randomPosition = playerPosition + new Vector3(Random.Range(-10.0f, 10.0f), 2, Random.Range(-10.0f, 10.0f));
                GameObject bullet = Instantiate(baseAttackPrefab, gameObject.transform.position, Quaternion.identity); // �Ѿ� ����
                bullet.name = "ButtFireAttack";
                Vector3 direction = (randomPosition - transform.position).normalized;
                bullet.GetComponent<Rigidbody>().velocity = direction * bu_AttackSpd;
                Destroy(bullet, 2.5f);
                yield return new WaitForSeconds(0.15f);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }


    public void StartEatingAttack()
    {
        Eating = true;
        StartCoroutine(EatingAttack());
        EatingMonster();
    }

    IEnumerator EatingAttack()
    {
        for (int j = 0; j < bu_BulletNum; j++)
        {
            GameObject player = GameObject.Find("Player");
            Vector3 playerPosition = player.transform.position;
            GameObject bullet = Instantiate(baseAttackPrefab, gameObject.transform.position, Quaternion.identity); // �Ѿ� ����
            bullet.name = "ButtFireAttack";
            Vector3 direction = (playerPosition - transform.position).normalized;
            bullet.GetComponent<Rigidbody>().velocity = direction * bu_AttackSpd;
            Destroy(bullet, 2.5f);
            yield return new WaitForSeconds(0.2f);
        }
    }

    void EatingMonster()
    {
        EatingNum = Random.Range(0, 2);

        if (EatingNum == 0)
        {
            GameObject e_Moneter = Instantiate(EatingPrefab, EatingPos[EatingNum].transform.position, Quaternion.Euler(0, 90, 0)); // ���� ����
            e_Moneter.name = "EatingMonster";
        }
        if (EatingNum == 1)
        {
            GameObject e_Moneter = Instantiate(EatingPrefab, EatingPos[EatingNum].transform.position, Quaternion.Euler(0, -90, 0)); // ���� ����
            e_Moneter.name = "EatingMonster";
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CannonBullet"))
        {
            Bullet bulletComponent = collision.gameObject.GetComponent<Bullet>();
            if (bulletComponent != null)
            {
                currentHealth -= bulletComponent.damage;
                hpBarScript.UpdateHP(currentHealth, maxHealth);
                anim.SetTrigger("Hit");
            }
            Debug.Log(currentHealth);
            Destroy(collision.gameObject);
        }
    }

}
