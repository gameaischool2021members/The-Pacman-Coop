using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneOnClickQuit : MonoBehaviour
{

    public void LoadByIndex(int sceneIndex)
    {	
		Destroy(GameObject.Find ("Main Camera").GetComponent<FlareLayer>());
		// Destroy(GameObject.Find ("Main Camera").GetComponent<GUILayer>());
		Destroy(GameObject.Find ("Main Camera").GetComponent<Animator>());
		Destroy(GameObject.Find ("Main Camera").GetComponent<Camera>());
		GameObject.Find ("Main Camera").name = "Son";
		DontDestroyOnLoad(GameObject.Find ("Son"));
        PlayerPrefs.SetInt("num", 0);
		PlayerPrefs.SetString ("lien", "");
        SceneManager.LoadScene(sceneIndex);
    }
}