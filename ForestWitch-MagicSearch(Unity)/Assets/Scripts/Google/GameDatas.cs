using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.BasicApi;
using System.Text;
using System;
using System.Reflection;
using TMPro;
 
public class DataSettings
{
    // ���� �ɷ�
    public int ability1Num = 0;
    public int ability2Num = 0;
    public int ability3Num = 0;
    public int ability4Num = 0;
    public int ability5Num = 0;
    public int ability6Num = 0;

    // ���� ����
    public int maxLevel = 1;
    public int adventLevel = 1;

    // Ŭ���� ����
    public bool onStory = false;

    // ���� ����
    public int maxExp = 50;
    public int currentExp = 0;
    public int gameLevel = 50;

    // ���� ����
    public int playerNum = 1;
    public int cannonNum1 = 1;
    public int cannonNum2 = 2;

    // �����
    public float bgmVolume = 1.0f;
    public float fncVolume = 1.0f;
    public float monsterVolume = 1.0f;
}

public class GameDatas : MonoBehaviour
{
    public DataSettings dataSettings = new DataSettings();

    private string fileName = "file.dat";

    //public TMP_Text text;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Update()
    {
        /*string infoText = "������ Ȯ��\n" +
                          "Abilities: 0, 0, 0, 0, 0, 0\n" +
                          $"Adventure Level: {dataSettings.maxLevel} (Current: {dataSettings.adventLevel})\n" +
                          $"Story Cleared: {dataSettings.onStory}\n" +
                          $"Max Exp: {dataSettings.maxExp} / Currnet Exp: {dataSettings.currentExp}\n" +
                          $"Game Level: {dataSettings.gameLevel}\n" +
                          $"Player Count: {dataSettings.playerNum}\n" +
                          $"Cannons: {dataSettings.cannonNum1}, {dataSettings.cannonNum2}\n" +
                          $"Audio Settings - BGM: {dataSettings.bgmVolume}, Effects: {dataSettings.fncVolume}, Monster: {dataSettings.monsterVolume}";

        text.text = infoText;*/
    }

    public void BasicData()
    {
        // ���� �ɷ� ����
        dataSettings.ability1Num = 0;
        dataSettings.ability2Num = 0;
        dataSettings.ability3Num = 0;
        dataSettings.ability4Num = 0;
        dataSettings.ability5Num = 0;
        dataSettings.ability6Num = 0;

        // ���� ���� ����
        dataSettings.maxLevel = 1;
        dataSettings.adventLevel = 1;

        // Ŭ���� ���� ����
        dataSettings.onStory = false;

        // ���� ���� ����
        dataSettings.maxExp = 0;
        dataSettings.currentExp = 0;
        dataSettings.gameLevel = 1;

        // ���� ���� ����
        dataSettings.playerNum = 1;
        dataSettings.cannonNum1 = 1;
        dataSettings.cannonNum2 = 2;

        // ����� ���� ����
        dataSettings.bgmVolume = 1.0f;
        dataSettings.fncVolume = 1.0f;
        dataSettings.monsterVolume = 1.0f;
    }

    #region ���� 
    public void UpdateAbility<T>(string fieldName, T fieldValue)
    {
        SaveFieldData(fieldName, fieldValue);
    }

    public void SaveFieldData<T>(string fieldName, T fieldValue)
    {
        // �ʵ� ������ ������
        FieldInfo fieldInfo = typeof(DataSettings).GetField(fieldName);

        // �ʵ� Ÿ���� ��ġ�ϴ� ��쿡�� ���� ����
        if (fieldInfo != null && fieldInfo.FieldType == typeof(T))
        {
            fieldInfo.SetValue(dataSettings, fieldValue);

            // �����͸� JSON���� ��ȯ
            var json = JsonUtility.ToJson(dataSettings);

            // JSON �����͸� Ŭ���忡 ����
            SaveJsonToCloud(json);
        }
        else
        {
            Debug.LogError("Field not found or type mismatch");
        }
    }

    private void SaveJsonToCloud(string json)
        {
            // Google Play Games ���񽺸� ����Ͽ� Ŭ���忡 ����
            OpenSaveGame(json);
        }

    private void OpenSaveGame(string json)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        savedGameClient.OpenWithAutomaticConflictResolution(fileName,
                                                            DataSource.ReadCacheOrNetwork,
                                                            ConflictResolutionStrategy.UseLastKnownGood,
                                                            (status, game) => OnsavedGameOpened(status, game, json));
    }

    private void OnsavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game, string json)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Save successful");

            // �����͸� ����Ʈ �迭�� ��ȯ
            byte[] bytes = Encoding.UTF8.GetBytes(json);

            var update = new SavedGameMetadataUpdate.Builder().Build();

            // ����� �����͸� Ŭ���忡 ����
            savedGameClient.CommitUpdate(game, update, bytes, OnSavedGameWritten);
        }
        else
        {
            Debug.Log("Save failed");
        }
    }

    private void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Save completed successfully");
        }
        else
        {
            Debug.Log("Failed to save data");
        }
    }
    #endregion

    #region �ҷ�����

    public void LoadData()
    {
        OpenLoadGame();
    }

    private void OpenLoadGame()
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        savedGameClient.OpenWithAutomaticConflictResolution(fileName,
                                                             DataSource.ReadCacheOrNetwork,
                                                             ConflictResolutionStrategy.UseLastKnownGood,
                                                            LoadGameData);
    }

    private void LoadGameData(SavedGameRequestStatus status, ISavedGameMetadata data)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Save completed successfully");

            savedGameClient.ReadBinaryData(data, OnSavedGameDataRead);
        }
        else
        {
            Debug.Log("Failed to save data");
        }
    }

    private void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] loadedData)
    {
        if (status == SavedGameRequestStatus.Success && loadedData.Length > 0)
        {
            string data = System.Text.Encoding.UTF8.GetString(loadedData);
            if(data != "")
            {
                dataSettings = JsonUtility.FromJson<DataSettings>(data);
            }
        }
    }
    #endregion

    #region ����
    public void DeleteData()
    {
        DeleteGameData();
    }

    private void DeleteGameData()
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        savedGameClient.OpenWithAutomaticConflictResolution(fileName,
                                                            DataSource.ReadCacheOrNetwork,
                                                            ConflictResolutionStrategy.UseLastKnownGood,
                                                            DeleteSaveGame);
    }

    private void DeleteSaveGame(SavedGameRequestStatus status, ISavedGameMetadata data)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        if (status == SavedGameRequestStatus.Success)
        {
            // ���������� ���� ������ ������ ��������, ������ ����
            savedGameClient.Delete(data);
            // ���� ������ ���������� ����Ǿ��ٰ� �����ϰ� �⺻ �����ͷ� �ʱ�ȭ
            BasicData();
            Debug.Log("���� ��û ����, �����͸� �ʱ�ȭ�մϴ�.");
        }
        else
        {
            Debug.Log("���� ����: ���� ������ ���� ���⿡ �����߽��ϴ�.");
        }
    }

    #endregion
}
