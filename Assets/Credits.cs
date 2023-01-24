using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Sprite> creditImage;
    public Button nextArrow;
    public Button prevArrow;
    public Image imageGO;
    public int pageCount;



    private void OnEnable()
    {
        imageGO.sprite = creditImage[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NextPage()
    {

        if (pageCount >= creditImage.Count - 1)
        {
            pageCount = creditImage.Count - 1;


        }
        else
        {
            prevArrow.gameObject.SetActive(true);
            pageCount++;
          ////  tempPage++;
         //   textGO.text = instructionText[pageCount];


            if (pageCount >= creditImage.Count - 1)
            {
                nextArrow.gameObject.SetActive(false);

            }


            if (creditImage[pageCount] == null)
            {
                imageGO.gameObject.SetActive(false);

            }
            else
            {
                if (creditImage.Count > 0)
                {
                 //   videoImage.gameObject.SetActive(false);
                    imageGO.gameObject.SetActive(true);
                    imageGO.sprite = creditImage[pageCount];
                }
            }

        }

        //    int temp = pageCount + 1;
     ///  currentPage.text = tempPage.ToString();

    }

    public void PreviousPage()
    {

        if (pageCount <= 0)
        {

            pageCount = 0;
          //  tempPage = 1;
           // textGO.text = instructionText[0];

        }
        else
        {
            nextArrow.gameObject.SetActive(true);
            pageCount--;
          //  tempPage--;
           // textGO.text = instructionText[pageCount];

            if (creditImage.Count > 0)
                imageGO.sprite = creditImage[pageCount];

            
            
            if (pageCount <= 0)
            {
                prevArrow.gameObject.SetActive(false);
            }
            if (creditImage[pageCount] == null)
            {
                imageGO.gameObject.SetActive(false);
               // videoImage.gameObject.SetActive(true);
 
            }
            else
            {
                if (creditImage.Count > 0)
                {
                   // videoImage.gameObject.SetActive(false);
                    imageGO.gameObject.SetActive(true);
                    imageGO.sprite = creditImage[pageCount];
                }
            }

        }

    }
}
