using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private GameSetting gameSetting;
    private PortionSlot portions;

    public GameObject[] ItemPos; // ȹ�� ������ ǥ�� ��ġ
    public List<GameObject> ltemList = new List<GameObject>(); // ȹ�� ������

    public GameObject arrowPrefab; // ������ ������
    public bool arrow; // ������ ȹ�� ����
    private bool onArrow; // ��� �ߺ����� ����

    public GameObject bagPrefab;
    public bool bag;
    private bool onBag;

    public GameObject bonePrefab;
    public bool bone;
    private bool onBone;

    public GameObject bookPrefab;
    public bool book;
    private bool onBook;

    public GameObject bowPrefab;
    public bool bow;
    private bool onBow;

    public GameObject crownPrefab;
    public bool crown;
    private bool onCrown;

    public GameObject daggerPrefab;
    public bool dagger;
    private bool onDagger;

    public GameObject fishPrefab;
    public bool fish;
    private bool onFish;

    public GameObject goldPrefab;
    public bool gold;
    private bool onGold;

    public GameObject growPrefab;
    public bool grow;
    private bool onGrow;

    public GameObject hoodPrefab;
    public bool hood;
    private bool onHood;

    public GameObject jewelPrefab;
    public bool jewel;
    private bool onJewel;

    public GameObject coinPrefab;
    public bool coin;
    private bool onCoin;

    public GameObject mushroomPrefab;
    public bool mushroom;
    private bool onMushroom;

    public GameObject necklacePrefab;
    public bool necklace;
    private bool onNecklace;

    public GameObject pickPrefab;
    public bool pick;
    private bool onPick;

    public GameObject ringPrefab;
    public bool ring;
    private bool onRing;

    public GameObject shieldPrefab;
    public bool shield;
    private bool onShield;

    public GameObject skullPrefab;
    public bool skull;
    private bool onSkull;

    public GameObject swordPrefab;
    public bool sword;
    private bool onSword;

    private void Awake()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        gameSetting = GameObject.Find("Manager").GetComponent<GameSetting>();
        portions = GameObject.Find("Manager").GetComponent<PortionSlot>();
    }

    void Update()
    {
        if (arrow && !onArrow)
        {
            onArrow = true;
            Arrow();
        }
        if (bag && !onBag)
        {
            onBag = true;
            Bag();
        }
        if (bone && !onBone)
        {
            onBone = true;
            Bone();
        }
        if (book && !onBook)
        {
            onBook = true;
            Book();
        }
        if (bow && !onBow)
        {
            onBow = true;
            Bow();
        }
        if (crown && !onCrown)
        {
            onCrown = true;
            Crown();
        }
        if (dagger && !onDagger)
        {
            onDagger = true;
            Dagger();
        }
        if (fish && !onFish)
        {
            onFish = true;
            Fish();
        }
        if (gold && !onGold)
        {
            onGold = true;
            Gold();
        }
        if (grow && !onGrow)
        {
            onGrow = true;
            Grow();
        }
        if (hood && !onHood)
        {
            onHood = true;
            Hood();
        }
        if (jewel && !onJewel)
        {
            onJewel = true;
            Jewel();
        }
        if (coin && !onCoin)
        {
            onCoin = true;
            Coin();
        }
        if (mushroom && !onMushroom)
        {
            onMushroom = true;
            Mushroom();
        }
        if (necklace && !onNecklace)
        {
            onNecklace = true;
            Necklace();
        }
        if (pick && !onPick)
        {
            onPick = true;
            Pick();
        }
        if (ring && !onRing)
        {
            onRing = true;
            Ring();
        }
        if (shield && !onShield)
        {
            onShield = true;
            Shield();
        }
        if (skull && !onSkull)
        {
            onSkull = true;
            Skull();
        }
        if (sword && !onSword)
        {
            onSword = true;
            Sword();
        }
    }

    void Arrow() // ���� ��ź�� �ӵ��� ���� �մϴ�.
    {
        // ȹ�� ������ ǥ��
        int index = ltemList.Count;
        GameObject arrowObject = Instantiate(arrowPrefab, Vector3.zero, Quaternion.identity);
        arrowObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(arrowObject);

        // ������ ���
        playerMovement.arrow = true;
    }
    
    void Bag() // ������ ��� â�� 1�� �����մϴ�.
    {
        // ȹ�� ������ ǥ��
        int index = ltemList.Count;
        GameObject bagObject = Instantiate(bagPrefab, Vector3.zero, Quaternion.identity);
        bagObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(bagObject);

        // ������ ���
        playerMovement.bag = true;
    }

    void Bone() // �ִ� ü���� 3���� �մϴ�.
    {
        // ȹ�� ������ ǥ��
        int index = ltemList.Count;
        GameObject boneObject = Instantiate(bonePrefab, Vector3.zero, Quaternion.identity);
        boneObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(boneObject);

        // ������ ���
        playerMovement.maxHealth += 3; // �ִ� ü�� 3����
        playerMovement.currentHealth += 3; // ������ ��ŭ ȸ��
    }

    void Book() // ���� �������� 1ȸ �ʱ�ȭ�� �� �ֽ��ϴ�. (���� ���߰�)
    {
        // ȹ�� ������ ǥ��
        int index = ltemList.Count;
        GameObject bookObject = Instantiate(bookPrefab, Vector3.zero, Quaternion.identity);
        bookObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(bookObject);

        // ������ ���
    }

    void Bow() // ������ �ִ� �Ѿ� ���� 1���� �մϴ�.
    {
        // ȹ�� ������ ǥ��
        int index = ltemList.Count;
        GameObject bowObject = Instantiate(bowPrefab, Vector3.zero, Quaternion.identity);
        bowObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(bowObject);

        // ������ ���
        playerMovement.bow = true;
    }

    void Crown() // �ִ� ü���� 3�����մϴ�, �̵��ӵ��� 1�����մϴ�, ü���� 5ȸ���˴ϴ�, 300������ ȹ���մϴ�.
    {
        // ȹ�� ������ ǥ��
        int index = ltemList.Count;
        GameObject crownObject = Instantiate(crownPrefab, Vector3.zero, Quaternion.identity);
        crownObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(crownObject);

        // ������ ���
        playerMovement.maxHealth += 3; // �ִ� ü�� 5����
        playerMovement.currentHealth += 8; // ������ ��ġ + 5 ȸ��
        playerMovement.moveSpd++; // �̵��ӵ� 1����
        playerMovement.money += 300; // 300���� �߰�
    }

    void Dagger() // ���� ���ݷ��� 1���� �մϴ�.
    {
        // ȹ�� ������ ǥ��
        int index = ltemList.Count;
        GameObject daggerObject = Instantiate(daggerPrefab, Vector3.zero, Quaternion.identity);
        daggerObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(daggerObject);

        // ������ ���
        playerMovement.dagger = true;
    }

    void Fish() // �������� 1ȸ ����� �������� ������ �� �ֽ��ϴ�. (���� ���߰�)
    {
        // ȹ�� ������ ǥ��
        int index = ltemList.Count;
        GameObject fishObject = Instantiate(fishPrefab, Vector3.zero, Quaternion.identity);
        fishObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(fishObject);

        // ������ ���
    }

    void Gold() // 1000������ ȹ���մϴ�.
    {
        // ȹ�� ������ ǥ��
        int index = ltemList.Count;
        GameObject goldObject = Instantiate(goldPrefab, Vector3.zero, Quaternion.identity);
        goldObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(goldObject);

        // ������ ���
        playerMovement.money += 1000; // 1000���� �߰�
    }

    void Grow() // �̵��ӵ��� 1�����մϴ�.
    {
        // ȹ�� ������ ǥ��
        int index = ltemList.Count;
        GameObject growObject = Instantiate(growPrefab, Vector3.zero, Quaternion.identity);
        growObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(growObject);

        // ������ ���
        playerMovement.moveSpd++; // �̵��ӵ� 1����
    }

    void Hood() // ������ 1�����մϴ�.
    {
        // ȹ�� ������ ǥ��
        int index = ltemList.Count;
        GameObject hoodObject = Instantiate(hoodPrefab, Vector3.zero, Quaternion.identity);
        hoodObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(hoodObject);

        // ������ ���
        playerMovement.defence++; // ���� 1����
    }

    public void Jewel() // 1500������ ȹ���մϴ�.
    {
        // ȹ�� ������ ǥ��
        int index = ltemList.Count;
        GameObject jeweldObject = Instantiate(jewelPrefab, Vector3.zero, Quaternion.identity);
        jeweldObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(jeweldObject);

        // ������ ���
        playerMovement.money += 1500; // 1500���� �߰�
    }

    public void Coin() // 500������ ȹ���մϴ�.
    {
        // ȹ�� ������ ǥ��
        int index = ltemList.Count;
        GameObject coinObject = Instantiate(coinPrefab, Vector3.zero, Quaternion.identity);
        coinObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(coinObject);

        // ������ ���
        playerMovement.money += 500; // 500���� �߰�
    }

    public void Mushroom() // �̵��ӵ��� 2�����մϴ�.
    {
        // ȹ�� ������ ǥ��
        int index = ltemList.Count;
        GameObject mushroomObject = Instantiate(mushroomPrefab, Vector3.zero, Quaternion.identity);
        mushroomObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(mushroomObject);

        // ������ ���
        playerMovement.moveSpd += 2; // �̵��ӵ� 2����
    }

    public void Necklace() // ���� ���� �� ü���� 3ȸ�� �մϴ�.
    {
        // ȹ�� ������ ǥ��
        int index = ltemList.Count;
        GameObject necklaceObject = Instantiate(necklacePrefab, Vector3.zero, Quaternion.identity);
        necklaceObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(necklaceObject);

        // ������ ���
        playerMovement.necklace = true;
    }

    public void Pick() // Ȯ�������� 2���� ������� �����ϴ�.
    {
        // ȹ�� ������ ǥ��
        int index = ltemList.Count;
        GameObject pickObject = Instantiate(pickPrefab, Vector3.zero, Quaternion.identity);
        pickObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(pickObject);

        // ������ ���
        playerMovement.pick = true;
    }

    public void Ring() // ���� ���� �� ü���� 2ȸ�� �մϴ�.
    {
        // ȹ�� ������ ǥ��
        int index = ltemList.Count;
        GameObject ringObject = Instantiate(ringPrefab, Vector3.zero, Quaternion.identity);
        ringObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(ringObject);

        // ������ ���
        playerMovement.ring = true;
    }

    public void Shield() // ������ 1�� ������ ����մϴ�.
    {
        // ȹ�� ������ ǥ��
        int index = ltemList.Count;
        GameObject shieldObject = Instantiate(shieldPrefab, Vector3.zero, Quaternion.identity);
        shieldObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(shieldObject);

        // ������ ���
        playerMovement.shield = true;
    }

    public void Skull() // �ִ� ü���� 5���� �մϴ�.
    {
        // ȹ�� ������ ǥ��
        int index = ltemList.Count;
        GameObject skullObject = Instantiate(skullPrefab, Vector3.zero, Quaternion.identity);
        skullObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(skullObject);

        // ������ ���
        playerMovement.maxHealth += 5; // �ִ� ü�� 5����
        playerMovement.currentHealth += 5; // ������ ��ŭ ȸ��
    }

    public void Sword() // ���� ���ݷ��� 2���� �մϴ�.
    {
        // ȹ�� ������ ǥ��
        int index = ltemList.Count;
        GameObject swordObject = Instantiate(swordPrefab, Vector3.zero, Quaternion.identity);
        swordObject.transform.SetParent(ItemPos[index].transform, false);
        ltemList.Add(swordObject);

        // ������ ���
        playerMovement.sword = true;
    }
}
