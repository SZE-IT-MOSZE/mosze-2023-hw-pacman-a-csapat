using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using EasyProgressBar;

/// <summary>
/// A főmenü vezérlője, amely felelős a főmenü UI és játékmenet közötti átmenetekért.
/// </summary>
public class MainMenuController : MonoBehaviour {
    /// <summary>
    /// A következő játékmeneti szint azonosítója.
    /// </summary>
    public int nextScene;

    /// <summary>
    /// A játék gombja.
    /// </summary>
    public Button playButton;

    /// <summary>
    /// A kilépés gombja.
    /// </summary>
    public Button exitButton;

    /// <summary>
    /// A logóhoz tartozó RectTransform.
    /// </summary>
    public RectTransform logo;

    /// <summary>
    /// A betöltési folyamatot kijelző haladássáv.
    /// </summary>
    public ProgressBar progressBar;

    /// <summary>
    /// Az átmenet ideje.
    /// </summary>
    public float transitionDuration = 1.6f;

    /// <summary>
    /// Az osztály példányosításakor hívott metódus.
    /// </summary>
    void Start() {
        // Esetleges inicializációk itt
    }

    /// <summary>
    /// Játékmenet betöltését végző folyamatot kezdeményező metódus.
    /// </summary>
    IEnumerator LoadScene() {
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);
        operation.allowSceneActivation = false;
        float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

        while (progressValue < .8f) {
            progressValue += .01f;
            progressBar._fillAmount = progressValue;

            yield return new WaitForSeconds(.01f);
        }

        while (!operation.isDone) {
            progressBar._fillAmount = progressValue - .8f;

            if (operation.progress >= 0.9f) {
                progressBar._fillAmount = 1f;

                yield return new WaitForSeconds(.4f);

                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    /// <summary>
    /// UI elemek elmozdítását és játékmenet betöltését végző folyamatot kezdeményező metódus.
    /// </summary>
    IEnumerator MoveOutUIElements() {
        RectTransform playButtonRect = playButton.GetComponent<RectTransform>();
        playButtonRect.DOMoveX(100f, transitionDuration).SetEase(Ease.OutQuad).SetRelative(true);

        RectTransform exitButtonRect = exitButton.GetComponent<RectTransform>();
        exitButtonRect.DOMoveX(100f, transitionDuration).SetEase(Ease.OutQuad).SetRelative(true);

        logo.DOMoveY(90f, transitionDuration).SetEase(Ease.OutQuad).SetRelative(true);

        yield return new WaitForSeconds(transitionDuration - .6f);

        progressBar.GetComponent<RectTransform>().DOMoveY(-105f, transitionDuration - .3f).SetEase(Ease.OutQuad).SetRelative(true);

        yield return new WaitForSeconds(transitionDuration - .3f);

        StartCoroutine(LoadScene());
    }

    /// <summary>
    /// Új játék indítását kezdeményező metódus.
    /// </summary>
    public void NewGame() {
        StartCoroutine(MoveOutUIElements());
    }

    /// <summary>
    /// Kilépést kezdeményező metódus.
    /// </summary>
    public void Quit() {
        Application.Quit();
    }
}
