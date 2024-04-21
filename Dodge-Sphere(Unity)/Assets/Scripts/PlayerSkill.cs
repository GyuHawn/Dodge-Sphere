using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public GameObject player;

    public GameObject skill;
    public int playerNum;
    public GameObject[] playerSkills;
    public Image skillCoolTime;

    public float coolTime;
    public float reLoadTime;
    public float purificatTime;

    void Start()
    {
        playerNum = PlayerPrefs.GetInt("Player");

        if (playerNum == 1)
        {
            purificatTime = 30;
        }
        else if (playerNum == 2)
        {
            reLoadTime = 30;
        }
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
            playerMovement = player.GetComponent<PlayerMovement>();
        }

        if (coolTime > 0)
        {
            skillCoolTime.gameObject.SetActive(true);
            coolTime -= Time.deltaTime;

            // ��Ÿ�ӿ� ���� fillAmount ������Ʈ
            if (playerNum == 1)
            {
                skillCoolTime.fillAmount = coolTime / purificatTime;
            }
            else if (playerNum == 2)
            {
                skillCoolTime.fillAmount = coolTime / reLoadTime;
            }
        }
        else
        {
            skillCoolTime.gameObject.SetActive(false);
            skillCoolTime.fillAmount = 0; // ��Ÿ���� ������ fillAmount�� 0����
        }

        if (playerMovement.game) // ���� ���϶� Ȱ��ȭ
        {
            skill.SetActive(true);

            // �÷��̾� ���� ��ų Ȱ��ȭ
            if (playerNum == 1)
            {
                playerSkills[0].SetActive(true);
            }
            else if (playerNum == 2)
            {
                playerSkills[1].SetActive(true);
            }
        }
        else
        {
            skill.SetActive(false);
        }
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
                Destroy(bullet);
            }
            
            // ��Ÿ�� ����
            coolTime = purificatTime;
            skillCoolTime.fillAmount = 1;
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

            coolTime = reLoadTime;
            skillCoolTime.fillAmount = 1;
        }
    }
}
