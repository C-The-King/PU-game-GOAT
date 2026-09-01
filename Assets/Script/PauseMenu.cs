using System.Collections;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private float scaleDuration = 0.5f;

    public const float TargetScale = 1f;
    private Coroutine scaleCoroutine;
    private bool isPaused;

    private void Start()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        isPaused = true;

        pauseMenu.SetActive(true);
        Time.timeScale = 0f;

        if (scaleCoroutine != null)
        {
            StopCoroutine(scaleCoroutine);
        }

        scaleCoroutine = StartCoroutine(ScaleMenu(Vector3.zero, Vector3.one * TargetScale));
    }

    private void ResumeGame()
    {
        isPaused = false;

        Time.timeScale = 1f;

        if (scaleCoroutine != null)
        {
            StopCoroutine(scaleCoroutine);
        }

        scaleCoroutine = StartCoroutine(CloseMenu());
    }

    private IEnumerator ScaleMenu(Vector3 startScale, Vector3 endScale)
    {
        float elapsed = 0f;

        pauseMenu.transform.localScale = startScale;

        while (elapsed < scaleDuration)
        {
            float t = elapsed / scaleDuration;
            t = Mathf.SmoothStep(0f, 1f, t);

            pauseMenu.transform.localScale =
                Vector3.Lerp(startScale, endScale, t);

            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        pauseMenu.transform.localScale = endScale;
    }

    private IEnumerator CloseMenu()
    {
        Vector3 startScale = pauseMenu.transform.localScale;
        Vector3 endScale = Vector3.zero;

        float elapsed = 0f;

        while (elapsed < scaleDuration)
        {
            float t = elapsed / scaleDuration;
            t = Mathf.SmoothStep(0f, 1f, t);

            pauseMenu.transform.localScale =
                Vector3.Lerp(startScale, endScale, t);

            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        pauseMenu.transform.localScale = endScale;
        pauseMenu.SetActive(false);
    }
}