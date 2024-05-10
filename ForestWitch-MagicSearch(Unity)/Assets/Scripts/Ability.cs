using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Ability : MonoBehaviour
{
    private PlayerMovement playerMovement;

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

    private void Start()
    {
        /*ability1Num = PlayerPrefs.GetInt("Ability1");
        ability2Num = PlayerPrefs.GetInt("Ability2");
        ability3Num = PlayerPrefs.GetInt("Ability3");
        ability4Num = PlayerPrefs.GetInt("Ability4");
        ability5Num = PlayerPrefs.GetInt("Ability5");
        ability6Num = PlayerPrefs.GetInt("Ability6");*/
        GPGSBinder.Inst.LoadCloud("Ability1", (success, data) => {
            if (int.TryParse(data, out int loadedAbility1))
            {
                ability1Num = loadedAbility1;
            }
            else
            {
                ability1Num = 0;
            }
        });
        GPGSBinder.Inst.LoadCloud("Ability2", (success, data) => {
            if (int.TryParse(data, out int loadedAbility1))
            {
                ability2Num = loadedAbility1;
            }
            else
            {
                ability2Num = 0;
            }
        });
        GPGSBinder.Inst.LoadCloud("Ability3", (success, data) => {
            if (int.TryParse(data, out int loadedAbility1))
            {
                ability3Num = loadedAbility1;
            }
            else
            {
                ability3Num = 0;
            }
        });
        GPGSBinder.Inst.LoadCloud("Ability4", (success, data) => {
            if (int.TryParse(data, out int loadedAbility1))
            {
                ability4Num = loadedAbility1;
            }
            else
            {
                ability4Num = 0;
            }
        });
        GPGSBinder.Inst.LoadCloud("Ability5", (success, data) => {
            if (int.TryParse(data, out int loadedAbility1))
            {
                ability5Num = loadedAbility1;
            }
            else
            {
                ability5Num = 0;
            }
        });
        GPGSBinder.Inst.LoadCloud("Ability6", (success, data) => {
            if (int.TryParse(data, out int loadedAbility1))
            {
                ability6Num = loadedAbility1;
            }
            else
            {
                ability6Num = 0;
            }
        });
    }

    void Update()
    {
        FindPlayer(); // �÷��̾� ã��
    }

    void FindPlayer()
    {
        if (player == null) // �÷��̾� ã��
        {
            player = GameObject.Find("Player");
            playerMovement = player.GetComponent<PlayerMovement>();
        }
    }

    public void GetPlayerMP() // (30%) �Ѿ� ȹ��� Ȯ�������� �Ѿ� ȹ�� (�ɷ� 1-1)
    {
        int num = Random.Range(0, 10);
        if (num < 3)
        {
            playerMovement.bulletNum++;
        }
    }

    public void GetCannonReload() // (10%) �Ѿ� ȹ��� Ȯ�������� ��� ���� �Ѿ� 1 ���� (�ɷ� 1-2) 
    {
        Debug.Log("1_2");
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

    public void MPExtraAttack() // (30%) �Ѿ� ȹ��� Ȯ�������� ����ü ���� (�ɷ� 2-1) 
    {
        int num = Random.Range(0, 10);
        if (num < 3)
        {
            GameObject attack = Instantiate(playerMovement.extraAttack, player.transform.position, Quaternion.identity);
            playerMovement.extraAttacks.Add(attack);
        }
    }

    public void CannonExtraAttack() // (50%) ���� �� Ȯ�������� ����ü ���� (�ɷ� 2-2) 
    {
        Debug.Log("2_2");
        int num = Random.Range(0, 10);
        if (num < 5)
        {
            GameObject attack = Instantiate(playerMovement.extraAttack, player.transform.position, Quaternion.identity);
            playerMovement.extraAttacks.Add(attack);
        }
    }

    public int GamblingCoin(int money) // ���� ȹ��� 50%:50% ����:2�� ȹ�� (�ɷ� 3-1) 
    {
        int num = Random.Range(0, 10);
        if (num < 5)
        {
            return money * 2;
        }
        else
        {
            return (int)(money * 0.5f);
        }
    }

    public int PlusCoin(int money) // ���� ȹ��� 150%�� ȹ�� (�ɷ� 3-2) 
    {
        Debug.Log("3_2");
        return (int)(money * 1.5f);
    }

    // (30%) ���ݽ� Ȯ�������� �ѹ��� ���� (�ɷ� 4-1)  - Cannon ��ũ��Ʈ�� ���� ����
                             
    public void PlusExtraAttack() // (80%) ���ݽ� Ȯ���� ����ü ���� (�ɷ� 4-2) 
    {
        Debug.Log("4_2");
        int num = Random.Range(0, 10);
        if (num < 8)
        {
            GameObject attack = Instantiate(playerMovement.extraAttack, player.transform.position, Quaternion.identity);
            playerMovement.extraAttacks.Add(attack);
        }
    }

    // ���Ӵ� 1�� ü���� 25% ���Ϸ� �پ��� 50%ȸ�� (�ɷ� 5-1)  - PlayerMovement ��ũ��Ʈ�� ���� ����
    // �ǰݽ� Ȯ���� ���� (�ɷ� 5-2)  - PlayerMovement ��ũ��Ʈ�� ���� ����

    public void HitCannonReload() // �ǰݽ� ��� ���� �Ѿ� 1���� (�ɷ� 6-1) 
    {
        GameObject[] cannons = GameObject.FindGameObjectsWithTag("Cannon");

        foreach (GameObject cannon in cannons)
        {
            Cannon p_Cannon = cannon.GetComponent<Cannon>();
            p_Cannon.currentBullet++;
        }
    }

    public void HitExtraAttack() // �ǰݽ� 2�� ����ü �߻� (�ɷ� 6-2) 
    {
        Debug.Log("6_2");
        for (int i = 0; i < 2; i++)
        {
            GameObject attack = Instantiate(playerMovement.extraAttack, player.transform.position, Quaternion.identity);
            playerMovement.extraAttacks.Add(attack);
        }
    }
}
