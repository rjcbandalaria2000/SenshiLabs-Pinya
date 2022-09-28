using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TutorialUIManager : MonoBehaviour
{
    // Start is called before the first frame update

    public List<string> instructionText;
    public List<Sprite> tutorialImages;
    public int pageCount;
    public TextMeshProUGUI textGO;
    public Image imageGO;


    private void OnEnable()
    {
        pageCount = 0;
        textGO.text = instructionText[0];
    }
    public void NextPage()
    {

        if (pageCount >= instructionText.Count - 1)
        {
            pageCount = instructionText.Count - 1;

        }
        else
        {
            pageCount++;
            textGO.text = instructionText[pageCount];

        }


    }

    public void PreviousPage()
    {

        if (pageCount <= 0)
        {
            pageCount = 0;
            textGO.text = instructionText[0];

        }
        else
        {
            pageCount--;

            textGO.text = instructionText[pageCount];

        }


    }
}
