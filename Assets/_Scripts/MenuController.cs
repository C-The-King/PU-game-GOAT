using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public static MenuController Instance;

    [SerializeField] GameObject winMenu;
    [SerializeField] GameObject loseMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject pauseMenuFirstSelected;
    [SerializeField] GameObject winMenuFirstSelected;
    [SerializeField] GameObject loseMenuFirstSelected;
    [SerializeField] GameObject startMenuFirstSelected;
    //[SerializeField] GameObject closedMenuUnSelected;

    [SerializeField] Image[] stars;
    

    public bool pausedGame;
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        if(loseMenu != null) {
            loseMenu.SetActive(false);
        }
        if (winMenu != null) { 
            winMenu.SetActive(false);
        }
        if (pauseMenu != null) { 
            pauseMenu.SetActive(false);
            pausedGame = pauseMenu.activeSelf;
        }
    }

    public void ShowHidePauseMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PauseGame();
        }
    }

    public void LoadLevel(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
    public void ReloadScene()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseGame()
    {
        pausedGame = !pausedGame;
        //clear selected options
        EventSystem.current.SetSelectedGameObject(null);
        if (pausedGame)
        {
            EventSystem.current.SetSelectedGameObject(pauseMenuFirstSelected);
        }
        //pause and unpause the pause menu
        pauseMenu.SetActive(pausedGame);
    }
    public void WinGame()
    {
        pausedGame = true;
        winMenu.SetActive(true);
        stars[0].gameObject.SetActive(false);
        stars[1].gameObject.SetActive(false);
        stars[2].gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(winMenuFirstSelected);
    }
    public void WinGame(float percentage)
    {
        pausedGame = true;
        winMenu.SetActive(true);
        stars[0].gameObject.SetActive(true);
        stars[1].gameObject.SetActive(true);
        stars[2].gameObject.SetActive(true);
        if (percentage < 0.33)
        {
            stars[0].color = Color.black;
            stars[1].color = Color.black;
            stars[2].color = Color.black;
        }
        else if (percentage < 0.66)
        {
            stars[0].color = Color.white;
            stars[1].color = Color.black;
            stars[2].color = Color.black;
        }else if (percentage < 0.99)
        {
            stars[0].color = Color.white;
            stars[1].color = Color.white;
            stars[2].color = Color.black;
        }else
        {
            stars[0].color = Color.white;
            stars[1].color = Color.white;
            stars[2].color = Color.white;
        }
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(winMenuFirstSelected);
    }
    public void LoseGame()
    {
        pausedGame = true;
        loseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(loseMenuFirstSelected);
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
