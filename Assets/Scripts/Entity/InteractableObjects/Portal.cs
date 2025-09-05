using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private SceneType sceneType;
    private Coroutine _coroutine;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            SceneChange();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    private void SceneChange()
    {
        _coroutine = StartCoroutine(nameof(SceneChangeProcess));
    }

    private IEnumerator SceneChangeProcess()
    {
        yield return new WaitForSeconds(1f);
        AudioManager.Instance.PlayPortalSoundEffect();
        UIManager.Instance.CreateUIDontDestroy<UIFadeIn>();
        yield return new WaitForSeconds(1.0f);
        SceneLoadManager.Instance.LoadScene(sceneType);
    }
}
