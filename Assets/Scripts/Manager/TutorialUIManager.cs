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

    public Button nextArrow;
    public Button prevArrow;

    private void OnEnable()
    {
        prevArrow.gameObject.SetActive(false);
        pageCount = 0;
        textGO.text = instructionText[0];
        tempPage = 1;
        currentPage.text = "1";

        if(tutorialImages.Count > 0)
        {
            imageGO.sprite = tutorialImages[0];
        }
           

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
            prevArrow.gameObject.SetActive(true);
            pageCount++;
            tempPage++;
            textGO.text = instructionText[pageCount];

            if (tutorialImages.Count > 0)
                imageGO.sprite = tutorialImages[pageCount];

            if(pageCount >= instructionText.Count - 1)
            {
                nextArrow.gameObject.SetActive(false);
            }

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
            nextArrow.gameObject.SetActive(true);
            pageCount--;
            tempPage--;
            textGO.text = instructionText[pageCount];

            if (tutorialImages.Count > 0)
                imageGO.sprite = tutorialImages[pageCount];

            if(pageCount <= 0)
            {
                prevArrow.gameObject.SetActive(false);
            }
        }

        currentPage.text = tempPage.ToString();
    }
}
