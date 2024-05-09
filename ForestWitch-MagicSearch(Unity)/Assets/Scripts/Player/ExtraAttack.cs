using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraAttack : MonoBehaviour
{
    public int damage; // ������
    public float spd; // ���ǵ�

    public GameObject[] monsters; // ����
    private int currentTargetIndex = 0; // ���� Ÿ���� ���� ����

    void Start()
    {
        monsters = GameObject.FindGameObjectsWithTag("Monster"); // ��� ���͸� ã��
    }

    void Update()
    {
        if (monsters.Length > 0 && monsters[currentTargetIndex] != null) // ���Ͱ� �ְ� ���͸� Ÿ�� �� �϶�
        {
            Vector3 monsterPos = new Vector3(monsters[currentTargetIndex].transform.position.x, 2f, monsters[currentTargetIndex].transform.position.z); // Ÿ���� ������ ��ġ���� y���� 2�� ����
            Vector3 newPosition = Vector3.MoveTowards(transform.position, monsterPos, spd * Time.deltaTime); // ������ġ���� ��ǥ�������� spd�� �ӵ��� �̵�
            transform.position = newPosition; // ������ġ ������Ʈ 

            Vector3 directionToMonster = monsterPos - transform.position; // ���� �Ÿ� ���
            if (directionToMonster != Vector3.zero) // ���Ϳ� �Ÿ��� 0�� �ƴҶ�
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToMonster); // ���͸� �ٶ󺸴� ȸ���� ���
                targetRotation *= Quaternion.Euler(0, 90, 0); // ȸ������ �߰��� y�� 90�� ����
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, spd * Time.deltaTime); // ���翡�� ��ǥ���� �ε巴�� ȸ��
            }

            // ���� ��ǥ�� ������ٸ� ���� ���͸� ��ǥ�� ����
            if (monsters[currentTargetIndex] == null || !monsters[currentTargetIndex].activeInHierarchy)
            {
                currentTargetIndex = (currentTargetIndex + 1) % monsters.Length;
            }
        }
    }
}
