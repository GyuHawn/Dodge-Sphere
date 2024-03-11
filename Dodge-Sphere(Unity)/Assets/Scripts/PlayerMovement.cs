using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int moveNum; // �÷��̾� �̵� ����(�ϴ� int���, Ȯ���� bool����)
    public GameObject[] moveBtn; // �÷��̾� �̵���ư
    private Vector3 targetPosition; // �̵� ��ġ
    public GameObject tileCheck; // Ÿ��üũ ����
    public bool tile; // Ÿ�ϸ�����
    public bool game; // ���Ӹ�����

    public float moveDistance = 2.3f; // �̵� �Ÿ�
    public float moveSpd = 5f; // �̵� �ӵ�

    private void Start()
    {
        moveNum = 1;
        tileCheck.SetActive(false);
        tile = true;
        game = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Ŭ���� �ش���ġ�� �̵�
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                for (int i = 0; i < moveBtn.Length; i++)
                {
                    if (hit.collider.gameObject == moveBtn[i])
                    {
                        MovePlayer(i);
                        break;
                    }
                }
            }
        }

        // Lerp�� ����Ͽ� ��ǥ ��ġ�� �̵�
        float newY = transform.position.y; // ���� y�� ����
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpd);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z); // y�� ����

        // Ÿ���� ���°����δ� �̵��Ұ�
        if (transform.position.x <= -4f)
        {
            moveBtn[0].SetActive(false);
            moveBtn[0].GetComponent<Collider>().enabled = false;
        }
        else
        {
            moveBtn[0].SetActive(true);
            moveBtn[0].GetComponent<Collider>().enabled = true;
        }

        if (transform.position.x >= 4f)
        {
            moveBtn[2].SetActive(false);
            moveBtn[2].GetComponent<Collider>().enabled = false;
        }
        else
        {
            moveBtn[2].SetActive(true);
            moveBtn[2].GetComponent<Collider>().enabled = true;
        }

        // �̵��Ұ��� 1�ʵ� Ÿ��üũ
        if (moveNum > 0)
        {
            tileCheck.SetActive(false);

            foreach (GameObject move in moveBtn)
            {
                move.SetActive(true);
                move.GetComponent<Collider>().enabled = true;
            }
        }
        else
        {
            StartCoroutine(TileCheck());
            foreach (GameObject move in moveBtn)
            {
                move.SetActive(false);
                move.GetComponent<Collider>().enabled = false;
            }
        }
    }

    IEnumerator TileCheck() // Ÿ��üũ
    {
        yield return new WaitForSeconds(1f);
        tileCheck.SetActive(true);
    }

    void MovePlayer(int direction) // �̵��Ÿ�
    {
        moveNum = 0;
        switch (direction)
        {
            case 0:
                targetPosition += new Vector3(-moveDistance, 0f, moveDistance);
                break;
            case 1:
                targetPosition += new Vector3(0f, 0f, moveDistance);
                break;
            case 2:
                targetPosition += new Vector3(moveDistance, 0f, moveDistance);
                break;
            default:
                break;
        }
    }
}
