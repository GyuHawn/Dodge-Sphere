using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMonster : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private MonsterMap monsterMap;
    private P_AttackSpawn p_AttackSpawn;
    private CameraMovement cameraMovement;
    private MonsterGetMoney monsterGetMoney;
    private MapSetting mapSetting;
    private HpBarScript hpBarScript;
    private ClearInfor clearInfor;
    private Ability ability;
    private MapConvert mapConvert;
    private AudioManager audioManager;

    // �⺻ ����
    public int maxHealth;
    public int currentHealth;
    public int money;

    // �⺻ ����
    public GameObject b_AttackPrefab; // �Ѿ� ������
    public float b_AttackSpd; // �Ѿ� �ӵ�
    public int b_BulletNum; // �߻� ��
    private int[] b_BulletNums; // �Ѿ� ���� �迭
    private int b_CurrentNumIndex; // ���� �Ѿ� �ε���

    // ��� ����
    public GameObject c_AttackPrefab; // �Ѿ� ������
    public float c_AttackSpd; // �Ѿ� �ӵ�
    public int c_AttackNum; // �߻� ��
    public int[] c_AttackAngles1; // �߻� ����
    public float[] c_AttackAngles2; // �߻� ����

    // ���� ����
    public GameObject j_AttackPrefab; // �Ѿ� ������
    public float j_AttackSpd = 20f; // �Ѿ� �ӵ�
    public int j_AttackNum = 10; // �߻� ��
    public int j_BulletNum = 10; // �Ѿ� ��

    // ������ ����
    public GameObject r_AttackPrefab; // �Ѿ� ������
    public GameObject[] r_AttackPos;
    public float r_AttackSpd; // �Ѿ� �ӵ�
    public int r_AttackNum; // �߻� ��

    public GameObject hitEffectPos; // ����Ʈ ��ġ
    public GameObject hitEffect; // �ǰ� ����Ʈ

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
        ability = GameObject.Find("Manager").GetComponent<Ability>();
        mapConvert = GameObject.Find("Manager").GetComponent<MapConvert>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetTrigger("Spawn");

        maxHealth = 12 + (mapSetting.adventLevel * 3);
        currentHealth = maxHealth;
        money = 300;

        b_AttackSpd = 10f;
        b_BulletNums = new int[] { 30, 29, 30, 29, 30 };

        c_AttackSpd = 10f;
        c_AttackAngles1 = new int[] { 135, 180, 225 };
        c_AttackAngles2 = new float[] { 157.5f, 202.5f };

        j_AttackSpd = 10f;
        j_AttackNum = 10;
        j_BulletNum = 6;

        r_AttackSpd = 8f;
        r_AttackNum = 3;
        
        //InvokeRepeating("StartRollAttack", 1f, 10f); // ���� Ȯ�ο�
        InvokeRepeating("StartPattern", 3f, 7f); // ���� ���� ����

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

        GetMoney();

        mapConvert.ConvertLoading(mapConvert.stageLoading, 2f, 5); // ���� Ŭ����� �������� ��ȯ �̹��� ���

        playerMovement.nextStage = true; // �������� ��ȯ ��
        playerMovement.OnTile();
        playerMovement.moveNum = 1;
        playerMovement.currentTile = 0;
        playerMovement.bulletNum = 0;
        playerMovement.PostionReset(); // �÷��̾� ��ġ �ʱ�ȭ

        monsterMap.fireMoved = false;
        p_AttackSpawn.spawned = false;

        cameraMovement.fix = true;

        monsterMap.DeleteCannon();

        clearInfor.killedBoss++;

        mapSetting.stage++; // �������� ���� Ŭ����� ���������� 1����
        mapSetting.MapReset(); // ���� Ŭ����� �� �ʱ�ȭ
        mapSetting.StageMapSetting(); // �� ����

        Destroy(gameObject);
    }
    void GetMoney()
    {
        // �ɷ� 3-1�� Ȱ��ȭ
        if (ability.ability3Num == 1)
        {
            money = ability.GamblingCoin(money); // �ɷ� 3-1�� ���� ���� ȹ���� ����
        }
        // �ɷ� 3-2�� Ȱ��ȭ
        else if (ability.ability3Num == 2)
        {
            money = ability.PlusCoin(money); // �ɷ� 3-2�� ���� ���� ȹ���� ����
        }

        monsterGetMoney.getMoney = money;
        monsterGetMoney.PickUpMoney();
    }

    void StartPattern() // ���� ���� ����
    {
        int randomPattern = Random.Range(0, 4); // 0 ~ 3 ����

        switch (randomPattern)
        {
            case 0:
                StartBaseAttack();
                break;
            case 1:
                StartCryAttack();
                break;
            case 2:
                StartJumpAttack();
                break;
            case 3:
                StartRollAttack();
                break;
        }
    }

    private void StartBaseAttack()
    {
        anim.SetTrigger("Base");
        StartCoroutine(BaseAttacks());
    }

    IEnumerator BaseAttacks()
    {
        audioManager.F_BaseAudio();
        for (int i = 0; i < 5; i++)
        {
            b_BulletNum = b_BulletNums[b_CurrentNumIndex]; // ���� �Ѿ� ���� ��������
            StartCoroutine(BaseBullet());
            b_CurrentNumIndex = (b_CurrentNumIndex + 1) % b_BulletNums.Length; // ���� �Ѿ� �ε��� ����
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator BaseBullet() // źȯ�� ���� ���� �������� �߻�
    {
        for (int i = 0; i < b_BulletNum; i++)
        {
            float angle = i * (360f / b_BulletNum); // źȯ�� ���� ���                                             
            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward; // ������ ���� ���� ���
            Vector3 bulletPos = new Vector3(transform.position.x, 2f, transform.position.z); // �Ѿ� ��ġ ����
            GameObject bullet = Instantiate(b_AttackPrefab, bulletPos, Quaternion.identity); // �Ѿ� ����
            bullet.name = "BaseFireAttack"; // �Ѿ� �̸� ����         
            bullet.GetComponent<Rigidbody>().velocity = direction * b_AttackSpd; // źȯ ���� ����

            Destroy(bullet, 2.5f);

            yield return new WaitForSeconds(0.05f);
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
            audioManager.F_CryAudio();
            for (int j = 0; j < 3; j++)
            {
                float angle = c_AttackAngles1[j]; // źȯ�� ���� ���                                             
                Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward; // ������ ���� ���� ���
                Vector3 bulletPos = new Vector3(transform.position.x, 2.55f, transform.position.z); // �Ѿ� ��ġ ����
                GameObject bullet = Instantiate(c_AttackPrefab, bulletPos, Quaternion.identity); // �Ѿ� ����
                bullet.name = "CryFireAttack"; // �Ѿ� �̸� ����         
                bullet.GetComponent<Rigidbody>().velocity = direction * c_AttackSpd; // źȯ ���� ����

                Destroy(bullet, 2.5f); // 4�� �� �Ѿ� ����
            }

            yield return new WaitForSeconds(0.75f); // 1�� ���

            for (int k = 0; k < 2; k++)
            {
                float angle = c_AttackAngles2[k]; // źȯ�� ���� ���                                             
                Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward; // ������ ���� ���� ���
                Vector3 bulletPos = new Vector3(transform.position.x, 2.55f, transform.position.z); // �Ѿ� ��ġ ����
                GameObject bullet = Instantiate(c_AttackPrefab, bulletPos, Quaternion.identity); // �Ѿ� ����
                bullet.name = "CryFireAttack"; // �Ѿ� �̸� ����         
                bullet.GetComponent<Rigidbody>().velocity = direction * c_AttackSpd; // źȯ ���� ����

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
        audioManager.F_JumpAudio();
        anim.SetTrigger("Jump");
        for (int j = 0; j < j_AttackNum; j++)
        {
            float angle = Random.Range(135, 225);
            StartCoroutine(Jumpbullet(angle));
        }

        yield return new WaitForSeconds(3);
        audioManager.F_JumpAudio();
        anim.SetTrigger("Jump");
        for (int j = 0; j < j_AttackNum; j++)
        {
            float angle = Random.Range(135, 225);
            StartCoroutine(Jumpbullet(angle));
        }
    }

    IEnumerator Jumpbullet(float angle) // źȯ�� ���� ���� �������� �߻�
    {
        for (int j = 0; j < j_BulletNum; j++)
        {
            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward; // ������ ���� ���� ���
            Vector3 bulletPos = new Vector3(transform.position.x, 2f, transform.position.z); // �Ѿ� ��ġ ����
            GameObject bullet = Instantiate(j_AttackPrefab, bulletPos, Quaternion.identity); // �Ѿ� ����
            bullet.name = "JumpFireAttack"; // �Ѿ� �̸� ����         
            bullet.GetComponent<Rigidbody>().velocity = direction * j_AttackSpd; // źȯ ���� ����

            Destroy(bullet, 2.5f);

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void StartRollAttack()
    {     
        StartCoroutine(RollAttack());
    }

    IEnumerator RollAttack()
    {
        for (int i = 0; i < 2; i++)
        {
            audioManager.F_RollAudio();
            anim.SetTrigger("Roll");

            Vector3 direction = Quaternion.Euler(0, 180, 0) * Vector3.forward; // ������ ���� ���� ���
            Vector3 bulletPos = new Vector3(r_AttackPos[0].transform.position.x, 2f, r_AttackPos[0].transform.position.z); // �Ѿ� ��ġ ����
            GameObject bullet = Instantiate(r_AttackPrefab, bulletPos, Quaternion.Euler(90,0,0)); // �Ѿ� ����
            bullet.name = "RollFireAttack"; // �Ѿ� �̸� ����         
            bullet.GetComponent<Rigidbody>().velocity = direction * r_AttackSpd; // źȯ ���� ����

            Destroy(bullet, 3f);

            yield return new WaitForSeconds(.8f);

            for(int j = 0 ; j < 2; j++)
            {
                for (int k = 0; k < r_AttackNum - 1; k++)
                {
                    direction = Quaternion.Euler(0, 180, 0) * Vector3.forward; // ������ ���� ���� ���
                    bulletPos = new Vector3(r_AttackPos[j + 1].transform.position.x, 2f, r_AttackPos[j + 1].transform.position.z); // �Ѿ� ��ġ ����
                    bullet = Instantiate(r_AttackPrefab, bulletPos, Quaternion.Euler(90, 0, 0)); // �Ѿ� ����
                    bullet.name = "RollFireAttack"; // �Ѿ� �̸� ����         
                    bullet.GetComponent<Rigidbody>().velocity = direction * r_AttackSpd; // źȯ ���� ����

                    Destroy(bullet, 3f);
                }
            }
            yield return new WaitForSeconds(1.5f);
        }       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CannonBullet"))
        {
            Bullet bulletComponent = collision.gameObject.GetComponent<Bullet>();
            if (bulletComponent != null)
            {
                audioManager.HitMonsterAudio();
                StartCoroutine(HitEffect());
                currentHealth -= bulletComponent.damage;
                hpBarScript.UpdateHP(currentHealth, maxHealth);
                anim.SetTrigger("Hit");
            }
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("ExtraAttack"))
        {
            ExtraAttack attack = collision.gameObject.GetComponent<ExtraAttack>();
            if (attack != null)
            {
                audioManager.HitMonsterAudio();
                StartCoroutine(HitEffect());
                currentHealth -= attack.damage;
                hpBarScript.UpdateHP(currentHealth, maxHealth);
                anim.SetTrigger("Hit");
            }
            Destroy(collision.gameObject);
        }
    }

    IEnumerator HitEffect()
    {
        GameObject effect = Instantiate(hitEffect, hitEffectPos.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Destroy(effect);
    }
} 
