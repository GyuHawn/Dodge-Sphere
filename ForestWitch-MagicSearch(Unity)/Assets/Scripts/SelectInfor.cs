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
        // Initialize buttons for cannon explanations
        for (int i = 0; i < cannonExBtn.Length; i++)
        {
            int index = i; // local copy of i to avoid closure issues in the lambda expression
            cannonExBtn[i].onClick.AddListener(() => ToggleGameObject(cannonEx[index]));
        }

        // Initialize buttons for player explanations
        for (int i = 0; i < PlayerExBtn.Length; i++)
        {
            int index = i; // local copy of i to avoid closure issues in the lambda expression
            PlayerExBtn[i].onClick.AddListener(() => ToggleGameObject(PlayerEx[index]));
        }
    }

    void ToggleGameObject(GameObject obj)
    {
        // Toggle the active state of the GameObject
        obj.SetActive(!obj.activeSelf);
    }
}
