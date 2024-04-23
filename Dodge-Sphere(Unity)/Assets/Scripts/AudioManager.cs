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
    public AudioSource fn_Button; // ��ư (o) 
    public AudioSource fn_ButtonFail; // ��ư (x)
    public AudioSource fn_Potion; // ����
    public AudioSource fn_Win; // �¸�
    public AudioSource fn_GetItem; // ������ȹ��
    public AudioSource fn_Clear; // Ŭ����
    public AudioSource fn_Die; // �÷��̾� ���
    public AudioSource fn_Convert; // ��ȯ
    public AudioSource fn_cannon; // ���� �߻�

    // Monster 
    // ��
    public AudioSource f_Base;
    public AudioSource f_Cry;
    public AudioSource f_Jump;
    public AudioSource f_Roll;

    // ������
    public AudioSource c_Bounce;
    public AudioSource c_Wave;
    public AudioSource c_Punch;
    public AudioSource c_Butt;

    // ����
    public AudioSource m_Butt;
    public AudioSource m_Spin;
    public AudioSource m_Uper;

    // ����
    public AudioSource cl_Push;
    public AudioSource cl_Shot;
    public AudioSource cl_Dance;

    // ����
    public AudioSource ch_Bite;
    public AudioSource ch_Butt;
    public AudioSource ch_Eating;

    // �ֽ���
    public AudioSource be_Laser; 
    public AudioSource be_Multi;
    public AudioSource be_Aiming;


    
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
        currentAudioSource = fn_Button;
        currentAudioSource.Play();
    }
     
    public void ButtonFailAudio()
    {
        StopCurrentAudio();
        currentAudioSource = fn_ButtonFail;
        currentAudioSource.Play();
    }

    public void PotionAudio()
    {
        StopCurrentAudio();
        currentAudioSource = fn_Potion;
        currentAudioSource.Play();
    }

    public void WinAudio()
    {
        StopCurrentAudio();
        currentAudioSource = fn_Win;
        currentAudioSource.Play();
    }

    public void GetItemAudio()
    {
        StopCurrentAudio();
        currentAudioSource = fn_GetItem;
        currentAudioSource.Play();
    }

    public void ClearAudio()
    {
        StopCurrentAudio();
        currentAudioSource = fn_Clear;
        currentAudioSource.Play();
    }

    public void DieAudio()
    {
        StopCurrentAudio();
        currentAudioSource = fn_Die;
        currentAudioSource.Play();
    }
    public void ConvertAudio()
    {
        StopCurrentAudio();
        currentAudioSource = fn_Convert;
        currentAudioSource.Play();
    }
    public void CannonAudio()
    {
        fn_cannon.Play();
    }

    // ����
    // ������
    public void C_BounceAudio()
    {
        c_Bounce.Play();
    }
    public void C_WaveAudio()
    {
        c_Wave.Play();
    }
    public void C_PunchAudio()
    {
        c_Punch.Play();
    }
    public void C_ButtAudio()
    {
        c_Butt.Play();
    }

    // ����
    public void M_ButtAudio()
    {
        m_Butt.Play();
    }
    public void M_SpinAudio()
    {
        m_Spin.Play();
    }
    public void M_UperAudio()
    {
        m_Uper.Play();
    }

    // ��
    public void F_BaseAudio()
    {
        f_Base.Play();
    }
    public void F_CryAudio()
    {
        f_Cry.Play();
    }
    public void F_JumpAudio()
    {
        f_Jump.Play();
    }
    public void F_RollAudio()
    {
        f_Roll.Play();
    }

    // ����
    public void Ch_BiteAudio()
    {
        ch_Bite.Play();
    }
    public void Ch_ButtAudio()
    {
        ch_Butt.Play();
    }
    public void Ch_EatingAudio()
    {
        ch_Eating.Play();
    }

    // �ֽ���
    public void Be_LazerAudio()
    {
        be_Laser.Play();
    }
    public void Be_MultiAudio()
    {
        be_Multi.Play();
    }
    public void Be_AimingAudio()
    {
        be_Aiming.Play();
    }

    // ����
    public void Cl_PushAudio()
    {
        cl_Push.Play();
    }
    public void Cl_ShotAudio()
    {
        cl_Shot.Play();
    }
    public void Cl_DanceAudio()
    {
        cl_Dance.Play();
    }

    // ���۽� �Ҹ� ����
    void SartAudioSetting()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            MainAudio();
            b_Story.Stop();
            fn_Button.Stop();
        }
        else if (SceneManager.GetActiveScene().name == "Game")
        {
            StopBGMAudio(); // BGM
            StopFunctionAudio(); // ���
            StopMonsterAudio(); // ����
        }
    }

    void StopBGMAudio()
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
    }
    void StopFunctionAudio()
    {
        fn_Button.Stop();
        fn_ButtonFail.Stop();
        fn_Potion.Stop();
        fn_Win.Stop();
        fn_GetItem.Stop();
        fn_Clear.Stop();
        fn_Die.Stop();
        fn_Convert.Stop();
        fn_cannon.Stop();
    }
    void StopMonsterAudio()
    {
        f_Base.Stop();
        f_Cry.Stop();
        f_Jump.Stop();
        f_Roll.Stop();
        c_Bounce.Stop();
        c_Wave.Stop();
        c_Punch.Stop();
        c_Butt.Stop();
        m_Butt.Stop();
        m_Spin.Stop();
        m_Uper.Stop();
        cl_Dance.Stop();
        cl_Push.Stop();
        cl_Shot.Stop();
        ch_Bite.Stop();
        ch_Butt.Stop();
        ch_Eating.Stop();
        be_Laser.Stop();
        be_Multi.Stop();
        be_Aiming.Stop();
    }
}