using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimRangeGizmo : MonoBehaviour
{
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.matrix = Matrix4x4.TRS(transform.position,transform.rotation,Vector3.one);
    //    Gizmos.DrawCube(transform.position, new Vector3(6,4,9));
    //}
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        // ���� Matrix ����
        Matrix4x4 oldMatrix = Gizmos.matrix;
        // ���� ������Ʈ�� ��ġ, ȸ��, �������� ����
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        // ����� ��Ʈ������ ����� �׸�
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(6, 6, 14));
        // ���� ��Ʈ������ ���� (�� �ϸ� �ٸ� ����𿡵� ������ ��)
        Gizmos.matrix = oldMatrix;
    }

}
