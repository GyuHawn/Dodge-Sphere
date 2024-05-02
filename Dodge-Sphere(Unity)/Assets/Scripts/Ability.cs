using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Ability : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Cannon cannon;

    public GameObject player;

    // ������ �ɷ�
    public int ability1Num;
    public int ability2Num;
    public int ability3Num;
    public int ability4Num;
    public int ability5Num;
    public int ability6Num;

    // �ɷ� 
    public bool healing = true; // �ѹ��� �ߵ��ϵ���

    private void Awake()
    {
        cannon = GameObject.Find("Manager").GetComponent<Cannon>();
    }

    private void Start()
    {
        ability1Num = PlayerPrefs.GetInt("Ability1");
        ability2Num = PlayerPrefs.GetInt("Ability2");
        ability3Num = PlayerPrefs.GetInt("Ability3");
        ability4Num = PlayerPrefs.GetInt("Ability4");
        ability5Num = PlayerPrefs.GetInt("Ability5");
        ability6Num = PlayerPrefs.GetInt("Ability6");
    }

    void Update()
    {
        if(player != null)
        {
            player = GameObject.Find("Player");
            playerMovement = player.GetComponent<PlayerMovement>();
        }
    }

    public void GetPlayerMP() // (30%) �Ѿ� ȹ��� Ȯ�������� �Ѿ� ȹ�� (�ɷ� 1-1)
    {
        if (ability1Num == 1)
        {
            int num = Random.Range(0, 10);
            if (num < 3)
            {
                playerMovement.bulletNum++;
            }
        }
    }

    public void GetCannonReload() // (10%) �Ѿ� ȹ��� Ȯ�������� ��� ���� �Ѿ� ���� (�ɷ� 1-2) 
    {
        if (ability1Num == 2)
        {
            int num = Random.Range(0, 10);
            if (num < 1)
            {
                GameObject[] cannons = GameObject.FindGameObjectsWithTag("Cannon");

                foreach (GameObject cannon in cannons)
                {
                    Cannon p_Cannon = cannon.GetComponent<Cannon>();
                    p_Cannon.currentBullet++;
                }
            }
        }
    }

    public void MPExtraAttack() // (30%) �Ѿ� ȹ��� Ȯ�������� ����ü ���� (�ɷ� 2-1) 
    {
        if (ability2Num == 1)
        {
            int num = Random.Range(0, 10);
            if (num < 3)
            {
                GameObject attack = Instantiate(playerMovement.extraAttack, player.transform.position, Quaternion.identity);
            }
        }
    }

    public void CannonExtraAttack() // (50%) ���� �� Ȯ�������� ����ü ���� (�ɷ� 2-2) 
    {
        if (ability2Num == 2)
        {
            int num = Random.Range(0, 10);
            if (num < 5)
            {
                GameObject attack = Instantiate(playerMovement.extraAttack, player.transform.position, Quaternion.identity);
            }
        }
    }

    public void GamblingCoin() // ���� ȹ��� 50%:50% ����:2�� ȹ�� (�ɷ� 3-1) 
    {
        if (ability3Num == 1)
        {
            int num = Random.Range(0, 10);
            if (num < 5)
            {

            }
        }
    }

    public void PlusCoin() // ���� ȹ��� 150%�� ȹ�� (�ɷ� 3-2) 
    {
        if (ability3Num == 2)
        {
        }
    }

    public void DoubleAttack() // (30%) ���ݽ� Ȯ�������� 2�� ���� (�ɷ� 4-1) 
    {
        if (ability4Num == 1)
        {
            int num = Random.Range(0, 10);
            if (num < 3)
            {
                cannon.AttackMonster();
            }
        }
    }

    public void PlusExtraAttack() // (80%) ���ݽ� Ȯ���� ����ü ���� (�ɷ� 4-2) 
    {
        if (ability4Num == 2)
        {
            int num = Random.Range(0, 10);
            if (num < 8)
            {
                GameObject attack = Instantiate(playerMovement.extraAttack, player.transform.position, Quaternion.identity);
            }
        }
    }

    public void Healing() // ���Ӵ� 1�� ü���� 25% ���Ϸ� �پ��� 50%ȸ�� (�ɷ� 5-1) 
    {
        if (ability5Num == 1)
        {
            if (playerMovement.currentHealth <= (playerMovement.maxHealth / 4) && healing)
            {
                healing = false;
                playerMovement.currentHealth = playerMovement.currentHealth + (playerMovement.maxHealth / 2);
            }
        }
    }

    public void Avoid() // (40%) �ǰݽ� Ȯ���� ���� (�ɷ� 5-2) 
    {
        if (ability5Num == 2)
        {
            int num = Random.Range(0, 10);
            if (num < 4)
            {

            }
        }
    }

    public void HitCannonReload() // �ǰݽ� ��� ���� �Ѿ� 1���� (�ɷ� 6-1) 
    {
        if (ability6Num == 1)
        {
            GameObject[] cannons = GameObject.FindGameObjectsWithTag("Cannon");

            foreach (GameObject cannon in cannons)
            {
                Cannon p_Cannon = cannon.GetComponent<Cannon>();
                p_Cannon.currentBullet++;
            }
        }
    }

    public void HitExtraAttack() // �ǰݽ� 2�� ����ü �߻� (�ɷ� 6-2) 
    {
        if (ability6Num == 2)
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject attack = Instantiate(playerMovement.extraAttack, player.transform.position, Quaternion.identity);
            }
        }
    }
}
