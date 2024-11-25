using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeMoved : MonoBehaviour
{
    public float moveSpeed = 1.0f; // 移动速度
    public float minX = 62; // 限制的最小X坐标
    public float maxX = 134; // 限制的最大X坐标
    public GameObject startPrefab; // 开始时生成的预制体
    public GameObject pausePrefab; // 暂停时生成的预制体
    public GameObject startObject1; // 开始1游戏场景物体
    public GameObject pauseObject1; // 暂停1游戏场景物体
    private GameObject startInstance; // 开始2预制体的实例
    private GameObject pauseInstance; // 暂停2预制体的实例
    private Vector3 tStartPosition; // 'T' 键按下时的位置
    private bool isMoving = false; // 是否正在移动

    void Update()
    {
        // 检查空格键是否被按下
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isMoving = !isMoving; // 切换移动状态
            ManageInstances(isMoving, startPrefab, startObject1);
        }

        // 检查 'T' 键是否被按下
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!isMoving)
            {
                tStartPosition = transform.position; // 记录按下 'T' 键时的位置
                isMoving = true;
                ManageInstances(true, startPrefab, startObject1);
            }
        }
        else if (Input.GetKeyUp(KeyCode.T))
        {
            isMoving = false;
            transform.position = tStartPosition; // 返回到按下 'T' 键时的位置
            ManageInstances(false, startPrefab, startObject1);
        }

        // 检查鼠标左键是否被按下
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
        // 检查鼠标左键是否被释放
        if (Input.GetMouseButtonUp(0))
        {
            ManageInstances(false, startPrefab, startObject1);
            ManageInstances(false, pausePrefab, pauseObject1);
        }

        // 如果正在移动，则向右移动物体
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
            Vector3 spawnPosition = object1.transform.position; // 获取对象1的位置
            Quaternion spawnRotation = Quaternion.Euler(90, 0, 0); // 获取对象1的旋转

            if (prefab == startPrefab && startInstance == null)
            {
                // 实例化开始2预制体在开始1的位置，并旋转90度
                startInstance = Instantiate(startPrefab, spawnPosition, spawnRotation);
            }
            else if (prefab == pausePrefab && pauseInstance == null)
            {
                // 实例化暂停2预制体在暂停1的位置，并旋转90度
                pauseInstance = Instantiate(pausePrefab, spawnPosition, spawnRotation);
            }
        }
        else
        {
            // 销毁开始2预制体
            Destroy(startInstance);
            startInstance = null;
            // 销毁暂停2预制体
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