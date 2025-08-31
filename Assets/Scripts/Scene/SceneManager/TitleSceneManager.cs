using UnityEngine;
using UnityEngine.UI;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField] private Button startButton;

    private void Start()
    {
        startButton.onClick.AddListener(OnClickStartButton);
    }
    
    public void OnClickStartButton()
    {
        Debug.Log("게임 시작 버튼 클릭함");
        SceneLoadManager.Instance.LoadScene(SceneType.BaseCamp);
    }
}
