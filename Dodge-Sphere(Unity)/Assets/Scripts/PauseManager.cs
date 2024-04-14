using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public bool isPaused = false; // ���� �Ͻ�����

    public GameObject pause;
    public GameObject play;


    void Update()
    {
        if (isPaused)
        {
            pause.SetActive(false);
            play.SetActive(true);
        }
        else
        {
            pause.SetActive(true);
            play.SetActive(false);
        }
    }

    public void GamePause()
    {
        isPaused = !isPaused; // �Ͻ����� ���� ����

        if (isPaused)
        {
            Time.timeScale = 0; // �Ͻ�����
        }
        else
        {
            Time.timeScale = 1; // �Ͻ����� ����
        }
    }
}
