using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public GameObject targetObject; // Ҫ�ƶ���Ŀ������
    public float movementAmount = 1.0f; // ÿ�ι���ʱ�����ƶ�����
    public float minX = 62; // ���Ƶ���СX����
    public float maxX = 134; // ���Ƶ����X����

    private bool isMouseOver = false; // ����Ƿ���ͣ��������

    void Update()
    {
        // �������Ƿ���ͣ��������
        isMouseOver = IsMouseOverObject();

        if (isMouseOver && targetObject != null)
        {
            // ���������������ƶ�����
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            if (Mathf.Abs(scrollInput) >= 0.09f) // �������������ڻ����0.09
            {
                // ���ݹ��ַ�������õ��ƶ����ƶ�����
                Vector3 movement = new Vector3((scrollInput > 0 ? 1 : -1) * movementAmount, 0, 0);
                Vector3 newPosition = targetObject.transform.position + movement;

                // ���������X������ָ����Χ��
                newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

                targetObject.transform.position = newPosition;
            }
        }
    }

    // �������Ƿ���ͣ��������
    bool IsMouseOverObject()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                return true;
            }
        }

        return false;
    }
}