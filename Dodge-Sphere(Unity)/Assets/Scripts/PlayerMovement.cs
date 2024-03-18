using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private MonsterMap monsterMap;

    // �������� �÷��̾� ����
    public int maxHealth;
    public int currentHealth;
    public float moveSpd;
    public float rotateSpd;

    private float hAxis;
    private float vAxis;
    private Vector3 moveVec;

    public GameObject[] cannons; // ���� ����Ʈ

    // ���� ����
    public int bulletNum;

    // Ÿ�ϸ� ����
    public int moveNum; // �÷��̾� �̵� ����(�ϴ� int���, Ȯ���� bool����)
    public GameObject[] moveBtn; // �÷��̾� �̵���ư
    private Vector3 targetPosition; // �̵� ��ġ
    public GameObject tileCheck; // Ÿ��üũ ����
    public bool tile; // Ÿ�ϸ�����
    public float currentTile; // 0 - �� Ÿ��, 1 - �޽� Ÿ��, 2 - ������ Ÿ��, 3 - �̺�Ʈ Ÿ��, 4 - ���� Ÿ��, 5 - ���� Ÿ��
    public bool game; // ���Ӹ�����
    public float tileMoveSpd = 5f; // �̵� �ӵ�
    public float moveDistance = 2.3f; // �̵� �Ÿ�

    private Vector3 previousPosition; // ���� �÷��̾� ��ġ
    private float timeSinceLastMovement; // ���������� ������ �ð�
    public Vector3 finalPlayerPos; // ������ Ÿ�� ��ġ

    private Rigidbody rigid;
    private Collider collider;

    private void Awake()
    {
        monsterMap = GameObject.Find("Manager").GetComponent<MonsterMap>();
    }

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();

        moveSpd = 5;
        rotateSpd = 3f;

        maxHealth = 10;
        currentHealth = maxHealth;

        moveNum = 1;
        currentTile = 0;
        tileCheck.GetComponent<Collider>().enabled = false;
        tile = true;
        game = false;
    }


    void Update()
    {      
        if (currentHealth <= 0)
        {
            Die();
        }

        if (currentTile < 5 && !game)
        {
            if (Input.GetMouseButtonDown(0)) // Ŭ���� �ش���ġ�� �̵�
            {
                monsterMap.monsterNum = 1;

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
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * tileMoveSpd);
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

            // �̵��� 1�ʵ� Ÿ��üũ
            if (moveNum > 0)
            {
                tileCheck.GetComponent<Collider>().enabled = false;

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
        else if (game)
        {
            tileCheck.GetComponent<Collider>().enabled = false;

            foreach (GameObject move in moveBtn)
            {
                move.SetActive(false);
                move.GetComponent<Collider>().enabled = false;
            }

            // Ű���� ������ ���
            GetInput();
            Move();
            Rotate();
        }
/*        else // ���� �ο� ������
        {
            tileCheck.GetComponent<Collider>().enabled = true;

            foreach (GameObject move in moveBtn)
            {
                move.SetActive(true);
                move.GetComponent<Collider>().enabled = true;
            }
        }*/
    }

    /*void OnCollider() // �÷��̾� �浹 �ѱ�
    {
        collider.enabled = true;
    }
    IEnumerator delayOnCollider(float delay) // �����ð� �� �÷��̾� �浹 �ѱ�
    {
        yield return new WaitForSeconds(delay);
        collider.enabled = true;
    }
    void OffCollider() // �÷��̾� �浹 ����
    {
        collider.enabled = false;
    }*/

    public void OnTile() // Ÿ�� ������ �̵�
    {
        tile = true;
        game = false;

        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
    public void OnGame() // ���Ӹ����� �̵�
    {
        tile = false;
        game = true;
    }

    IEnumerator SaveFinalPosition() // ������ ��ġ ����
    {
        yield return new WaitForSeconds(1.5f);
        finalPlayerPos = transform.position;    
    }

    public void MoveFinalPosition() // Ÿ ��ũ��Ʈ �ڵ��
    {
        transform.position = finalPlayerPos;
    }

    IEnumerator TileCheck() // Ÿ��üũ
    {
        yield return new WaitForSeconds(1f);
        tileCheck.GetComponent<Collider>().enabled = true;
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
        StartCoroutine(SaveFinalPosition());
    }

    // ������ �̵� ����
    private void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        moveVec = new Vector3(hAxis, 0, vAxis);
    }

    private void Move()
    {
        transform.position += moveVec * moveSpd * Time.deltaTime;
    }

    private void Rotate()
    {
        if (moveVec != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveVec.normalized, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpd);
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            currentHealth -= bullet.damage;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("P_Attack"))
        {
            bulletNum++;
            Destroy(collision.gameObject);
        }
    }
}
 