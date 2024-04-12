using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeManager : MonoBehaviour
{
    private ClearInfor clearInfor;

    public TMP_Text currnetTimerText; // ����ð� �ؽ�Ʈ

    public float currentTime = 0f; // ���� �ð�

    private void Awake()
    {
        clearInfor = GameObject.Find("Manager").GetComponent<ClearInfor>();
    }

    void Start()
    {
        currentTime = 0f; // ����ð� �ʱ�ȭ
    }

    void Update()
    {
        if (!clearInfor.result) // ���â ǥ�� ���� �ƴҶ�
        {
            currentTime += Time.deltaTime;

            // ��, �� ��ȯ
            int minutes = Mathf.FloorToInt(currentTime / 60f);
            int seconds = Mathf.FloorToInt(currentTime % 60f);

            currnetTimerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
        }
    }
}
