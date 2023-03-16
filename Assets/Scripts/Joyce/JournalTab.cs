using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Joyce Mai
public class JournalTab : MonoBehaviour
{
    [Header("Content")]
    [SerializeField] private GameObject content;

    [Header("Tab Lists")]
    [SerializeField] private List<GameObject> ruleTabs;
    [SerializeField] private List<GameObject> npcTabs;
    private List<GameObject> currList;
    private int currTabIndex = 0;

    [Header("Buttons")]
    [SerializeField] private GameObject ruleButtonGO;
    [SerializeField] private Button ruleButton;
    [SerializeField] private GameObject npcButtonGO;
    [SerializeField] private Button npcButton;
    private Dictionary<GameObject, Button> buttons = new Dictionary<GameObject, Button>();
    private GameObject currTab;

    private void Awake()
    {
        buttons.Add(ruleButtonGO, ruleButton);
        buttons.Add(npcButtonGO, npcButton);
        CloseContent();
    }

    private void SetUpButtons()
    {
        ruleButton.interactable = true;
        npcButton.interactable = true;

        buttons[currTab].interactable = false;
    }

    private void CloseTabs(List<GameObject> tabs)
    {
        if (tabs.Count <= 0)
            return;

        foreach (GameObject tab in tabs)
        {
            tab.SetActive(false);   
        }
    }

    private void ShowCurrentIndex()
    {
        for(int i = 0; i < currList.Count; i++)
        {
            currList[i].SetActive(i == currTabIndex);
        }
    }

    public void CloseContent()
    {
        currTab = ruleButtonGO;

        CloseTabs(ruleTabs);
        CloseTabs(npcTabs);

        currTabIndex = 0;
        currList = ruleTabs;
        ShowCurrentIndex();
        SetUpButtons();
        content.SetActive(false);
    }

    public void OpenContent()
    {
        content.SetActive(true);
        ShowCurrentIndex();
    }

    public void LeftArrowClicked()
    {
        currTabIndex--;
        if (currTabIndex < 0)
            currTabIndex = currList.Count - 1;
        ShowCurrentIndex();
    }

    public void RightArrowClicked()
    {
        currTabIndex++;
        if (currTabIndex >= currList.Count)
            currTabIndex = 0;
        ShowCurrentIndex();
    }

    public void RuleTabButton()
    {
        currTabIndex = 0;
        currTab = ruleButtonGO;
        currList = ruleTabs;
        CloseTabs(npcTabs);
        SetUpButtons();
        ShowCurrentIndex();
    }

    public void NPCTabButton()
    {
        currTabIndex = 0;
        currTab = npcButtonGO;
        currList = npcTabs;
        CloseTabs(ruleTabs);
        SetUpButtons();
        ShowCurrentIndex();
    }
}
