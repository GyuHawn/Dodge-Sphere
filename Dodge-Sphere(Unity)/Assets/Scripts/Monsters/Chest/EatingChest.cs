using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatingChest : MonoBehaviour
{
    public GameObject playerBullet;
    public bool findBullet;
    public float speed = 300f;

    void Update()
    {
        if (!findBullet)
        {
            if (!IsInvoking("FindBullet")) // FindBullet �ڷ�ƾ�� �̹� ���۵��� �ʾҴٸ�
            {
                StartCoroutine(FindBullet());
            }
        }
        else
        {
            if (playerBullet == null)
            {
                // playerBullet�� �ı��Ǿ��ų� �� �̻� �������� �ʴ� ���
                findBullet = false; // findBullet�� false�� �����Ͽ� ���ο� playerBullet�� ã�� �� �ֵ��� ��
            }
            else
            {
                EatingBullet(); // findBullet�� true�̰� playerBullet�� ������ �� ����ؼ� EatingBullet�� ȣ��
            }
        }
    }

    IEnumerator FindBullet()
    {
        findBullet = false; // �ڷ�ƾ�� ���۵� �� findBullet�� �ʱ�ȭ
        while (true)
        {
            playerBullet = GameObject.FindWithTag("P_Attack");
            if (playerBullet != null)
            {
                findBullet = true; // �÷��̾��� ������ ã�Ҵٸ� findBullet�� true�� ����
                yield break; // �ڷ�ƾ ����
            }
            else
            {
                yield return new WaitForSeconds(2); // �÷��̾��� ������ ã�� ���ߴٸ� 2�� �� �ٽ� �õ�
            }
        }
    }


    void EatingBullet()
    {
        if (playerBullet != null)
        {
            float step = speed * Time.deltaTime;
            playerBullet.transform.position = Vector3.MoveTowards(playerBullet.transform.position, transform.position, step);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ChestMonster monster = GameObject.Find("ChestMonster").GetComponent<ChestMonster>();
            monster.e_Eating = false;
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("P_Attack"))
        {
            findBullet = false;
            Destroy(collision.gameObject);
        }
    }
}
