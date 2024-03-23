using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public TMP_Text currnetTimerText; // ����ð� �ؽ�Ʈ

    private float currentTime = 0f; // ���� �ð�

    void Start()
    {
        currentTime = 0f; // ����ð� �ʱ�ȭ
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        // ��, �� ��ȯ
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        currnetTimerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
