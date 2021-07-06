using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MenuPause : MonoBehaviour
{
    public bool activerTouches;
    private bool isPaused = false; // Permet de savoir si le jeu est en pause ou non.
    private Canvas CanvasObject;

    void Start()
    {
        CanvasObject = GetComponent<Canvas>();
		CanvasObject.enabled = false;
        isPaused = false;
    }
 
    void Update()
    {
        // Si le joueur appuis sur Echap alors la valeur de isPaused devient le contraire.
        if (Input.GetKeyDown(KeyCode.Escape) && activerTouches)
        {
            isPaused = !isPaused;
            CanvasObject.enabled = !CanvasObject.enabled;
        }
        if (isPaused)
            Time.timeScale = 0; // Le temps s'arrete

        else
            Time.timeScale = 1; // Le temps reprend
            
    }

    public void ChangeState()
    {
        CanvasObject.enabled = !CanvasObject.enabled;

        if (!CanvasObject.enabled)
            isPaused = false;
        else
            isPaused = true;
    }

    public void CloseIfOpen()
    {
        if(CanvasObject.enabled)
            CanvasObject.enabled = !CanvasObject.enabled;

        if (!CanvasObject.enabled)
            isPaused = false;
        else
            isPaused = true;
    }

    public void OpenIfClose()
    {
        if (!CanvasObject.enabled)
            CanvasObject.enabled = !CanvasObject.enabled;

        if (!CanvasObject.enabled)
            isPaused = false;
        else
            isPaused = true;
    }
}
