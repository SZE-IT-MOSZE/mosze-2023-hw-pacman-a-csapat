using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// A jelenetek kezel�s��rt felel�s oszt�ly.
/// </summary>
public class SceneController : MonoBehaviour {
    /// <summary>
    /// A f�men� jelenet sorsz�ma.
    /// </summary>
    public int mainMenuScene = 0;

    /// <summary>
    /// A v�ros jelenet sorsz�ma.
    /// </summary>
    public int cityScene = 1;

    /// <summary>
    /// A k�vetkez� jelenet sorsz�ma.
    /// </summary>
    public int nextScene;

    /// <summary>
    /// A jobb fels� UI wrapper RectTransform-je.
    /// </summary>
    public RectTransform topRightUiWrapper;

    /// <summary>
    /// A bal als� UI wrapper RectTransform-je.
    /// </summary>
    public RectTransform bottomLeftUiWrapper;

    /// <summary>
    /// A j�t�k v�ge panel RectTransform-je.
    /// </summary>
    public RectTransform gameOverPanel;

    /// <summary>
    /// Az �tmeneti fed� CanvasGroup.
    /// </summary>
    public CanvasGroup overlay;

    /// <summary>
    /// Az �tmenet id�tartama m�sodpercben.
    /// </summary>
    public float transitionDuration = 1.3f;

    void Start() {
        // Start met�dus jelenleg �res.
    }

    /// <summary>
    /// Aszinkron jelenet bet�lt�s.
    /// </summary>
    /// <param name="scene">Bet�ltend� jelenet sorsz�ma.</param>
    /// <param name="destroyGameManager">True, ha a GameManager objektumot t�r�lni kell.</param>
    /// <returns>IEnumerator, amely a folyamatot vez�rli.</returns>
    IEnumerator AsyncLoadScene(int scene, bool destroyGameManager) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        operation.allowSceneActivation = false;

        DOTween.KillAll();

        while (!operation.isDone) {
            if (operation.progress >= 0.9f) {
                if (destroyGameManager) {
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

    /// <summary>
    /// UI elemek anim�l�sa �tmenetkor.
    /// </summary>
    /// <param name="isGameOver">True, ha a j�t�k v�ge �llapotban vagyunk.</param>
    /// <returns>IEnumerator, amely a folyamatot vez�rli.</returns>
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

    /// <summary>
    /// Visszat�r�s a f�men�be.
    /// </summary>
    public void ReturnToMainMenu() {
        StartCoroutine(AsyncLoadScene(mainMenuScene, true));
    }

    /// <summary>
    /// J�t�k �jraind�t�sa.
    /// </summary>
    public void RestartGame() {
        StartCoroutine(AsyncLoadScene(cityScene, true));
    }

    /// <summary>
    /// Kil�p�s a j�t�kb�l.
    /// </summary>
    public void ExitGame() {
        Application.Quit();
    }

    void Update() {
        // Update met�dus jelenleg �res.
    }
}
