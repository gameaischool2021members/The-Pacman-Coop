using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionBox : MonoBehaviour {

    public GUIStyle selectTexture;
    private Vector2 orgBoxPos = Vector2.zero;
    private Vector2 endBoxPos = Vector2.zero;
    private bool activateBox = false;
    private bool activateCopyBox = false;

    // Use this for initialization
    void Start () {
		
	}
    /// <summary>
    /// Handles the case where the user draws a rectangle to select some units.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            orgBoxPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            endBoxPos = new Vector2(Input.mousePosition.x+1, Input.mousePosition.y-3);
        }
        else
        {
            if (!activateCopyBox)
            {
                orgBoxPos = Vector2.zero;
                endBoxPos = Vector2.zero;
            }
        }
    }
    /// <summary>
    /// Draws the selection rectangle if the user is holding the mouse down.
    /// </summary>
    void OnGUI()
    {
        if (activateBox)
        {
            if (orgBoxPos != Vector2.zero && endBoxPos != Vector2.zero)
            {
                GUI.Box(new Rect(orgBoxPos.x, Screen.height - orgBoxPos.y, endBoxPos.x - orgBoxPos.x, -1 * ((Screen.height - orgBoxPos.y) - (Screen.height - endBoxPos.y))), "", selectTexture); // -
            }
        }
    }

    public void setActive(bool active)
    {
        activateBox = active;
        activateCopyBox = false;
    }

    public void setActiveWhenRealease(bool active)
    {
        activateBox = active;
        activateCopyBox = active;
    }
}