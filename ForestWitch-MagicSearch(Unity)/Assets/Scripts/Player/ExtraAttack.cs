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
        if (monsters.Length > 0 && monsters[currentTargetIndex] != null)
        {
            Vector3 monsterPos = new Vector3(monsters[currentTargetIndex].transform.position.x, 2f, monsters[currentTargetIndex].transform.position.z);
            Vector3 newPosition = Vector3.MoveTowards(transform.position, monsterPos, spd * Time.deltaTime);
            transform.position = newPosition;

            Vector3 directionToFace = monsterPos - transform.position;
            if (directionToFace != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToFace);
                targetRotation *= Quaternion.Euler(0, 90, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, spd * Time.deltaTime);
            }

            // ���� ��ǥ�� ������ٸ� ���� ���͸� ��ǥ�� ����
            if (monsters[currentTargetIndex] == null || !monsters[currentTargetIndex].activeInHierarchy)
            {
                currentTargetIndex = (currentTargetIndex + 1) % monsters.Length;
            }
        }
    }
}
