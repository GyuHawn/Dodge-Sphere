using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // ȸ�� �ӵ�
    public float rotationSpeed;
    public bool x;
    public bool y;
    public bool z;

    void Update()
    {
        if (x)
        {
            // x�� �������� ȸ��
            transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
        }
        else if(y)
        {
            // y�� �������� ȸ��
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
        else if (z)
        {
            // z�� �������� ȸ��
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
        
    }
}
