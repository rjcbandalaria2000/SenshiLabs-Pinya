using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Stun : MonoBehaviour
{
    public float        Duration;
    public GameObject   Parent;

    private Coroutine   stunEffectRoutine;
    private MouseFollow mouseFollow;
    private Catcher     catcher;
    // Start is called before the first frame update
    void Start()
    {
        stunEffectRoutine = null;
        if (Parent)
        {
            mouseFollow = Parent.GetComponent<MouseFollow>();  
            catcher = Parent.GetComponent<Catcher>();
        }
    }

    IEnumerator StunUnit()
    {
        if (mouseFollow)
        {
            Debug.Log("Cannot move");
            mouseFollow.canMove = false;
            catcher.CanCatch = false;
            yield return new WaitForSeconds(Duration);
            mouseFollow.canMove = true;
            catcher.CanCatch = true;
            Destroy(this);
        }
        
    }

    public void StartEffect()
    {
        Assert.IsNotNull(Parent, "UnitTarget is null or is not set");
        mouseFollow = Parent.GetComponent<MouseFollow>();
        catcher = Parent.GetComponent<Catcher>();
        stunEffectRoutine = StartCoroutine(StunUnit());
        
        Debug.Log("Stunned");
    }

    public void ActivateEffect(GameObject target)
    {
       
    }

    public void DeactivateEffect(GameObject target)
    {

    }
}
