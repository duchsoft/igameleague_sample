using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System;

public class CreateTournamentScript : MonoBehaviour
{
    public GameObject DropdownPanel_LevelTypeObject;
    public GameObject DropdownPanel_LevelType_Duplicate;
    public GameObject DropdownPanel_LevelTypeChild;

    public GameObject DropdownPanel_Duration;
    public GameObject DropdownPanel_Days;
    public GameObject DropdownPanel_Hours;
    static GameLevelDetail CurrentLevel;
    public Button DropDownButton_LevelType;
    public Button DropDownButton_Duration;
    public GameObject JumboButton;
    public Button CreateButton;
    public Text AdminPercentMessage;
    bool populated = false;
    public Text DurationText;
    int selectedDay = 1;
    int selectedHour = 0;

    // Use this for initialization
    void Start()
    {
        CurrentLevel = null;
        RefreshDuration();
        Refresh(false);
        JumboButton.SetActive(false);
        StartCoroutine(UIStateManager.GameAPI.GetGameDetails(UIStateManager.Manager.GameID, OnGetGameDetails));
    }

    void OnGetGameDetails(string errorString, GameDetail result, params object[] userParam)
    {
        if (string.IsNullOrEmpty(errorString) && result != null)
        {
            AdminPercentMessage.text = "(Tournament creator gets " + result.LeaderboardAdminSharePercent.ToString() + "% of total points collected in the tournament)";
        }
    }

    void RefreshDuration()
    {
        if (selectedDay + selectedHour <= 0)
        {
            DurationText.text = "Select Validity";
        }
        else
        {
            DurationText.text = string.Format(
            "Valid for {0}{1} {2}{3}",
            selectedDay > 0 ? selectedDay.ToString() + " day" : "",
            selectedDay > 1 ? "s" : "",
            selectedHour > 0 ? selectedHour.ToString() + " hour" : "",
            selectedHour > 1 ? "s" : ""
            );
        }
        ValidateCreate();
    }

    private void ValidateCreate()
    {
        CreateButton.interactable = (selectedDay + selectedHour) > 0;
    }

    public void SelectLevelType()
    {
        JumboButton.SetActive(true);
        MenuSoundManager.Instance.PlayMenuClick();
        DropdownPanel_LevelTypeObject.SetActive(false);
        DropdownPanel_LevelTypeChild.SetActive(false);
        DropdownPanel_LevelType_Duplicate.SetActive(false);
        Refresh(true);
    }

    public void SelectDuration()
    {
        JumboButton.SetActive(true);
        MenuSoundManager.Instance.PlayMenuClick();
        if (!populated)
        {
            StartCoroutine(PopulateDuration());
            StartCoroutine(PopulateHour());
            populated = true;
        }

        DropdownPanel_Duration.SetActive(true);
    }

    private void Refresh(bool createDropDownItems)
    {
        UIStateManager.Manager.SetLoading(true);
        StartCoroutine(UIStateManager.GameAPI.GetSubGameLevelsAvailableToPlayer(
            playerUserId : UIStateManager.Manager.PlayerId,
            levelId : UIStateManager.Manager.UserTournamentID,
            sortType : (int)UIStateManager.Manager.GameLevelSortType,
            startIndex : 1,
            endIndex : 100,
            onGetGameLevelDetails : OnGetGameLevelDetails,
            userParam : createDropDownItems));
    }

    IEnumerator PopulateDuration()
    {
        for (int days = 0; days < 31; days++)
        {
            GameObject obj = GameObject.Instantiate(UIStateManager.Manager.ListItemsTemplate_Buttons);
            obj.transform.SetParent(DropdownPanel_Days.transform, false);
            yield return new WaitForSeconds(0.001f);
            obj.name = (days).ToString();
            Button objButton = obj.GetComponent<Button>();
            Text objText = objButton.GetComponentInChildren<Text>();
            objText.fontSize = 25;
            objText.text = (days).ToString();
            AddDaysEvent(days, objButton);
        }
    }

    private void AddDaysEvent(int day, Button objButton)
    {
        objButton.onClick.AddListener(() =>
        {
            selectedDay = day;
            RefreshDuration();
        });
    }

    IEnumerator PopulateHour()
    {
        for (int hours = 0; hours < 24; hours++)
        {
            GameObject obj = GameObject.Instantiate(UIStateManager.Manager.ListItemsTemplate_Buttons);
            obj.transform.SetParent(DropdownPanel_Hours.transform, false);
            yield return new WaitForSeconds(0.001f);
            obj.name = (hours).ToString();
            Button objButton = obj.GetComponent<Button>();
            Text objText = objButton.GetComponentInChildren<Text>();
            objText.text = (hours).ToString();
            objText.fontSize = 25;
            AddHourEvent(hours, objButton);
        }
    }

    private void AddHourEvent(int hours, Button objButton)
    {
        objButton.onClick.AddListener(() =>
        {
            selectedHour = hours;
            RefreshDuration();
        });
    }

