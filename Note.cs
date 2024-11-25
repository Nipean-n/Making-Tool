using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public GameObject prefab; // ��һ��Ҫ���ɵ�Ԥ����
    public GameObject secondPrefab; // �ڶ���Ҫ���ɵ�Ԥ����
    private GameObject spawnedObject; // ���ɵĵ�һ��Ԥ����ʵ��

    void Update()
    {
        // ����������Ƿ񱻰���
        if (Input.GetMouseButtonDown(0))
        {
            // ��ȡ����·�����Ϸ����
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject) // ȷ�����߼�⵽���ǹ��ش˽ű�������
                {
                    // ʵ������һ��Ԥ����
                    spawnedObject = Instantiate(prefab, transform.position + new Vector3(0, 0, -1), Quaternion.identity);
                }
            }
        }
        // ����������Ƿ��ͷ�
        else if (Input.GetMouseButtonUp(0) && spawnedObject != null)
        {
            // ɾ�����ɵĵ�һ��Ԥ����
            Destroy(spawnedObject);
            spawnedObject = null; // �������ɵĵ�һ��Ԥ����ʵ������

            // ��ָ�����괦ʵ�����ڶ���Ԥ����
            Vector3 secondPrefabPosition = new Vector3(96.5f, -14.6f, -3f);
            GameObject secondSpawnedObject = Instantiate(secondPrefab, secondPrefabPosition, Quaternion.identity);
        }
    }
}