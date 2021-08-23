using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupCaculator : BasePopup
{
    [SerializeField] GameObject panelConcre;
    [SerializeField] GameObject panelSteel;
    [SerializeField] GameObject buttonNext;
    [SerializeField] GameObject buttonPre;

    private int indexPanel = 0;
    private int indexConcree = 0;
    private int indexSteel = 1;

    private void Awake()
    {
        ActivePanel(indexConcree);
    }
    public static PopupCaculator ShowUp()
    {
        GameObject prefab = Utils.LoadPrefab("Prefabs/PopupCaculator");
        GameObject goPopup = GameObject.Instantiate(prefab, CanvasHelper.CanvasPopupPanel, false);
        PopupCaculator pop = goPopup.GetComponent<PopupCaculator>();
        return pop;
    }
    public void ButtonNextClick()
    {
        ActivePanel(indexSteel);
    }
    public void ButtonPreClick()
    {
        ActivePanel(indexConcree);
    }

    private void ActivePanel(int id)
    {
        panelConcre.SetActive(false);
        panelSteel.SetActive(false);
        buttonNext.SetActive(false);
        buttonPre.SetActive(false);

        switch (id)
        {
            case 0:
                {
                    panelConcre.SetActive(true);
                    buttonNext.SetActive(true);
                    break;
                }
            case 1:
                {
                    panelSteel.SetActive(true);
                    buttonPre.SetActive(true);
                    break;
                }
        }
    }
}
