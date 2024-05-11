using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.BasicApi;
using System.Text;
using System;
using System.Reflection;

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
    public int currentLevel = 1;

    // Ŭ���� ����
    public bool onStory = false;
    public int gameExp = 0;

    // ���� ����
    public int maxExp = 0;
    public int currentExp = 0;
    public int gameLevel = 1;

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



    void BasicData()
    {
        // ���� �ɷ� �ʱ�ȭ
        dataSettings.ability1Num = 0;
        dataSettings.ability2Num = 0;
        dataSettings.ability3Num = 0;
        dataSettings.ability4Num = 0;
        dataSettings.ability5Num = 0;
        dataSettings.ability6Num = 0;

        // ���� ���� �ʱ�ȭ
        dataSettings.maxLevel = 1;
        dataSettings.currentLevel = 1;

        // Ŭ���� ���� �ʱ�ȭ
        dataSettings.onStory = false;
        dataSettings.gameExp = 0;

        // ���� ���� �ʱ�ȭ
        dataSettings.maxExp = 0;
        dataSettings.currentExp = 0;
        dataSettings.gameLevel = 1;

        // ���� ���� �ʱ�ȭ
        dataSettings.playerNum = 1;
        dataSettings.cannonNum1 = 1;
        dataSettings.cannonNum2 = 2;

        // ����� ���� �ʱ�ȭ
        dataSettings.bgmVolume = 1.0f;
        dataSettings.fncVolume = 1.0f;
        dataSettings.monsterVolume = 1.0f;
    }

    #region ���� 
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

    // �ʵ� �����͸� �ҷ����� ���� �޼���
    public void LoadFieldData<T>(string fieldName, Action<T> onSuccess, Action onFailure)
    {
        OpenLoadGame((dataSettings) =>
        {
            FieldInfo fieldInfo = typeof(DataSettings).GetField(fieldName);
            if (fieldInfo != null && fieldInfo.FieldType == typeof(T))
            {
                T value = (T)fieldInfo.GetValue(dataSettings);
                onSuccess?.Invoke(value);  // �ݹ��� ȣ���Ͽ� ���� ó��
            }
            else
            {
                Debug.LogError("Field not found or type mismatch");
                onFailure?.Invoke();  // ���� �ݹ� ȣ��
            }
        });
    }

    private void OpenLoadGame(Action<DataSettings> onLoadedCallback)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution(fileName,
                                                            DataSource.ReadCacheOrNetwork,
                                                            ConflictResolutionStrategy.UseLastKnownGood,
                                                            (status, data) => LoadGameData(status, data, onLoadedCallback));
    }

    private void LoadGameData(SavedGameRequestStatus status, ISavedGameMetadata data, Action<DataSettings> onLoadedCallback)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Load successful");
            savedGameClient.ReadBinaryData(data, (readStatus, loadedData) => OnSavedGameDataRead(readStatus, loadedData, onLoadedCallback));
        }
        else
        {
            Debug.Log("Load failed");
        }
    }

    private void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] loadedData, Action<DataSettings> onLoadedCallback)
    {
        if (status == SavedGameRequestStatus.Success && loadedData.Length > 0)
        {
            string jsonData = System.Text.Encoding.UTF8.GetString(loadedData);
            Debug.Log("Loaded data: " + jsonData);

            DataSettings loadedSettings = JsonUtility.FromJson<DataSettings>(jsonData);
            onLoadedCallback(loadedSettings);
        }
        else
        {
            Debug.Log("No data found, initializing default data.");
            BasicData();
            onLoadedCallback(dataSettings);
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
