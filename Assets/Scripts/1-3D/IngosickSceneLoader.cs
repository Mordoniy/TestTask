using UnityEngine;
using UnityEngine.SceneManagement;

public class IngosickSceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneName;
    
    private void Start()
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }
}
