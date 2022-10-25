using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tagging : MonoBehaviour
{
    private ChildrenTag childrenAI;
    private ChildrenTag cacheChild;

    //private PlayerTag cachePlayer;

    // Start is called before the first frame update
    void Start()
    {
        childrenAI = GetComponentInParent<ChildrenTag>();


        if(childrenAI.isTag == true)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponentInParent<ChildrenTag>())
        {
            cacheChild = other.GetComponent<ChildrenTag>();
        }
        //else if(other.GetComponentInParent<PlayerTag>())
        //{
        //    cachePlayer = other.GetComponent<PlayerTag>();
        //}
    
        if (cacheChild != null) //other AI
        {
            if (childrenAI.isTag == true && cacheChild.isTag == false)
            {
                
                    updateTag(cacheChild);

                    this.gameObject.SetActive(false);
                    Debug.Log("AI Tag");

            }
        }
        //else if(cachePlayer != null)
        //{
        //    if (childrenAI.isTag == true && cachePlayer.isTag == false)
        //    {
        //        updatePlayerTag(cachePlayer);

        //        this.gameObject.SetActive(false);
        //        Debug.Log("AI Tag");
        //    }
        //}
    }

    public void updateTag(ChildrenTag otherChild)
    {
        otherChild.isTag = true;
        childrenAI.isTag = false;

        otherChild.spriteUpdate();
        childrenAI.spriteUpdate();

        otherChild.previousTag = childrenAI.gameObject;
        childrenAI.previousTag = null;

        childrenAI.setTarget();
        StartCoroutine(targetCooldown(otherChild));
    }

    IEnumerator targetCooldown(ChildrenTag otherChild)
    {
        otherChild.speed = 0;
        yield return new WaitForSeconds(1.0f);
        otherChild.setTarget();
    }
    //public void updatePlayerTag(PlayerTag otherPlayer)
    //{
    //    otherPlayer.isTag = true;
    //    childrenAI.isTag = false;

    //    otherPlayer.spriteUpdate();
    //    childrenAI.spriteUpdate();

    //    childrenAI.setTarget();
        
    //}
}
