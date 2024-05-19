using System.Collections;
using UnityEngine;

public class FaintBullet : MonoBehaviour
{
    public float speed;
    private GameObject player; // ���� �÷��̾�

    void Start()
    {
        player = GameObject.Find("Player");

        speed = 4f;

        Destroy(gameObject, 8f);  // 5�� �� ����
    }

    void Update()
    {
        if (player != null) // �÷��̾ ����ٴ�
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.Normalize();

            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
