using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMonster : MonoBehaviour
{
    // �⺻ ����
    public int maxHealth;
    public int currentHealth;

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

    void Start()
    {
        b_AttackSpd = 10f;
        b_BulletNums = new int[] { 30, 29, 30, 29, 30 };

        c_AttackSpd = 10f;
        c_AttackNum = 4;
        c_AttackAngles1 = new int[] { 135, 180, 225 };
        c_AttackAngles2 = new float[] { 157.5f, 202.5f };

        j_AttackSpd = 15f;
        j_AttackNum = 15;
        j_BulletNum = 10;

        r_AttackSpd = 7f;
        r_AttackNum = 3;

        InvokeRepeating("StartPattern", 1f, 10f); // ���� ���� ����
    }

    void Update()
    {

    }

    void StartPattern() // ���� ���� ����
    {
        int randomPattern = Random.Range(0, 4); // 0 ~ 3 ����

        switch (randomPattern)
        {
            case 0:
                StartBaseAttacks();
                break;
            case 1:
                StartCryAttacks();
                break;
            case 2:
                StartJumpAttacks();
                break;
            case 3:
                StartRollAttack();
                break;
        }
    }

    private void StartBaseAttacks()
    {
       StartCoroutine(BaseAttacks());
    }

    IEnumerator BaseAttacks()
    {
        for (int i = 0; i < 5; i++)
        {
            b_BulletNum = b_BulletNums[b_CurrentNumIndex]; // ���� �Ѿ� ���� ��������
            StartCoroutine(Basebullet());
            b_CurrentNumIndex = (b_CurrentNumIndex + 1) % b_BulletNums.Length; // ���� �Ѿ� �ε��� ����
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator Basebullet() // źȯ�� ���� ���� �������� �߻�
    {
        for (int i = 0; i < b_BulletNum; i++)
        {
            float angle = i * (360f / b_BulletNum); // źȯ�� ���� ���                                             
            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward; // ������ ���� ���� ���
            Vector3 bulletPos = new Vector3(transform.position.x, 2f, transform.position.z); // �Ѿ� ��ġ ����
            GameObject bullet = Instantiate(b_AttackPrefab, bulletPos, Quaternion.identity); // �Ѿ� ����
            bullet.name = "BaseFireAttack"; // �Ѿ� �̸� ����         
            bullet.GetComponent<Rigidbody>().velocity = direction * b_AttackSpd; // źȯ ���� ����
            
            Destroy(bullet, 4f); // 4�� �� �Ѿ� ����

            yield return new WaitForSeconds(0.05f);
        }
    }

    private void StartCryAttacks()
    {
        StartCoroutine(CryAttacks());
    }

    IEnumerator CryAttacks() // źȯ�� ���� ���� �������� �߻�
    {
        for (int i = 0; i < 4; i++) // �� 2�� �߻�
        {
            // c_AttackAngles1���� 3�� �߻�
            for (int j = 0; j < 3; j++)
            {
                float angle = c_AttackAngles1[j]; // źȯ�� ���� ���                                             
                Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward; // ������ ���� ���� ���
                Vector3 bulletPos = new Vector3(transform.position.x, 2.55f, transform.position.z); // �Ѿ� ��ġ ����
                GameObject bullet = Instantiate(c_AttackPrefab, bulletPos, Quaternion.identity); // �Ѿ� ����
                bullet.name = "CryFireAttack"; // �Ѿ� �̸� ����         
                bullet.GetComponent<Rigidbody>().velocity = direction * c_AttackSpd; // źȯ ���� ����

                Destroy(bullet, 4f); // 4�� �� �Ѿ� ����
            }

            yield return new WaitForSeconds(0.75f); // 1�� ���

            // c_AttackAngles2���� 2�� �߻�
            for (int k = 0; k < 2; k++)
            {
                float angle = c_AttackAngles2[k]; // źȯ�� ���� ���                                             
                Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward; // ������ ���� ���� ���
                Vector3 bulletPos = new Vector3(transform.position.x, 2.55f, transform.position.z); // �Ѿ� ��ġ ����
                GameObject bullet = Instantiate(c_AttackPrefab, bulletPos, Quaternion.identity); // �Ѿ� ����
                bullet.name = "CryFireAttack"; // �Ѿ� �̸� ����         
                bullet.GetComponent<Rigidbody>().velocity = direction * c_AttackSpd; // źȯ ���� ����

                Destroy(bullet, 4f); // 4�� �� �Ѿ� ����
            }
        }
    }

    public void StartJumpAttacks()
    {       
        StartCoroutine(JumpAttack());
    }

    IEnumerator JumpAttack()
    {
        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(3);
            for (int j = 0; j < j_AttackNum; j++)
            {
                float angle = Random.Range(135, 225);
                StartCoroutine(Jumpbullet(angle));
            }
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

            Destroy(bullet, 4f); // 4�� �� �Ѿ� ����

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void StartRollAttack()
    {
        StartCoroutine(RollAttack());
    }

    IEnumerator RollAttack()
    {
        Vector3 direction = Quaternion.Euler(0, 180, 0) * Vector3.forward; // ������ ���� ���� ���
        Vector3 bulletPos = new Vector3(r_AttackPos[0].transform.position.x, 2f, r_AttackPos[0].transform.position.z); // �Ѿ� ��ġ ����
        GameObject bullet = Instantiate(r_AttackPrefab, bulletPos, Quaternion.identity); // �Ѿ� ����
        bullet.name = "RollFireAttack"; // �Ѿ� �̸� ����         
        bullet.GetComponent<Rigidbody>().velocity = direction * r_AttackSpd; // źȯ ���� ����

        Destroy(bullet, 8f);

        yield return new WaitForSeconds(1f);

        for (int j = 0; j < r_AttackNum - 1; j++)
        {
            direction = Quaternion.Euler(0, 180, 0) * Vector3.forward; // ������ ���� ���� ���
            bulletPos = new Vector3(r_AttackPos[j + 1].transform.position.x, 2f, r_AttackPos[j + 1].transform.position.z); // �Ѿ� ��ġ ����
            bullet = Instantiate(r_AttackPrefab, bulletPos, Quaternion.identity); // �Ѿ� ����
            bullet.name = "RollFireAttack"; // �Ѿ� �̸� ����         
            bullet.GetComponent<Rigidbody>().velocity = direction * r_AttackSpd; // źȯ ���� ����

            Destroy(bullet, 8f);
        }
    }
}
