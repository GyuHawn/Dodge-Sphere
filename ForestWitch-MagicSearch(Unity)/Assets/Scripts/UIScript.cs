using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public GameObject ex; // ����â

    public void OnEx()
    {
        ex.SetActive(true);
    }

    public void OffEx()
    {
        ex.SetActive(false);
    }
}
