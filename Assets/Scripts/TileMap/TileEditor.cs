using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TileEditor : MonoBehaviour {

    protected TileMatrix tileMat;
    public int hauteur;
    public int largeur;
    public Text nbColonnes;
    public Text nbLignes;
    public List<string> MapsNames;
    
    protected string tilesPath = "Sprites/Default/tiles";
    //protected TileMatrix tempTileMat;

    protected bool isPlaying = false;
    protected bool allowMouseInteraction = true;

    /// <summary>
    /// selection et rotation tiles
    /// </summary>
    protected int spriteValOnClick = 1;
    protected int selectedTile = 1;
    protected bool rotation360 = true;
    protected bool rotation90 = false;
    protected int nbRotation = 0;

    protected bool drag = false;

    protected bool rectangularSelection = false;
    protected bool rectFull;
    protected bool rectCorner;
    protected bool rectCopy;
    protected bool copyRectIsSet;

    protected int IOnClick = -1;
    protected int JOnClick = -1;
    protected int IOnRelease = -1;
    protected int JOnRelease = -1;

    protected int IMin = -1;
    protected int JMin = -1;
    protected int IMax = -1;
    protected int JMax = -1;

    protected List<int> copyRectBuffer = null;

    protected float CameraInitSize;
    protected Vector3 CameraInitPos;

    Vector3 oldCam;
    Vector3 newCam;

    /// <summary>
    /// gestion curseur souris
    /// </summary>
    protected GameObject cursorSprite;
    private Vector3 mousePosition;
    public float moveSpeed = 1.0f;

    public void UpdateText()
    {
        nbColonnes.text = "Colonne: " + tileMat.largeur;
        nbLignes.text = "Ligne: " + tileMat.hauteur;
    }

    public TileMatrix tileMatrix
    {
        get
        {
            return tileMat;
        }
    }

    public void MakeLvlFitOnScreen()
    {
        float ScreenSize = Screen.height;
        float HauteurNiveau = tileMat.hauteur;
        Camera.main.orthographicSize = 5;

        if (HauteurNiveau >= 42)
        {
            while (HauteurNiveau >= 42)
            {
                HauteurNiveau -= 8.4f;
                Camera.main.orthographicSize++;
            }
            Camera.main.orthographicSize++;
        }
        else
        {
            while (HauteurNiveau < 42)
            {
                HauteurNiveau += 8.4f;
                Camera.main.orthographicSize--;
            }
            Camera.main.orthographicSize+=2;
        }
    }

    public void CenterCamera()
    {
        if(CameraInitPos != Vector3.zero)
        {
            Camera.main.transform.position = new Vector3(CameraInitPos.x + ((tileMat.largeur - 1) * 0.24f) / 2, CameraInitPos.y - ((tileMat.hauteur - 1) * 0.24f) / 2, CameraInitPos.z);
            MakeLvlFitOnScreen();
        }
        else
        {
            Debug.Log("Camera non initialisé");
        }
        if(tileMat.hauteur == 0 && tileMat.largeur == 0)
        {
            Debug.Log("TileMatrixe non initialisé");
        }
    }

    void printList(List<int> L){
        for(int i = 0; i < L.Count; i++)
        {
            print(i + " : " + L[i]);
        }
    }

    // Use this for initialization
    void Start () {
        CameraInitSize = Camera.main.orthographicSize;
        CameraInitPos = Camera.main.transform.position;
        tileMat = new TileMatrix(hauteur, largeur, transform.position, "Sprites/Default/tiles", 10);
        //tileMat = new TileMatrix("Sprites/Default/tiles", transform.position);
        //loadLvl("test");
        UpdateText();
        initCursor();
        CenterCamera();
    }

    public void initCursor()
    {
        GameObject GO = new GameObject();
        cursorSprite = Instantiate(GO, Vector3.zero, Quaternion.identity) as GameObject;
        cursorSprite.AddComponent<SpriteRenderer>();
        cursorSprite.GetComponent<SpriteRenderer>().sortingOrder = 1;
        Destroy(GO);
        loadSpriteIn(cursorSprite, selectedTile);
    }

    // Update is called once per frame
    void Update () {
        InteractionManager();
    }

    public void createNewMap()
    {
        if (!tileMat.isEmpty())
        {
            tileMat.destroy();
        }
        tileMat = new TileMatrix(hauteur, largeur, transform.position, "Sprites/Default/tiles", 10);
        CenterCamera();
        UpdateText();
        isPlaying = false;
    }

    void loadSpriteIn(GameObject GO, int numSprite)
    {
        GO.GetComponent<SpriteRenderer>().sprite = tileMat.loadSprite(numSprite);

        if (GO.GetComponent<Tile>() != null)
        {
            GO.GetComponent<Tile>().code = numSprite;
        }
    }

    void makeSpriteFollowCursor()
    {
        mousePosition = new Vector2(Input.mousePosition.x + 22, Input.mousePosition.y - 28);
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        cursorSprite.transform.position = Vector2.Lerp(cursorSprite.transform.position, mousePosition, moveSpeed);
        cursorSprite.transform.localScale = new Vector2(Camera.main.orthographicSize/CameraInitSize, Camera.main.orthographicSize / CameraInitSize) *1.5f;
    }

    void InteractionManager()
    {
        if (allowMouseInteraction)
        {
            SelectAssets();

            toolManager();

            if (Input.GetMouseButtonDown(2))
            {
                rotateTile();
            }
        }
        makeSpriteFollowCursor();
    }

    public void toolManager()
    {
        
        if (rectangularSelection || !Input.GetMouseButton(0))
        {
            if (Input.GetMouseButtonDown(1)) {
                oldCam = Camera.main.transform.position;
            }
            if (Input.GetMouseButtonUp(1))
            {
                newCam = Camera.main.transform.position;
                if(oldCam != newCam)
                {
                    drag = true;
                }
                else
                {
                    drag = false;
                }
            }

            if (copyRectIsSet && Input.GetMouseButtonUp(1) && !drag)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

                if (hit)
                {
                    int indI = hit.collider.gameObject.GetComponent<Tile>().i;
                    int indJ = hit.collider.gameObject.GetComponent<Tile>().j;
                    tileMat.drawBufferRectToPos(IMin, IMax, JMin, JMax, indI, indJ, copyRectBuffer);
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

                if (hit)
                {
                    IOnClick = hit.collider.gameObject.GetComponent<Tile>().i;
                    JOnClick = hit.collider.gameObject.GetComponent<Tile>().j;
                }
            }

            if (Input.GetMouseButtonUp(0) && !Input.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

                if (hit)
                {
                    IOnRelease = hit.collider.gameObject.GetComponent<Tile>().i;
                    JOnRelease = hit.collider.gameObject.GetComponent<Tile>().j;

                    if (IOnClick != -1 && JOnClick != -1 && IOnRelease != -1 && JOnRelease != -1)
                    {

                        if (rectFull)
                        {
                            tileMat.drawRectFull(Mathf.Min(IOnClick, IOnRelease), Mathf.Max(IOnClick, IOnRelease), Mathf.Min(JOnClick, JOnRelease), Mathf.Max(JOnClick, JOnRelease), selectedTile);
                        }
                        else if (rectCorner)
                        {
                            tileMat.drawRectCorner(Mathf.Min(IOnClick, IOnRelease), Mathf.Max(IOnClick, IOnRelease), Mathf.Min(JOnClick, JOnRelease), Mathf.Max(JOnClick, JOnRelease), selectedTile);
                        }
                        else if (rectCopy)
                        {
                            copyRectIsSet = true;
                            IMin = Mathf.Min(IOnClick, IOnRelease);
                            IMax = Mathf.Max(IOnClick, IOnRelease);
                            JMin = Mathf.Min(JOnClick, JOnRelease);
                            JMax = Mathf.Max(JOnClick, JOnRelease);
                            copyRectBuffer = tileMat.drawRectToBuffer(IMin, IMax, JMin, JMax);
                        }

                        if (!copyRectIsSet)
                        {
                            IOnClick = -1; JOnClick = -1; IOnRelease = -1; JOnRelease = -1;
                        }
                    }
                }
            }
        }
        else if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

            if (hit && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                loadSpriteIn(hit.collider.gameObject, selectedTile);
            }
        }
    }

    public void rotateTile()
    {
        if (rotation360)
        {
            RotationTile360(spriteValOnClick);
        }
        else if (rotation90)
        {
            RotationTile90(spriteValOnClick);
        }
    }

    public void saveLvl(string mapName)
    {
        if (allowMouseInteraction)
        {
            tileMat.save("Maps/" + mapName + ".map", false);
        }
    }

    public void loadLvl(string mapName)
    {
        if (allowMouseInteraction)
        {
            MapsNames.Insert(0, mapName);
            PlayerPrefs.SetInt("Maps/" + mapName + ".map", 0);
            /*
            print(mapName);
            SceneManager.LoadScene("Victoire");
            */

            tileMat.load("Maps/" + mapName + ".map");
            UpdateText();
            CenterCamera();
        }
    }

    public void addMap(string mapName)
    {
        if (!MapsNames.Contains(mapName))
        {
            MapsNames.Add(mapName);
        }
        else
        {
            MapsNames.Remove(mapName);
        }

        foreach(string s in MapsNames)
        {
            print(s);
        }
        print("-");
    }

    //ici pour jouer avec plusieurs cartes

    public void playLvls(List<string> MapsNames)
    {
        string allMaps = "";

        foreach (string s in MapsNames)
        {
            allMaps += s +".map"+ "/";
        }
        PlayerPrefs.SetInt("num", 0);
        PlayerPrefs.SetString("lien", allMaps);
        SceneManager.LoadScene("Jouer");
    }

    public void playLvl()
    {
		PlayerPrefs.SetString("lien", "Maps/" + MapsNames[0] + ".map");
        print(MapsNames[0]);
        SceneManager.LoadScene("Jouer");
        /*
        if (!isPlaying)
        {
            isPlaying = true;
            allowMouseInteraction = false;
            tileMat.save("playMap.temp", false);
            //pathFindableGrid = new PathFind.Grid(tileMat.largeur, tileMat.hauteur, tileMat.TileMatrixToPathFindableMatrix());

            for (int i = 0; i < tileMat.hauteur; i++)
            {
                for (int j = 0; j < tileMat.largeur; j++)
                {
                    if (tileMat.getTileCodeAt(i, j) == 0 || tileMat.getTileCodeAt(i, j) == 10)
                    {
                        Destroy(tileMat[i, j]);
                    }

                    else if (tileMat.getTileCodeAt(i, j) == 36)
                    {
                        Vector3 pos = tileMat[i, j].GetComponent<Transform>().position;
                        Destroy(tileMat[i, j]);
                        tileMat[i, j] = Instantiate(Resources.Load("Prefabs/Pacman1"), pos, Quaternion.identity) as GameObject;
                    }

                    else if(tileMat.getTileCodeAt(i, j) == 37)
                    {
                        Vector3 pos = tileMat[i, j].GetComponent<Transform>().position;
                        Destroy(tileMat[i, j]);
                        tileMat[i, j] = Instantiate(Resources.Load("Prefabs/Blinky"), pos, Quaternion.identity) as GameObject;
                    }

                    else if(tileMat.getTileCodeAt(i, j) == 38)
                    {
                        Vector3 pos = tileMat[i, j].GetComponent<Transform>().position;
                        Destroy(tileMat[i, j]);
                        tileMat[i, j] = Instantiate(Resources.Load("Prefabs/Inky"), pos, Quaternion.identity) as GameObject;
                    }

                    else if(tileMat.getTileCodeAt(i, j) == 39)
                    {
                        Vector3 pos = tileMat[i, j].GetComponent<Transform>().position;
                        Destroy(tileMat[i, j]);
                        tileMat[i, j] = Instantiate(Resources.Load("Prefabs/Pinky"), pos, Quaternion.identity) as GameObject;
                    }

                    else if(tileMat.getTileCodeAt(i, j) == 40)
                    {
                        Vector3 pos = tileMat[i, j].GetComponent<Transform>().position;
                        Destroy(tileMat[i, j]);
                        tileMat[i, j] = Instantiate(Resources.Load("Prefabs/Clyde"), pos, Quaternion.identity) as GameObject;
                    }
                    
                }
            }
        }
        else
        {
            tileMat.load("playMap.temp");
            CenterCamera();
            allowMouseInteraction = true;
            isPlaying = false;
            File.Delete("playMap.temp");
        }
        */
    }

    public void AddLine()
    {
        if (allowMouseInteraction)
        {
            tileMat.AddLine(10);
            UpdateText();
        }
    }

    public void RemoveRow()
    {
        if (allowMouseInteraction && tileMat.hauteur > 0)
        {
            tileMat.removeLigneAt(tileMat.hauteur - 1);
            UpdateText();
        }
    }

    public void AddColumn()
    {
        if (allowMouseInteraction)
        {
            tileMat.AddColumn(10);
            UpdateText();
        }
    }

    public void RemoveColumn()
    {
        if (allowMouseInteraction && tileMat.largeur > 0)
        {
            tileMat.removeColonneAt(tileMat.largeur - 1);
            UpdateText();
        }
    }

    public void toggleMonoTileTool()
    {
        rectangularSelection = false;
        rectFull = false;
        rectCorner = false;
        rectCopy = false;
    }

    public void toggleDrawRectFullTool()
    {
        rectangularSelection = true;
        rectFull = true;
        rectCorner = false;
        rectCopy = false;
    }

    public void toggleDrawRectCornerTool()
    {
        rectangularSelection = true;
        rectFull = false;
        rectCorner = true;
        rectCopy = false;
    }

    public void toggleCopySelector()
    {
        rectangularSelection = true;
        rectFull = false;
        rectCorner = false;
        rectCopy = true;
    }

    void RotationTile360(int numTile)
    {
        nbRotation++;
        selectedTile = numTile + (nbRotation % 4);
        loadSpriteIn(cursorSprite, selectedTile);
    }

    void RotationTile90(int numTile)
    {
        nbRotation++;
        selectedTile = numTile + (nbRotation % 2);
        loadSpriteIn(cursorSprite, selectedTile);
    }

    public void SelectEmpty()
    {
        spriteValOnClick = 10;
        rotation360 = false;
        rotation90 = false;
        selectedTile = spriteValOnClick;
        nbRotation = 0;
        cursorSprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Interface/delete_low_pix");
    }

    public void SelectAngle1()
    {
        spriteValOnClick = 1;
        rotation360 = true;
        rotation90 = false;
        selectedTile = spriteValOnClick;
        nbRotation = 0;
        loadSpriteIn(cursorSprite, spriteValOnClick);
    }

    public void SelectAngle2()
    {
        spriteValOnClick = 11;
        rotation360 = true;
        rotation90 = false;
        selectedTile = spriteValOnClick;
        nbRotation = 0;
        loadSpriteIn(cursorSprite, spriteValOnClick);
    }

    public void SelectAngle3()
    {
        spriteValOnClick = 21;
        rotation360 = true;
        rotation90 = false;
        selectedTile = spriteValOnClick;
        nbRotation = 0;
        loadSpriteIn(cursorSprite, spriteValOnClick);
    }

    public void SelectAngle4()
    {
        spriteValOnClick = 29;
        rotation360 = true;
        rotation90 = false;
        selectedTile = spriteValOnClick;
        nbRotation = 0;
        loadSpriteIn(cursorSprite, spriteValOnClick);
    }

    public void SelectMur1()
    {
        spriteValOnClick = 5;
        rotation360 = false;
        rotation90 = true;
        selectedTile = spriteValOnClick;
        nbRotation = 0;
        loadSpriteIn(cursorSprite, spriteValOnClick);
    }

    public void SelectMur2()
    {
        spriteValOnClick = 15;
        rotation360 = true;
        rotation90 = false;
        selectedTile = spriteValOnClick;
        nbRotation = 0;
        loadSpriteIn(cursorSprite, spriteValOnClick);
    }

    public void SelectMur3()
    {
        spriteValOnClick = 25;
        rotation360 = true;
        rotation90 = false;
        selectedTile = spriteValOnClick;
        nbRotation = 0;
        loadSpriteIn(cursorSprite, spriteValOnClick);
    }

    public void SelectBlock1()
    {
        spriteValOnClick = 9;
        rotation360 = false;
        rotation90 = false;
        selectedTile = spriteValOnClick;
        nbRotation = 0;
        loadSpriteIn(cursorSprite, spriteValOnClick);
    }

    public void SelectBlock2()
    {
        spriteValOnClick = 19;
        rotation360 = false;
        rotation90 = false;
        selectedTile = spriteValOnClick;
        nbRotation = 0;
        loadSpriteIn(cursorSprite, spriteValOnClick);
    }

    public void SelectBlock3()
    {
        spriteValOnClick = 20;
        rotation360 = false;
        rotation90 = false;
        selectedTile = spriteValOnClick;
        nbRotation = 0;
        loadSpriteIn(cursorSprite, spriteValOnClick);
    }

    public void SelectPacman()
    {
        spriteValOnClick = 36;
        rotation360 = false;
        rotation90 = false;
        selectedTile = spriteValOnClick;
        nbRotation = 0;
        loadSpriteIn(cursorSprite, spriteValOnClick);
    }

    public void SelectBlinky()
    {
        spriteValOnClick = 37;
        rotation360 = false;
        rotation90 = false;
        selectedTile = spriteValOnClick;
        nbRotation = 0;
        loadSpriteIn(cursorSprite, spriteValOnClick);
    }

    public void SelectInky()
    {
        spriteValOnClick = 38;
        rotation360 = false;
        rotation90 = false;
        selectedTile = spriteValOnClick;
        nbRotation = 0;
        loadSpriteIn(cursorSprite, spriteValOnClick);
    }

    public void SelectPinky()
    {
        spriteValOnClick = 39;
        rotation360 = false;
        rotation90 = false;
        selectedTile = spriteValOnClick;
        nbRotation = 0;
        loadSpriteIn(cursorSprite, spriteValOnClick);
    }

    public void SelectClyde()
    {
        spriteValOnClick = 40;
        rotation360 = false;
        rotation90 = false;
        selectedTile = spriteValOnClick;
        nbRotation = 0;
        loadSpriteIn(cursorSprite, spriteValOnClick);
    }

    public void SelectPower1()
    {
        spriteValOnClick = 35;
        rotation360 = false;
        rotation90 = false;
        selectedTile = spriteValOnClick;
        nbRotation = 0;
        loadSpriteIn(cursorSprite, spriteValOnClick);
    }

    public void SelectBonus()
    {
        spriteValOnClick = 41;
        rotation360 = false;
        rotation90 = false;
        selectedTile = spriteValOnClick;
        nbRotation = 0;
        loadSpriteIn(cursorSprite, spriteValOnClick);
    }

    public void SelectPorte()
    {
        spriteValOnClick = 7;
        rotation360 = false;
        rotation90 = true;
        selectedTile = spriteValOnClick;
        nbRotation = 0;
        loadSpriteIn(cursorSprite, spriteValOnClick);
    }

    bool SelectAssets()
    {
        bool selectionDone = false;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectAngle1();
            selectionDone = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectMur1();
            selectionDone = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectAngle2();
            selectionDone = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectMur2();
            selectionDone = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectAngle3();
            selectionDone = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SelectMur3();
            selectionDone = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SelectAngle4();
            selectionDone = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SelectBlock1();
            selectionDone = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SelectBlock2();
            selectionDone = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SelectBlock3();
            selectionDone = true;
        }
        return selectionDone;
    }
}