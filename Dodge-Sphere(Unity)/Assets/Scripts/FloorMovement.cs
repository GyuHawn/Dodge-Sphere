using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMovement : MonoBehaviour
{
    public GameObject pFloorCheck; // �÷��̾� �ֺ� �ٴ� üũ
    public GameObject[] floors; // ��ü �ٴ�
    public float moveSpd = 2f; // �ٴ��� �̵� �ӵ�

    void Update()
    {
        FloorUpMove();
    }

    void FloorUpMove() // �÷��̾� �ֺ� �ٴ� �̵�
    {
        foreach (GameObject floor in floors)
        {           
            if (floor.GetComponent<Collider>().bounds.Intersects(pFloorCheck.GetComponent<Collider>().bounds)) // �ٴ��� pFloorCheck�� �浹�ϴ��� Ȯ��
            {           
                float newYPosition = Mathf.Lerp(floor.transform.position.y, 0f, Time.deltaTime * moveSpd); // �ٴ��� y0���� ������ �̵�
                floor.transform.position = new Vector3(floor.transform.position.x, newYPosition, floor.transform.position.z);
            }
        }
    }
}
