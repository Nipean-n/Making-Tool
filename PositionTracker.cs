using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTracker : MonoBehaviour
{
    public GameObject ccPrefab; // 预制体cc的引用
    public GameObject rayDetector; // 用于射线检测的物体
    private GameObject currentCC; // 当前生成的预制体的引用

    void Update()
    {
        // 将鼠标位置从屏幕空间转换为射线
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // 发射射线并检测碰撞
        if (Physics.Raycast(ray, out hit))
        {
            // 检查碰撞的物体是否是我们的射线检测器
            if (hit.collider.gameObject == rayDetector)
            {
                // 获取射线碰撞点的x坐标
                float hitX = hit.point.x;
                float hitY = hit.point.y;

                // 找到最接近的x坐标和y坐标
                float closestX = FindClosestXCoordinate(hitX);
                float closestY = FindClosestYCoordinate(hitY);

                // 如果上一帧生成了预制体，则删除它
                if (currentCC != null)
                {
                    Destroy(currentCC);
                }

                // 在最接近的x坐标和y坐标位置生成预制体
                currentCC = Instantiate(ccPrefab, new Vector3(closestX, closestY, 0), Quaternion.identity);
            }
        }
    }

    // 找到最接近鼠标x轴位置的x坐标
    private float FindClosestXCoordinate(float mouseX)
    {
        List<float> xCoordinates = GetXCoordinatesOfShuObjects(); // 获取所有x轴坐标
        return FindClosestCoordinate(mouseX, xCoordinates);
    }

    // 找到最接近鼠标y轴位置的y坐标
    private float FindClosestYCoordinate(float mouseY)
    {
        List<float> yCoordinates = GetYCoordinatesOfXiansonObjects(); // 获取所有y轴坐标
        return FindClosestCoordinate(mouseY, yCoordinates);
    }

    // 获取所有标签为"Shu"的物体的x轴坐标
    private List<float> GetXCoordinatesOfShuObjects()
    {
        List<float> xCoordinates = new List<float>();
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Shu"); // 获取所有标签为"Shu"的物体

        foreach (GameObject obj in allObjects)
        {
            if (obj != null) // 确保物体不是null
            {
                xCoordinates.Add(obj.transform.position.x); // 添加x轴坐标到列表
            }
        }

        return xCoordinates; // 返回坐标列表
    }

    // 获取所有标签为"Xianson"的物体的y轴坐标，且y值在-300到300之间
    private List<float> GetYCoordinatesOfXiansonObjects()
    {
        List<float> yCoordinates = new List<float>();
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Xianson"); // 获取所有标签为"Xianson"的物体

        foreach (GameObject obj in allObjects)
        {
            if (obj != null && Mathf.Abs(obj.transform.position.y) <= 300) // 确保物体不是null且y值在-300到300之间
            {
                yCoordinates.Add(obj.transform.position.y); // 添加y轴坐标到列表
            }
        }

        return yCoordinates; // 返回坐标列表
    }

    // 找到最接近给定值的坐标
    private float FindClosestCoordinate(float value, List<float> coordinates)
    {
        if (coordinates.Count == 0)
        {
            Debug.LogError("No valid coordinates found.");
            return 0;
        }

        float closest = coordinates[0]; // 默认为第一个坐标
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