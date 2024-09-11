using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Stage1() {
        SceneManager.LoadScene("Stage1Scene");
    }

    public void Stage2() {
        SceneManager.LoadScene("Stage2Scene");
    }

    public void Stage3() {
        SceneManager.LoadScene("Stage3Scene");
    }

    public void Select() {
        SceneManager.LoadScene("SelectScene");
    }

    public void Clear() {
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);//1‚ÍŸ—˜‚ÌØ
        SceneManager.LoadScene("SelectScene");
    }

    public void Title() {
        SceneManager.LoadScene("StartScene");
    }
}
