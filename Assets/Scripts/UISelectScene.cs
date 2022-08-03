using UnityEngine;
using UnityEngine.SceneManagement;

public class UISelectScene : MonoBehaviour
{
    [SerializeField] private string arSceneName;
    [SerializeField] private string uiSceneName;

    public void LoadARScene()
    {
        SceneManager.LoadScene(arSceneName);
    }

    public void LoadUIScene()
    {
        SceneManager.LoadScene(uiSceneName);
    }
}
