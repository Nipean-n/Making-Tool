using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputReceiverUp : MonoBehaviour
{
    public TextMeshPro textMeshPro; // �ı����
    public int number = 4; // ����ʼ��������Ϊ4
    private bool enableScrollInput = false; // ���������Ƿ����õı�־

    // ����һ���¼��������ָ���ʱ����
    public event System.Action<int> OnNumberChanged;

    // �������������ڻ�ȡ��ǰ����
    public int GetNumber()
    {
        return number;
    }

    void Start()
    {
        // �����Inspector��û���ֶ���ֵ�������Զ���ȡTextMesh Pro���
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshPro>();
            if (textMeshPro == null)
            {
                Debug.LogError("TextMeshPro component not found on " + gameObject.name);
            }
            else
            {
                // ��ʼ��ʾ
                UpdateDisplay();
            }
        }
    }

    public void ReceiveInput()
    {
        // ���ù�������
        enableScrollInput = true;
    }

    void Update()
    {
        // �������Ƿ��� "ShuruUp" ��
        bool isOverShuruUp = IsMouseOverGameObject("ShuruUp");

        // �������� "ShuruUp" �ϣ����ù�������
        if (isOverShuruUp && !enableScrollInput)
        {
            enableScrollInput = true;
        }
        // �����겻�� "ShuruUp" �ϣ����ù�������
        else if (!isOverShuruUp && enableScrollInput)
        {
            enableScrollInput = false;
        }

        // ֻ�е� enableScrollInput Ϊ true ʱ�������¼��Żᱻ����
        if (enableScrollInput)
        {
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            // ���ݹ��ַ����������
            if (scrollInput > 0.09f) // ���Ϲ���
            {
                number = Mathf.Min(number + 1, 32);
                UpdateDisplay();
                enableScrollInput = false; // һ�θ��º���ù�������
                OnNumberChanged?.Invoke(number); // �����¼�
            }
            else if (scrollInput < -0.09f) // ���¹���
            {
                number = Mathf.Max(number - 1, 1);
                UpdateDisplay();
                enableScrollInput = false; // һ�θ��º���ù�������
                OnNumberChanged?.Invoke(number); // �����¼�
            }
        }
    }

    private bool IsMouseOverGameObject(string gameObjectName)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider.gameObject.name == gameObjectName;
        }
        return false;
    }

    private void UpdateDisplay()
    {
        // ����TextMesh Pro������ı�Ϊ��ǰ����
        textMeshPro.text = number.ToString();
        Debug.Log("Number updated to: " + number);
    }
}