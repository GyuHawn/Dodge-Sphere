using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBullet : MonoBehaviour
{
    private ClownMonster clownMonster;

    public float speed;
    public GameObject targetCannon;

    private void Awake()
    {
        clownMonster = GameObject.Find("ClownMonster").GetComponent<ClownMonster>();
    }

    void Start()
    {
        // ���� �� 1�� ����
        if (clownMonster.playerCannons.Count > 0)
        {
            int randomIndex = Random.Range(0, clownMonster.playerCannons.Count);
            targetCannon = clownMonster.playerCannons[randomIndex];
        }

        speed = 5f;
    }

    void Update()
    {
        if (targetCannon != null) // �÷��̾ ����ٴ�
        {
            Vector3 direction = targetCannon.transform.position - transform.position;
            direction.Normalize();

            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cannon"))
        {
            Destroy(targetCannon);
            clownMonster.playerCannons.Remove(targetCannon);
            Destroy(gameObject);
        }
    }
}
