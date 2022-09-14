using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tagging : MonoBehaviour
{
    private ChildrenTag childrenAI;
    private ChildrenTag cacheChild;

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
        cacheChild = other.GetComponent<ChildrenTag>();
        //PlayerTag playerTag = other.GetComponent<PlayerTag>();

        if (cacheChild != null) //other AI
        {
            if (childrenAI.isTag == true)
            {
                if (cacheChild.isTag == false)
                {
                    updateTag(cacheChild);

                    updateCollider(cacheChild);
                    //updateCollider(collidedChildren);
                    //updateCollider(collidedChildren);
                    //StartCoroutine(updateCollider(collidedChildren));
                    //if (childrenAI.isTag == false && collidedChildren.canTag == true)
                    //{
                    //    this.gameObject.SetActive(false);
                    //    collidedChildren.tagCollider.SetActive(true);
                    //    Debug.Log("ActiveCollider");
                    //}

                    Debug.Log("AI Tag");
                    
                }
            }
        }
    }

    public void updateTag(ChildrenTag otherChild)
    {
        otherChild.isTag = true;
        childrenAI.isTag = false;

        otherChild.canTag = false;
        childrenAI.canTag = true;

        otherChild.spriteUpdate();
        childrenAI.spriteUpdate();
    }

    //private void updateCollider(ChildrenTag otherChildren)
    //{
    //    Debug.Log("Swap");
    //    float delay = 1.0f;
    //    float time = 0f;

    //    this.gameObject.SetActive(false);

    //    while (time <= delay && childrenAI.isTag == false)
    //    {
    //        time += 0.1f;
    //        if (time == delay)
    //        {
    //            otherChildren.tagCollider.SetActive(true);
    //            Debug.Log("ActiveCollider");
    //        }
    //    }

    //    //if (childrenAI.isTag == false)
    //    //{
    //    //    //childrenAI.tagCollider.SetActive(true);
    //    //    this.gameObject.SetActive(false);
    //    //    otherChildren.tagCollider.SetActive(true);
    //    //    Debug.Log("ActiveCollider");
    //    //}

    //}

    IEnumerator updateCollider(ChildrenTag otherChild)
    {
        Debug.Log("Swap");

        if (childrenAI.isTag == false)
        {
            //childrenAI.tagCollider.SetActive(true);
            this.gameObject.SetActive(false);
            yield return new WaitForSeconds(1.0f);
            otherChild.tagCollider.SetActive(true);
            Debug.Log("ActiveCollider");


            //yield return new WaitForSeconds(0.5f);
        }
        //yield return null;
    }

}
