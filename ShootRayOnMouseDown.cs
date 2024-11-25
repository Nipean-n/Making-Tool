using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootRayOnMouseDown : MonoBehaviour
{
    public LayerMask codeLayer; // �������߼���LayerMask

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // �������������
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, codeLayer))
            {


                // �����������϶�������߼�
                DraggableCode draggableCode = hit.collider.gameObject.GetComponent<DraggableCode>();
                if (draggableCode != null)
                {
                    draggableCode.StartDragging();
                }
                DraggableCode1 draggableCode1 = hit.collider.gameObject.GetComponent<DraggableCode1>();
                if (draggableCode1 != null)
                {
                    draggableCode1.StartDragging();
                }
                DraggableCode2 draggableCode2 = hit.collider.gameObject.GetComponent<DraggableCode2>();
                if (draggableCode2 != null)
                {
                    draggableCode2.StartDragging();
                }
                DraggableCode3 draggableCode3 = hit.collider.gameObject.GetComponent<DraggableCode3>();
                if (draggableCode3 != null)
                {
                    draggableCode3.StartDragging();
                }
            }
        }
            
                
        }
    }
