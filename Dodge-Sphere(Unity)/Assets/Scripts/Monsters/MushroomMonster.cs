using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MushRoomMonster : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private MonsterMap monsterMap;
    private P_AttackSpawn p_AttackSpawn;
    private CameraMovement cameraMovement;
    private GetMoney getMoney;

    public GameObject monster;

    // �⺻ ����
    public int maxHealth;
    public int currentHealth;
    public int money;

    public GameObject baseAttackPrefab; // �Ѿ� ������
    public int baseMinAngle; // �߻� ��
    public int baseMaxAngle; // �߻� ��

    // ��ġ�� ����
    public float b_AttackSpd; // �Ѿ� �ӵ�
    public int b_BulletNum; // �߻� ��
    private int b_CurrentNumIndex; // ���� �Ѿ� �ε���

    // ���� ����
    public float s_AttackSpd; // �Ѿ� �ӵ�
    public int s_BulletNum; // �߻� ��
    private int[] s_BulletNums; // �Ѿ� ���� �迭
    private int s_CurrentNumIndex; // ���� �Ѿ� �ε���

    // �÷�ġ�� ����
    public float u_AttackSpd = 20f; // �Ѿ� �ӵ�
    public int u_AttackNum = 10; // �߻� ��
    public int u_BulletNum = 10; // �Ѿ� ��

    public bool smlie; // smlie - true, angry - false (���� Ȯ��)

    private Animator anim;

    private void Awake()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        monsterMap = GameObject.Find("Manager").GetComponent<MonsterMap>();
        p_AttackSpawn = GameObject.Find("Manager").GetComponent<P_AttackSpawn>();
        cameraMovement = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
        getMoney = GameObject.Find("Manager").GetComponent<GetMoney>();
    }

    void Start()
    {
        anim = GetComponent<Animator>();

        //maxHealth = 1;
        maxHealth = 10;
        currentHealth = maxHealth;
        money = 150;
        
        b_AttackSpd = 10;
        b_BulletNum = 3;

        s_AttackSpd = 8f;
        s_BulletNums = new int[] { 15, 14, 15, 14, 15 };

        u_AttackSpd = 10f;
        u_AttackNum = 5;
        u_BulletNum = 3;

        InvokeRepeating("StartPattern", 1f, 7f); // ���� ���� ����
    }

    void Update()
    {
        if (!smlie && monster == null)
        {
            monster = GameObject.Find("SmileMonster");
        }
        else if(smlie && monster == null)
        {
            monster = GameObject.Find("AngryMonster");
        }

        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {       
        anim.SetTrigger("Die");
        yield return new WaitForSeconds(1f);
               
        if(monster == null)
        {
            getMoney.getMoney = money;
            getMoney.PickUpMoney();

            playerMovement.OnTile();
            playerMovement.MoveFinalPosition();
            playerMovement.moveNum = 1;
            playerMovement.currentTile = 0;

            monsterMap.fireMoved = false;
            p_AttackSpawn.spawned = false;

            cameraMovement.fix = true;

            monsterMap.DeleteCannon();
        }
        Destroy(gameObject);
    }

    void StartPattern() // ���� ���� ����
    {
        int randomPattern = Random.Range(0, 3); // 0 ~ 2 ����

        switch (randomPattern)
        {
            case 0:
                StartButtAttack();
                break;
            case 1:
                StartSpinAttack();
                break;
            case 2:
                StartUperAttack();
                break;
        }
    }

    private void StartButtAttack()
    {        
        StartCoroutine(ButtAttacks());
    }

    IEnumerator ButtAttacks()
    {
        for (int i = 0; i < 2; i++)
        {
            anim.SetTrigger("Butt");
            int startAngle = Random.Range(baseMinAngle, baseMaxAngle);
            for (int j = 0; j < 3; j++)
            {
                int angle = startAngle + (10 * j); // ���� �������� 10���� ����
                Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward;
                Vector3 bulletPos = new Vector3(transform.position.x, 2f, transform.position.z);
                GameObject bullet = Instantiate(baseAttackPrefab, bulletPos, Quaternion.identity);
                bullet.name = "ButtAttack";
                bullet.GetComponent<Rigidbody>().velocity = direction * b_AttackSpd;

                Destroy(bullet, 2.5f); // 2.5�� �� �Ѿ� ����
            }
            yield return new WaitForSeconds(2f); // ���� �߻� ���
        }
    }


    private void StartSpinAttack()
    {
        anim.SetTrigger("Spin");
        StartCoroutine(SpinAttacks());
    }

    IEnumerator SpinAttacks()
    {
        for (int i = 0; i < 5; i++)
        {
            s_BulletNum = s_BulletNums[b_CurrentNumIndex]; // ���� �Ѿ� ���� ��������
            StartCoroutine(SpinBullet());
            s_CurrentNumIndex = (s_CurrentNumIndex + 1) % s_BulletNums.Length; // ���� �Ѿ� �ε��� ����
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator SpinBullet() // źȯ�� ���� ���� �������� �߻�
    {
        for (int i = 0; i < s_BulletNum; i++)
        {
            float angle = i * (360f / s_BulletNum); // źȯ�� ���� ���                                             
            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward; // ������ ���� ���� ���
            Vector3 bulletPos = new Vector3(transform.position.x, 2f, transform.position.z); // �Ѿ� ��ġ ����
            GameObject bullet = Instantiate(baseAttackPrefab, bulletPos, Quaternion.identity); // �Ѿ� ����
            bullet.name = "BaseFireAttack"; // �Ѿ� �̸� ����         
            bullet.GetComponent<Rigidbody>().velocity = direction * s_AttackSpd; // źȯ ���� ����

            Destroy(bullet, 3f); 

            yield return new WaitForSeconds(0.05f);
        }
    }

    public void StartUperAttack()
    {
        StartCoroutine(UperAttack());
    }

    IEnumerator UperAttack()
    {
        anim.SetTrigger("Uper");
        for (int j = 0; j < u_AttackNum; j++)
        {
            float angle = Random.Range(baseMinAngle, baseMaxAngle);
            StartCoroutine(Uperbullet(angle));
        }

        yield return new WaitForSeconds(3);
        anim.SetTrigger("Uper");
        for (int j = 0; j < u_AttackNum; j++)
        {
            float angle = Random.Range(baseMinAngle, baseMaxAngle);
            StartCoroutine(Uperbullet(angle));
        }
    }

    IEnumerator Uperbullet(float angle)
    {
        for (int j = 0; j < u_BulletNum; j++)
        {
            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward; // ������ ���� ���� ���
            Vector3 bulletPos = new Vector3(transform.position.x, 2f, transform.position.z); // �Ѿ� ��ġ ����
            GameObject bullet = Instantiate(baseAttackPrefab, bulletPos, Quaternion.identity); // �Ѿ� ����
            bullet.name = "UperFireAttack"; // �Ѿ� �̸� ����         
            bullet.GetComponent<Rigidbody>().velocity = direction * u_AttackSpd; // źȯ ���� ����

            Destroy(bullet, 2.5f);

            yield return new WaitForSeconds(0.2f);
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