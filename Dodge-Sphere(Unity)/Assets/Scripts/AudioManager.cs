using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    // BGM
    public AudioSource b_MainMenu; // ���θ޴�
    public AudioSource b_Story; // ���丮
    public AudioSource b_Loading; // �ε�
    public AudioSource[] b_Monsters; // ����
    public AudioSource b_Boss; // ����
    public AudioSource b_Shop; // ����
    public AudioSource b_Event; // �̺�Ʈ
    public AudioSource b_Rest; // �޽�
    public AudioSource b_TileMap; // Ÿ�ϸ�

    // Function
    public AudioSource f_Button; // ��ư (o) 
    public AudioSource f_ButtonFail; // ��ư (x)
    public AudioSource f_Spwan; // ���� ��ȯ
    public AudioSource f_Potion; // ����
    public AudioSource f_Win; // �¸�
    public AudioSource f_GetItem; // ������ȹ��
    public AudioSource f_Clear; // Ŭ����
    public AudioSource f_Die; // �÷��̾� ���
    public AudioSource f_Heal; // ȸ��
  


    // Slider bgmSlider;
    //public Slider generalSlider;

    private void Awake()
    {

    }

    void Start()
    {
        StopAudio();
        /*
        // ��ü ���� ����
        float bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 1.0f);
        float genVolume = PlayerPrefs.GetFloat("GenVolume", 1.0f);

        bgmSlider.value = bgmVolume;
        generalSlider.value = genVolume;

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            bgmMainMenu.volume = bgmVolume;

            startAudio.volume = genVolume;
            buttonAudio.volume = genVolume;
            bgmMainMenu.Play();
        }
        else if (SceneManager.GetActiveScene().name == "Loading")
        {
            bgmMainMenu.volume = bgmVolume;
        }
        else if (SceneManager.GetActiveScene().name == "Game")
        {
            bgmStage.volume = bgmVolume;
            bgmBossStage.volume = bgmVolume;
            bgmSelectMenu.volume = bgmVolume;
            bgmResultMenu.volume = bgmVolume;
        }*/
    }

    /*void Update()
    {
        // ��ü ���� ����
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            bgmMainMenu.volume = bgmSlider.value;

            startAudio.volume = generalSlider.value;
            buttonAudio.volume = generalSlider.value;
        }
        else if (SceneManager.GetActiveScene().name == "Loding")
        {
            bgmMainMenu.volume = bgmSlider.value;
        }
        else if (SceneManager.GetActiveScene().name == "Character")
        {
            bgmCharacterMenu.volume = bgmSlider.value;

            startAudio.volume = generalSlider.value;
            buttonAudio.volume = generalSlider.value;
        }
        else if (SceneManager.GetActiveScene().name == "Game")
        {
            bgmStage.volume = bgmSlider.value;
            bgmBossStage.volume = bgmSlider.value;
            bgmSelectMenu.volume = bgmSlider.value;
            bgmResultMenu.volume = bgmSlider.value;

            attackAudio.volume = generalSlider.value;
            defenseAudio.volume = generalSlider.value;
            hitAudio.volume = generalSlider.value;
            // monsterAttackAudio.volume = generalSlider.value;
            buttonAudio.volume = generalSlider.value;

        }

       PlayerPrefs.SetFloat("BGMVolume", bgmSlider.value);
       PlayerPrefs.SetFloat("GenVolume", generalSlider.value);
    }*/
    
    // BGM ���
    public void MainAudio()
    {
        b_MainMenu.Play();
    }
    public void StoryAudio()
    {
        b_Story.Play();
    }
    public void LoadinAudio()
    {
        b_Loading.Play();
    }
    public void MonsterAudio(int num)
    {
        num = UnityEngine.Random.Range(0, b_Monsters.Length);

        b_Monsters[num].Play();
    }
    public void BossAudio()
    {
        b_Boss.Play();
    }
    public void ShopAudio()
    {
        b_Shop.Play();
    }
    public void EventAudio()
    {
        b_Event.Play();
    }
    public void RestAudio()
    {
        b_Rest.Play();
    }
    public void TileMapAudio()
    {
        b_TileMap.Play();
    }

    // ��� ����� ���
    public void ButtonAudio()
    {
        f_Button.Play();
    }
    public void ButtonFailAudio()
    {
        f_ButtonFail.Play();
    }
    public void SpwanAudio()
    {
        f_Spwan.Play();
    } 
    public void PotionAudio()
    {
        f_Potion.Play();
    }
    public void WinAudio()
    {
        f_Win.Play();
    } 
    public void GetItemAudio()
    {
        f_GetItem.Play();
    } 
    public void ClearAudio()
    {
        f_Clear.Play();
    }
    public void DieAudio()
    {
        f_Die.Play();
    }
    public void HealAudio()
    {
        f_Heal.Play();
    }


    // ���۽� �Ҹ� �ߺ� ���ſ�
    void StopAudio()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            b_Story.Stop();
            f_Button.Stop();
        }
        else if (SceneManager.GetActiveScene().name == "Game")
        {
            foreach(AudioSource audio in b_Monsters)
            {
                audio.Stop();
            }
            b_Boss.Stop();
            b_Shop.Stop();
            b_Event.Stop();
            b_Rest.Stop();
            b_TileMap.Stop();


            f_Button.Stop();
            f_ButtonFail.Stop();
            f_Spwan.Stop();
            f_Potion.Stop();
            f_Win.Stop();
            f_GetItem.Stop();
            f_Clear.Stop();
            f_Die.Stop();
            f_Heal.Stop();
        }
    }
}