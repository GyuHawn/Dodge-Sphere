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
    public AudioSource f_Potion; // ����
    public AudioSource f_Win; // �¸�
    public AudioSource f_GetItem; // ������ȹ��
    public AudioSource f_Clear; // Ŭ����
    public AudioSource f_Die; // �÷��̾� ���
    public AudioSource f_Convert; // ��ȯ

    private AudioSource currentAudioSource; // ���� ������� �����

    // Slider bgmSlider;
    //public Slider generalSlider; 

    void Start()
    {
        SartAudioSetting();
        if (SceneManager.GetActiveScene().name == "Game")
        {
            TileMapAudio();
        }
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

    void Update()
    {
        Debug.Log("currentAudioSource : " + currentAudioSource);
        /*
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
       PlayerPrefs.SetFloat("GenVolume", generalSlider.value);*/
    }

    // ���� ����ǰ� �ִ� ����� ����
    public void StopCurrentAudio()
    {
        if (currentAudioSource != null && currentAudioSource.isPlaying)
        {
            currentAudioSource.Stop();
        }
    }

    // BGM ���
    public void MainAudio()
    {
        // ���� ������� ����� ���� �� ���
        StopCurrentAudio(); 
        currentAudioSource = b_MainMenu;
        currentAudioSource.Play();
    }

    public void StoryAudio()
    {
        StopCurrentAudio();
        currentAudioSource = b_Story;
        currentAudioSource.Play();
    }

    public void LoadinAudio()
    {
        StopCurrentAudio();
        currentAudioSource = b_Loading;
        currentAudioSource.Play();
    }

    public void MonsterAudio()
    {
        StopCurrentAudio();
        int num = UnityEngine.Random.Range(0, b_Monsters.Length);
        currentAudioSource = b_Monsters[num];
        currentAudioSource.Play();
    }

    public void BossAudio()
    {
        StopCurrentAudio();
        currentAudioSource = b_Boss;
        currentAudioSource.Play();
    }

    public void ShopAudio()
    {
        StopCurrentAudio();
        currentAudioSource = b_Shop;
        currentAudioSource.Play();
    }

    public void EventAudio()
    {
        StopCurrentAudio();
        currentAudioSource = b_Event;
        currentAudioSource.Play();
    }

    public void RestAudio()
    {
        StopCurrentAudio();
        currentAudioSource = b_Rest;
        currentAudioSource.Play();
    }

    public void TileMapAudio()
    {
        StopCurrentAudio();
        currentAudioSource = b_TileMap;
        currentAudioSource.Play();
    }

    // ��� ����� ���
    public void ButtonAudio()
    {
        StopCurrentAudio();
        currentAudioSource = f_Button;
        currentAudioSource.Play();
    }
     
    public void ButtonFailAudio()
    {
        StopCurrentAudio();
        currentAudioSource = f_ButtonFail;
        currentAudioSource.Play();
    }

    public void PotionAudio()
    {
        StopCurrentAudio();
        currentAudioSource = f_Potion;
        currentAudioSource.Play();
    }

    public void WinAudio()
    {
        StopCurrentAudio();
        currentAudioSource = f_Win;
        currentAudioSource.Play();
    }

    public void GetItemAudio()
    {
        StopCurrentAudio();
        currentAudioSource = f_GetItem;
        currentAudioSource.Play();
    }

    public void ClearAudio()
    {
        StopCurrentAudio();
        currentAudioSource = f_Clear;
        currentAudioSource.Play();
    }

    public void DieAudio()
    {
        StopCurrentAudio();
        currentAudioSource = f_Die;
        currentAudioSource.Play();
    }
    public void ConvertAudio()
    {
        StopCurrentAudio();
        currentAudioSource = f_Convert;
        currentAudioSource.Play();
    }


    // ���۽� �Ҹ� ����
    void SartAudioSetting()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            MainAudio();
            b_Story.Stop();
            f_Button.Stop();
        }
        else if (SceneManager.GetActiveScene().name == "Game")
        {
            b_TileMap.Stop();
            b_Story.Stop();
            foreach (AudioSource audio in b_Monsters)
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
            f_Potion.Stop();
            f_Win.Stop();
            f_GetItem.Stop();
            f_Clear.Stop();
            f_Die.Stop();
            f_Convert.Stop();
        }
    }
}