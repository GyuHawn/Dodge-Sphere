using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private GameDatas gameDatas;
    
    public GameObject player; // �÷��̾�

    public int playerNum; // ĳ���� Ȯ��
    public GameObject skillUI; // ��ų UI
    public GameObject[] playerSkills; // ĳ���� ��ų
    public Image skillCoolTime; // ��Ÿ�� UI

    public float coolTime; // ���� ��Ÿ��
    public float purificatTime; // 1�� ��ų ��Ÿ��
    public float reLoadTime; // 2�� ��ų ��Ÿ��

    public GameObject purificatEffect;

    private void Awake()
    {
        gameDatas = GameObject.Find("GameData").GetComponent<GameDatas>();
    }

    void Start()
    {

      /*  gameDatas.LoadFieldData<int>("playerNum", value => {
            playerNum = value;
        }, () => {
            playerNum = 1;
        });*/

        // ĳ���Ϳ� ���� ��Ÿ�� ����
        if (playerNum == 1)
        {
            purificatTime = 25;
        }
        else if (playerNum == 2)
        {
            reLoadTime = 30;
        }
    }

    void Update()
    {
        FindPlayer(); // �÷��̾� ã��
        UpdateCoolTime(); // ��Ÿ�� ������Ʈ
        UpdateSkillUI(); // �ʿ� ���� UI Ȱ��ȭ
    }
    
    void FindPlayer()
    {
        if (player == null) // �÷��̾� ã��
        {
            player = GameObject.Find("Player");
            playerMovement = player.GetComponent<PlayerMovement>();
        }
    }

    void UpdateCoolTime()  // ��Ÿ�� ������Ʈ
    {
        if (coolTime > 0) // ��Ÿ�� �� �ð� ���̱�
        {
            coolTime -= Time.deltaTime;
            skillCoolTime.gameObject.SetActive(true);

            // ��Ÿ�ӿ� ���� fillAmount ������Ʈ
            skillCoolTime.fillAmount = coolTime / (playerNum == 1 ? purificatTime : reLoadTime);
        }
        else
        {
            skillCoolTime.gameObject.SetActive(false);
            skillCoolTime.fillAmount = 0; // ��Ÿ���� ������ fillAmount�� 0����
        }
    }

    void UpdateSkillUI() // �ʿ� ���� UI Ȱ��ȭ
    {
        if (playerMovement.game) // ���� ���϶� Ȱ��ȭ
        {
            skillUI.SetActive(true);

            // �÷��̾� ���� ��ų Ȱ��ȭ
            playerSkills[0].SetActive(playerNum == 1);
            playerSkills[1].SetActive(playerNum == 2);
        }
        else
        {
            skillUI.SetActive(false);
        }
    }

    public void StartCooldown(float time) // ��Ÿ�� ����
    {
        coolTime = time;
        skillCoolTime.fillAmount = 1;
    }

    public void Purification() // ��� �Ѿ� ����
    {
        if (coolTime <= 0)
        {
            // ��� �Ѿ� ã��
            GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");

            // ã�� �Ѿ� ����
            foreach (GameObject bullet in bullets)
            {
                GameObject effect = Instantiate(purificatEffect, bullet.transform.position, Quaternion.identity);           
                Destroy(effect, 1f);
                Destroy(bullet);
            }
            
            // ��Ÿ�� ����
            StartCooldown(purificatTime);
        }
    }
    public void Reload() // ��� ���� ����
    {
        if(coolTime <= 0)
        {
            // ��ü ���� ã��
            GameObject[] cannons = GameObject.FindGameObjectsWithTag("Cannon");

            // ��ü ���� ����
            foreach (GameObject cannon in cannons)
            {
                Cannon p_Cannon = cannon.GetComponent<Cannon>();
                p_Cannon.currentBullet = p_Cannon.maxBullet;
            }

            StartCooldown(reLoadTime);
        }
    } 
}
