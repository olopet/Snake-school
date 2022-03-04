using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    
    public GameObject pauseText;

    public GameObject prefabBoost;
    public GameObject prefabDebuff;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        StartCoroutine("Timer");
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseText.SetActive(true);

    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseText.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 0;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator Timer()
    {
        while (true) { 
        yield return new WaitForSeconds(10.0f);
        Instantiate(prefabBoost, new Vector3(0, 0, 0), Quaternion.identity);
        yield return new WaitForSeconds(5.0f);
        Instantiate(prefabDebuff, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
 
  
}



