using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneOnClickCont : MonoBehaviour
{

    public void LoadByIndex(int sceneIndex)
    {
        Destroy(GameObject.Find("Main Camera").GetComponent<FlareLayer>());
        // Destroy(GameObject.Find ("Main Camera").GetComponent<GUILayer>());
        Destroy(GameObject.Find("Main Camera").GetComponent<Animator>());
        Destroy(GameObject.Find("Main Camera").GetComponent<Camera>());

        GameObject.Find("Main Camera").name = "Sound";
        DontDestroyOnLoad(GameObject.Find("Sound"));
        SceneManager.LoadScene(sceneIndex);
    }
}