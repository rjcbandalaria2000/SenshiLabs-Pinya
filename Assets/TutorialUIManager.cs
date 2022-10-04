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
    public TextMeshProUGUI currentPage;
    public TextMeshProUGUI maxPage;
    int tempPage;

    private void OnEnable()
    {
        pageCount = 0;
        textGO.text = instructionText[0];
        tempPage = 1;
        currentPage.text = "1";
        maxPage.text = instructionText.Count.ToString();
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
            tempPage++;
            textGO.text = instructionText[pageCount];

        }
        //    int temp = pageCount + 1;
        currentPage.text = tempPage.ToString();

    }

    public void PreviousPage()
    {

        if (pageCount <= 0)
        {
            pageCount = 0;
            tempPage = 1;
            textGO.text = instructionText[0];

        }
        else
        {
            pageCount--;
            tempPage--;
            textGO.text = instructionText[pageCount];

        }

        currentPage.text = tempPage.ToString();
    }
}
