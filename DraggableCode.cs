using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableCode : MonoBehaviour
{
    public float minX; // X����Сֵ
    public float maxX; // X�����ֵ

    private bool isDragging = false; // �Ƿ������϶�
    private Vector3 clickOffset; // �����λ���������ƫ����
    private Vector3 initialPosition; // ��ʼλ��

    void Start()
    {
        // ��¼��ʼλ��
        initialPosition = transform.position;
    }

    public void StartDragging()
    {
        isDragging = true;
        // ���������λ���������ƫ����
        Vector3 mousePosition = Input.mousePosition;
        // ��������Ļ����ת��Ϊ��������
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));
        clickOffset = transform.position - worldPosition;
        Debug.Log("���߻�����Code");
    }

    void Update()
    {
        if (isDragging)
        {
            // ��������λ��
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));
            Vector3 newPosition = worldPosition + clickOffset;
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX); // ����X��
            newPosition.y = initialPosition.y; // Y������̶�Ϊ��ʼY������
            newPosition.z = initialPosition.z; // Z������̶�Ϊ��ʼZ������
            transform.position = newPosition;
        }

        if (Input.GetMouseButtonUp(0)) // ����������ɿ�
        {
            isDragging = false;
        }
    }
}