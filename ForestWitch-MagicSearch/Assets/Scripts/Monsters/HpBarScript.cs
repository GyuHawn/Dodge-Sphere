using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HpBarScript : MonoBehaviour
{
    public Image healthBarFill;
    public TMP_Text healthPercent;

    private void Update()
    {
        // ü�¹ٰ��� ���� �ۼ�Ʈ�� ������Ʈ
        healthPercent.text = ((int)(healthBarFill.fillAmount * 100)).ToString() + "%";
    }

    // ü�� ���� ���� ü�¹� ������Ʈ
    public void UpdateHP(int currentHp, int maxHp)
    {
        if (currentHp > 0)
        {
            UpdateHealthBar(currentHp, maxHp);
        }
    }

    // ���� ü�¹� ������Ʈ
    void UpdateHealthBar(int currentHp, int maxHp)
    {
        float fillAmount = (float)currentHp / maxHp;
        healthBarFill.fillAmount = fillAmount;
    }

    // ü�¹� �缳��
    public void ResetHealthBar(int current, int max)
    {
        // ����ü���� �ִ�ü�� �̻��϶� 1.0����
        if (current >= max)
        {
            healthBarFill.fillAmount = 1.0f;
        }
        else
        {
            // ����ü���� �ִ�ü�¿� ���� ������ ����
            healthBarFill.fillAmount = (float)current / max;
        }
    }

    // ü�¹ٸ� Ư����ġ�� �̵�
    public void MoveToYStart(float targetY, float time)
    {
        StartCoroutine(MoveToY(targetY, time));
    }

    // ü�¹� �̵�
    IEnumerator MoveToY(float targetY, float time)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        float elapsedTime = 0;
        float startY = rectTransform.anchoredPosition.y;
        while (elapsedTime < time)
        {
            float newY = Mathf.Lerp(startY, targetY, elapsedTime / time);
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, newY);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, targetY);
    }
}
