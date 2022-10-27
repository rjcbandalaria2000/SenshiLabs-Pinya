using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTagCollider : MonoBehaviour
{
    public TagMinigamePlayer parent;

    public Coroutine delayPlayerToAiColliderRoutine;
    // Start is called before the first frame update
    void Start()
    {
        parent = this.GetComponentInParent<TagMinigamePlayer>();
        if (parent.isTag == false)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<AITagMinigame>() != null)
        {
            if (parent.isTag == true)
            {
                playerTagAi(collision.gameObject.GetComponent<AITagMinigame>());
            }
        }
    }

    public void playerTagAi(AITagMinigame otherAI)
    {
        otherAI.target = null;

        parent.isTag = false;
        otherAI.isTag = true;

        parent.spriteUpdate();
        otherAI.spriteUpdate();

        delayPlayerToAiColliderRoutine = StartCoroutine(delayPlayerToAiCollider(otherAI));
    }

    IEnumerator delayPlayerToAiCollider(AITagMinigame otherAI)
    {
        otherAI.speed = 0;

        yield return new WaitForSeconds(1.5f);
        otherAI.setTarget();

        otherAI.tagCollider.SetActive(true);

        Debug.Log(this.gameObject.name + " collide " + otherAI.gameObject.name);

        this.gameObject.SetActive(false);
    }
}
