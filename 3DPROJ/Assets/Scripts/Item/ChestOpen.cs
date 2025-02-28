using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChestOpen : MonoBehaviour
{
    float maxAngle;
    float currentAngle;
    private void Start()
    {
        maxAngle = -40;
        currentAngle = 0;
    }
    public IEnumerator OpenCover()
    {
        while (currentAngle > -40)
        {
            currentAngle -= 0.5f;
            transform.rotation = Quaternion.Euler(currentAngle, 0, 0);
            yield return null;
        }
    }
}
