using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthVisualizer : MonoBehaviour
{
    public static HealthVisualizer Instance;
    [SerializeField] List<Image> hpIconList;
    [SerializeField] Image healthIcon;
    int livesLeft;
    public int maxLives = 3;
    [SerializeField] float loseGameDelay = 1.2f;
    [SerializeField] Image deathAnimator;
    [SerializeField] float deathAnimationSpeedinSec = 0.25f;
    private void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        deathAnimationSpeedinSec *= Time.fixedDeltaTime;
        ResetHealth();
    }

    public void ResetHealth()
    {
        livesLeft = maxLives;
        for (int i = 0; i < livesLeft; i++)
        {
            hpIconList.Add(Instantiate(healthIcon, transform));
            //heartIcons[i].color = Color.red;
        }
    }
    public bool LoseLife()
    {
        livesLeft--;
        if (livesLeft >= 0) { 
            Destroy(hpIconList[livesLeft].gameObject);
            hpIconList.RemoveAt(livesLeft);
            //heartIcons[livesLeft].color = Color.black;
            if (deathAnimator != null)
            {
                deathAnimator.color = new Color(0,0,0,1);
            }
        }
        if (livesLeft <= 0)
        {
            StartCoroutine(LoseGameDelay(loseGameDelay));
            return false;
        }
        return true;
    }

    IEnumerator LoseGameDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        MenuController.Instance.LoseGame();
    }
    private void FixedUpdate()
    {
        if(deathAnimator != null && deathAnimator.color.a > 0)
        {
            deathAnimator.color = new Color(0, 0, 0, Mathf.Max(0, deathAnimator.color.a - deathAnimationSpeedinSec));
        }
    }
}
