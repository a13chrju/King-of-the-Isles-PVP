using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showUI : MonoBehaviour {

    public bool isshown = false;
    public GameObject helppanel;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        HideIfClickedOutside(helppanel);

    }
    public void ShowUI()
    {

            this.gameObject.SetActive(true);
            isshown = true;

     
    }
    private void HideIfClickedOutside(GameObject panel)
    {
        if (Input.GetMouseButton(0) && panel.activeSelf &&
            !RectTransformUtility.RectangleContainsScreenPoint(
                panel.GetComponent<RectTransform>(),
                Input.mousePosition,
                Camera.main))
        {
            panel.SetActive(false);
        }
    }


}
