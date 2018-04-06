using System.Collections;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    public void FadeMe()
    {
        StartCoroutine(DoFade());
    }

    IEnumerator DoFade()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>(); 

        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime / 2;
            yield return null;
        }

        canvasGroup.interactable = false;
        this.gameObject.SetActive(false);
        yield return null;
    }

}

