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
        // 기존 Matrix 저장
        Matrix4x4 oldMatrix = Gizmos.matrix;
        // 현재 오브젝트의 위치, 회전, 스케일을 적용
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        // 적용된 매트릭스로 기즈모를 그림
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(6, 6, 14));
        // 원래 매트릭스로 복원 (안 하면 다른 기즈모에도 영향이 감)
        Gizmos.matrix = oldMatrix;
    }

}
