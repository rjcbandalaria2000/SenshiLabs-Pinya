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

                    this.gameObject.SetActive(false);

                   

                    Debug.Log("AI Tag");
                    
                }
            }
        }
    }

    public void updateTag(ChildrenTag otherChild)
    {
        otherChild.isTag = true;
        childrenAI.isTag = false;

        otherChild.spriteUpdate();
        childrenAI.spriteUpdate();
    }

   
}
