using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float a_AttackSpd; // �Ѿ� �ӵ�
    public int a_AttackNum; // �߻� ��

    // �� ����
    public float bu_AttackSpd; // �Ѿ� �ӵ�
    public int bu_AttackNum; // �߻� ��
    public float bu_AttackAngles; // �߻� ����

    // �ı� ����
    public GameObject faintAttackPrefab; // ���� Ư�� �Ѿ�
    public float f_AttackSpd; // �Ѿ� �ӵ�
    public int f_AttackNum; // �߻� ��

    // ��ȯ ����
    public int e_EatingNum;
    public bool e_Eating; // �Ա� ���� ��
    public GameObject[] e_EatingPos; // ���� ���� ��ġ
    public GameObject[] e_EatingPrefabs;

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

        bu_AttackSpd = 15f;
        bu_AttackNum = 4;

        a_AttackSpd = 30f;
        a_AttackNum = 5;

       // InvokeRepeating("StartPattern", 1f, 7f); // ���� ���� ����
        //InvokeRepeating("StartFaintAttack", 3f, 8f); // ���� ���� ����
        InvokeRepeating("StartPushAttack", 1f, 20f); // ���� ���� ����
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
                StartAimingAttack();
                break;
            case 2:
                StartLaserAttack();
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

    private void StartAimingAttack()
    {
        StartCoroutine(AimingAttacks());
    }

    IEnumerator AimingAttacks()
    {
        for (int i = 0; i < a_AttackNum; i++)
        {
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

    void EatingMonster()
    {
        e_EatingNum = Random.Range(0, 2);

        if (e_EatingNum == 0)
        {
            GameObject e_Moneter = Instantiate(e_EatingPrefabs[e_EatingNum], e_EatingPos[e_EatingNum].transform.position, Quaternion.Euler(0, 90, 0)); // ���� ����
            e_Moneter.name = "EatingMonster";
        }
        if (e_EatingNum == 1)
        {
            GameObject e_Moneter = Instantiate(e_EatingPrefabs[e_EatingNum], e_EatingPos[e_EatingNum].transform.position, Quaternion.Euler(0, -90, 0)); // ���� ����
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
                anim.SetTrigger("Hit");
            }
            Debug.Log(currentHealth);
            Destroy(collision.gameObject);
        }
    }

}
