using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FinishScene()
    {
        SceneManager.LoadScene("title");
    }
    public void RetryScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
