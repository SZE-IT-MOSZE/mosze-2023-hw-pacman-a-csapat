using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public int mainMenuScene = 0;
    public int cityScene = 1;
    public int nextScene;
    public RectTransform topRightUiWrapper;
    public RectTransform bottomLeftUiWrapper;
    public RectTransform gameOverPanel;
    public CanvasGroup overlay;
    public float transitionDuration = 1.3f;

    void Start() {
        
    }

    IEnumerator AsyncLoadScene(int scene, bool destroyGameManager) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        operation.allowSceneActivation = false;

        DOTween.KillAll();

        while (!operation.isDone) {
            if (operation.progress >= 0.9f) {
                if(destroyGameManager) {
                    GameObject gameManager = GameObject.Find("GameManager");

                    if (gameManager) {
                        Destroy(gameManager);
                    }
                }
                Time.timeScale = 1;
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    public IEnumerator AnimateUIElements(bool isGameOver) {
        topRightUiWrapper.DOLocalMoveY(120f, transitionDuration).SetEase(Ease.OutQuad).SetRelative(true).SetUpdate(true);
        bottomLeftUiWrapper.DOLocalMoveY(-120f, transitionDuration).SetEase(Ease.OutQuad).SetRelative(true).SetUpdate(true);
        overlay.DOFade(.6f, transitionDuration).SetUpdate(true);

        yield return new WaitForSecondsRealtime(transitionDuration);

        if (isGameOver) { 
            gameOverPanel.parent.gameObject.SetActive(true);
            gameOverPanel.parent.DOMoveY(Screen.height / 2, transitionDuration).SetUpdate(true);
        } else {
            StartCoroutine(AsyncLoadScene(nextScene, false));
        }

        yield return null;
    }

    public void ReturnToMainMenu() {
        StartCoroutine(AsyncLoadScene(mainMenuScene, true));
    }

    public void RestartGame() {
        StartCoroutine(AsyncLoadScene(cityScene, true));
    }

    public void ExitGame() {
        Application.Quit();
    }

    void Update() {

    }
}
