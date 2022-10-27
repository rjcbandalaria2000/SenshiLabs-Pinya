using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagCollider : MonoBehaviour
{
    public AITagMinigame parent;

    [Header("Coroutines")]
    public Coroutine delayAiColliderRoutine;
    public Coroutine delayPlayerAiColliderRoutine;
    
    private void Start()
    {
        parent = this.GetComponentInParent<AITagMinigame>();
        if(parent.isTag == false)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<AITagMinigame>() != null) //AI->AI Tag
        {
            AITagMinigame otherAI = collision.gameObject.GetComponent<AITagMinigame>();

            Debug.Log(parent.gameObject.name + " " + parent.isTag);
            Debug.Log(collision.gameObject.name + " " + collision.gameObject.GetComponent<AITagMinigame>().isTag);
          
            if (parent.isTag == true)
            {
              

                AiTagAi(otherAI);
                delayAiColliderRoutine = StartCoroutine(delayAICollider(otherAI));

                
                //collideRoutine = StartCoroutine(collide(collision));

            }
        }
        else if(collision.gameObject.GetComponent<TagMinigamePlayer>() != null)
        {
            if (parent.isTag == true)
            {
                AiTagPlayer(collision.gameObject.GetComponent<TagMinigamePlayer>());
                delayPlayerAiColliderRoutine = StartCoroutine(delayAiToPlayerCollider(collision.gameObject.GetComponent<TagMinigamePlayer>()));

            }
        }
    }

    public void AiTagAi(AITagMinigame otherAI)
    {
        parent.target = null;
        otherAI.target = null;

        parent.isTag = false;
        otherAI.isTag = true;

        parent.spriteUpdate();
        otherAI.spriteUpdate();

        parent.setTarget();

    }

    public void AiTagPlayer(TagMinigamePlayer otherPlayer)
    {
        parent.target = null;

        parent.isTag = false;
        otherPlayer.isTag = true;

        parent.spriteUpdate();
        otherPlayer.spriteUpdate();

        parent.setTarget();
    }


    IEnumerator delayAICollider(AITagMinigame otherAI)
    {
        otherAI.speed = 0;

        yield return new WaitForSeconds(1.5f);

        otherAI.setTarget();

        otherAI.tagCollider.SetActive(true);

        Debug.Log(this.gameObject.name + " collide " + otherAI.gameObject.name);

        this.gameObject.SetActive(false);
    }

    IEnumerator delayAiToPlayerCollider(TagMinigamePlayer otherPlayer)
    {
       
        yield return new WaitForSeconds(1f);

        otherPlayer.tagCollider.SetActive(true);

        Debug.Log(this.gameObject.name + " collide " + otherPlayer.gameObject.name);

        this.gameObject.SetActive(false);
    }

}
