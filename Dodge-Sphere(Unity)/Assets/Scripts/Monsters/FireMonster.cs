using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMonster : MonoBehaviour
{
    public GameObject bulletPrefab; // �Ѿ� ������
    public float bulletSpeed = 20f; // �Ѿ� �ӵ�
    public int baseAttackNum = 10; // �߻��� źȯ�� ��

    private int[] baseAttackNums = { 20, 19, 20, 19, 20 }; // �Ѿ� ���� �迭
    private int currentBaseNumIndex = 0; // ���� �Ѿ� �ε���

    void Start()
    {
        bulletSpeed = 10f;
        InvokeRepeating("StartBaseAttacks", 1f, 8f); // 1�ʸ��� ����
    }

    void Update()
    {

    }

    private void StartBaseAttacks()
    {
       StartCoroutine(BaseAttacks());
    }

    IEnumerator BaseAttacks()
    {
        for (int i = 0; i < 5; i++)
        {
            baseAttackNum = baseAttackNums[currentBaseNumIndex]; // ���� �Ѿ� ���� ��������
            Basebullet();
            currentBaseNumIndex = (currentBaseNumIndex + 1) % baseAttackNums.Length; // ���� �Ѿ� �ε��� ����
            yield return new WaitForSeconds(1f);
        }
    }

    public void Basebullet() // źȯ�� ���� ���� �������� �߻�
    {
        for (int i = 0; i < baseAttackNum; i++)
        {
            float angle = i * (360f / baseAttackNum); // źȯ�� ���� ���                                             
            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward; // ������ ���� ���� ���
            Vector3 bulletPos = new Vector3(transform.position.x, 2f, transform.position.z); // �Ѿ� ��ġ ����
            GameObject bullet = Instantiate(bulletPrefab, bulletPos, Quaternion.identity); // �Ѿ� ����
            bullet.name = "FireAttack"; // �Ѿ� �̸� ����         
            bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed; // źȯ ���� ����

            Destroy(bullet, 4f); // 4�� �� �Ѿ� ����
        }
    }

    public void CryAttack()
    {

    }

    public void JumpAttack()
    {

    }

    public void RollAttack()
    {

    }
}
