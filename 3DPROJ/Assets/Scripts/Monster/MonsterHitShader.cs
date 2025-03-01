using System.Collections;
using UnityEngine;

public class MonsterHitShader : MonoBehaviour
{
    Monster monster;
    Renderer render;
    public bool complate;
    private void Start()
    {
        monster = transform.parent.GetComponent<Monster>();
        render = GetComponent<Renderer>();
        complate = true;
    }
    public IEnumerator HitMotion()
    {
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
        if (monster.isDie == true)
        {
            render.material.color = startColor;
        }
        monster.hitMotionCor = null;
    }

    public IEnumerator HitMotionCooldown()
    {
        yield return new WaitForSeconds(2.5f);
    }
}
