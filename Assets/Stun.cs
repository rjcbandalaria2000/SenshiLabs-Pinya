using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : MonoBehaviour
{
    public float        Duration;
    public GameObject   Parent;

    private Coroutine   stunEffectRoutine;
    private MouseFollow mouseFollow;
    // Start is called before the first frame update
    void Start()
    {
        stunEffectRoutine = null;
        if (Parent)
        {
            mouseFollow = Parent.GetComponent<MouseFollow>();  
        }
    }

    IEnumerator StunUnit()
    {
        if(mouseFollow)
        {
            Debug.Log("Cannot move");
            mouseFollow.canMove = false;
            yield return new WaitForSeconds(Duration);
            mouseFollow.canMove = true;
            Destroy(this);
        }
        
    }

    public void StartEffect()
    {   
        mouseFollow = Parent.GetComponent<MouseFollow>();
        stunEffectRoutine = StartCoroutine(StunUnit());
        
        Debug.Log("Stunned");
    }
}
