using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeholderMonster : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private MonsterMap monsterMap;
    private P_AttackSpawn p_AttackSpawn;
    private CameraMovement cameraMovement;
    private MonsterGetMoney monsterGetMoney;
    private MapSetting mapSetting;
    private HpBarScript hpBarScript;
    private ClearInfor clearInfor;
    private AudioManager audioManager;

    // �⺻ ����
    public int maxHealth;
    public int currentHealth;
    public int money;

    public GameObject baseAttackPrefab; // �Ѿ� ������

    // ��Ƽ�� ����
    public float m_AttackSpd; // �Ѿ� �ӵ�
    public int m_BulletNum; // �Ѿ� ��

    // ���� ����
    public float a_AttackSpd; // �Ѿ� �ӵ�
    public int a_AttackNum; // �߻� ��

    // ��ȭ ����
    public GameObject faintAttackPrefab; // ���� Ư�� �Ѿ�
    public float f_AttackSpd; // �Ѿ� �ӵ�
    public int f_AttackNum; // �߻� ��

    // ������ ����
    public float bu_AttackSpd; // �Ѿ� �ӵ�
    public int bu_AttackNum; // �߻� ��
    public float bu_AttackAngles; // �߻� ����

    private Animator anim;

    private void Awake()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        monsterMap = GameObject.Find("Manager").GetComponent<MonsterMap>();
        p_AttackSpawn = GameObject.Find("Manager").GetComponent<P_AttackSpawn>();
        cameraMovement = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
        monsterGetMoney = GameObject.Find("Manager").GetComponent<MonsterGetMoney>();
        mapSetting = GameObject.Find("Manager").GetComponent<MapSetting>();
        hpBarScript = GameObject.Find("MosterHP").GetComponent<HpBarScript>();
        clearInfor = GameObject.Find("Manager").GetComponent<ClearInfor>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Start()
    {
        anim = GetComponent<Animator>();

        maxHealth = 15;
        currentHealth = maxHealth;
        money = 300;

        m_AttackSpd = 10f;
        m_BulletNum = 100;

        bu_AttackSpd = 15f;
        bu_AttackNum = 4;

        a_AttackSpd = 30f;
        a_AttackNum = 5;

        //InvokeRepeating("StartPattern", 3f, 7f); // ���� ���� ����
        InvokeRepeating("StartLaserAttack", 3f, 7f); // ���� ���� ����
        InvokeRepeating("StartFaintAttack", 6f, 10f); // ���� ���� ����

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

        hpBarScript.MoveToYStart(150, 0.1f);
        hpBarScript.ResetHealthBar();

        monsterGetMoney.getMoney = money;
        monsterGetMoney.PickUpMoney();

        playerMovement.OnTile();
        playerMovement.MoveFinalPosition();
        playerMovement.moveNum = 1;
        playerMovement.currentTile = 0;

        monsterMap.beholderMoved = false;
        p_AttackSpawn.spawned = false;

        cameraMovement.fix = true;

        monsterMap.DeleteCannon();

        clearInfor.killedMonster++;

        Destroy(gameObject);
    }

    void StartPattern() // ���� ���� ����
    {
        int randomPattern = Random.Range(0, 3);
        switch (randomPattern)
        {
            case 0:
                StartMultiAttack();
                break;
            case 1:
                StartAimingAttack();
                break;
            case 2:
                StartLaserAttack();
                break;
        }
    }

    private void StartMultiAttack()
    {
        StartCoroutine(MultiAttacks());
    }

    IEnumerator MultiAttacks()
    {
        anim.SetBool("Multi", true);
        for (int j = 0; j < m_BulletNum; j++)
        {
            audioManager.Be_MultiAudio();

            float m_AttackAngle = Random.Range(135, 226); // źȯ�� ���� ���                                             
            Vector3 direction = Quaternion.Euler(0, m_AttackAngle, 0) * Vector3.forward; // ������ ���� ���� ���
            Vector3 bulletPos = new Vector3(transform.position.x, 2f, transform.position.z); // �Ѿ� ��ġ ����
            GameObject bullet = Instantiate(baseAttackPrefab, bulletPos, Quaternion.identity); // �Ѿ� ����
            bullet.name = "MultiShotAttack"; // �Ѿ� �̸� ����         
            bullet.GetComponent<Rigidbody>().velocity = direction * m_AttackSpd; // źȯ ���� ����

            Destroy(bullet, 3f);

            yield return new WaitForSeconds(0.05f);
        }
        anim.SetBool("Multi", false);
    }

    private void StartAimingAttack()
    {
        StartCoroutine(AimingAttacks());
    }

    IEnumerator AimingAttacks()
    {
        for (int i = 0; i < a_AttackNum; i++)
        {
            audioManager.Be_MultiAudio();

            anim.SetTrigger("Aiming");
            yield return new WaitForSeconds(0.5f);
            GameObject player = GameObject.Find("Player");
            Vector3 playerPosition = player.transform.position;

            Vector3 bulletPos = new Vector3(transform.position.x, 2f, transform.position.z);
            GameObject bullet = Instantiate(baseAttackPrefab, bulletPos, Quaternion.identity); // �Ѿ� ����
            bullet.name = "AimingAttack"; // �Ѿ� �̸� ����

            Vector3 direction = (playerPosition - transform.position).normalized;
            bullet.GetComponent<Rigidbody>().velocity = direction * a_AttackSpd; // źȯ ���� ����

            Destroy(bullet, 2f);
        }
    }

    private void StartFaintAttack() // ���� ����
    {
        Vector3 bulletPos = new Vector3(transform.position.x, 2f, transform.position.z);
        GameObject bullet = Instantiate(faintAttackPrefab, bulletPos, Quaternion.identity); // �Ѿ� ����
        bullet.name = "AimingAttack"; // �Ѿ� �̸� ����
    }

    public void StartLaserAttack()
    {
        StartCoroutine(LaserAttack());
    }

    IEnumerator LaserAttack()
    {
        anim.SetBool("Laser", true);
        int num = Random.Range(0, 2);
        bool start = true;

        audioManager.Be_LazerAudio();

        if (num == 0)
        {
            bu_AttackAngles = 130f; // �Ѿ� ���� ����

            while (start)
            {
                if (bu_AttackAngles >= 220) // �ε��Ҽ��� �񱳸� ���� ����
                {
                    start = false;
                }

                Vector3 direction = Quaternion.Euler(0, bu_AttackAngles, 0) * Vector3.forward;
                Vector3 bulletPos = new Vector3(transform.position.x, 2f, transform.position.z);
                GameObject bullet = Instantiate(baseAttackPrefab, bulletPos, Quaternion.identity);
                bullet.name = "LaserAttack";
                bullet.GetComponent<Rigidbody>().velocity = direction * a_AttackSpd;

                transform.rotation = Quaternion.Euler(0, bu_AttackAngles, 0); // �ڽ��� ȸ���� ����

                Destroy(bullet, 2f);

                bu_AttackAngles += 2; // ���� ������Ʈ

                yield return new WaitForSeconds(0.1f);
            }
        }
        else if (num == 1)
        {
            bu_AttackAngles = 220f; // �Ѿ� ���� ����

            while (start)
            {
                if (bu_AttackAngles <= 130) // �ε��Ҽ��� �񱳸� ���� ����
                {
                    start = false;
                }

                Vector3 direction = Quaternion.Euler(0, bu_AttackAngles, 0) * Vector3.forward;
                Vector3 bulletPos = new Vector3(transform.position.x, 2f, transform.position.z);
                GameObject bullet = Instantiate(baseAttackPrefab, bulletPos, Quaternion.identity);
                bullet.name = "LaserAttack";
                bullet.GetComponent<Rigidbody>().velocity = direction * a_AttackSpd;

                transform.rotation = Quaternion.Euler(0, bu_AttackAngles, 0); // �ڽ��� ȸ���� ����

                Destroy(bullet, 2f);

                bu_AttackAngles -= 2; // ���� ������Ʈ

                yield return new WaitForSeconds(0.1f);
            }
        }

        yield return new WaitForSeconds(0.5f);

        transform.rotation = Quaternion.Euler(0, 180, 0); // ǥ�� �ڽ��� ȸ���� ����
        anim.SetBool("Laser", false);
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
            Destroy(collision.gameObject);
        }
    }

}
