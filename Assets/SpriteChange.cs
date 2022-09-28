using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChange : MonoBehaviour
{
    [Header("Sprites")]
    public GameObject model;
    public List<Sprite> toySprites = new();

    private int spriteIndex;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        if(model == null)
        {
            model = this.gameObject;
        }
        spriteRenderer = model.GetComponent<SpriteRenderer>();
        ChangeSprite();
        
    }

    public void ChangeSprite()
    {
        if(toySprites.Count <= 0) { return; }
        spriteIndex = Random.Range(0, toySprites.Count);
        if(spriteRenderer == null) { return; }
        spriteRenderer.sprite = toySprites[spriteIndex];
    }

    

}
