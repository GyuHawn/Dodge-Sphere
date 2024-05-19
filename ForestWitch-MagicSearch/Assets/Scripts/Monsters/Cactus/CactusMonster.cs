using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CactusMonster : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private MonsterMap monsterMap;
    private P_AttackSpawn p_AttackSpawn;
    private CameraMovement cameraMovement;
    private MonsterGetMoney monsterGetMoney;
    private HpBarScript hpBarScript;
    private ClearInfor clearInfor;
    private Ability ability;
    private MapSetting mapSetting;
    private AudioManager audioManager;

    // �⺻ ����
    public int maxHealth;
    public int currentHealth;
    public int money;

    // �ٿ ����
    public GameObject b_AttackPrefab; // �Ѿ� ������
    public float b_AttackSpd; // �Ѿ� �ӵ�
    public int b_BulletNum; // �߻� ��
    private int[] b_BulletNums; // �Ѿ� ���� �迭
    private int b_CurrentNumIndex; // ���� �Ѿ� �ε���

    // ���̺� ����
    public GameObject w_AttackPrefab; // �Ѿ� ������
    public GameObject[] w_SpwanPos; // �Ѿ� �߻� ��ġ
    public float w_AttackSpd; // �Ѿ� �ӵ�
    public int w_AttackNum; // �߻� ��
    public int w_AttackAngle; // �߻� ����

    // ��ġ ����
    public GameObject p_BaseAttackPrefab; // �Ѿ� ������
    public float p_AttackSpd; // �Ѿ� �ӵ�  
    public int p_BulletNum; // �Ѿ� ��

    // ��ġ�� ����
    public GameObject bu_AttackPrefab; // �Ѿ� ������
    public float bu_AttackSpd; // �Ѿ� �ӵ�
    public int bu_AttackNum; // �߻� ��
    public int bu_AttackAngle; // �߻� ����

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
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Start()
    {
        anim = GetComponent<Animator>();

        maxHealth = 7 + (mapSetting.adventLevel * 3);
        currentHealth = maxHealth;
        money = 200;

        b_AttackSpd = 10f;
        b_BulletNums = new int[] { 30, 29, 30, 29, 30 };

        w_AttackSpd = 12f;
        w_AttackNum = 10;

        p_AttackSpd = 10f;        
        p_BulletNum = 3;

        bu_AttackSpd = 8f;
        bu_AttackNum = 3;
                
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
        yield return new WaitForSeconds(1f);

        hpBarScript.MoveToYStart(150, 0.1f);
        hpBarScript.ResetHealthBar(currentHealth, maxHealth);
        hpBarScript.healthBarFill.fillAmount = 1.0f;

        GetMoney();

        playerMovement.OnTile();
        playerMovement.MoveFinalPosition();
        playerMovement.moveNum = 1;
        playerMovement.currentTile = 0;
        playerMovement.bulletNum = 0;

        monsterMap.cactusMoved = false;
        p_AttackSpawn.spawned = false;

        cameraMovement.fix = true;

        monsterMap.DeleteObject();

        clearInfor.killedMonster++;

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
                StartBounceAttack();
                break;
            case 1:
                StartWaveAttack();
                break;
            case 2:
                StartPunchAttack();
                break;
            case 3:
                StartButtAttack();
                break;
        }
    }

    private void StartBounceAttack()
    {     
        anim.SetBool("Bounce", true);
        StartCoroutine(BounceAttacks());
    }

    IEnumerator BounceAttacks()
    {
        for (int i = 0; i < 5; i++)
        {
            audioManager.C_BounceAudio();
            b_BulletNum = b_BulletNums[b_CurrentNumIndex]; // ���� �Ѿ� ���� ��������
            BounceBullet();
            b_CurrentNumIndex = (b_CurrentNumIndex + 1) % b_BulletNums.Length; // ���� �Ѿ� �ε��� ����
            yield return new WaitForSeconds(1f);
        }
        anim.SetBool("Bounce", false);
    }
    
    void BounceBullet() // źȯ�� ���� ���� �������� �߻�
    {
        for (int i = 0; i < b_BulletNum; i++)
        {
            float angle = i * (360f / b_BulletNum); // źȯ�� ���� ���                                             
            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward; // ������ ���� ���� ���
            Vector3 bulletPos = new Vector3(transform.position.x, 2f, transform.position.z); // �Ѿ� ��ġ ����
            GameObject bullet = Instantiate(b_AttackPrefab, bulletPos, Quaternion.identity); // �Ѿ� ����
            bullet.name = "BounceAttack"; // �Ѿ� �̸� ����         
            bullet.GetComponent<Rigidbody>().velocity = direction * b_AttackSpd; // źȯ ���� ����

            Destroy(bullet, 2.5f); // 2.5�� �� �Ѿ� ����
        }
    }

    private void StartWaveAttack()
    {
        anim.SetTrigger("Wave");
        StartCoroutine(WaveAttacks());
    }

    IEnumerator WaveAttacks() // ��տ��� ������ �Ѿ� �߻�
    {
        yield return new WaitForSeconds(0.3f);

        int num = 0;
        bool pos = true;
        for (int i = 0; i < 4; i++)
        {
            audioManager.C_WaveAudio();
            for (int j = 0; j < w_AttackNum; j++)
            {
                num = pos ? 1 : 0;
                w_AttackAngle = Random.Range(135, 226); // źȯ�� ���� ���                                             
                Vector3 direction = Quaternion.Euler(0, w_AttackAngle, 0) * Vector3.forward; // ������ ���� ���� ���
                Vector3 bulletPos = new Vector3(w_SpwanPos[num].transform.position.x, 2f, w_SpwanPos[1].transform.position.z); // �Ѿ� ��ġ ����
                GameObject bullet = Instantiate(w_AttackPrefab, bulletPos, Quaternion.identity); // �Ѿ� ����
                bullet.name = "WaveFireAttack"; // �Ѿ� �̸� ����         
                bullet.GetComponent<Rigidbody>().velocity = direction * w_AttackSpd; // źȯ ���� ����

                Destroy(bullet, 2.5f); // 2.5�� �� �Ѿ� ����

                yield return new WaitForSeconds(0.11f);
            }
            pos = pos ? false : true;
        }
    }

    public void StartPunchAttack()
    {
        StartCoroutine(PunchBullet());
    }

    IEnumerator PunchBullet() // źȯ�� ���� ���� �������� �߻�
    {
        for (int j = 0; j < p_BulletNum; j++)
        {
            anim.SetTrigger("Punch");
            yield return new WaitForSeconds(0.1f);
            audioManager.C_PunchAudio();

            Vector3 direction = Quaternion.Euler(0, 180, 0) * Vector3.forward; // �� ���� 
            Vector3 bulletPos = new Vector3(transform.position.x, 2.5f, transform.position.z); // �Ѿ� ��ġ ����
            GameObject bullet = Instantiate(p_BaseAttackPrefab, bulletPos, Quaternion.identity); // �Ѿ� ����
            bullet.name = "PunchAttack"; // �Ѿ� �̸� ����         
            bullet.GetComponent<Rigidbody>().velocity = direction * p_AttackSpd; // źȯ ���� ����

            yield return new WaitForSeconds(1f);
        }
    }

    public void StartButtAttack()
    {
        StartCoroutine(ButtAttack());
    }

    IEnumerator ButtAttack()
    {
        anim.SetTrigger("Butt");
        yield return new WaitForSeconds(0.4f);
        audioManager.C_ButtAudio();

        int bu_AttackAngle = Random.Range(160, 200);
        Vector3 direction = Quaternion.Euler(0, bu_AttackAngle, 0) * Vector3.forward; // ������ ���� ���� ���
        Vector3 bulletPos = new Vector3(transform.position.x, 2f, transform.position.z); // �Ѿ� ��ġ ����
        GameObject bullet = Instantiate(bu_AttackPrefab, bulletPos, Quaternion.identity); // �Ѿ� ����
        bullet.name = "ButtAttack"; // �Ѿ� �̸� ����         
        bullet.GetComponent<Rigidbody>().velocity = direction * bu_AttackSpd; // źȯ ���� ����
        yield return new WaitForSeconds(1.5f);
        if(bullet != null)
        {
            bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        yield return new WaitForSeconds(4f);
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
