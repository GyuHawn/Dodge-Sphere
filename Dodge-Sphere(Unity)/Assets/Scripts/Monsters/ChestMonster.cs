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
    public int[] bu_AttackAngles1; // �߻� ����
    public float[] bu_AttackAngles2; // �߻� ����

    // �Ա� ����
    public float e_AttackSpd = 20f; // �Ѿ� �ӵ�
    public int e_AttackNum = 10; // �߻� ��
    public int e_BulletNum = 10; // �Ѿ� ��

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

        b_AttackSpd = 10f;
        b_AttackNum = 50;

        bu_AttackSpd = 10f;
        bu_AttackNum = 4;
        bu_AttackAngles1 = new int[] { 135, 180, 225 };
        bu_AttackAngles2 = new float[] { 157.5f, 202.5f };

        e_AttackSpd = 10f;
        e_AttackNum = 10;
        e_BulletNum = 6;

        InvokeRepeating("StartBiteAttack", 1f, 10f); // ���� Ȯ�ο�
        //InvokeRepeating("StartPattern", 1f, 7f); // ���� ���� ����
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
        int randomPattern = Random.Range(0, 4); // 0 ~ 3 ����

        switch (randomPattern)
        {
            case 0:
                StartBiteAttack();
                break;
            case 1:
                StartCryAttack();
                break;
            case 2:
                StartJumpAttack();
                break;
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

    private void StartCryAttack()
    {
        anim.SetTrigger("Cry");
        StartCoroutine(CryAttacks());
    }

    IEnumerator CryAttacks() // źȯ�� ���� ���� �������� �߻�
    {
        for (int i = 0; i < 4; i++) // �� 2�� �߻�
        {
            for (int j = 0; j < 3; j++)
            {
                float angle = bu_AttackAngles1[j]; // źȯ�� ���� ���                                             
                Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward; // ������ ���� ���� ���
                Vector3 bulletPos = new Vector3(transform.position.x, 2.55f, transform.position.z); // �Ѿ� ��ġ ����
                GameObject bullet = Instantiate(baseAttackPrefab, bulletPos, Quaternion.identity); // �Ѿ� ����
                bullet.name = "CryFireAttack"; // �Ѿ� �̸� ����         
                bullet.GetComponent<Rigidbody>().velocity = direction * bu_AttackSpd; // źȯ ���� ����

                Destroy(bullet, 2.5f); // 4�� �� �Ѿ� ����
            }

            yield return new WaitForSeconds(0.75f); // 1�� ���

            for (int k = 0; k < 2; k++)
            {
                float angle = bu_AttackAngles2[k]; // źȯ�� ���� ���                                             
                Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward; // ������ ���� ���� ���
                Vector3 bulletPos = new Vector3(transform.position.x, 2.55f, transform.position.z); // �Ѿ� ��ġ ����
                GameObject bullet = Instantiate(baseAttackPrefab, bulletPos, Quaternion.identity); // �Ѿ� ����
                bullet.name = "CryFireAttack"; // �Ѿ� �̸� ����         
                bullet.GetComponent<Rigidbody>().velocity = direction * bu_AttackSpd; // źȯ ���� ����

                Destroy(bullet, 2.5f);
            }
        }
    }

    public void StartJumpAttack()
    {
        StartCoroutine(JumpAttack());
    }

    IEnumerator JumpAttack()
    {
        anim.SetTrigger("Jump");
        for (int j = 0; j < e_AttackNum; j++)
        {
            float angle = Random.Range(135, 225);
            StartCoroutine(Jumpbullet(angle));
        }

        yield return new WaitForSeconds(3);
        anim.SetTrigger("Jump");
        for (int j = 0; j < e_AttackNum; j++)
        {
            float angle = Random.Range(135, 225);
            StartCoroutine(Jumpbullet(angle));
        }
    }

    IEnumerator Jumpbullet(float angle) // źȯ�� ���� ���� �������� �߻�
    {
        for (int j = 0; j < e_BulletNum; j++)
        {
            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward; // ������ ���� ���� ���
            Vector3 bulletPos = new Vector3(transform.position.x, 2f, transform.position.z); // �Ѿ� ��ġ ����
            GameObject bullet = Instantiate(baseAttackPrefab, bulletPos, Quaternion.identity); // �Ѿ� ����
            bullet.name = "JumpFireAttack"; // �Ѿ� �̸� ����         
            bullet.GetComponent<Rigidbody>().velocity = direction * e_AttackSpd; // źȯ ���� ����

            Destroy(bullet, 2.5f);

            yield return new WaitForSeconds(0.1f);
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
                anim.SetTrigger("Hit");
            }
            Debug.Log(currentHealth);
            Destroy(collision.gameObject);
        }
    }

}
