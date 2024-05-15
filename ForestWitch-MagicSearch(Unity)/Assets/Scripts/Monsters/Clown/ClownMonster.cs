using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class ClownMonster : MonoBehaviour
{
    private GameDatas gameDatas;
    private PlayerMovement playerMovement;
    private MonsterMap monsterMap;
    private P_AttackSpawn p_AttackSpawn;
    private CameraMovement cameraMovement;
    private MonsterGetMoney monsterGetMoney;
    private MapSetting mapSetting;
    private HpBarScript hpBarScript;
    private ClearInfor clearInfor;
    private Ability ability;
    private AudioManager audioManager;

    // 기본 스탯
    public int maxHealth;
    public int currentHealth;
    public int money;

    public GameObject baseAttackPrefab; // 총알 프리팹

    // 밀기 패턴
    public GameObject[] pushPos1;
    public GameObject[] pushPos2;
    public float p_AttackSpd; // 총알 속도
    public int p_AttackNum; // 공격 수

    // 발사 패턴
    public float s_AttackSpd; // 총알 속도
    public int s_AttackNum; // 발사 수

    // 댄스 패턴
    public GameObject[] dancePos;
    public float d_AttackSpd; // 총알 속도
    public int d_AttackNum; // 공격 수
    public int d_BulletNum; // 공격 수
    public float d_AttackAngles; // 발사 각도

    // 파괴 패턴
    public GameObject breakAttackPrefab; // 기절 특수 총알
    public float b_AttackSpd; // 총알 속도
    public int b_AttackNum; // 발사 수

    // 소환 패턴
    public List<GameObject> summonMonsters = new List<GameObject>();
    public GameObject defenceEffect;
    public int summonNum;
    public bool isSummon; // 먹기 패턴 중
    public GameObject[] summonPos; // 패턴 생성 위치
    public GameObject[] summonPrefabs;

    public List<GameObject> playerCannons = new List<GameObject>();

    public GameObject hitEffectPos; // 이펙트 위치
    public GameObject hitEffect; // 피격 이펙트

    public bool die = false;

    private Animator anim;

    private void Awake()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        monsterMap = GameObject.Find("Manager").GetComponent<MonsterMap>();
        p_AttackSpawn = GameObject.Find("Manager").GetComponent<P_AttackSpawn>();
        cameraMovement = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
        monsterGetMoney = GameObject.Find("Manager").GetComponent<MonsterGetMoney>();
        mapSetting = GameObject.Find("Manager").GetComponent<MapSetting>();
        hpBarScript = GameObject.Find("MosterHP").GetComponent<HpBarScript>();
        clearInfor = GameObject.Find("Manager").GetComponent<ClearInfor>();
        ability = GameObject.Find("Manager").GetComponent<Ability>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        gameDatas = GameObject.Find("GameData").GetComponent<GameDatas>();
    }

    void Start()
    {
        anim = GetComponent<Animator>();

        maxHealth = 20 + (mapSetting.adventLevel * 3);
        currentHealth = maxHealth;
        money = 300;

        p_AttackSpd = 8f;
        p_AttackNum = 8;

        s_AttackSpd = 20f;
        s_AttackNum = 5;

        d_AttackSpd = 10f;
        d_AttackNum = 12;
        d_BulletNum = 10;

        FindCannons();

        InvokeRepeating("StartPattern", 3f, 7f); // 랜덤 패턴 실행
        InvokeRepeating("StartBreakAttack", 6f, 15f); // 특수 패턴 1실행
        InvokeRepeating("SummonMonster", 10f, 30f); // 특수 패턴 2실행

        hpBarScript.MoveToYStart(10, 0.5f);
    }

    public void FindCannons() // 모든 대포 찾기
    {
        GameObject[] Cannons = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject cannon in Cannons)
        {
            if (cannon.name == "PlayerCannon")
            {
                playerCannons.Add(cannon);
            }
        }
    }

    void Update()
    {
        if (currentHealth <= 0 && !die)
        {
            die = true;
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        anim.SetTrigger("Die");
        yield return new WaitForSeconds(1.5f);

        hpBarScript.MoveToYStart(150, 0.1f);
        hpBarScript.ResetHealthBar(currentHealth, maxHealth);
        hpBarScript.healthBarFill.fillAmount = 1.0f;
        
        GetMoney();

        clearInfor.clear = true; // 2스테이지 클리어시 게임 클리어
        clearInfor.clearStateText.text = "정복 완료!!";
        mapSetting.adventLevel++;

        gameDatas.SaveFieldData("maxLevel", mapSetting.adventLevel);

        /* playerMovement.OnTile();
         playerMovement.moveNum = 1;
         playerMovement.currentTile = 0;
         playerMovement.bulletNum = 0;*/
        // playerMovement.PostionReset(); // 플레이어 위치 초기화 (현재 2스테이지가 마지막)

        monsterMap.clownMoved = false;
        p_AttackSpawn.spawned = false;

        cameraMovement.fix = true;

        monsterMap.DeleteObject();

        clearInfor.killedBoss++;

        //mapSetting.stage++; // 스테이지 보스 클리어시 스테이지로 1증가
       // mapSetting.MapReset(); // 보스 클리어시 맵 초기화
        //mapSetting.StageMapSetting(); // 맵 셋팅

        clearInfor.result = true;

        Destroy(gameObject);
    }

    void GetMoney()
    {
        // 능력 3-1이 활성화
        if (ability.ability3Num == 1)
        {
            money = ability.GamblingCoin(money); // 능력 3-1에 따라 코인 획득을 조절
        }
        // 능력 3-2가 활성화
        else if (ability.ability3Num == 2)
        {
            money = ability.PlusCoin(money); // 능력 3-2에 따라 코인 획득을 조절
        }

        monsterGetMoney.getMoney = money;
        monsterGetMoney.PickUpMoney();
    }
    void StartPattern() // 랜덤 패턴 선택
    {
        int randomPattern = Random.Range(0, 3);
        switch (randomPattern)
        {
            case 0:
                StartPushAttack();
                break;
            case 1:
                StartShotAttack();
                break;
            case 2:
                StartDanceAttack();
                break;
        }
    }

    private void StartPushAttack()
    {
        StartCoroutine(PushAttacks());
    }

    IEnumerator PushAttacks()
    {
        int pushNum = 0;
        Vector3 direction = Quaternion.Euler(0, 180, 0) * Vector3.forward; // 각도에 따른 방향 계산

        for (int i = 0; i < p_AttackNum; i++)
        {
            audioManager.Cl_PushAudio();
            anim.SetTrigger("Push");
            if (pushNum == 0)
            {
                pushNum++;
                for (int j = 0; j < pushPos1.Length; j++)
                {
                    Vector3 bulletPos = new Vector3(pushPos1[j].transform.position.x, 2f, pushPos1[j].transform.position.z); // 총알 위치 설정
                    GameObject bullet = Instantiate(baseAttackPrefab, bulletPos, Quaternion.identity); // 총알 생성
                    bullet.name = "PushAttack"; // 총알 이름 변경         
                    bullet.GetComponent<Rigidbody>().velocity = direction * p_AttackSpd; // 탄환 방향 설정
                    Destroy(bullet, 3f);
                }
            }
            else if (pushNum == 1)
            {
                pushNum--;
                for (int j = 0; j < pushPos2.Length; j++)
                {
                    Vector3 bulletPos = new Vector3(pushPos2[j].transform.position.x, 2f, pushPos2[j].transform.position.z); // 총알 위치 설정
                    GameObject bullet = Instantiate(baseAttackPrefab, bulletPos, Quaternion.identity); // 총알 생성
                    bullet.name = "PushAttack"; // 총알 이름 변경         
                    bullet.GetComponent<Rigidbody>().velocity = direction * p_AttackSpd; // 탄환 방향 설정
                    Destroy(bullet, 3f);
                }
            }

            yield return new WaitForSeconds(0.7f);
        }
    }

    private void StartShotAttack()
    {
        StartCoroutine(ShotAttacks());
    }

    IEnumerator ShotAttacks()
    {
        for (int i = 0; i < s_AttackNum; i++)
        {
            audioManager.Cl_ShotAudio();
            anim.SetTrigger("Shot");
            yield return new WaitForSeconds(0.5f);

            GameObject player = GameObject.Find("Player");
            Vector3 playerPosition = player.transform.position;

            // 플레이어로와 5만큼 거리의 랜덤 위치에 총알 생성
            Vector3 randomDirection = Random.insideUnitSphere.normalized * 5;
            randomDirection.y = 0; // 높이 그대로
            Vector3 bulletPos = playerPosition + randomDirection;
            GameObject bullet = Instantiate(baseAttackPrefab, bulletPos, Quaternion.identity);
            bullet.name = "ShotAttack";

            // 총알을 1초 후 플레이어 방향으로 발사
            yield return new WaitForSeconds(0.8f);
            if (bullet != null)
            {
                Vector3 direction = (playerPosition - bullet.transform.position).normalized;
                bullet.GetComponent<Rigidbody>().velocity = direction * s_AttackSpd;

                Destroy(bullet, 5f); // 5초 후 총알 삭제
            }
        }
    }

    private void StartBreakAttack() // 파괴 패턴
    {
        if (playerCannons.Count > 1)
        {
            Vector3 bulletPos = new Vector3(transform.position.x, 3f, transform.position.z);
            GameObject bullet = Instantiate(breakAttackPrefab, bulletPos, Quaternion.identity); // 총알 생성
            bullet.name = "BreakAttack"; // 총알 이름 변경
        }
        else if (playerCannons.Count <= 1)
        {
            CancelInvoke("StartBreakAttack");
        }
    }

    public void StartDanceAttack()
    {
        StartCoroutine(DanceAttack());
    }

    IEnumerator DanceAttack()
    {
        anim.SetBool("Dance", true);
        int num = Random.Range(0, 2);

        for (int i = 0; i < d_AttackNum; i++)
        {
            if (num == 0)
            {
                for (int j = 0; j < d_BulletNum; j++)
                {
                    audioManager.Cl_DanceAudio();
                    float angle = 130 + j * (90f / d_BulletNum); // 수정된 부분
                    Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward;
                    Vector3 bulletPos = new Vector3(dancePos[0].transform.position.x, 2f, dancePos[0].transform.position.z);
                    GameObject bullet = Instantiate(baseAttackPrefab, bulletPos, Quaternion.identity);
                    bullet.name = "DanceAttack";
                    bullet.GetComponent<Rigidbody>().velocity = direction * d_AttackSpd;

                    Destroy(bullet, 2.5f);
                }
                num = 1;
            }
            else if (num == 1)
            {
                for (int j = 0; j < d_BulletNum; j++)
                {
                    audioManager.Cl_DanceAudio();
                    float angle = 135 + j * (90f / d_BulletNum);
                    Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward;
                    Vector3 bulletPos = new Vector3(dancePos[1].transform.position.x, 2f, dancePos[1].transform.position.z);
                    GameObject bullet = Instantiate(baseAttackPrefab, bulletPos, Quaternion.identity);
                    bullet.name = "DanceAttack";
                    bullet.GetComponent<Rigidbody>().velocity = direction * d_AttackSpd;

                    Destroy(bullet, 2.5f);
                }
                num = 0;
            }
            yield return new WaitForSeconds(0.3f);
        }
        anim.SetBool("Dance", false);
    }


    void SummonMonster()
    {
        if (!isSummon)
        {
            defenceEffect.SetActive(true);
            isSummon = true;

            summonNum = Random.Range(0, 2);

            // 첫 번째 몬스터 소환
            GameObject firstMonster = Instantiate(summonPrefabs[summonNum], summonPos[0].transform.position, Quaternion.Euler(0, 90, 0));
            summonMonsters.Add(firstMonster);
            firstMonster.name = "SummonMonster";

            // 0이면 1, 1이면 0을 사용
            GameObject secondMonster = Instantiate(summonPrefabs[1 - summonNum], summonPos[1].transform.position, Quaternion.Euler(0, -90, 0));
            summonMonsters.Add(secondMonster);
            secondMonster.name = "SummonMonster";
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CannonBullet"))
        {
            if (!isSummon)
            {
                Bullet bulletComponent = collision.gameObject.GetComponent<Bullet>();
                if (bulletComponent != null)
                {
                    audioManager.HitMonsterAudio();
                    StartCoroutine(HitEffect());
                    currentHealth -= bulletComponent.damage;
                    hpBarScript.UpdateHP(currentHealth, maxHealth);
                    anim.SetTrigger("Hit");
                }
                Destroy(collision.gameObject);
            }
            else
            {
                Destroy(collision.gameObject);
            }

            if (collision.gameObject.CompareTag("ExtraAttack"))
            {
                if (!isSummon)
                {
                    ExtraAttack attack = collision.gameObject.GetComponent<ExtraAttack>();
                    if (attack != null)
                    {
                        audioManager.HitMonsterAudio();
                        StartCoroutine(HitEffect());
                        currentHealth -= attack.damage;
                        hpBarScript.UpdateHP(currentHealth, maxHealth);
                        anim.SetTrigger("Hit");
                    }
                    Destroy(collision.gameObject);
                }
                else
                {
                    Destroy(collision.gameObject);
                }
            }

            IEnumerator HitEffect()
            {
                GameObject effect = Instantiate(hitEffect, hitEffectPos.transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.5f);
                Destroy(effect);
            }
        }
    }
}
