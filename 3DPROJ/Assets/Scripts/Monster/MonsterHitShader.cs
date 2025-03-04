using System.Collections;
using UnityEngine;

public class MonsterHitShader : MonoBehaviour
{
    Monster monster;
    Renderer render;
    public bool complate;
    Color startColor;
    Color hitColor;
    private void Start()
    {
        monster = transform.parent.GetComponent<Monster>();
        render = GetComponent<Renderer>();
        complate = true;
        startColor = render.material.color;  // 원래 색상 저장
        hitColor = Color.red;                // 타격 시 빨간색
    }
    public IEnumerator HitMotion()
    {
        float duration = 1f;                        // 빨갛게 변하는 시간
        float elapsedTime = 0f;
        render.material.color = hitColor;
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            render.material.color = Color.Lerp(hitColor, startColor, t);
            yield return null;
        }
            monster.hitMotionCor = null;
    }

    public void ResetColor()
    {
        if (monster.isDie == true)
        {
            render.material.color = startColor;
            monster.hitMotionCor = null;
        }
    }

    public IEnumerator HitMotionCooldown()
    {
        yield return new WaitForSeconds(2.5f);
    }
}
