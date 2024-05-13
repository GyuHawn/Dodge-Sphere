using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectInfor : MonoBehaviour
{
    public Button[] cannonExBtn; // ���� ���� ��ư
    public GameObject[] cannonEx; // ���� ����
    public Button[] PlayerExBtn; // �÷��̾� ���� ��ư
    public GameObject[] PlayerEx; // �÷��̾� ����

    void Start()
    {
        for (int i = 0; i < cannonExBtn.Length; i++)
        {
            int index = i;
            cannonExBtn[i].onClick.AddListener(() => ToggleGameObject(cannonEx[index]));
        }

        for (int i = 0; i < PlayerExBtn.Length; i++)
        {
            int index = i;
            PlayerExBtn[i].onClick.AddListener(() => ToggleGameObject(PlayerEx[index]));
        }
    }

    void ToggleGameObject(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
    }
}
