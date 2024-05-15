using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Diagnostics.Tracing;

public class PlayerMovement : MonoBehaviour
{
    private GameDatas gameDatas;
    private MonsterMap monsterMap;
    private ClearInfor clearInfor;
    private MapSetting mapSetting;
    private Ability ability;

    public FixedJoystick fixedJoyStick;
    public GameObject joyStick;

    // ĳ���� Ȯ��
    public int playerNum; // 1 = ����, 2 = �Ķ�

    // �������� �÷��̾� ����
    public int maxHealth; // �ִ� ü��
    public int currentHealth; // ���� ü��
    public float moveSpd; // �̵� �ӵ�
    public float rotateSpd; // ȸ�� �ӵ�
    public bool itemShield; // ���� ������ ȹ��� ��ȣ�� ���

    // �̵�����
    private float hAxis;
    private float vAxis;
    private Vector3 moveVec;

    // ���� ����
    public int bulletNum; // �Ѿ� ����
    public TMP_Text bulletNumText;

    public GameObject extraAttack; // ����ü ���� 
    public List<GameObject> extraAttacks = new List<GameObject>();

    // ��Ÿ
    public int money; // ��
    public bool faint; // ��������
     
    // Ÿ�ϸ� ����
    public int moveNum; // �÷��̾� �̵� (5.1~ : ����)
    public GameObject[] moveBtn; // �÷��̾� �̵���ư
    private Vector3 targetPosition; // �̵� ��ġ
    public GameObject tileCheck; // Ÿ��üũ ����
    public bool tile; // Ÿ�ϸ�����
    public float currentTile; // 0 - �� Ÿ��, 2 - ������ Ÿ��, 3 - �̺�Ʈ Ÿ��, 4 - ���� Ÿ��, 5 - ���� Ÿ��, 6 - �޽� Ÿ��
    public bool game; // ���Ӹ�����
    public float tileMoveSpd; // �̵� �ӵ�
    public float moveDistance; // �̵� �Ÿ�

    public Vector3 finalPlayerPos; // ������ Ÿ�� ��ġ

    // ������ �޽� Ÿ��
    public GameObject bossRest1;
    public GameObject bossRest2;
    public GameObject bossRest3;

    // UI �ؽ�Ʈ
    public TMP_Text healthText;
    public TMP_Text spdText;
    public TMP_Text moneyText;
    
    // Ư�� �� ������
    public bool isShop;
    public bool isEvent;
    public bool isItem;
    public bool isRest;

    // �������� �Ѿ
    public GameObject nextStageUI;
    public bool nextStage;

    // ������ ȹ�� ����
    public bool arrow;
    public bool bag;
    public bool book;
    public bool bow;
    public bool dagger;
    public bool fish;
    public bool hood;
    public bool necklace;
    public bool pick;
    public bool ring;
    public bool shield;
    public bool sword;

    // ����Ʈ
    public GameObject shieldEffect; // ���� ����Ʈ

    private Animator anim;

    private void Awake()
    {
        monsterMap = GameObject.Find("Manager").GetComponent<MonsterMap>();
        clearInfor = GameObject.Find("Manager").GetComponent<ClearInfor>();
        mapSetting = GameObject.Find("Manager").GetComponent<MapSetting>();
        ability = GameObject.Find("Manager").GetComponent<Ability>();
        gameDatas = GameObject.Find("GameData").GetComponent<GameDatas>();
    }

    private void Start()
    {
        anim = GetComponent<Animator>();

        PlayerSetting(); // �÷��̾� ���� ����
        MapSetting(); // �� ���� ����
    }

    void PlayerSetting() // �÷��̾� ���� ����
    {
        playerNum = gameDatas.dataSettings.playerNum;

        // ĳ���Ϳ� ���� ����
        if (playerNum == 1)
        {
            maxHealth = 10;
            moveSpd = 6;
        }
        else if (playerNum == 2)
        {
            maxHealth = 8;
            moveSpd = 8;
        }
        
        rotateSpd = 3f; // ȸ���ӵ�
        currentHealth = maxHealth; // ü�� ����
        moveNum = 1; // �÷��̾� �̵����� 
        tileMoveSpd = 3f; // Ÿ�ϸ� �̵��ӵ�
        moveDistance = 2.3f; // Ÿ�ϸ� �̵��Ÿ�
        currentTile = 0; // ���� Ÿ������
    }

