using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LodjingText : MonoBehaviour
{
    public List<string> lodingtexts = new List<string>();
    public TMP_Text text;

    void Start()
    {
        lodingtexts.Add("���ϰ� �غ��ϴ� ��...");
        lodingtexts.Add("�� �Դ� ��...");
        lodingtexts.Add("���� �Ҿ����ϴ�...");
        lodingtexts.Add("������ ���� ��...");
        lodingtexts.Add("���ڷ縦 �Ҿ���Ƚ��ϴ�...");
        lodingtexts.Add("å ���� ��...");
        lodingtexts.Add("���� �� �����Դϴ�...");
        lodingtexts.Add("��� ����ϴ�...");

        TextUpdate();
    }

    void TextUpdate()
    {
        int num = Random.Range(0, lodingtexts.Count);

        text.text = lodingtexts[num];
    }

}
