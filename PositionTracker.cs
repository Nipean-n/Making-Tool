using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTracker : MonoBehaviour
{
    public GameObject ccPrefab; // Ԥ����cc������
    public GameObject rayDetector; // �������߼�������
    private GameObject currentCC; // ��ǰ���ɵ�Ԥ���������

    void Update()
    {
        // �����λ�ô���Ļ�ռ�ת��Ϊ����
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // �������߲������ײ
        if (Physics.Raycast(ray, out hit))
        {
            // �����ײ�������Ƿ������ǵ����߼����
            if (hit.collider.gameObject == rayDetector)
            {
                // ��ȡ������ײ���x����
                float hitX = hit.point.x;
                float hitY = hit.point.y;

                // �ҵ���ӽ���x�����y����
                float closestX = FindClosestXCoordinate(hitX);
                float closestY = FindClosestYCoordinate(hitY);

                // �����һ֡������Ԥ���壬��ɾ����
                if (currentCC != null)
                {
                    Destroy(currentCC);
                }

                // ����ӽ���x�����y����λ������Ԥ����
                currentCC = Instantiate(ccPrefab, new Vector3(closestX, closestY, 0), Quaternion.identity);
            }
        }
    }

    // �ҵ���ӽ����x��λ�õ�x����
    private float FindClosestXCoordinate(float mouseX)
    {
        List<float> xCoordinates = GetXCoordinatesOfShuObjects(); // ��ȡ����x������
        return FindClosestCoordinate(mouseX, xCoordinates);
    }

    // �ҵ���ӽ����y��λ�õ�y����
    private float FindClosestYCoordinate(float mouseY)
    {
        List<float> yCoordinates = GetYCoordinatesOfXiansonObjects(); // ��ȡ����y������
        return FindClosestCoordinate(mouseY, yCoordinates);
    }

    // ��ȡ���б�ǩΪ"Shu"�������x������
    private List<float> GetXCoordinatesOfShuObjects()
    {
        List<float> xCoordinates = new List<float>();
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Shu"); // ��ȡ���б�ǩΪ"Shu"������

        foreach (GameObject obj in allObjects)
        {
            if (obj != null) // ȷ�����岻��null
            {
                xCoordinates.Add(obj.transform.position.x); // ���x�����굽�б�
            }
        }

        return xCoordinates; // ���������б�
    }

    // ��ȡ���б�ǩΪ"Xianson"�������y�����꣬��yֵ��-300��300֮��
    private List<float> GetYCoordinatesOfXiansonObjects()
    {
        List<float> yCoordinates = new List<float>();
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Xianson"); // ��ȡ���б�ǩΪ"Xianson"������

        foreach (GameObject obj in allObjects)
        {
            if (obj != null && Mathf.Abs(obj.transform.position.y) <= 300) // ȷ�����岻��null��yֵ��-300��300֮��
            {
                yCoordinates.Add(obj.transform.position.y); // ���y�����굽�б�
            }
        }

        return yCoordinates; // ���������б�
    }

    // �ҵ���ӽ�����ֵ������
    private float FindClosestCoordinate(float value, List<float> coordinates)
    {
        if (coordinates.Count == 0)
        {
            Debug.LogError("No valid coordinates found.");
            return 0;
        }

        float closest = coordinates[0]; // Ĭ��Ϊ��һ������
        float smallestDifference = Mathf.Abs(closest - value);

        foreach (float coord in coordinates)
        {
            float difference = Mathf.Abs(coord - value);
            if (difference < smallestDifference)
            {
                smallestDifference = difference;
                closest = coord;
            }
        }

        return closest;
    }
}