    void MapSetting() // �� ���� ����
    {
        tileCheck.GetComponent<Collider>().enabled = false;
        tile = true;
        game = false;
    }
    
    
    void Update()
    {
        UpdateText(); // �ؽ�Ʈ ������Ʈ
        StateEffects(); // ������ ����Ʈ ����
        UpdateAnimations(); // �̵� ���� �ִϸ��̼�
        PlayerState(); // �÷��̾� ����
        PlayerMoveManager(); // �÷��̾� �̵� ����
        NextStage(); // �������� �̵�            
    }

    // �ؽ�Ʈ ������Ʈ -------------------------------
    void UpdateText() 
    {
        bulletNumText.text = bulletNum.ToString(); // ���� �Ѿ� �� ǥ��

        // UI �ؽ�Ʈ
        healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString(); // ü��
        spdText.text = moveSpd.ToString(); // �̵��ӵ�
        moneyText.text = "$ " + money.ToString(); // ��
    }

    // ������ ����Ʈ ���� ------------------------------------
    void StateEffects() 
    {
        shieldEffect.SetActive(itemShield); // ���¿� ���� ����Ʈ ����
    }

    // �̵� ���� �ִϸ��̼� ----------------------------------
    void UpdateAnimations() // �ʿ� ���� �ִϸ��̼� ����
    {
        if (tile)
        {
            anim.SetBool("Game", false);
            anim.SetBool("GameRun", false);
            anim.SetBool("Tile", true);
        }
        else if (game)
        {
            anim.SetBool("Tile", false);
            anim.SetBool("Game", true);
            anim.SetBool("GameRun", moveVec != Vector3.zero);
        }
    }

    // �÷��̾� ���� ----------------------------------------
    void PlayerState() 
    {
        // ���� ü���� �ִ� ü�º��� ���� �� ������
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        // ü���� 0�����̸� ���
        if (currentHealth <= 0)
        {
            Die();
        }

        // �ɷ� 5-1�� Ȱ��ȭ
        if (ability.ability5Num == 1) // �ɷ� 5-1�� ���� ���Ӵ� 1�� ü���� 25% ���Ϸ� �پ��� 50%ȸ��
        {
            if (currentHealth <= (maxHealth / 4) && ability.healing)
            {
                currentHealth += maxHealth / 2;
                ability.healing = false;
            }
        }
    }
    public void Die() // �����
    {
        clearInfor.result = true; // ���â Ȱ��ȭ
        clearInfor.clearStateText.text = "��Ҽ�����..."; //���â �ؽ�Ʈ

        // �����ִ� ���� ����
        GameObject monster = GameObject.FindWithTag("Monster");
        Destroy(monster);

        // ���� ������ 
        transform.position = Vector3.zero;
        currentHealth += 1; // ���� �Լ� �ߺ����� 
    }

    // �÷��̾� �̵����� ----------------------------------
    void PlayerMoveManager() // �÷��̾� �̵�����
    {
        if (game && !faint) // ���͸� �϶�
        {
            joyStick.SetActive(true);

            GetInput();
            Move();
            Rotate();
        }
        else // Ÿ�ϸ� �϶�
        {
            joyStick.SetActive(false);

            if (currentTile < 5 && !game)
            {
                PlayerMove();
            }
        }

        if (game && transform.position.x > 100) // ���͸� �϶� ���ʿ��� ������Ʈ ��Ȱ��ȭ 
        {
            tileCheck.GetComponent<Collider>().enabled = false;

            foreach (GameObject move in moveBtn)
            {
                move.SetActive(false);
                move.GetComponent<Collider>().enabled = false;
            }
        }
    }
    // ������ �̵�����
    private void GetInput() // ���̽�ƽ �Է� �� �ޱ�
    {
        hAxis = fixedJoyStick.Horizontal;
        vAxis = fixedJoyStick.Vertical;

        moveVec = new Vector3(hAxis, 0, vAxis);
    }

    private void Move() // �̵�
    {
        transform.position += moveVec * moveSpd * Time.deltaTime;
    }

