using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusPunch : MonoBehaviour
{
    public GameObject p_AttackPrefab; // �Ѿ� ������
    public int p_AttackNum; // �߻� ��
    public int p_AttackAngle; // �߻� �ޱ�
    public int p_AttackSpd; // �߻� �ޱ�

    void Start()
    {
        p_AttackNum = 25;
        p_AttackSpd = 8;

        Invoke("Attack", 0.8f);
    }

    void Attack()
    {
        for (int j = 0; j < p_AttackNum; j++)
        {
            p_AttackAngle = Random.Range(0, 361); // źȯ�� ���� ���                                             
            Vector3 direction = Quaternion.Euler(0, p_AttackAngle, 0) * Vector3.forward; // ������ ���� ���� ���
            Vector3 bulletPos = new Vector3(transform.position.x, 2f, transform.position.z); // �Ѿ� ��ġ ����
            GameObject bullet = Instantiate(p_AttackPrefab, bulletPos, Quaternion.identity); // �Ѿ� ����
            bullet.name = "WaveFireAttack"; // �Ѿ� �̸� ����         
            bullet.GetComponent<Rigidbody>().velocity = direction * p_AttackSpd; // źȯ ���� ����

            Destroy(bullet, 2.5f); // 2.5�� �� �Ѿ� ����
        }
        Destroy(gameObject);
    }
}
