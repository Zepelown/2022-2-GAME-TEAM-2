using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CirSector : MonoBehaviour
{
    private MovingObject theMovingObject; 

    public Transform target;                                // üũ ���
        
    public float angleRange = 45f;                          // �þ߰�
    public float distance = 800f;                           // �þ� �Ÿ�
    public bool isCollision = false;                      

    private Vector2 vector;                                 // ��ü �ִϸ����Ͱ� �ٶ󺸰� �ִ� ����

    Color _blue = new Color(0f, 0f, 1f, 0.2f);
    Color _red = new Color(1f, 0f, 0f, 0.2f);

    Vector3 direction;                                      // ��ü���� �������� ����

    private void Start()
    {
        theMovingObject = GetComponent<MovingObject>();
    }

    void Update()
    {
        // ��ü �ִϸ����Ͱ� �ٶ󺸰� �ִ� ���� ������Ʈ
        vector.Set(theMovingObject.animator.GetFloat("DirX"), theMovingObject.animator.GetFloat("DirY"));
        // ��ü���� �������� ����
        direction = target.position - transform.position;
        
        // target�� ��ü ������ �Ÿ��� distance���� �۴ٸ�
        if (direction.magnitude < distance)
        {
            // (Ÿ��-��ü)���Ϳ� (��ü����)���� ����
            float dot = Vector3.Dot(direction.normalized, -transform.up);   // �ӽ� �⺻��(�Ʒ����� ���� ���)

            // ĳ���Ͱ� �������� ���� ���
            if (vector.x == 1f)
            {
                dot = Vector3.Dot(direction.normalized, transform.right);
            }
            // ������ ���� ���
            else if (vector.x == -1f)
            {
                dot = Vector3.Dot(direction.normalized, -transform.right);
            }
            // ������ ���� ���
            else if (vector.y == 1f)
            {
                dot = Vector3.Dot(direction.normalized, transform.up);
            }
            // �Ʒ����� ���� ���
            else if (vector.y == -1f)
            {
                dot = Vector3.Dot(direction.normalized, -transform.up);       
            }

            // �� ���� ��� ���� �����̹Ƿ� ���� ����� cos�� ���� ���ؼ� theta�� ����
            float theta = Mathf.Acos(dot);                              // �� �ڻ���
            // angleRange�� �񱳸� ���� ������ ��ȯ
            float degree = Mathf.Rad2Deg * theta;                       // ������ ��ȯ

            // �þ߰� �Ǻ�
            if (degree <= angleRange / 2f)
            {
                isCollision = true;
            }
            else
            {
                isCollision = false;
            }
        }
        else
        {
            isCollision = false;
        }
        Debug.Log("isCollision: " + isCollision);
    }

    // �����Ϳ����� draw gizmos
    private void OnDrawGizmos()
    {
        Vector3 drawVector = -transform.up;
        // vector.Set(theMovingObject.animator.GetFloat("DirX"), theMovingObject.animator.GetFloat("DirY"));
        direction = target.position - transform.position;

        // target�� ��ü ������ �Ÿ��� distance���� �۴ٸ�
        if (direction.magnitude < distance)
        {
            // (Ÿ��-��ü)���Ϳ� (��ü����)���� ����
            float dot = Vector3.Dot(direction.normalized, -transform.up);

            // ĳ���Ͱ� �������� ���� ���
            if (vector.x == 1f)
            {
                dot = Vector3.Dot(direction.normalized, transform.right);
                drawVector = transform.right;
            }
            // ������ ���� ���
            else if (vector.x == -1f)
            {
                dot = Vector3.Dot(direction.normalized, -transform.right);
                drawVector = -transform.right;
            }
            // ������ ���� ���
            else if (vector.y == 1f)
            {
                dot = Vector3.Dot(direction.normalized, transform.up);
                drawVector = transform.up;
            }
            // �Ʒ����� ���� ���
            else if (vector.y == -1f)
            {
                dot = Vector3.Dot(direction.normalized, -transform.up);
                drawVector = -transform.up;
            }

            float theta = Mathf.Acos(dot);                                  // �� �ڻ���
            float degree = Mathf.Rad2Deg * theta;                           // ������ ��ȯ

            // �þ߰� �Ǻ�
            if (degree <= angleRange / 2f)
            {
                isCollision = true;
            }
            else
            {
                isCollision = false;
            }
        }
        else
        {
            isCollision = false;
        }
        Handles.color = isCollision ? _red : _blue;
        Handles.DrawSolidArc(transform.position, Vector3.forward, drawVector, angleRange / 2, distance);
        Handles.DrawSolidArc(transform.position, Vector3.forward, drawVector, -angleRange / 2, distance);
    }
}
