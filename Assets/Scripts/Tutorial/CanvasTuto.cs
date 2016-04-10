using UnityEngine;
using System.Collections;

public class CanvasTuto : MonoBehaviour {

    private GameObject Text1;
    private GameObject Text2;
    private GameObject Text3;
    private GameObject Text4;
    private GameObject Img1;
    private GameObject Img2;
    private GameObject Img3;
    private GameObject TextEnd;
    private GameObject TextNext;
    private GameObject ImgEnd;
    private GameObject ImgNext;
    private float widthScreen;
    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void NouvellePage(int i)
    {
        widthScreen = Screen.width;
        if (i==1)
        {
            Text1 = GameObject.Find("Text1_1");
            Text2 = GameObject.Find("Text1_2");
            Text3 = GameObject.Find("Text1_3");
            Img1 = GameObject.Find("Image1_1");
            Img2 = GameObject.Find("Image1_2");
            Text1.GetComponent<RectTransform>().position = new Vector3(.5f * widthScreen, 0.52f * widthScreen, 0);
            Text2.GetComponent<RectTransform>().position = new Vector3(.250f * widthScreen, 0.40f * widthScreen, 0);
            Text3.GetComponent<RectTransform>().position = new Vector3(.750f * widthScreen, 0.40f * widthScreen, 0);
            Img1.GetComponent<RectTransform>().position = new Vector3(.250f * widthScreen, 0.3f * widthScreen, 0);
            Img2.GetComponent<RectTransform>().position = new Vector3(.750f * widthScreen, 0.3f * widthScreen, 0);
        }
        else if (i==2)
        {
            Text1 = GameObject.Find("Text"+ i.ToString() +"_" + 1);
            Text2 = GameObject.Find("Text" + i.ToString() + "_" + 2);
            Text3 = GameObject.Find("Text" + i.ToString() + "_" + 3);
            Text4 = GameObject.Find("Text" + i.ToString() + "_" + 4);
            Img1 = GameObject.Find("Image" + i.ToString() + "_" + 1);
            Img2 = GameObject.Find("Image" + i.ToString() + "_" + 2);
            Img3 = GameObject.Find("Image" + i.ToString() + "_" + 3);
            TextNext = GameObject.Find("TextNext");
            TextEnd = GameObject.Find("TextEnd");
            ImgNext = GameObject.Find("ImageNext");
            ImgEnd = GameObject.Find("ImageEnd");
            Text1.GetComponent<RectTransform>().position = new Vector3(.5f * widthScreen, 0.52f * widthScreen, 0);
            Text2.GetComponent<RectTransform>().position = new Vector3(.20f * widthScreen, 0.40f * widthScreen, 0);
            Text3.GetComponent<RectTransform>().position = new Vector3(.5f * widthScreen, 0.40f * widthScreen, 0);
            Text4.GetComponent<RectTransform>().position = new Vector3(.8f * widthScreen, 0.40f * widthScreen, 0);
            Img1.GetComponent<RectTransform>().position = new Vector3(.2f * widthScreen, 0.3f * widthScreen, 0);
            Img2.GetComponent<RectTransform>().position = new Vector3(.5f * widthScreen, 0.3f * widthScreen, 0);
            Img3.GetComponent<RectTransform>().position = new Vector3(.8f * widthScreen, 0.3f * widthScreen, 0);
            TextNext.GetComponent<RectTransform>().position = new Vector3(.90f * widthScreen, 0.10f * widthScreen, 0);
            TextEnd.GetComponent<RectTransform>().position = new Vector3(.1f * widthScreen, 0.1f * widthScreen, 0);
            ImgNext.GetComponent<RectTransform>().position = new Vector3(.90f * widthScreen, 0.05f * widthScreen, 0);
            ImgEnd.GetComponent<RectTransform>().position = new Vector3(.1f * widthScreen, 0.05f * widthScreen, 0);
        }
        else
        {
            Text1 = GameObject.Find("Text" + i.ToString() + "_" + 1);
            Text2 = GameObject.Find("Text" + i.ToString() + "_" + 2);
            Text3 = GameObject.Find("Text" + i.ToString() + "_" + 3);
            Img1 = GameObject.Find("Image" + i.ToString() + "_" + 1);
            Img2 = GameObject.Find("Image" + i.ToString() + "_" + 2);
            TextNext = GameObject.Find("TextNext");
            TextEnd = GameObject.Find("TextEnd");
            ImgNext = GameObject.Find("ImageNext");
            ImgEnd = GameObject.Find("ImageEnd");
            Text1.GetComponent<RectTransform>().position = new Vector3(.5f * widthScreen, 0.52f * widthScreen, 0);
            Text2.GetComponent<RectTransform>().position = new Vector3(.250f * widthScreen, 0.40f * widthScreen, 0);
            Text3.GetComponent<RectTransform>().position = new Vector3(.750f * widthScreen, 0.40f * widthScreen, 0);
            Img1.GetComponent<RectTransform>().position = new Vector3(.250f * widthScreen, 0.3f * widthScreen, 0);
            Img2.GetComponent<RectTransform>().position = new Vector3(.750f * widthScreen, 0.3f * widthScreen, 0);
            TextNext.GetComponent<RectTransform>().position = new Vector3(.90f * widthScreen, 0.10f * widthScreen, 0);
            TextEnd.GetComponent<RectTransform>().position = new Vector3(.1f * widthScreen, 0.1f * widthScreen, 0);
            ImgNext.GetComponent<RectTransform>().position = new Vector3(.90f * widthScreen, 0.05f * widthScreen, 0);
            ImgEnd.GetComponent<RectTransform>().position = new Vector3(.1f * widthScreen, 0.05f * widthScreen, 0);
        }
    }
}
