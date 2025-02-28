using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class MonsterHitShader : MonoBehaviour
{
    Renderer render;
    float currentTime;
    float endTime;
    float middleTime;
    bool incerease;
    bool decerease;
    public bool complate;
    private void Start()
    {
        render = GetComponent<Renderer>();
        currentTime = 0;
        middleTime = endTime /2;
        endTime = 2;
        complate = true;
    }
    public IEnumerator HitMotion()
    {
        if (complate == false)
        {
            yield break;
        }
        complate = false;
        Color startColor = render.material.color;  // 원래 색상 저장
        Color hitColor = Color.red;                // 타격 시 빨간색
        float duration = 1f;                        // 빨갛게 변하는 시간
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            render.material.color = Color.Lerp(startColor, hitColor, t);
            yield return null;
        }

        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            render.material.color = Color.Lerp(hitColor, startColor, t);
            yield return null;
        }
        complate = true;
    }

    public IEnumerator HitMotionCooldown()
    {
        yield return new WaitForSeconds(2.5f);
    }
}
