using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChallengesScript : MonoBehaviour
{
    GameLevelSortType SortType;
    const int ItemPerPages = 5;
    int m_PageNo = 1;
    public Text TitleText;

    public GameObject ListerPanel;

    public Button ForwardButton;
    public Button BackwardButton;

    void Start()
    {
        m_PageNo = 1;
        Refresh();
    }

    private void Refresh()
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
                UIStateManager.Manager.PlayerId,
                UIStateManager.Manager.LiveTournamentID,
                (int)UIStateManager.Manager.GameLevelSortType,
                ((ItemPerPages * m_PageNo) - ItemPerPages) + 1,
                (ItemPerPages * m_PageNo) + 1,
                OnGetGameLevelDetails));

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
                    objText.text = item.Name;

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
            UIStateManager.Manager.SwapToSubLevelPage(item);

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
        UIStateManager.Manager.SwapToTournamentPage();
    }
}
