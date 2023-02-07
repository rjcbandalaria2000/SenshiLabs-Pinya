using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMinigame : MonoBehaviour
{
    public PlayerAI AIPlayer;
    public int waypointIndex;

    public GameObject hoverEffect;
    //public SpriteRenderer objSprite;
    //public Sprite unHoverSprite;
    //public Sprite hoverSprite;

    // Start is called before the first frame update
    void Start()
    {
        AIPlayer = GameObject.FindObjectOfType<PlayerAI>();
        if(hoverEffect != null )
        {
            this.hoverEffect.SetActive(false);
        }
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }


     void OnMouseDown()
    {
      // AIPlayer.goToTarget(waypointIndex);
       StartCoroutine(AIPlayer.goToTargetRoutine(waypointIndex));
        Debug.Log("Go To Target");
    }

    private void OnMouseOver()
    {
        //change sprite
        if(hoverEffect != null)
        {
            this.hoverEffect.SetActive(true);
        }

        Debug.Log("Mouse Over Object;");
    }
}
