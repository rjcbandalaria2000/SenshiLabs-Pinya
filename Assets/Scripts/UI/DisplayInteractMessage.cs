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

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {

        if (Parent == null)
        {
            Parent = this.gameObject.transform.parent.GetComponent<UnitInfo>().Parent;
        }
        Assert.IsNotNull(Parent, "Parent is not set or is null");

        
        Events.OnEnterInteraction.AddListener(ChangeMessage);
        Events.OnFinishInteract.AddListener(RemoveMessage);
        Events.OnSceneChange.AddListener(OnSceneChange);
        this.gameObject.SetActive(false);
    }

    public void ChangeMessage()
    {
        //Debug.Log("Message appear ");
        if (!this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(true);
        }
       


    }

    public void RemoveMessage(GameObject player = null)
    {
        this.gameObject.SetActive(false);
    }

    public void OnSceneChange()
    {
        Events.OnSceneChange.RemoveListener(OnSceneChange);
        Events.OnEnterInteraction.RemoveListener(ChangeMessage);
        Events.OnFinishInteract.RemoveListener(RemoveMessage);
        Debug.Log("Removed Interact Message listener");
    }


}
