using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour{

    public int code;
    public int i;
    public int j;

    public void move(int x, int y) {

        float posX = transform.position.x;
        float posY = transform.position.y;

        float largeur = GetComponent<SpriteRenderer>().sprite.textureRect.width / 100;
        float hauteur = GetComponent<SpriteRenderer>().sprite.textureRect.height / 100;

        transform.position = new Vector2(posX + (x * largeur), posY + (y * hauteur));
        i += -y;
        j += x;
    }
}
