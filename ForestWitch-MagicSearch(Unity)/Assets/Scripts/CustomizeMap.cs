using UnityEngine;

public class CustomizeMap : MonoBehaviour
{
    public GameObject[] mapTile; // �� Ÿ��
    public GameObject[] tilesObjects; // ������ ������

    void Start()
    {
        for (int i = 0; i < mapTile.Length; i++) // ��� Ÿ�Ͽ� ����
        {
            Transform tileTransform = mapTile[i].transform; // �θ� Ÿ���� ��ġ
            Instantiate(RandomObject(), tileTransform.position, Quaternion.identity, tileTransform); // ���� ������ ������ ����
        }
    }

    GameObject RandomObject() // ������ ������ ������ ����
    {
        int num = Random.Range(0, tilesObjects.Length);
        return tilesObjects[num];
    }
}
