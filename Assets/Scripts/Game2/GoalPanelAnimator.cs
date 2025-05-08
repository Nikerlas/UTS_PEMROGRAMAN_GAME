using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GoalPanelAnimator : MonoBehaviour
{
    public RectTransform panelTransform;
    public CanvasGroup canvasGroup;

    public Vector2 startPosition;
    public Vector2 endPosition;
    public float animationDuration = 1f;
    public float stayDuration = 1.5f;

    public float duration = 1f;

    public void PlayGoalAnimation(bool fromLeft)
    {
        Vector2 startPos = new Vector2(fromLeft ? -800 : 800, 0);
        Vector2 endPos = Vector2.zero;

        StopAllCoroutines();
        StartCoroutine(AnimatePanel(startPos, endPos));
    }

    IEnumerator AnimatePanel(Vector2 startPos, Vector2 endPos)
    {
        float timer = 0f;
        canvasGroup.alpha = 0f;
        panelTransform.anchoredPosition = startPos;

        while (timer < animationDuration)
        {
            timer += Time.deltaTime;
            float t = timer / animationDuration;

            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t);
            panelTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }

        yield return new WaitForSeconds(stayDuration);

        timer = 0f;
        Vector2 offScreen = new Vector2(endPos.x + (endPos.x >= 0 ? 500 : -500), endPos.y);

        while (timer < animationDuration)
        {
            timer += Time.deltaTime;
            float t = timer / animationDuration;

            canvasGroup.alpha = Mathf.Lerp(1f, 0f, t);
            panelTransform.anchoredPosition = Vector2.Lerp(endPos, offScreen, t);
            yield return null;
        }

        canvasGroup.alpha = 0f;
    }
}