    void onCreateLeaderboard(string errorString, string result, params object[] userParam)
    {
        if (string.IsNullOrEmpty(errorString) && result != null)
        {
            LeaderboardDetail leaderboardDetail = new LeaderboardDetail();
            leaderboardDetail.Id = result;
            UIStateManager.Manager.SettingsSystem.AddToLeaderboardCache(UIStateManager.Manager.PlayerId, leaderboardDetail, "", true);
            UIStateManager.Manager.SwapToPostCreateLeaderboardPage(leaderboardDetail, CurrentLevel);
            StartCoroutine(UIStateManager.GameAPI.MakeLeaderboardLive(
                leaderBoardId: result,
                onMakeLeaderboardLive: onMakeLeaderboardLive));
        }

        UIStateManager.Manager.SetLoading(false);
    }

    void onMakeLeaderboardLive(string errorString, bool result, params object[] userParams)
    {
        if (string.IsNullOrEmpty(errorString) && result)
        {
            Debug.Log("Leaderboard is Live");
        }
        else
        {
            Debug.Log("We have a problem here");
        }
    }

    public void SelectCreateTournament()
    {
        MenuSoundManager.Instance.PlayMenuClick();
        if (CurrentLevel != null)
        {
            UIStateManager.Manager.SetLoading(true);
            long lifeTime = Convert.ToInt64((selectedDay * 24 + selectedHour) * 60);
            StartCoroutine(UIStateManager.GameAPI.CreateLeaderboard(
                leaderboardName : "",
                leaderboardComment : "",
                playerCount : 0,
                levelId : CurrentLevel.Id,
                country : "",
                region : "",
                city : "",
                restrictByDateOfBirthStart : "",
                restrictByDateOfBirthEnd : "",
                useRestrictByDateOfBirthStart : false,
                useRestrictByDateOfBirthEnd : false,
                gender : GenderType.NotSpecified,
                reference : "",
                availableOnlyToPlayerUserNames : "",
                extras : "",
                createdBy : "",
                useAutoClose : true,
                lifeTime : lifeTime,
                inviteOnly : true,
                onCreateLeaderboard : onCreateLeaderboard));
        }
    }

    public void SelectBack()
    {
        MenuSoundManager.Instance.PlayMenuClick();
        UIStateManager.Manager.SwapToUserTournamentsPage();
    }

    void OnGetGameLevelDetails(string errorString, GameLevelDetail[] result, params object[] userParam)
    {

        bool createDropDownItems = (bool)userParam[0];
        for (int i = 0, childCount = DropdownPanel_LevelTypeChild.transform.childCount; i < childCount; i++)
        {
            GameObject childObject = DropdownPanel_LevelTypeChild.transform.GetChild(i).gameObject;
            GameObject.Destroy(childObject);
        }
        if (string.IsNullOrEmpty(errorString) && result != null)
        {
            if (result.Length > 0)
            {
                SetPickedItem(result[0]);
            }

            if (createDropDownItems)
            {
                DropdownPanel_LevelTypeObject.SetActive(true);
                DropdownPanel_LevelTypeChild.SetActive(true);
                DropdownPanel_LevelType_Duplicate.SetActive(true);
                //DropdownPanel_LevelTypeObject.GetComponent<Image>().color = _color;
                foreach (GameLevelDetail item in result)
                {
                    GameObject obj = GameObject.Instantiate(UIStateManager.Manager.ListItemsTemplate_Buttons);
                    obj.transform.SetParent(DropdownPanel_LevelTypeChild.transform, false);
                    obj.name = item.Id;
                    Button objButton = obj.GetComponent<Button>();
                    Text objText = objButton.GetComponentInChildren<Text>();
                    objText.fontSize = 25;
                    GetDropDownItemName(item, objText);
                    AddEventToButton(item, objButton);
                }
            }
        }
        UIStateManager.Manager.SetLoading(false);
    }

    void SetPickedItem(GameLevelDetail item)
    {
        CurrentLevel = item;
        Text objText = DropDownButton_LevelType.GetComponentInChildren<Text>();
        GetDropDownItemName(item, objText);
    }

    private static void GetDropDownItemName(GameLevelDetail item, Text objText)
    {
        objText.text = string.Format("Joining Point = {0}",
                item.JoiningPoint == 0 ? "0" : item.JoiningPoint.ToString());
    }

    private void AddEventToButton(GameLevelDetail item, Button objButton)
    {
        objButton.onClick.AddListener(() =>
        {
            MenuSoundManager.Instance.PlayMenuClick();
            SetPickedItem(item);
            DropdownPanel_LevelTypeObject.SetActive(false);
            DropdownPanel_LevelTypeChild.SetActive(false);
            DropdownPanel_LevelType_Duplicate.SetActive(false);
        });
    }

    public void SelectJumboButton()
    {
        DropdownPanel_LevelTypeObject.SetActive(false);
        DropdownPanel_LevelTypeChild.SetActive(false);
        DropdownPanel_LevelType_Duplicate.SetActive(false);
        DropdownPanel_Duration.SetActive(false);
        JumboButton.SetActive(false);
    }
}