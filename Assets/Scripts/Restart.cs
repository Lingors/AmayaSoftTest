using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public GameObject loadingScreen;

    private void OnMouseDown()
    {
        loadingScreen.GetComponent<SpriteRenderer>().DOFade(1, 1);
        Invoke(nameof(LoadGame), 1.5f);
    }

    private void LoadGame()
    {
        SceneManager.LoadSceneAsync("Game");
    }
}