using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBadFood : FallingFood
{
    public GameObject Parent;
    public float StunDuration;
  //  SFXManager sFX;
   // public AudioClip pinyaCatch;
    // Start is called before the first frame update
    void Start()
    {
      //  sFX = GetComponent<SFXManager>();
    }

    public override void OnCollided(GameObject unit = null)
    {
        Stun unitStun = unit.GetComponent<Stun>();
        if(unitStun != null) { return; }
        Stun stun = unit.AddComponent<Stun>();
        stun.Duration = StunDuration;
        stun.Parent = unit;
       // sFX.PlaySFX(pinyaCatch);
        stun.StartEffect();

    }


}
