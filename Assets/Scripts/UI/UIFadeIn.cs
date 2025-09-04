using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFadeIn : UIBase
{
    [SerializeField] private Image image;

    private void Start()
    {
        DontDestroyOnLoad(transform.root.gameObject);
        image.canvasRenderer.SetAlpha(0f);
        var c = image.color; 
        c.a = 1f;
        image.color = c;
        StartCoroutine(FadeCoroutine());
    }
    
    private void FadeInImage()
    {
        image.CrossFadeAlpha(1f, 1.0f, false);
    }

    private IEnumerator FadeCoroutine()
    {
        FadeInImage();
        yield return new WaitForSeconds(1.2f);
        Destroy(this.gameObject);
    }
}
