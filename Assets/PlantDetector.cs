using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantDetector : MonoBehaviour
{
    public GameObject Parent;

    private WateringCan wateringCan;
    private UnitInfo unitInfo;
    private Plant targetPlant;
    // Start is called before the first frame update
    void Start()
    {
        if(Parent == null)
        {
            Parent = this.transform.parent.GetComponent<UnitInfo>().Parent;
        }
        wateringCan = Parent.GetComponent<WateringCan>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Plant collidedPlant = collision.gameObject.GetComponent<Plant>();
        if (collidedPlant)
        {
            wateringCan.PlantToWater = collidedPlant.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Plant collidedPlant = collision.gameObject.GetComponent<Plant>();
        if (collidedPlant)
        {
            wateringCan.PlantToWater = null;
        }
    }

}
