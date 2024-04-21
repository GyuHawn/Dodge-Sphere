using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cannon : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private AudioManager audioManager;

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
    public float bulletSpd; // �Ѿ��� �ӵ�

    // ������ ����
    public bool book;

    private void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Start()
    {
        ready = false;
    }

    void ItemSetting()
    {
        // ȭ�� ������ ȹ��� �ӵ���
        if (!playerMovement.arrow) 
        {
            bulletSpd = 30f;
        }
        else
        {
            bulletSpd = 40f;
        }

        // å ������ ȹ��� �ִ� �Ѿ˼� 1����
        if (playerMovement.bow && !book) 
        {
            book = true;

            if (maxBullet > 1)
            {
                maxBullet--;
            }
        }
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
            playerMovement = player.GetComponent<PlayerMovement>();
        }

        // ������ ����
        ItemSetting();

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
        audioManager.CannonAudio();
        shotBullet = Instantiate(shotBulletPrefab, shotPos.transform.position, Quaternion.identity);
    }

    void AttackMonster()
    {
        // ��� ���͸� ã���ϴ�.
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

        if (monsters.Length > 0 && shotBullet != null)
        {
            
            foreach (var monster in monsters)
            {
                Vector3 shotBulletPosition = shotBullet.transform.position;
                Vector3 fixedYVector = new Vector3(0f, 0.5f, 0f);

                Vector3 monsterDirection = (monster.transform.position - shotBulletPosition).normalized;

                Vector3 direction = monsterDirection + fixedYVector;

                direction.Normalize();

                Vector3 force = direction * bulletSpd;

                Rigidbody bulletRb = shotBullet.GetComponent<Rigidbody>();
                bulletRb.velocity = force;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BreakBullet"))
        {
            Destroy(gameObject);
        }
    }
}
