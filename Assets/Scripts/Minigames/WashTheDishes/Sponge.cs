using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sponge : MonoBehaviour
{
    public Transform returnPosition;
   

    private void Awake()
    {
        Events.OnMouseUp.AddListener(ReturnToSpongePosition);
        Events.OnSceneChange.AddListener(OnSceneChange);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ReturnToSpongePosition()
    {
        if(returnPosition == null) { return; }
        this.transform.position = returnPosition.position;
       
    }

    public void OnSceneChange()
    {
        Events.OnMouseUp.RemoveListener(ReturnToSpongePosition);
        Events.OnSceneChange.RemoveListener(OnSceneChange);
    }
}
