using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Assertions;

public class DisplayInteractMessage : MonoBehaviour
{
    public GameObject       Parent;
    public TextMeshProUGUI  Text;

    private MinigameDetector interactable;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        
        if(Parent == null)
        {
            Parent = this.gameObject.transform.parent.GetComponent<UnitInfo>().Parent;
        }
        Assert.IsNotNull(Parent, "Parent is not set or is null");
        if(interactable == null)
        {
            interactable = Parent.GetComponent<MinigameDetector>(); 
            
        }
        if (interactable)
        {
            interactable.EvtInteract.AddListener(ChangeMessage);
            interactable.EvtFinishInteract.AddListener(RemoveMessage);
        }
        this.gameObject.SetActive(false);
    }

    public void ChangeMessage()
    {
        this.gameObject.SetActive(true);
    }

    public void RemoveMessage()
    {
        this.gameObject.SetActive(false);
    }



}
