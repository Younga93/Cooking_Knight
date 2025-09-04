using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class UIFadeOut : UIBase
{
    [SerializeField] private Image image;
    private void Start()
    {
        image.canvasRenderer.SetAlpha(1f);
        var c = image.color; 
        c.a = 1f;
        image.color = c;
        StartCoroutine(FadeCoroutine());
    }
    
    private void FadeOutImage()
    {
        image.CrossFadeAlpha(0f, 1.0f, false);
    }

    private IEnumerator FadeCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        FadeOutImage();
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }
}
