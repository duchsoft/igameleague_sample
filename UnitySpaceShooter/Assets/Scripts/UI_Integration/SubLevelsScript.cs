using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SubLevelsScript : MonoBehaviour
{
    public static GameLevelDetail Level;
    public Text TitleText;
    const int ItemPerPages = 5;
    int m_PageNo = 1;
    public int ButtonFontSize = 20;

    public GameObject ListerPanel;

    public Button ForwardButton;
    public Button BackwardButton;

    void Start()
    {
        m_PageNo = 1;
        this.TitleText.text = Level.Name;
        Refresh();
    }

    public void Refresh()
    {
        for (int i = 0, childCount = ListerPanel.transform.childCount; i < childCount; i++)
        {
            GameObject.Destroy(ListerPanel.transform.GetChild(i).gameObject);
        }
        ForwardButton.interactable = false;
        BackwardButton.interactable = false;
        UIStateManager.Manager.SetLoading(true);
        StartCoroutine(
            UIStateManager.GameAPI.GetSubGameLevelsAvailableToPlayer(
            playerUserId : UIStateManager.Manager.PlayerId,
            levelId : Level.Id,
            sortType : (int)UIStateManager.Manager.GameLevelSortType,
            startIndex : ((ItemPerPages * m_PageNo) - ItemPerPages) + 1,
            endIndex : (ItemPerPages * m_PageNo) + 1,
            onGetGameLevelDetails : OnGetGameLevelDetails));
    }

    void OnGetGameLevelDetails(string errorString, GameLevelDetail[] result, params object[] userParam)
    {
        if (string.IsNullOrEmpty(errorString) && result != null)
        {
            int index = 0;

            foreach (GameLevelDetail item in result)
            {
                if (index < ItemPerPages)
                {
                    GameObject obj = GameObject.Instantiate(UIStateManager.Manager.ListItemTemplate_LeaderboardTiles);
                    obj.transform.SetParent(ListerPanel.transform, false);

                    obj.name = item.Id;
                    Button objButton = obj.GetComponent<Button>();
                    Text objText = objButton.GetComponentInChildren<Text>();
                    objText.fontSize = ButtonFontSize;
                    objText.text = string.Format("Joining Point = {0}",
                            item.JoiningPoint == 0 ? "0" : item.JoiningPoint.ToString());
                    AddEventToButton(item, objButton);
                }
                index++;
            }

            if (m_PageNo > 1)
            {
                BackwardButton.interactable = true;
            }

            if (result.Length == ItemPerPages + 1)
            {
                ForwardButton.interactable = true;
            }
        }
        else
        {
            m_PageNo = 1;
        }
        UIStateManager.Manager.SetLoading(false);
    }

    private static void AddEventToButton(GameLevelDetail item, Button objButton)
    {
        objButton.onClick.AddListener(() =>
        {
            MenuSoundManager.Instance.PlayMenuClick();
            UIStateManager.Manager.SwapToLeaderboardPage();
        });
    }
	
    public void SelectForward()
    {
        MenuSoundManager.Instance.PlayMenuClick();
        m_PageNo++;
        Refresh();
    }

    public void SelectBackward()
    {
        MenuSoundManager.Instance.PlayMenuClick();
        if (m_PageNo > 1)
        {
            m_PageNo--;
        }
        Refresh();
    }

    public void SelectBack()
    {
        MenuSoundManager.Instance.PlayMenuClick();
        UIStateManager.Manager.SwapToChallengesPage();
    }
}
