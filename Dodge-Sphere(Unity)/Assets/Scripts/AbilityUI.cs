using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityUI : MonoBehaviour
{
    public GameLevel gameLevel;

    // �ɷ� UI
    public GameObject abilityUI;
    public GameObject[] abilitys;
    public GameObject[] abilitys1;
    public GameObject[] abilitys2;
    public GameObject[] abilitys3;
    public GameObject[] abilitys4;
    public GameObject[] abilitys5;
    public GameObject[] abilitys6;

    // ������ �ɷ�
    public GameObject[] ability1Check;
    public int ability1Num;
    public GameObject[] ability2Check;
    public int ability2Num;
    public GameObject[] ability3Check;
    public int ability3Num;
    public GameObject[] ability4Check;
    public int ability4Num;
    public GameObject[] ability5Check;
    public int ability5Num;
    public GameObject[] ability6Check;
    public int ability6Num;

    public GameObject[] locks; // �ɷ� ���

    public bool on = true;

    public GameObject abilityEx;
    public TMP_Text abilityExText;

    private void Awake()
    {
        gameLevel = GameObject.Find("Manager").GetComponent<GameLevel>();
    }

    void Start()
    {
        LoadAbility();
    }

    void LoadAbility() // ���۽� ������ �ɷ� ����
    {
        ability1Num = PlayerPrefs.GetInt("Ability1", 0);
        ability2Num = PlayerPrefs.GetInt("Ability2", 0);
        ability3Num = PlayerPrefs.GetInt("Ability3", 0);
        ability4Num = PlayerPrefs.GetInt("Ability4", 0);
        ability5Num = PlayerPrefs.GetInt("Ability5", 0);
        ability6Num = PlayerPrefs.GetInt("Ability6", 0);
    }
    
    void Update()
    {
        AbilityLock(); // ���� ������ ���� �ɷ¿���
        CheckAbility(); // ������ �ɷ� ǥ��
    }

    void CheckAbility()
    {
        if(ability1Num == 1)
        {
            ability1Check[0].SetActive(true);
            ability1Check[1].SetActive(false);
        }
        else if (ability1Num == 2)
        {
            ability1Check[0].SetActive(false);
            ability1Check[1].SetActive(true);
        }
        else
        {
            ability1Check[0].SetActive(false);
            ability1Check[1].SetActive(false);
        }

        if (ability2Num == 1)
        {
            ability2Check[0].SetActive(true);
            ability2Check[1].SetActive(false);
        }
        else if (ability2Num == 2)
        {
            ability2Check[0].SetActive(false);
            ability2Check[1].SetActive(true);
        }
        else
        {
            ability2Check[0].SetActive(false);
            ability2Check[1].SetActive(false);
        }

        if (ability3Num == 1)
        {
            ability3Check[0].SetActive(true);
            ability3Check[1].SetActive(false);
        }
        else if (ability3Num == 2)
        {
            ability3Check[0].SetActive(false);
            ability3Check[1].SetActive(true);
        }
        else
        {
            ability3Check[0].SetActive(false);
            ability3Check[1].SetActive(false);
        }

        if (ability4Num == 1)
        {
            ability4Check[0].SetActive(true);
            ability4Check[1].SetActive(false);
        }
        else if (ability4Num == 2)
        {
            ability4Check[0].SetActive(false);
            ability4Check[1].SetActive(true);
        }
        else
        {
            ability4Check[0].SetActive(false);
            ability4Check[1].SetActive(false);
        }

        if (ability5Num == 1)
        {
            ability5Check[0].SetActive(true);
            ability5Check[1].SetActive(false);
        }
        else if (ability5Num == 2)
        {
            ability5Check[0].SetActive(false);
            ability5Check[1].SetActive(true);
        }
        else
        {
            ability5Check[0].SetActive(false);
            ability5Check[1].SetActive(false);
        }

        if (ability6Num == 1)
        {
            ability6Check[0].SetActive(true);
            ability6Check[1].SetActive(false);
        }
        else if (ability6Num == 2)
        {
            ability6Check[0].SetActive(false);
            ability6Check[1].SetActive(true);
        }
        else
        {
            ability6Check[0].SetActive(false);
            ability6Check[1].SetActive(false);
        }
    }

    public void AbilityLock() // ���� ������ ���� �ɷ¿���
    {
        if (!on)
        {

            if (gameLevel.gameLevel >= 2)
            {
                locks[0].SetActive(false);
            }
            else
            {
                locks[0].SetActive(true);
            }
            if (gameLevel.gameLevel >= 3)
            {
                locks[1].SetActive(false);
            }
            else
            {
                locks[1].SetActive(true);
            }
            if (gameLevel.gameLevel >= 5)
            {
                locks[2].SetActive(false);
            }
            else
            {
                locks[2].SetActive(true);
            }
            if (gameLevel.gameLevel >= 6)
            {
                locks[3].SetActive(false);
            }
            else
            {
                locks[3].SetActive(true);
            }
            if (gameLevel.gameLevel >= 8)
            {
                locks[4].SetActive(false);
            }
            else
            {
                locks[4].SetActive(true);
            }
            if (gameLevel.gameLevel >= 9)
            {
                locks[5].SetActive(false);
            }
            else
            {
                locks[5].SetActive(true);
            }
        }
        else
        {
            locks[0].SetActive(false);
            locks[1].SetActive(false);
            locks[2].SetActive(false);
            locks[3].SetActive(false);
            locks[4].SetActive(false);
            locks[5].SetActive(false);
        }
    }

    public void OnAbility() // UI �̵�
    {
        if (on)
        {
            on = false;

            for (int i = 0; i < abilitys.Length; i++)
            {
                StartCoroutine(MoveAbility_X(abilitys[i], -100 * (i + 1), true));
            }

            foreach (GameObject abili in abilitys1)
            {
                abili.SetActive(true);
            }
            foreach (GameObject abili in abilitys2)
            {
                abili.SetActive(true);
            }
            foreach (GameObject abili in abilitys3)
            {
                abili.SetActive(true);
            }
            foreach (GameObject abili in abilitys4)
            {
                abili.SetActive(true);
            }
            foreach (GameObject abili in abilitys5)
            {
                abili.SetActive(true);
            }
            foreach (GameObject abili in abilitys6)
            {
                abili.SetActive(true);
            }     
        }
        else
        {
            abilityEx.SetActive(false);
            abilityExText.text = "";
            on = true;

            for (int i = 0; i < abilitys.Length; i++)
            {
                StartCoroutine(MoveAbility_X(abilitys[i], +100 * (i + 1), false));
            }

            foreach (GameObject abili in abilitys1)
            {
                abili.SetActive(false);
            }
            foreach (GameObject abili in abilitys2)
            {
                abili.SetActive(false);
            }
            foreach (GameObject abili in abilitys3)
            {
                abili.SetActive(false);
            }
            foreach (GameObject abili in abilitys4)
            {
                abili.SetActive(false);
            }
            foreach (GameObject abili in abilitys5)
            {
                abili.SetActive(false);
            }
            foreach (GameObject abili in abilitys6)
            {
                abili.SetActive(false);
            }        
        }
    }

    IEnumerator MoveAbility_X(GameObject ability, float moveX, bool on) // �ε巴�� UI�̵�
    {
        if (on)
        {
            ability.gameObject.SetActive(true);
        }

        RectTransform rectTransform = ability.GetComponent<RectTransform>();
        Vector2 targetPosition = new Vector2(rectTransform.anchoredPosition.x + moveX, rectTransform.anchoredPosition.y);

        while (Vector2.Distance(rectTransform.anchoredPosition, targetPosition) > 0.05f)
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, targetPosition, 300f * Time.deltaTime);
            yield return null;
        }

        if(!on)
        {
            ability.gameObject.SetActive(false);
        }
    }

    public void Ability1_1()
    {
        abilityEx.SetActive(true);
        ability1Num = 1;
        PlayerPrefs.SetInt("Ability1", 1);
        abilityExText.text = "�Ѿ��� ȹ��� 30%�� Ȯ����\n�߰� �Ѿ��� 1�� �� ȹ���մϴ�.";
    }
    public void Ability1_2()
    {
        abilityEx.SetActive(true);
        ability1Num = 2;
        PlayerPrefs.SetInt("Ability1", 2);
        abilityExText.text = "�Ѿ��� ȹ��� 10%�� Ȯ����\n��� ������ 1���� �Ѿ��� �����մϴ�.";
    }
    public void Ability2_1()
    {
        abilityEx.SetActive(true);
        ability2Num = 1;
        PlayerPrefs.SetInt("Ability2", 1);
        abilityExText.text = "�Ѿ��� ȹ��� 30%�� Ȯ����\n�������� ���� ���� �����Ͽ� ���� �մϴ�.";
    }
    public void Ability2_2()
    {
        abilityEx.SetActive(true);
        ability2Num = 2;
        PlayerPrefs.SetInt("Ability2", 2);
        abilityExText.text = "���ݽ� 30%�� Ȯ����\n�������� ���� ���� �����Ͽ� ���� �մϴ�.";
    }
    public void Ability3_1()
    {
        abilityEx.SetActive(true);
        ability3Num = 1;
        PlayerPrefs.SetInt("Ability3", 1);
        abilityExText.text = "���� ȹ��� 50%�� Ȯ����\nȹ���� ���� ���� or 2��� ȹ�� �մϴ�.";
    }
    public void Ability3_2()
    {
        abilityEx.SetActive(true);
        ability3Num = 2;
        PlayerPrefs.SetInt("Ability3", 2);
        abilityExText.text = "���� ȹ��� 50% �߰��� ȹ���մϴ�.";
    }
    public void Ability4_1()
    {
        abilityEx.SetActive(true);
        ability4Num = 1;
        PlayerPrefs.SetInt("Ability4", 1);
        abilityExText.text = "���ݽ� 30%�� Ȯ���� 1ȸ �߰� ������ �մϴ�.";
    }
    public void Ability4_2()
    {
        abilityEx.SetActive(true);
        ability4Num = 2;
        PlayerPrefs.SetInt("Ability4", 2);
        abilityExText.text = "���ݽ� 80%�� Ȯ����\n�������� ���� ���� �����Ͽ� ���� �մϴ�.";
    }
    public void Ability5_1()
    {
        abilityEx.SetActive(true);
        ability5Num = 1;
        PlayerPrefs.SetInt("Ability5", 1);
        abilityExText.text = "���� �� 1ȸ ü���� 25% ���Ϸ� �پ���\n �ڵ����� ü���� 50%�� ȸ���մϴ�.";
    }
    public void Ability5_2()
    {
        abilityEx.SetActive(true);
        ability5Num = 2;
        PlayerPrefs.SetInt("Ability5", 2);
        abilityExText.text = "�ǰ� �� ���� Ȯ���� ���� ���ظ� �����մϴ�.";
    }
    public void Ability6_1()
    {
        abilityEx.SetActive(true);
        ability6Num = 1;
        PlayerPrefs.SetInt("Ability6", 1);
        abilityExText.text = "�ǰ� �ø��� ��� ������ �Ѿ� 1���� �����մϴ�.";
    }
    public void Ability6_2()
    {
        abilityEx.SetActive(true);
        ability6Num = 2;
        PlayerPrefs.SetInt("Ability6", 2);
        abilityExText.text = "�ǰ� �ø��� �������� ���� ���� �����Ͽ� 2�� ���� �մϴ�.";
    }
}
