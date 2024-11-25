using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeMoved : MonoBehaviour
{
    public float moveSpeed = 1.0f; // �ƶ��ٶ�
    public float minX = 62; // ���Ƶ���СX����
    public float maxX = 134; // ���Ƶ����X����
    public GameObject startPrefab; // ��ʼʱ���ɵ�Ԥ����
    public GameObject pausePrefab; // ��ͣʱ���ɵ�Ԥ����
    public GameObject startObject1; // ��ʼ1��Ϸ��������
    public GameObject pauseObject1; // ��ͣ1��Ϸ��������
    private GameObject startInstance; // ��ʼ2Ԥ�����ʵ��
    private GameObject pauseInstance; // ��ͣ2Ԥ�����ʵ��
    private Vector3 tStartPosition; // 'T' ������ʱ��λ��
    private bool isMoving = false; // �Ƿ������ƶ�

    void Update()
    {
        // ���ո���Ƿ񱻰���
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isMoving = !isMoving; // �л��ƶ�״̬
            ManageInstances(isMoving, startPrefab, startObject1);
        }

        // ��� 'T' ���Ƿ񱻰���
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!isMoving)
            {
                tStartPosition = transform.position; // ��¼���� 'T' ��ʱ��λ��
                isMoving = true;
                ManageInstances(true, startPrefab, startObject1);
            }
        }
        else if (Input.GetKeyUp(KeyCode.T))
        {
            isMoving = false;
            transform.position = tStartPosition; // ���ص����� 'T' ��ʱ��λ��
            ManageInstances(false, startPrefab, startObject1);
        }

        // ����������Ƿ񱻰���
        if (Input.GetMouseButtonDown(0))
        {
            if (IsMouseOverObject(startObject1))
            {
                ManageInstances(true, startPrefab, startObject1);
            }
            else if (IsMouseOverObject(pauseObject1))
            {
                ManageInstances(true, pausePrefab, pauseObject1);
            }
        }
        // ����������Ƿ��ͷ�
        if (Input.GetMouseButtonUp(0))
        {
            ManageInstances(false, startPrefab, startObject1);
            ManageInstances(false, pausePrefab, pauseObject1);
        }

        // ��������ƶ����������ƶ�����
        if (isMoving)
        {
            MoveObject();
        }
    }

    void MoveObject()
    {
        Vector3 newPosition = transform.position + Vector3.right * moveSpeed * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        transform.position = newPosition;
    }

    void ManageInstances(bool instantiate, GameObject prefab, GameObject object1)
    {
        if (instantiate)
        {
            Vector3 spawnPosition = object1.transform.position; // ��ȡ����1��λ��
            Quaternion spawnRotation = Quaternion.Euler(90, 0, 0); // ��ȡ����1����ת

            if (prefab == startPrefab && startInstance == null)
            {
                // ʵ������ʼ2Ԥ�����ڿ�ʼ1��λ�ã�����ת90��
                startInstance = Instantiate(startPrefab, spawnPosition, spawnRotation);
            }
            else if (prefab == pausePrefab && pauseInstance == null)
            {
                // ʵ������ͣ2Ԥ��������ͣ1��λ�ã�����ת90��
                pauseInstance = Instantiate(pausePrefab, spawnPosition, spawnRotation);
            }
        }
        else
        {
            // ���ٿ�ʼ2Ԥ����
            Destroy(startInstance);
            startInstance = null;
            // ������ͣ2Ԥ����
            Destroy(pauseInstance);
            pauseInstance = null;
        }
    }

    bool IsMouseOverObject(GameObject objectToCheck)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider.gameObject == objectToCheck;
        }

        return false;
    }
}