    private void Rotate() // ȸ��
    {
        if (moveVec != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveVec.normalized, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpd);
        }
    }

    // Ÿ�ϸ� �̵�����
    void PlayerMove() // �÷��̾� �̵�
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
        TileCheckAndMoveLimited(); // �̵��� 1�ʵ� Ÿ��üũ �� Ư�� ��ġ���� �̵�����
    }

    void MovePlayer(int direction) // �̵��Ÿ�
    {
        anim.SetBool("TileRun", true); // �̵� �ִϸ��̼� ����
        moveNum = 0;
        switch (direction) // Ŭ���� ��ư�� ���� �̵�
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
        StartCoroutine(SaveFinalPosition()); // ������ ��ġ ���� 
    }
    IEnumerator SaveFinalPosition() // ������ ��ġ ����
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("TileRun", false); // ������ �̵� �ִϸ��̼� ����
        finalPlayerPos = transform.position; // ������ ��ġ ����
    }


    void TileCheckAndMoveLimited() // �̵��� 1�ʵ� Ÿ��üũ �� Ư�� ��ġ���� �̵�����
    {
        if (moveNum > 0 && transform.position.z <= 27) // ������ �޽� Ÿ����
        {
            tileCheck.GetComponent<Collider>().enabled = false;

            foreach (GameObject move in moveBtn) // �̵� ��ư Ȱ��ȭ
            {
                move.SetActive(true);
                move.GetComponent<Collider>().enabled = true;
            }

            // �̵��Ҽ� ���� ������ �̵� �Ұ�
            if (transform.position.x <= -4f)
            {
                moveBtn[0].SetActive(false);
                moveBtn[0].GetComponent<Collider>().enabled = false;

                if (transform.position.z >= 25)
                {
                    moveBtn[1].SetActive(false);
                    moveBtn[1].GetComponent<Collider>().enabled = false;
                }
            }
            else if (transform.position.x >= 4f)
            {
                moveBtn[2].SetActive(false);
                moveBtn[2].GetComponent<Collider>().enabled = false;

                if (transform.position.z >= 25)
                {
                    moveBtn[1].SetActive(false);
                    moveBtn[1].GetComponent<Collider>().enabled = false;
                }
            }
            else if (transform.position.x <= -2f && transform.position.z >= 25)
            {
                moveBtn[0].SetActive(false);
                moveBtn[0].GetComponent<Collider>().enabled = false;
            }
            else if (transform.position.x >= 2f && transform.position.z >= 25)
            {
                moveBtn[2].SetActive(false);
                moveBtn[2].GetComponent<Collider>().enabled = false;
            }
            else
            {
                foreach (GameObject move in moveBtn)
                {
                    move.SetActive(true);
                    move.GetComponent<Collider>().enabled = true;
                }
            }
        }
        else if (transform.position.z >= 27) // ������ �޽� Ÿ��
        {
            tileCheck.GetComponent<Collider>().enabled = false;

            if (gameObject.GetComponent<Collider>().bounds.Intersects(bossRest1.GetComponent<Collider>().bounds))
            {
                moveBtn[0].SetActive(false);
                moveBtn[0].GetComponent<Collider>().enabled = false;
                moveBtn[1].SetActive(false);
                moveBtn[1].GetComponent<Collider>().enabled = false;
                moveBtn[2].SetActive(true);
                moveBtn[2].GetComponent<Collider>().enabled = true;
            }
            else if (gameObject.GetComponent<Collider>().bounds.Intersects(bossRest2.GetComponent<Collider>().bounds))
            {
                moveBtn[0].SetActive(false);
                moveBtn[0].GetComponent<Collider>().enabled = false;
                moveBtn[1].SetActive(true);
                moveBtn[1].GetComponent<Collider>().enabled = true;
                moveBtn[2].SetActive(false);
                moveBtn[2].GetComponent<Collider>().enabled = false;
            }
            else if (gameObject.GetComponent<Collider>().bounds.Intersects(bossRest3.GetComponent<Collider>().bounds))
            {
                moveBtn[0].SetActive(true);
                moveBtn[0].GetComponent<Collider>().enabled = true;
                moveBtn[1].SetActive(false);
                moveBtn[1].GetComponent<Collider>().enabled = false;
                moveBtn[2].SetActive(false);
                moveBtn[2].GetComponent<Collider>().enabled = false;
            }
        }

        if (moveNum <= 0) // �̵��Ϸ� �� �̵���ư ��Ȱ��ȭ �� Ÿ��üũ
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
        tileCheck.GetComponent<Collider>().enabled = true;
    }

    // �������� �̵� ---------------------------------
    void NextStage() 
    {
        if (nextStage)
        {
            nextStage = false;
            StartCoroutine(OnNextStage());
        }
    }
    IEnumerator OnNextStage() // ���������� �Ѿ�� ��� UI���
    {
        yield return new WaitForSeconds(1f);
        nextStageUI.SetActive(true);

        yield return new WaitForSeconds(2f);
        nextStageUI.SetActive(false);
    }


    // Ÿ ��ũ��Ʈ -----------------------------
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

        if (necklace) // ����� ������ ȹ�� �� ���Ӹ� �̵��� ü�� ȸ��
        {
            currentHealth += 3;
        }

        if (ring) // ���� ������ ȹ�� �� ���Ӹ� �̵��� ü�� ȸ��
        {
            currentHealth += 2;
        }

        if (shield) // ���� ������ ȹ���� ���Ӹ� �̵��� �� ���
        {
            itemShield = true;
        }
    }

    public void PostionReset() // ��ġ�� ����
    {
        finalPlayerPos = new Vector3(0, 1, 0);
        targetPosition = new Vector3(0, 1, 0);
        transform.position = finalPlayerPos;
    }

    public void MoveFinalPosition() // Ÿ ��ũ��Ʈ �ڵ��
    {
        transform.position = finalPlayerPos;
    }    

    // �浹���� ------------------------------------------------------
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) // ���� �Ѿ˰� �浹��
        {
            if (itemShield) // ���� ������ 1ȸ �ǰ� ����
            {
                itemShield = false;
                Destroy(collision.gameObject);
            }
            else if (!itemShield) // �ǰ�
            {
                Bullet bullet = collision.gameObject.GetComponent<Bullet>();

                // �ɷ� 5-2�� Ȱ��ȭ
                if (ability.ability5Num == 2)
                {
                    int num = Random.Range(0, 10);
                    if (num < 4)
                    {
                        Destroy(collision.gameObject); // �ɷ� 5-2�� ���� �ǰݽ� Ȯ���� ����
                        return;
                    }
                }

                currentHealth -= bullet.damage;
                Destroy(collision.gameObject);
            }

            // �ɷ� 6-1�� Ȱ��ȭ
            if (ability.ability6Num == 1)
            {
                ability.HitCannonReload(); // �ɷ� 6-1�� ���� �ǰݽ� ��� ���� �Ѿ� 1����
            }
            // �ɷ� 6-2�� Ȱ��ȭ
            else if (ability.ability6Num == 2)
            {
                ability.HitExtraAttack(); // �ɷ� 6-2�� ���� �ǰݽ� 2�� ����ü �߻�
            }
        }

        // Ư�� �Ѿ� �ǰݽ� (ü���� ��������)
        if (collision.gameObject.CompareTag("FakeBullet") || collision.gameObject.CompareTag("BreakBullet"))
        {
            Destroy(collision.gameObject);
        }

        // ���� �Ѿ� �ǰݽ� 1�� �̵��Ұ�
        if (collision.gameObject.CompareTag("FaintBullet"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(PlayerFaint(1f));
        }

        // �÷��̾� �Ѿ� ȹ���
        if (collision.gameObject.CompareTag("P_Attack"))
        {
            bulletNum++; // �Ѿ� ����
            Destroy(collision.gameObject);

            if (hood) // �ĵ� ������ ���� �� �Ѿ� ȹ��� Ȯ���� ����ü ����
            {
                ability.MPExtraAttack();
            }

            // �ɷ� 1-1�� Ȱ��ȭ
            if (ability.ability1Num == 1)
            {
                ability.GetPlayerMP(); // �ɷ� 1-1�� ���� �Ѿ� ȹ��� Ȯ���� �Ѿ� ȹ��
            }
            // �ɷ� 1-2�� Ȱ��ȭ
            else if (ability.ability1Num == 2)
            {
                ability.GetCannonReload(); // �ɷ� 1-2�� ���� �Ѿ� ȹ��� Ȯ�������� ��� ���� �Ѿ� 1 ����
            }

            // �ɷ� 2-1�� Ȱ��ȭ
            if (ability.ability2Num == 1)
            {
                ability.MPExtraAttack(); // �ɷ� 2-1�� ���� �Ѿ� ȹ��� Ȯ�������� ����ü ����
            }
        }

        if (collision.gameObject.CompareTag("Wall")) // ���͸� �� �浹�� ó����ġ�� �̵�
        {
            // ���������� ���� �̵���ġ ����
            if(mapSetting.stage == 1)
            {
                transform.position = monsterMap.playerMapSpawnPos[0].transform.position;
            }
            else if(mapSetting.stage == 2)
            {
                transform.position = monsterMap.playerMapSpawnPos[1].transform.position;
            }
        }
    }

    IEnumerator PlayerFaint(float time) // ���� �� �����ð��� ��������
    {
        faint = true;

        yield return new WaitForSeconds(time);
        faint = false;
    }
}
 