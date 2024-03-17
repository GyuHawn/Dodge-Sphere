using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cannon : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private GameObject player; // �÷��̾�

    public bool ready; // ���� ����
    public int maxBullet; // �ִ� �Ѿ�
    public int currentBullet; // ���� �Ѿ�
    public GameObject loadBullet; // �Ѿ� ���� ��ġ
    public GameObject shotPos; // �Ѿ� �߻� ��ġ
    public float reloadDelay = 1f; // ������ ������
    private bool reloading = false; // �Ѿ� ������ �� ����
    public GameObject shotBullet; // �߻� �غ�� �Ѿ� 
    public GameObject shotBulletPrefab; // �߻��� �Ѿ� ������
    public TMP_Text currentBulletText; // ���� �Ѿ� ���� �ؽ�Ʈ


    private void Awake()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void Start()
    {
        player = GameObject.Find("Player");
        ready = false;
    }

    void Update()
    {
        currentBulletText.text = currentBullet + " / " + maxBullet;

        if (currentBullet < maxBullet)
        {
            ready = false;
        }
        else if (currentBullet >= maxBullet)
        {
            ready = true;
        }

        if (!reloading && !ready)
        {
            LoadBullet(); // �Ѿ� �ֱ�
        }
        else if (ready)
        {
            ShotBullet(); // �Ѿ� ����
        }

        if (shotBullet != null)
        {
            AttackMonster();
        }
    }

    void LoadBullet() // �Ѿ� �ֱ�
    {
        if (player != null && player.GetComponent<Collider>().bounds.Intersects(loadBullet.GetComponent<Collider>().bounds))
        {
            if (playerMovement.bulletNum > 0) // �Ѿ��� ������
            {
                reloading = true; // ������ ����
                StartCoroutine(ReloadDelayCoroutine());
            }
        }
    }

    IEnumerator ReloadDelayCoroutine()
    {
        yield return new WaitForSeconds(reloadDelay); // ������ ������
        reloading = false; // ������ ����

        if (playerMovement.bulletNum >= maxBullet)
        {
            if (currentBullet > 0)
            {
                playerMovement.bulletNum -= (maxBullet + currentBullet);
                currentBullet = maxBullet;
            }
            else
            {
                playerMovement.bulletNum -= maxBullet;
                currentBullet = maxBullet;
            }
        }
        else if (playerMovement.bulletNum < maxBullet)
        {
            if (currentBullet > 0)
            {
                currentBullet = playerMovement.bulletNum + currentBullet;
                playerMovement.bulletNum = 0;
            }
            else
            {
                currentBullet = playerMovement.bulletNum;
                playerMovement.bulletNum = 0;
            }
        }
    }

    void ShotBullet() // �Ѿ� ����
    {
        ready = false;
        currentBullet = 0;
        shotBullet = Instantiate(shotBulletPrefab, shotPos.transform.position, Quaternion.identity);
    }

    void AttackMonster()
    {
        GameObject monster = GameObject.FindGameObjectWithTag("Monster");

        if (monster != null && shotBullet != null)
        {
            // �Ѿ��� ���� ��ġ�� �������� y ���� ������ ���� ����
            Vector3 shotBulletPosition = shotBullet.transform.position;
            Vector3 fixedYVector = new Vector3(0f, 0.5f, 0f); // ���⼭ y ���� ���ϴ� ������ ����

            // ���� �������� ���ϴ� ���� ���
            Vector3 monsterDirection = (monster.transform.position - shotBulletPosition).normalized;

            // y ���� ������ ���Ϳ� ���� ���� ���͸� ���Ͽ� ���� ���� ���� ���
            Vector3 direction = monsterDirection + fixedYVector;

            // ���� ���� ���� ����ȭ
            direction.Normalize();

            // �Ѿ˿� ������ ���� ����
            float bulletSpeed = 50f; // �Ѿ��� �ӵ�
            Vector3 force = direction * bulletSpeed;

            // �Ѿ˿� ���� ���Ͽ� �߻�
            Rigidbody bulletRb = shotBullet.GetComponent<Rigidbody>();
            bulletRb.velocity = force;
        }
    }

}
