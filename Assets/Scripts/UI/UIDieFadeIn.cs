using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIDieFadeIn : UIBase
{
    [SerializeField] private TextMeshProUGUI countdownText;
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
        image.CrossFadeAlpha(1f, 2.0f, false);
    }

    private IEnumerator FadeCoroutine()
    {
        FadeInImage();
        countdownText.text = "3";
        yield return new WaitForSeconds(1.0f);
        countdownText.text = "2";
        yield return new WaitForSeconds(1.0f);
        countdownText.text = "1";
        AudioManager.Instance.PlayPortalSoundEffect();//필요시 제거
        yield return new WaitForSeconds(1.2f);
        Destroy(this.gameObject);
    }
}
