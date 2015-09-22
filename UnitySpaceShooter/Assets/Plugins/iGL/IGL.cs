﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;

[Serializable]
public enum GenderType
{
    NotSpecified,
    Male,
    Female,
    Others
}

[Serializable]
public enum CurrencyType
{
    INR,
    USD
}

[Serializable]
public enum GameOrientation
{
    Landscape,
    Portrait,
}

[Serializable]
public enum GameLevelSortType
{
    CreatedDate,
    JoiningPoint,
    Name
}

[Serializable]
public class GameLevelDetail
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Comment { get; set; }
    public long JoiningPoint { get; set; }
    public string GameId { get; set; }
    public string ParentLevelId { get; set; }
    public bool Active { get; set; }
    public string Extras { get; set; }
    public string Reference { get; set; }
    public bool IsUserTournamentAllowed { get; set; }
    public int JoiningPointType { get; set; }
}

[Serializable]
public class LeaderboardDetail
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Comment { get; set; }
    public string Country { get; set; }
    public string Region { get; set; }
    public string City { get; set; }
    public string GameId { get; set; }
    public string ParentLevelId { get; set; }
    public string ClosedTime { get; set; }
    public string CreatedTime { get; set; }
    public string LiveTime { get; set; }
    public bool Active { get; set; }
    public bool Live { get; set; }
    public long JoiningPoint { get; set; }
    public bool IsPlayerParticipated { get; set; }
    public bool IsFreePlayerWillBeAwarded { get; set; }
    public long PlayerCountLimit { get; set; }
    public long CurrentPlayerCount { get; set; }
    public string RestrictByDateOfBirthStart { get; set; }
    public string RestrictByDateOfBirthEnd { get; set; }
    public bool UseRestrictByDateOfBirthStart { get; set; }
    public bool UseRestrictByDateOfBirthEnd { get; set; }
    public string Reference { get; set; }
    public int Gender { get; set; }
    public string[] AvailableOnlyToPlayerUserNames { get; set; }
    public string CurrentTime { get; set; }
    public long TotalRewardPoints { get; set; }
    public bool IsSponsored { get; set; }
    public string Extras { get; set; }
    public long TopWinnersCount { get; set; }
    public string Tag { get; set; }
    public string AdminName { get; set; }
    public long LifeTime { get; set; }
    public bool UseAutoClose { get; set; }
    public bool InviteOnly { get; set; }
    public int JoiningPointType { get; set; }
}

[Serializable]
public class GameDetail
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Comment { get; set; }
    public bool Active { get; set; }
    public string CreatedDate { get; set; }
    public bool IsAvailableForSponsorship { get; set; }
    public float RewardPercentage { get; set; }
    public float TopWinnersPercentage { get; set; }
    public float DeveloperPercentage { get; set; }
    public string FullDescription { get; set; }
    public string Extras { get; set; }
    public long TotalPlayersParticipated { get; set; }
    public string[] DistributionUrls { get; set; }
    public string[] DistributionLogoUrls { get; set; }
    public string GameLogoUrl { get; set; }
    public float LeaderboardAdminSharePercent { get; set; }
    public float LeaderboardPlayershare { get; set; }
    public string GameDescriptionLink { get; set; }
}

[Serializable]
public class PlayerDetail
{
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public bool Active { get; set; }
}

[Serializable]
public class LeaderboardStatus
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Comment { get; set; }
    public string Country { get; set; }
    public string Region { get; set; }
    public string City { get; set; }
    public string GameId { get; set; }
    public string ParentLevelId { get; set; }
    public bool Active { get; set; }
    public bool Live { get; set; }
    public string[] PlayerUserNames { get; set; }
    public double[] Scores { get; set; }
    public long PlayerCountLimit { get; set; }
    public string RestrictByDateOfBirthStart { get; set; }
    public string RestrictByDateOfBirthEnd { get; set; }
    public bool UseRestrictByDateOfBirthStart { get; set; }
    public bool UseRestrictByDateOfBirthEnd { get; set; }
    public string Reference { get; set; }
    public int Gender { get; set; }
    public string[] AvailableOnlyToPlayerUserNames { get; set; }
    public string ClosedTime { get; set; }
    public string CreatedTime { get; set; }
    public string CurrentTime { get; set; }
    public long TotalRewardPoints { get; set; }
    public bool IsSponsored { get; set; }
    public string Extras { get; set; }
    public long TopWinnersCount { get; set; }
    public string Tag { get; set; }
    public string AdminName { get; set; }
    public long LifeTime { get; set; }
    public bool UseAutoClose { get; set; }
    public bool InviteOnly { get; set; }
}

[Serializable]
public class BannerDetail
{
    public string BannerUrl { get; set; }
    public string BannerClickUrl { get; set; }
    public string BannerCategoryType { get; set; }
    public string BannerHeight { get; set; }
    public string BannerWidth { get; set; }
    public string LeaderboardId { get; set; }
    public string InterstitialPotraitUrl { get; set; }
    public string InterstitialLandscapeUrl { get; set; }
    public string InterstitialClickUrl { get; set; }
}

[Serializable]
public class VoucherDetail
{
    public string ReturnString { get; set; }
    public string AvailableVouchers { get; set; }
    public string TransactionDate { get; set; }
}

[Serializable]
public class RankDetail
{
    public string PlayerUsername { get; set; }
    public string LeaderboardId { get; set; }
    public double Score { get; set; }
    public long Rank { get; set; }
}

public class IGL
{
    public static string URL_FORMAT = string.Empty;
    public static string REGISTRATION_URL = string.Empty;
    public static string LEADERBOARD_URL = string.Empty;
    public static string PLAYER_PAGE_URL = string.Empty;
    public static string ENTRYKEY_URL = string.Empty;

    public string m_DeveloperId = string.Empty; //Your developer user ID at iGL
    public string m_IntegrationKey = string.Empty; //Your developer integrationKey at iGL

    public IGL(string developerId, string integrationKey)
    {
        m_DeveloperId = developerId;
        m_IntegrationKey = integrationKey;
    }

    string GetUrl(string actionName)
    {
        return string.Format(URL_FORMAT, actionName);
    }

    WWW POST(string url, ref Dictionary<string, string> post)
    {
        WWWForm form = new WWWForm();
        foreach (KeyValuePair<String, String> post_arg in post)
        {
            form.AddField(post_arg.Key, post_arg.Value);
        }
        WWW www = new WWW(url, form);
        return www;
    }

    bool ToBooleanResult(string result, ref string errorString)
    {
        try
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(result);
            XmlNodeList nodes = xmlDoc.GetElementsByTagName("boolean");
            if (nodes.Count > 0)
            {
                return nodes.Item(0).InnerText == "true";
            }
        }
        catch (Exception ex)
        {
            errorString = ex.Message;
        }
        return false;
    }

    string ToStringResult(string result, ref string errorString)
    {
        try
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(result);
            XmlNodeList nodes = xmlDoc.GetElementsByTagName("string");
            if (nodes.Count > 0)
            {
                return nodes.Item(0).InnerText;
            }
        }
        catch (Exception ex)
        {
            errorString = ex.Message;
        }
        return string.Empty;
    }

    long ToLongResult(string result, ref string errorString)
    {
        try
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(result);
            XmlNodeList nodes = xmlDoc.GetElementsByTagName("long");
            if (nodes.Count > 0)
            {
                if (!string.IsNullOrEmpty(nodes.Item(0).InnerText))
                {
                    return Convert.ToInt64(nodes.Item(0).InnerText);
                }
            }
        }
        catch (Exception ex)
        {
            errorString = ex.Message;
        }
        return -1;
    }

    PlayerDetail ToPlayerDetail(string result, ref string errorString)
    {
        try
        {
            if (!string.IsNullOrEmpty(result))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(result);
                XmlNodeList nodes = xmlDoc.GetElementsByTagName("PlayerDetail");
                if (nodes.Count > 0)
                {
                    XmlNode node = nodes.Item(0);
                    return ToPlayerDetail(node, ref errorString);
                }
            }
        }
        catch (Exception ex)
        {
            errorString = ex.Message;
        }
        return null;
    }

    PlayerDetail ToPlayerDetail(XmlNode node, ref string errorString)
    {
        try
        {
            PlayerDetail player = null;
            if (node != null)
            {
                player = new PlayerDetail();
                player.UserName = node["UserName"].InnerText;
                player.FirstName = node["FirstName"].InnerText;
                player.MiddleName = node["MiddleName"].InnerText;
                player.LastName = node["LastName"].InnerText;
                player.Active = ToBooleanResult(node["Active"].InnerText, ref errorString);
                return player;
            }
        }
        catch (Exception ex)
        {
            errorString = ex.Message;
        }
        return null;
    }

    GameLevelDetail ToGameLevelDetail(XmlNode node, ref string errorString)
    {
        try
        {
            GameLevelDetail gameLevel = null;
            if (node != null && !string.IsNullOrEmpty(node.InnerXml))
            {
                gameLevel = new GameLevelDetail();
                gameLevel.Id = node["Id"].InnerText;
                gameLevel.Name = node["Name"].InnerText;
                gameLevel.Comment = node["Comment"].InnerText;

                long longVal = -1;
                if (long.TryParse(node["JoiningPoint"].InnerText, out longVal))
                {
                    gameLevel.JoiningPoint = longVal;
                }

                gameLevel.GameId = node["GameId"].InnerText;
                gameLevel.ParentLevelId = node["ParentLevelId"].InnerText;

                bool boolVal = false;
                if (bool.TryParse(node["Active"].InnerText, out boolVal))
                {
                    gameLevel.Active = boolVal;
                }
                boolVal = false;
                if (bool.TryParse(node["IsUserTournamentAllowed"].InnerText, out boolVal))
                {
                    gameLevel.IsUserTournamentAllowed = boolVal;
                }

                gameLevel.Extras = node["Extras"].InnerText;
                gameLevel.Reference = node["Reference"].InnerText;

                return gameLevel;
            }
        }
        catch (Exception ex)
        {
            errorString = ex.Message;
        }
        return null;
    }

    GameLevelDetail ToGameLevelDetail(string result, ref string errorString)
    {
        try
        {
            if (!string.IsNullOrEmpty(result))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(result);
                XmlNodeList nodes = xmlDoc.GetElementsByTagName("GameLevelDetail");
                if (nodes.Count > 0)
                {
                    XmlNode node = nodes.Item(0);
                    return ToGameLevelDetail(node, ref errorString);
                }
            }
        }
        catch (Exception ex)
        {
            errorString = ex.Message;
        }
        return null;
    }

    GameLevelDetail[] ToGameLevelsDetail(string result, ref string errorString)
    {
        List<GameLevelDetail> levels = new List<GameLevelDetail>();
        try
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(result);
            XmlNodeList nodes = xmlDoc.GetElementsByTagName("GameLevelDetail");

            foreach (XmlNode levelNode in nodes)
            {
                GameLevelDetail level = ToGameLevelDetail(levelNode, ref errorString);
                if (level != null)
                {
                    levels.Add(level);
                }
            }
        }
        catch (Exception ex)
        {
            errorString = ex.Message;
        }
        return levels.ToArray(); ;
    }

    LeaderboardDetail ToLeaderboardDetail(XmlNode node, ref string errorString)
    {
        try
        {
            LeaderboardDetail board = null;
            if (node != null && !string.IsNullOrEmpty(node.InnerXml))
            {
                board = new LeaderboardDetail();
                board.Id = node["Id"].InnerText;
                board.Name = node["Name"].InnerText;
                board.Comment = node["Comment"].InnerText;
                board.Country = node["Country"].InnerText;
                board.Region = node["Region"].InnerText;
                board.City = node["City"].InnerText;
                board.GameId = node["GameId"].InnerText;
                board.ParentLevelId = node["ParentLevelId"].InnerText;
                board.ClosedTime = node["ClosedTime"].InnerText;
                board.CreatedTime = node["CreatedTime"].InnerText;
                board.LiveTime = node["LiveTime"].InnerText;
                bool boolVal = false;
                if (bool.TryParse(node["Active"].InnerText, out boolVal))
                {
                    board.Active = boolVal;
                }

                if (bool.TryParse(node["Live"].InnerText, out boolVal))
                {
                    board.Live = boolVal;
                }

                long longVal = -1;
                if (long.TryParse(node["JoiningPoint"].InnerText, out longVal))
                {
                    board.JoiningPoint = longVal;
                }

                if (bool.TryParse(node["IsPlayerParticipated"].InnerText, out boolVal))
                {
                    board.IsPlayerParticipated = boolVal;
                }

                if (bool.TryParse(node["IsFreePlayerWillBeAwarded"].InnerText, out boolVal))
                {
                    board.IsFreePlayerWillBeAwarded = boolVal;
                }

                if (long.TryParse(node["PlayerCountLimit"].InnerText, out longVal))
                {
                    board.PlayerCountLimit = longVal;
                }

                if (long.TryParse(node["CurrentPlayerCount"].InnerText, out longVal))
                {
                    board.CurrentPlayerCount = longVal;
                }
                board.RestrictByDateOfBirthStart = node["RestrictByDateOfBirthStart"].InnerText;
                board.RestrictByDateOfBirthEnd = node["RestrictByDateOfBirthEnd"].InnerText;
                if (bool.TryParse(node["UseRestrictByDateOfBirthStart"].InnerText, out boolVal))
                {
                    board.UseRestrictByDateOfBirthStart = boolVal;
                }
                if (bool.TryParse(node["UseRestrictByDateOfBirthEnd"].InnerText, out boolVal))
                {
                    board.UseRestrictByDateOfBirthEnd = boolVal;
                }
                board.Reference = node["Reference"].InnerText;
                int intVal = -1;
                if (int.TryParse(node["Gender"].InnerText, out intVal))
                {
                    board.Gender = intVal;
                }
                List<string> availableOnlyToPlayerUserNames = new List<string>();
                XmlNodeList playerNodes = node["AvailableOnlyToPlayerUserNames"].ChildNodes;
                foreach (XmlNode playerNode in playerNodes)
                {
                    availableOnlyToPlayerUserNames.Add(playerNode.InnerText);
                }

                board.AvailableOnlyToPlayerUserNames = availableOnlyToPlayerUserNames.ToArray();
                board.CurrentTime = node["CurrentTime"].InnerText;

                long rewardPoints = -1;
                if (long.TryParse(node["TotalRewardPoints"].InnerText, out rewardPoints))
                {
                    board.TotalRewardPoints = rewardPoints;
                }

                if (bool.TryParse(node["IsSponsored"].InnerText, out boolVal))
                {
                    board.IsSponsored = boolVal;
                }
                board.Extras = node["Extras"].InnerText;

                long topWinnersCount = 0;
                if (long.TryParse(node["TopWinnersCount"].InnerText, out topWinnersCount))
                {
                    board.TopWinnersCount = topWinnersCount;
                }
                board.Tag = node["Tag"].InnerText;
                board.AdminName = node["AdminName"].InnerText;

                long lifetime = 0;
                board.LifeTime = lifetime;
                board.UseAutoClose = false;
                boolVal = false;
                if (bool.TryParse(node["UseAutoClose"].InnerText, out boolVal))
                {
                    board.UseAutoClose = boolVal;
                }
                if (long.TryParse(node["LifeTime"].InnerText, out lifetime))
                {
                    board.LifeTime = lifetime;
                }
                if (bool.TryParse(node["InviteOnly"].InnerText, out boolVal))
                {
                    board.InviteOnly = boolVal;
                }

                return board;
            }
        }
        catch (Exception ex)
        {
            errorString = ex.Message;
        }
        return null;
    }

    LeaderboardDetail ToLeaderboardDetail(string result, ref string errorString)
    {
        try
        {
            if (!string.IsNullOrEmpty(result))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(result);
                XmlNodeList nodes = xmlDoc.GetElementsByTagName("LeaderboardDetail");
                if (nodes.Count > 0)
                {
                    XmlNode node = nodes.Item(0);
                    return ToLeaderboardDetail(node, ref errorString);
                }
            }
        }
        catch (Exception ex)
        {
            errorString = ex.Message;
        }
        return null;
    }

    LeaderboardDetail[] ToLeaderboardsDetail(string result, ref string errorString)
    {
        List<LeaderboardDetail> boards = new List<LeaderboardDetail>();
        try
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(result);
            XmlNodeList nodes = xmlDoc.GetElementsByTagName("LeaderboardDetail");

            foreach (XmlNode boardNode in nodes)
            {
                LeaderboardDetail board = ToLeaderboardDetail(boardNode, ref errorString);
                if (board != null)
                {
                    boards.Add(board);
                }
            }
        }
        catch (Exception ex)
        {
            errorString = ex.Message;
        }
        return boards.ToArray(); ;
    }

    LeaderboardStatus ToLeaderboardStatus(XmlNode node, ref string errorString)
    {
        try
        {
            LeaderboardStatus board = null;
            if (node != null && !string.IsNullOrEmpty(node.InnerXml))
            {
                board = new LeaderboardStatus();
                board.Id = node["Id"].InnerText;
                board.Name = node["Name"].InnerText;
                board.Comment = node["Comment"].InnerText;
                board.Country = node["Country"].InnerText;
                board.Region = node["Region"].InnerText;
                board.City = node["City"].InnerText;
                board.GameId = node["GameId"].InnerText;
                board.ParentLevelId = node["ParentLevelId"].InnerText;
                bool boolVal = false;
                bool.TryParse(node["Active"].InnerText, out boolVal);
                board.Active = boolVal;

                bool.TryParse(node["Live"].InnerText, out boolVal);
                board.Live = boolVal;

                long longVal = -1;
                List<string> playerUserNames = new List<string>();
                XmlNodeList playerNodes = node["PlayerUserNames"].ChildNodes;
                foreach (XmlNode playerNode in playerNodes)
                {
                    playerUserNames.Add(playerNode.InnerText);
                }

                board.PlayerUserNames = playerUserNames.ToArray();

                List<double> scores = new List<double>();
                playerNodes = node["Scores"].ChildNodes;

                foreach (XmlNode playerScoreNode in playerNodes)
                {
                    scores.Add(Convert.ToDouble(playerScoreNode.InnerText));
                }

                board.Scores = scores.ToArray();
                long.TryParse(node["PlayerCountLimit"].InnerText, out longVal);
                board.PlayerCountLimit = longVal;

                board.RestrictByDateOfBirthStart = node["RestrictByDateOfBirthStart"].InnerText;
                board.RestrictByDateOfBirthEnd = node["RestrictByDateOfBirthEnd"].InnerText;
                if (bool.TryParse(node["UseRestrictByDateOfBirthStart"].InnerText, out boolVal))
                {
                    board.UseRestrictByDateOfBirthStart = boolVal;
                }
                if (bool.TryParse(node["UseRestrictByDateOfBirthEnd"].InnerText, out boolVal))
                {
                    board.UseRestrictByDateOfBirthEnd = boolVal;
                }
                board.Reference = node["Reference"].InnerText;
                int intVal = -1;
                if (int.TryParse(node["Gender"].InnerText, out intVal))
                {
                    board.Gender = intVal;
                }
                List<string> availableOnlyToPlayerUserNames = new List<string>();
                playerNodes = node["AvailableOnlyToPlayerUserNames"].ChildNodes;
                foreach (XmlNode playerNode in playerNodes)
                {
                    availableOnlyToPlayerUserNames.Add(playerNode.InnerText);
                }

                board.AvailableOnlyToPlayerUserNames = availableOnlyToPlayerUserNames.ToArray();
                board.ClosedTime = node["ClosedTime"].InnerText;
                board.CreatedTime = node["CreatedTime"].InnerText;
                board.CurrentTime = node["CurrentTime"].InnerText;

                long rewardPoints = -1;
                if (long.TryParse(node["TotalRewardPoints"].InnerText, out rewardPoints))
                {
                    board.TotalRewardPoints = rewardPoints;
                }


                if (bool.TryParse(node["IsSponsored"].InnerText, out boolVal))
                {
                    board.IsSponsored = boolVal;
                }
                board.Extras = node["Extras"].InnerText;

                long topWinnersCount = 0;
                if (long.TryParse(node["TopWinnersCount"].InnerText, out topWinnersCount))
                {
                    board.TopWinnersCount = topWinnersCount;
                }

                board.Tag = node["Tag"].InnerText;
                board.AdminName = node["AdminName"].InnerText;

                long lifetime = 0;
                board.LifeTime = lifetime;
                board.UseAutoClose = false;
                boolVal = false;
                if (bool.TryParse(node["UseAutoClose"].InnerText, out boolVal))
                {
                    board.UseAutoClose = boolVal;
                }
                if (long.TryParse(node["LifeTime"].InnerText, out lifetime))
                {
                    board.LifeTime = lifetime;
                }
                if (bool.TryParse(node["InviteOnly"].InnerText, out boolVal))
                {
                    board.InviteOnly = boolVal;
                }
                return board;
            }
        }
        catch (Exception ex)
        {
            errorString = ex.Message;
        }
        return null;
    }

    LeaderboardStatus ToLeaderboardStatus(string result, ref string errorString)
    {
        try
        {
            if (!string.IsNullOrEmpty(result))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(result);
                XmlNodeList nodes = xmlDoc.GetElementsByTagName("LeaderboardStatus");
                if (nodes.Count > 0)
                {
                    XmlNode node = nodes.Item(0);
                    return ToLeaderboardStatus(node, ref errorString);
                }
            }
        }
        catch (Exception ex)
        {
            errorString = ex.Message;
        }
        return null;
    }

    BannerDetail ToBannerDetail(string result, ref string errorString)
    {
        try
        {
            if (!string.IsNullOrEmpty(result))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(result);
                XmlNodeList nodes = xmlDoc.GetElementsByTagName("BannerDetail");
                if (nodes.Count > 0)
                {
                    XmlNode node = nodes.Item(0);
                    return ToBannerDetail(node, ref errorString);
                }
            }
        }
        catch (Exception ex)
        {
            errorString = ex.Message;
        }
        return null;
    }

    BannerDetail ToBannerDetail(XmlNode node, ref string errorString)
    {
        try
        {
            BannerDetail banner = null;
            if (node != null && !string.IsNullOrEmpty(node.InnerXml))
            {
                banner = new BannerDetail();
                banner.BannerUrl = node["BannerUrl"].InnerText;
                banner.BannerCategoryType = node["BannerCategoryType"].InnerText;
                banner.BannerHeight = node["BannerHeight"].InnerText;
                banner.BannerWidth = node["BannerWidth"].InnerText;
                banner.BannerClickUrl = node["BannerClickUrl"].InnerText;
                banner.LeaderboardId = node["LeaderboardId"].InnerText;
                banner.InterstitialClickUrl = node["InterstitialClickUrl"].InnerText;
                banner.InterstitialLandscapeUrl = node["InterstitialLandscapeUrl"].InnerText;
                banner.InterstitialPotraitUrl = node["InterstitialPotraitUrl"].InnerText;
                return banner;
            }
        }
        catch (Exception ex)
        {
            errorString = ex.Message;
        }
        return null;
    }

    VoucherDetail ToVoucherDetail(string result, ref string errorString)
    {
        try
        {
            if (!string.IsNullOrEmpty(result))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(result);
                XmlNodeList nodes = xmlDoc.GetElementsByTagName("VoucherDetail");
                if (nodes.Count > 0)
                {
                    XmlNode node = nodes.Item(0);
                    return ToVoucherDetail(node, ref errorString);
                }
            }
        }
        catch (Exception ex)
        {
            errorString = ex.Message;
        }
        return null;
    }

    VoucherDetail ToVoucherDetail(XmlNode node, ref string errorString)
    {
        try
        {
            VoucherDetail voucher = null;
            if (node != null && !string.IsNullOrEmpty(node.InnerXml))
            {
                voucher = new VoucherDetail();
                voucher.ReturnString = node["ReturnString"].InnerText;
                voucher.AvailableVouchers = node["AvailableVouchers"].InnerText;
                voucher.TransactionDate = node["TransactionDate"].InnerText;
                return voucher;
            }
        }
        catch (Exception ex)
        {
            errorString = ex.Message;
        }
        return null;
    }

    GameDetail ToGameDetail(string result, ref string errorString)
    {
        try
        {
            if (!string.IsNullOrEmpty(result))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(result);
                XmlNodeList nodes = xmlDoc.GetElementsByTagName("GameDetail");
                if (nodes.Count > 0)
                {
                    XmlNode node = nodes.Item(0);
                    return ToGameDetail(node, ref errorString);
                }
            }
        }
        catch (Exception ex)
        {
            errorString = ex.Message;
        }
        return null;
    }

    GameDetail[] ToGameDetails(string result, ref string errorString)
    {
        List<GameDetail> games = new List<GameDetail>();
        try
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(result);
            XmlNodeList nodes = xmlDoc.GetElementsByTagName("GameDetail");

            foreach (XmlNode gameNode in nodes)
            {
                GameDetail game = ToGameDetail(gameNode, ref errorString);
                if (game != null)
                {
                    games.Add(game);
                }
            }
        }
        catch (Exception ex)
        {
            errorString = ex.Message;
        }
        return games.ToArray(); ;
    }

    GameDetail ToGameDetail(XmlNode node, ref string errorString)
    {
        try
        {
            GameDetail game = null;
            if (node != null && !string.IsNullOrEmpty(node.InnerXml))
            {
                game = new GameDetail();
                game.Id = node["Id"].InnerText;
                game.Name = node["Name"].InnerText;
                game.Comment = node["Comment"].InnerText;
                bool boolVal = false;
                if (bool.TryParse(node["Active"].InnerText, out boolVal))
                {
                    game.Active = boolVal;
                }

                game.CreatedDate = node["CreatedDate"].InnerText;

                if (bool.TryParse(node["IsAvailableForSponsorship"].InnerText, out boolVal))
                {
                    game.IsAvailableForSponsorship = boolVal;
                }
                float floatVal = 0;

                if (float.TryParse(node["RewardPercentage"].InnerText, out floatVal))
                {
                    game.RewardPercentage = floatVal;
                }

                if (float.TryParse(node["TopWinnersPercentage"].InnerText, out floatVal))
                {
                    game.TopWinnersPercentage = floatVal;
                }

                if (float.TryParse(node["DeveloperPercentage"].InnerText, out floatVal))
                {
                    game.DeveloperPercentage = floatVal;
                }

                game.Extras = node["Extras"].InnerText;
                game.FullDescription = node["FullDescription"].InnerText;

                long longVal = 0;
                if (long.TryParse(node["TotalPlayersParticipated"].InnerText, out longVal))
                {
                    game.TotalPlayersParticipated = longVal;
                }

                List<string> urls = new List<string>();
                XmlNodeList urlNodes = node["DistributionUrls"].ChildNodes;

                foreach (XmlNode urlNode in urlNodes)
                {
                    urls.Add(urlNode.InnerText);
                }
                game.DistributionUrls = urls.ToArray();
                game.GameLogoUrl = node["GameLogoUrl"].InnerText;

                longVal = 0;
                if (long.TryParse(node["LeaderboardAdminSharePercent"].InnerText, out longVal))
                {
                    game.LeaderboardAdminSharePercent = longVal;
                }

                longVal = 0;
                if (long.TryParse(node["LeaderboardPlayershare"].InnerText, out longVal))
                {
                    game.LeaderboardPlayershare = longVal;
                }
                game.GameDescriptionLink = node["GameDescriptionLink"].InnerText;
                return game;
            }
        }
        catch (Exception ex)
        {
            errorString = ex.Message;
        }
        return null;
    }

    RankDetail ToRankDetail(string result, ref string errorString)
    {
        try
        {
            if (!string.IsNullOrEmpty(result))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(result);
                XmlNodeList nodes = xmlDoc.GetElementsByTagName("RankDetail");
                if (nodes.Count > 0)
                {
                    XmlNode node = nodes.Item(0);
                    return ToRankDetail(node, ref errorString);
                }
            }
        }
        catch (Exception ex)
        {
            errorString = ex.Message;
        }
        return null;
    }

    RankDetail ToRankDetail(XmlNode node, ref string errorString)
    {
        try
        {
            RankDetail rank = null;
            if (node != null)
            {
                rank = new RankDetail();
                rank.PlayerUsername = node["PlayerUsername"].InnerText;
                rank.LeaderboardId = node["LeaderboardId"].InnerText;
                long longVal = -1;
                long.TryParse(node["Rank"].InnerText, out longVal);

                double scoreVal = -1;
                double.TryParse(node["Score"].InnerText, out scoreVal);
                rank.Score = scoreVal;
                rank.Rank = longVal;
                return rank;
            }
        }
        catch (Exception ex)
        {
            errorString = ex.Message;
        }
        return null;
    }

    //homo
    public delegate void IGLBoolResult(string errorString, bool result, params object[] userParams);
    public delegate void IGLStringResult(string errorString, string result, params object[] userParam);
    public delegate void IGLLongResult(string errorString, long result, params object[] userParam);
    public delegate void OnGetPlayerDetail(string errorString, PlayerDetail result, params object[] userParam);
    public delegate void OnGetLeaderboardDetail(string errorString, LeaderboardDetail result, params object[] userParam);
    public delegate void OnGetLeaderboardsDetail(string errorString, LeaderboardDetail[] result, params object[] userParam);
    public delegate void OnGetLeaderboardStatus(string errorString, LeaderboardStatus result, params object[] userParam);
    public delegate void OnGetGameLevelDetails(string errorString, GameLevelDetail[] result, params object[] userParam);
    public delegate void OnGetGameLevelDetail(string errorString, GameLevelDetail result, params object[] userParam);
    public delegate void OnGetBannerDetails(string errorString, BannerDetail result, params object[] userParam);
    public delegate void OnGetVoucherDetails(string errorString, VoucherDetail result, params object[] userParam);
    public delegate void OnGetGameDetails(string errorString, GameDetail[] result, params object[] userParam);
    public delegate void OnGetGamesDetail(string errorString, GameDetail result, params object[] userParam);
    public delegate void OnGetRankDetails(string errorString, RankDetail result, params object[] userParam);


    public IEnumerator UpdatePlayerScore(
        string userName,
        string participationKey,
        string leaderBoardId,
        double score,
        bool updateIfBetterThanPrevious,
        IGLBoolResult onUpdatePlayerScoreFn,
        params object[] userParams)
    {
        string errorString = "";
        bool result = false;

        Dictionary<string, string> paramters = new Dictionary<string, string>();
        paramters.Add("developerUserId", m_DeveloperId);
        paramters.Add("integrationKey", m_IntegrationKey);
        paramters.Add("playerUserId", userName);
        paramters.Add("participationKey", participationKey);
        paramters.Add("leaderboardId", leaderBoardId);
        paramters.Add("score", score.ToString());
        paramters.Add("updateIfBetterThanPrevious", updateIfBetterThanPrevious.ToString());
        WWW w = POST(GetUrl("UpdatePlayerScore"), ref paramters);

        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            if (!string.IsNullOrEmpty(w.text))
            {
                result = ToBooleanResult(w.text, ref errorString);
            }
        }
        else
        {
            errorString = w.error;
        }

        onUpdatePlayerScoreFn(errorString, result, userParams);
    }

    public IEnumerator ValidateParticipation(string userName, string participationKey, string leaderBoardId, IGLBoolResult onAcceptPlayerParticipation, params object[] userParam)
    {
        string errorString = "";
        bool result = false;

        Dictionary<string, string> paramters = new Dictionary<string, string>();
        paramters.Add("developerUserId", m_DeveloperId);
        paramters.Add("integrationKey", m_IntegrationKey);
        paramters.Add("playerUserId", userName);
        paramters.Add("participationKey", participationKey);
        paramters.Add("leaderboardId", leaderBoardId);
        WWW w = POST(GetUrl("ValidateParticipation"), ref paramters);

        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            if (!string.IsNullOrEmpty(w.text))
            {
                result = ToBooleanResult(w.text, ref errorString);
            }
        }
        else
        {
            errorString = w.error;
        }

        onAcceptPlayerParticipation(errorString, result, userParam);
    }

    public IEnumerator GetPlayerParticipationTypeToJoin(string userName, string leaderBoardId, IGLBoolResult onGetPlayerParticipationType, params object[] userParam)
    {
        string errorString = "";
        bool result = false;

        Dictionary<string, string> paramters = new Dictionary<string, string>();
        paramters.Add("developerUserId", m_DeveloperId);
        paramters.Add("integrationKey", m_IntegrationKey);
        paramters.Add("playerUserId", userName);
        paramters.Add("leaderboardId", leaderBoardId);
        WWW w = POST(GetUrl("GetPlayerParticipationTypeToJoin"), ref paramters);

        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            if (!string.IsNullOrEmpty(w.text))
            {
                result = ToBooleanResult(w.text, ref errorString);
            }
        }
        else
        {
            errorString = w.error;
        }

        onGetPlayerParticipationType(errorString, result, userParam);
    }

    public IEnumerator CloseLeaderboard(string leaderBoardId, IGLBoolResult onCloseLeaderboard, params object[] userParam)
    {
        string errorString = "";
        bool result = false;

        Dictionary<string, string> paramters = new Dictionary<string, string>();
        paramters.Add("developerUserId", m_DeveloperId);
        paramters.Add("integrationKey", m_IntegrationKey);
        paramters.Add("leaderboardId", leaderBoardId);
        WWW w = POST(GetUrl("CloseLeaderboard"), ref paramters);

        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            if (!string.IsNullOrEmpty(w.text))
            {
                result = ToBooleanResult(w.text, ref errorString);
            }
        }
        else
        {
            errorString = w.error;
        }

        onCloseLeaderboard(errorString, result, userParam);
    }

    public IEnumerator BulkCloseLeaderboardsInGame(string gameId, double durationInMinutes, IGLBoolResult onBulkCloseLeaderboardsInGame, params object[] userParam)
    {
        string errorString = "";
        bool result = false;

        Dictionary<string, string> paramters = new Dictionary<string, string>();
        paramters.Add("developerUserId", m_DeveloperId);
        paramters.Add("integrationKey", m_IntegrationKey);
        paramters.Add("gameId", gameId);
        paramters.Add("durationInMinutes", durationInMinutes.ToString());
        WWW w = POST(GetUrl("BulkCloseLeaderboardsInGame"), ref paramters);

        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            if (!string.IsNullOrEmpty(w.text))
            {
                result = ToBooleanResult(w.text, ref errorString);
            }
        }
        else
        {
            errorString = w.error;
        }

        onBulkCloseLeaderboardsInGame(errorString, result, userParam);
    }

    public IEnumerator BulkCloseLeaderboardsInLevel(string levelId, double durationInMinutes, IGLBoolResult BulkCloseLeaderboardsInLevel, params object[] userParam)
    {
        string errorString = "";
        bool result = false;

        Dictionary<string, string> paramters = new Dictionary<string, string>();
        paramters.Add("developerUserId", m_DeveloperId);
        paramters.Add("integrationKey", m_IntegrationKey);
        paramters.Add("levelId", levelId);
        paramters.Add("durationInMinutes", durationInMinutes.ToString());
        WWW w = POST(GetUrl("BulkCloseLeaderboardsInLevel"), ref paramters);

        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            if (!string.IsNullOrEmpty(w.text))
            {
                result = ToBooleanResult(w.text, ref errorString);
            }
        }
        else
        {
            errorString = w.error;
        }

        BulkCloseLeaderboardsInLevel(errorString, result, userParam);
    }

    public IEnumerator MakeLeaderboardLive(string leaderBoardId, IGLBoolResult onMakeLeaderboardLive, params object[] userParam)
    {
        string errorString = "";
        bool result = false;

        Dictionary<string, string> paramters = new Dictionary<string, string>();
        paramters.Add("developerUserId", m_DeveloperId);
        paramters.Add("integrationKey", m_IntegrationKey);
        paramters.Add("leaderboardId", leaderBoardId);
        WWW w = POST(GetUrl("MakeLeaderboardLive"), ref paramters);

        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            if (!string.IsNullOrEmpty(w.text))
            {
                result = ToBooleanResult(w.text, ref errorString);
            }
        }
        else
        {
            errorString = w.error;
        }

        onMakeLeaderboardLive(errorString, result, userParam);
    }

    public IEnumerator IsVoucherProgramAvailable(string voucherProgramRefId, IGLBoolResult onIsVoucherProgramAvailable, params object[] userParam)
    {
        string errorString = "";
        bool result = false;

        Dictionary<string, string> paramters = new Dictionary<string, string>();
        paramters.Add("donorUserName", m_DeveloperId);
        paramters.Add("donorPassword", m_IntegrationKey);
        paramters.Add("voucherProgramRefId", voucherProgramRefId);
        WWW w = POST(GetUrl("IsVoucherProgramAvailable"), ref paramters);

        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            if (!string.IsNullOrEmpty(w.text))
            {
                result = ToBooleanResult(w.text, ref errorString);
            }
        }
        else
        {
            errorString = w.error;
        }

        onIsVoucherProgramAvailable(errorString, result, userParam);
    }

    public IEnumerator IsActivePlayer(string userName, IGLBoolResult onIsActivePlayer, params object[] userParam)
    {
        string errorString = "";
        bool result = false;

        Dictionary<string, string> paramters = new Dictionary<string, string>();
        paramters.Add("developerUserId", m_DeveloperId);
        paramters.Add("integrationKey", m_IntegrationKey);
        paramters.Add("playerUserId", userName);
        WWW w = POST(GetUrl("IsActivePlayer"), ref paramters);

        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            if (!string.IsNullOrEmpty(w.text))
            {
                result = ToBooleanResult(w.text, ref errorString);
            }
        }
        else
        {
            errorString = w.error;
        }

        onIsActivePlayer(errorString, result, userParam);
    }


    public IEnumerator GetPlayerDetail(string userName, OnGetPlayerDetail onGetPlayerDetail, params object[] userParam)
    {
        string errorString = "";
        PlayerDetail result = null;

        Dictionary<string, string> paramters = new Dictionary<string, string>();
        paramters.Add("developerUserId", m_DeveloperId);
        paramters.Add("integrationKey", m_IntegrationKey);
        paramters.Add("playerUserId", userName);
        WWW w = POST(GetUrl("GetPlayerDetail"), ref paramters);

        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            if (!string.IsNullOrEmpty(w.text))
            {
                result = ToPlayerDetail(w.text, ref errorString);
            }
        }
        else
        {
            errorString = w.error;
        }

        onGetPlayerDetail(errorString, result, userParam);
    }


    public IEnumerator GetGameLevelDetails(string levelId, OnGetGameLevelDetail onGetGameLevelDetail, params object[] userParam)
    {
        string errorString = "";
        GameLevelDetail result = null;

        Dictionary<string, string> paramters = new Dictionary<string, string>();
        paramters.Add("developerUserId", m_DeveloperId);
        paramters.Add("integrationKey", m_IntegrationKey);
        paramters.Add("levelId", levelId);
        WWW w = POST(GetUrl("GetGameLevelDetails"), ref paramters);

        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            if (!string.IsNullOrEmpty(w.text))
            {
                result = ToGameLevelDetail(w.text, ref errorString);
            }
        }
        else
        {
            errorString = w.error;
        }

        onGetGameLevelDetail(errorString, result, userParam);
    }


    public IEnumerator GetSubGameLevelsAvailableToPlayer(
        string playerUserId,
        string levelId,
        int sortType,
        long startIndex,
        long endIndex,
        OnGetGameLevelDetails onGetGameLevelDetails,
        params object[] userParam)
    {
        string errorString = "";
        GameLevelDetail[] result = null;

        Dictionary<string, string> paramters = new Dictionary<string, string>();
        paramters.Add("developerUserId", m_DeveloperId);
        paramters.Add("integrationKey", m_IntegrationKey);
        paramters.Add("playerUserId", playerUserId);
        paramters.Add("levelId", levelId);
        paramters.Add("sortType", sortType.ToString());
        paramters.Add("startIndex", startIndex.ToString());
        paramters.Add("endIndex", endIndex.ToString());
        WWW w = POST(GetUrl("GetSubGameLevelsAvailableToPlayer"), ref paramters);

        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            if (!string.IsNullOrEmpty(w.text))
            {
                result = ToGameLevelsDetail(w.text, ref errorString);
            }
        }
        else
        {
            errorString = w.error;
        }

        onGetGameLevelDetails(errorString, result, userParam);
    }

    public IEnumerator GetGameLevelsAvailableToPlayer(string userName, string gameId, int levelSortType, long startIndex, long endIndex, OnGetGameLevelDetails onGetGameLevelDetails, params object[] userParam)
    {
        string errorString = "";
        GameLevelDetail[] result = null;
        Dictionary<string, string> paramters = new Dictionary<string, string>();
        paramters.Add("developerUserId", m_DeveloperId);
        paramters.Add("integrationKey", m_IntegrationKey);
        paramters.Add("playerUserId", userName);
        paramters.Add("gameId", gameId);
        paramters.Add("levelSortType", levelSortType.ToString());
        paramters.Add("startIndex", startIndex.ToString());
        paramters.Add("endIndex", endIndex.ToString());
        WWW w = POST(GetUrl("GetGameLevelsAvailableToPlayer"), ref paramters);

        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            if (!string.IsNullOrEmpty(w.text))
            {
                result = ToGameLevelsDetail(w.text, ref errorString);
            }
        }
        else
        {
            errorString = w.error;
        }

        onGetGameLevelDetails(errorString, result, userParam);
    }

    public IEnumerator GetAllGameLevelsAvailableToPlayer(string userName, string gameId, int sortType, long startIndex, long endIndex, OnGetGameLevelDetails onGetGameLevelDetails, params object[] userParam)
    {
        string errorString = "";
        GameLevelDetail[] result = null;

        Dictionary<string, string> paramters = new Dictionary<string, string>();
        paramters.Add("developerUserId", m_DeveloperId);
        paramters.Add("integrationKey", m_IntegrationKey);
        paramters.Add("playerUserId", userName);
        paramters.Add("gameId", gameId);
        paramters.Add("sortType", sortType.ToString());
        paramters.Add("startIndex", startIndex.ToString());
        paramters.Add("endIndex", endIndex.ToString());
        WWW w = POST(GetUrl("GetAllGameLevelsAvailableToPlayer"), ref paramters);

        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            if (!string.IsNullOrEmpty(w.text))
            {
                result = ToGameLevelsDetail(w.text, ref errorString);
            }
        }
        else
        {
            errorString = w.error;
        }

        onGetGameLevelDetails(errorString, result, userParam);
    }


    public IEnumerator GetLeaderboardsByTagFromGame(
        string userName,
        string gameId,
        string tag,
        bool activeOnly,
        long startIndex,
        long endIndex,
        OnGetLeaderboardsDetail onGetLeaderboardDetail,
        params object[] userParam)
    {
        string errorString = "";
        LeaderboardDetail[] result = null;

        Dictionary<string, string> paramters = new Dictionary<string, string>();
        paramters.Add("developerUserId", m_DeveloperId);
        paramters.Add("integrationKey", m_IntegrationKey);
        paramters.Add("gameId", gameId);
        paramters.Add("tag", tag);
        paramters.Add("activeOnly", activeOnly.ToString());
        paramters.Add("startIndex", startIndex.ToString());
        paramters.Add("endIndex", endIndex.ToString());
        WWW w = POST(GetUrl("GetLeaderboardsByTagFromGame"), ref paramters);

        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            if (!string.IsNullOrEmpty(w.text))
            {
                result = ToLeaderboardsDetail(w.text, ref errorString);
            }
        }
        else
        {
            errorString = w.error;
        }

        onGetLeaderboardDetail(errorString, result, userParam);
    }

    public IEnumerator GetLeaderboardsByTag(string userName, string levelId, string tag, bool activeOnly, long startIndex, long endIndex, OnGetLeaderboardsDetail onGetLeaderboardDetail, params object[] userParam)
    {
        string errorString = "";
        LeaderboardDetail[] result = null;

        Dictionary<string, string> paramters = new Dictionary<string, string>();
        paramters.Add("developerUserId", m_DeveloperId);
        paramters.Add("integrationKey", m_IntegrationKey);
        paramters.Add("levelId", levelId);
        paramters.Add("tag", tag);
        paramters.Add("activeOnly", activeOnly.ToString());
        paramters.Add("startIndex", startIndex.ToString());
        paramters.Add("endIndex", endIndex.ToString());
        WWW w = POST(GetUrl("GetLeaderboardsByTag"), ref paramters);

        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            if (!string.IsNullOrEmpty(w.text))
            {
                result = ToLeaderboardsDetail(w.text, ref errorString);
            }
        }
        else
        {
            errorString = w.error;
        }

        onGetLeaderboardDetail(errorString, result, userParam);
    }


    public IEnumerator GetLeaderboardsAvailableToPlayer(string userName, string levelId, int sortBy, int sortOrder, long startIndex, long endIndex, OnGetLeaderboardsDetail onGetLeaderboardDetail, params object[] userParam)
    {
        string errorString = "";
        LeaderboardDetail[] result = null;

        Dictionary<string, string> paramters = new Dictionary<string, string>();
        paramters.Add("developerUserId", m_DeveloperId);
        paramters.Add("integrationKey", m_IntegrationKey);
        paramters.Add("playerUserId", userName);
        paramters.Add("levelId", levelId);
        paramters.Add("sortBy", sortBy.ToString());
        paramters.Add("sortOrder", sortOrder.ToString());
        paramters.Add("startIndex", startIndex.ToString());
        paramters.Add("endIndex", endIndex.ToString());
        WWW w = POST(GetUrl("GetLeaderboardsAvailableToPlayer"), ref paramters);

        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            if (!string.IsNullOrEmpty(w.text))
            {
                result = ToLeaderboardsDetail(w.text, ref errorString);
            }
        }
        else
        {
            errorString = w.error;
        }

        onGetLeaderboardDetail(errorString, result, userParam);
    }

    public IEnumerator GetBestLeaderboardsForPlayerToJoin(string userName, string levelId, int sortBy, int sortOrder, double[] scoreHints, long startIndex, long endIndex, OnGetLeaderboardsDetail onGetLeaderboardDetail, params object[] userParam)
    {
        string errorString = "";
        LeaderboardDetail[] result = null;

        Dictionary<string, string> paramters = new Dictionary<string, string>();
        paramters.Add("developerUserId", m_DeveloperId);
        paramters.Add("integrationKey", m_IntegrationKey);
        paramters.Add("playerUserId", userName);
        paramters.Add("levelId", levelId);
        paramters.Add("sortBy", sortBy.ToString());
        paramters.Add("sortOrder", sortOrder.ToString());
        StringBuilder scoreHintsStr = new StringBuilder();
        int i = 0;
        for (i = 0; i < scoreHints.Length; i++)
        {
            if (i != scoreHints.Length - 1)
            {
                scoreHintsStr.AppendFormat("{0},", scoreHints[i].ToString());
            }
            else
            {
                scoreHintsStr.AppendFormat("{0}", scoreHints[i].ToString());
            }
        }
        paramters.Add("scoreHints", scoreHintsStr.ToString());
        paramters.Add("startIndex", startIndex.ToString());
        paramters.Add("endIndex", endIndex.ToString());
        WWW w = POST(GetUrl("GetBestLeaderboardsForPlayerToJoin"), ref paramters);

        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            if (!string.IsNullOrEmpty(w.text))
            {
                result = ToLeaderboardsDetail(w.text, ref errorString);
            }
        }
        else
        {
            errorString = w.error;
        }

        onGetLeaderboardDetail(errorString, result, userParam);
    }

    public IEnumerator GetAllBestLeaderboardsForPlayerToJoin(string userName, string gameId, int sortBy, int sortOrder, double[] scoreHints, long startIndex, long endIndex, OnGetLeaderboardsDetail onGetLeaderboardDetail, params object[] userParam)
    {
        string errorString = "";
        LeaderboardDetail[] result = null;

        Dictionary<string, string> paramters = new Dictionary<string, string>();
        paramters.Add("developerUserId", m_DeveloperId);
        paramters.Add("integrationKey", m_IntegrationKey);
        paramters.Add("playerUserId", userName);
        paramters.Add("gameId", gameId);
        paramters.Add("sortBy", sortBy.ToString());
        paramters.Add("sortOrder", sortOrder.ToString());
        StringBuilder scoreHintsStr = new StringBuilder();
        int i = 0;
        for (i = 0; i < scoreHints.Length; i++)
        {
            if (i != scoreHints.Length - 1)
            {
                scoreHintsStr.AppendFormat("{0},", scoreHints[i].ToString());
            }
            else
            {
                scoreHintsStr.AppendFormat("{0}", scoreHints[i].ToString());
            }
        }
        paramters.Add("scoreHints", scoreHintsStr.ToString());
        paramters.Add("startIndex", startIndex.ToString());
        paramters.Add("endIndex", endIndex.ToString());
        WWW w = POST(GetUrl("GetAllBestLeaderboardsForPlayerToJoin"), ref paramters);

        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            if (!string.IsNullOrEmpty(w.text))
            {
                result = ToLeaderboardsDetail(w.text, ref errorString);
            }
        }
        else
        {
            errorString = w.error;
        }

        onGetLeaderboardDetail(errorString, result, userParam);
    }

    public IEnumerator GetAllLeaderboardsForPlayerToJoin(
        string userName,
        string gameId,
        int sortBy,
        int sortOrder,
        bool inviteOnly,
        long startIndex,
        long endIndex,
        OnGetLeaderboardsDetail
        onGetLeaderboardDetail,
        params object[] userParam)
    {
        string errorString = "";
        LeaderboardDetail[] result = null;

        Dictionary<string, string> paramters = new Dictionary<string, string>();
        paramters.Add("developerUserId", m_DeveloperId);
        paramters.Add("integrationKey", m_IntegrationKey);
        paramters.Add("playerUserId", userName);
        paramters.Add("gameId", gameId);
        paramters.Add("sortBy", sortBy.ToString());
        paramters.Add("sortOrder", sortOrder.ToString());
        paramters.Add("inviteOnly", inviteOnly.ToString());
        paramters.Add("startIndex", startIndex.ToString());
        paramters.Add("endIndex", endIndex.ToString());

        WWW w = POST(GetUrl("GetAllLeaderboardsForPlayerToJoin"), ref paramters);

        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            if (!string.IsNullOrEmpty(w.text))
            {
                result = ToLeaderboardsDetail(w.text, ref errorString);
            }
        }
        else
        {
            errorString = w.error;
        }

        onGetLeaderboardDetail(errorString, result, userParam);
    }

    public IEnumerator GetLeaderboardsForGameLevel(string userName, string levelId, bool activeOnly, bool inviteOnly, int sortBy, int sortOrder, long startIndex, long endIndex, OnGetLeaderboardsDetail onGetLeaderboardDetail, params object[] userParam)
    {
        string errorString = "";
        LeaderboardDetail[] result = null;

        Dictionary<string, string> paramters = new Dictionary<string, string>();
        paramters.Add("developerUserId", m_DeveloperId);
        paramters.Add("integrationKey", m_IntegrationKey);
        paramters.Add("levelId", levelId);
        paramters.Add("activeOnly", activeOnly.ToString());
        paramters.Add("inviteOnly", inviteOnly.ToString());
        paramters.Add("sortBy", sortBy.ToString());
        paramters.Add("sortOrder", sortOrder.ToString());
        paramters.Add("startIndex", startIndex.ToString());
        paramters.Add("endIndex", endIndex.ToString());

        WWW w = POST(GetUrl("GetLeaderboardsForGameLevel"), ref paramters);

        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            if (!string.IsNullOrEmpty(w.text))
            {
                result = ToLeaderboardsDetail(w.text, ref errorString);
            }
        }
        else
        {
            errorString = w.error;
        }

        onGetLeaderboardDetail(errorString, result, userParam);
    }

    public IEnumerator IsPlayerParticipatedToTheLeaderboard(string userName, string leaderBoardId, IGLBoolResult onIsPlayerParticipated, params object[] userParam)
    {
        string errorString = "";
        bool result = false;

        Dictionary<string, string> paramters = new Dictionary<string, string>();
        paramters.Add("developerUserId", m_DeveloperId);
        paramters.Add("integrationKey", m_IntegrationKey);
        paramters.Add("playerUserId", userName);
        paramters.Add("leaderboardId", leaderBoardId);
        WWW w = POST(GetUrl("IsPlayerParticipatedToTheLeaderboard"), ref paramters);

        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            if (!string.IsNullOrEmpty(w.text))
            {
                result = ToBooleanResult(w.text, ref errorString);
            }
        }
        else
        {
            errorString = w.error;
        }

        onIsPlayerParticipated(errorString, result, userParam);
    }

    public IEnumerator GetLeaderboardDetails(string leaderboardId, OnGetLeaderboardDetail onGetLeaderboardDetail, params object[] userParam)
    {
        string errorString = "";
        LeaderboardDetail result = null;

        Dictionary<string, string> paramters = new Dictionary<string, string>();
        paramters.Add("developerUserId", m_DeveloperId);
        paramters.Add("integrationKey", m_IntegrationKey);
        paramters.Add("leaderboardId", leaderboardId);


        WWW w = POST(GetUrl("GetLeaderboardDetails"), ref paramters);

        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            if (!string.IsNullOrEmpty(w.text))
            {
                result = ToLeaderboardDetail(w.text, ref errorString);
            }
        }
        else
        {
            errorString = w.error;
        }

        onGetLeaderboardDetail(errorString, result, userParam);
    }


    public IEnumerator GetLeaderboardCurrentStatus(string leaderboardId, long startIndex, long endIndex, OnGetLeaderboardStatus onGetLeaderboardStatus, params object[] userParam)
    {
        string errorString = "";
        LeaderboardStatus result = null;

        Dictionary<string, string> paramters = new Dictionary<string, string>();
        paramters.Add("developerUserId", m_DeveloperId);
        paramters.Add("integrationKey", m_IntegrationKey);
        paramters.Add("leaderboardId", leaderboardId);
        paramters.Add("startIndex", startIndex.ToString());
        paramters.Add("endIndex", endIndex.ToString());
        WWW w = POST(GetUrl("GetLeaderboardCurrentStatus"), ref paramters);

        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            if (!string.IsNullOrEmpty(w.text))
            {
                result = ToLeaderboardStatus(w.text, ref errorString);
            }
        }
        else
        {
            errorString = w.error;
        }

        onGetLeaderboardStatus(errorString, result, userParam);
    }


    public IEnumerator CreateGameLevel(string levelName, string levelComment, long joiningPoint, string extras, string reference, string gameId,
                                        bool isUserTournamentAllowed, bool isAvailableForSponsorship, IGLStringResult onCreateGameLevel, params object[] userParam)
    {
        string errorString = "";
        string result = string.Empty;

        Dictionary<string, string> paramters = new Dictionary<string, string>();
        paramters.Add("developerUserId", m_DeveloperId);
        paramters.Add("integrationKey", m_IntegrationKey);
        paramters.Add("levelName", levelName);
        paramters.Add("levelComment", levelComment);
        paramters.Add("joiningPoint", joiningPoint.ToString());
        paramters.Add("isAvailableForSponsorship", isAvailableForSponsorship.ToString());

        paramters.Add("extras", extras);
        paramters.Add("reference", reference);

        paramters.Add("gameId", gameId);
        paramters.Add("isUserTournamentAllowed", isUserTournamentAllowed.ToString());

        WWW w = POST(GetUrl("CreateGameLevel"), ref paramters);

        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            if (!string.IsNullOrEmpty(w.text))
            {
                result = ToStringResult(w.text, ref errorString);
            }
        }
        else
        {
            errorString = w.error;
        }
        onCreateGameLevel(errorString, result, userParam);
    }

    public IEnumerator CreateSubGameLevel(string levelName, string levelComment, long joiningPoint, string extras, string reference, bool isUserTournamentAllowed,
                                          string levelId, bool isAvailableForSponsorship, IGLStringResult onCreateGameLevel, params object[] userParam)
    {
        string errorString = "";
        string result = string.Empty;

        Dictionary<string, string> paramters = new Dictionary<string, string>();
        paramters.Add("developerUserId", m_DeveloperId);
        paramters.Add("integrationKey", m_IntegrationKey);
        paramters.Add("levelName", levelName);
        paramters.Add("levelComment", levelComment);
        paramters.Add("joiningPoint", joiningPoint.ToString());
        paramters.Add("extras", extras);
        paramters.Add("reference", reference);
        paramters.Add("isUserTournamentAllowed", isUserTournamentAllowed.ToString());
        paramters.Add("levelId", levelId);
        paramters.Add("isAvailableForSponsorship", isAvailableForSponsorship.ToString());

        WWW w = POST(GetUrl("CreateSubGameLevel"), ref paramters);

        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            if (!string.IsNullOrEmpty(w.text))
            {
                result = ToStringResult(w.text, ref errorString);
            }
        }
        else
        {
            errorString = w.error;
        }
        onCreateGameLevel(errorString, result, userParam);
    }


    public IEnumerator AddLeaderboardAdmin(string leaderboardId, string username, IGLStringResult onAddLeaderboardAdmin, params object[] userParam)
    {
        string errorString = "";
        string result = string.Empty;

        Dictionary<string, string> paramters = new Dictionary<string, string>();
        paramters.Add("developerUserId", m_DeveloperId);
        paramters.Add("integrationKey", m_IntegrationKey);
        paramters.Add("leaderboardId", leaderboardId);
        paramters.Add("username", username);

        WWW w = POST(GetUrl("AddLeaderboardAdmin"), ref paramters);

        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            if (!string.IsNullOrEmpty(w.text))
            {
                result = ToStringResult(w.text, ref errorString);
            }
        }
        else
        {
            errorString = w.error;
        }
        onAddLeaderboardAdmin(errorString, result, userParam);
    }


    public IEnumerator GetBannerDetails(string username, string leaderboardId, OnGetBannerDetails onGetBannerDetails, params object[] userParam)
    {
        string errorString = "";
        BannerDetail result = null;

        Dictionary<string, string> paramters = new Dictionary<string, string>();
        paramters.Add("developerUserName", m_DeveloperId);
        paramters.Add("integrationKey", m_IntegrationKey);
        paramters.Add("playerUserName", username);
        paramters.Add("leaderboardId", leaderboardId);

        WWW w = POST(GetUrl("GetBannerDetails"), ref paramters);

        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            if (!string.IsNullOrEmpty(w.text))
            {
                result = ToBannerDetail(w.text, ref errorString);
            }
        }
        else
        {
            errorString = w.error;
        }
        onGetBannerDetails(errorString, result, userParam);
    }

    public IEnumerator CreateLeaderboard(
        string leaderboardName,
        string leaderboardComment,
        long playerCount,
        string levelId,
        string country,
        string region,
        string city,
        string restrictByDateOfBirthStart,
        string restrictByDateOfBirthEnd,
        bool useRestrictByDateOfBirthStart,
        bool useRestrictByDateOfBirthEnd,
        GenderType gender,
        string reference,
        string availableOnlyToPlayerUserNames,
        string extras,
        string createdBy,
        bool useAutoClose,
        long lifeTime,
        bool inviteOnly,
        IGLStringResult onCreateLeaderboard,
        params object[] userParam)
    {
        string errorString = "";
        string result = string.Empty;

        Dictionary<string, string> paramters = new Dictionary<string, string>();
        paramters.Add("developerUserId", m_DeveloperId);
        paramters.Add("integrationKey", m_IntegrationKey);
        paramters.Add("leaderboardName", leaderboardName);
        paramters.Add("leaderboardComment", leaderboardComment);
        paramters.Add("country", country);
        paramters.Add("region", region);
        paramters.Add("city", city);
        paramters.Add("levelId", levelId);
        paramters.Add("playerCount", playerCount.ToString());
        paramters.Add("restrictByDateOfBirthStart", restrictByDateOfBirthStart);
        paramters.Add("restrictByDateOfBirthEnd", restrictByDateOfBirthEnd);
        paramters.Add("useRestrictByDateOfBirthStart", useRestrictByDateOfBirthStart.ToString());
        paramters.Add("useRestrictByDateOfBirthEnd", useRestrictByDateOfBirthEnd.ToString());
        paramters.Add("gender", ((int)gender).ToString());
        paramters.Add("reference", reference);
        paramters.Add("availableOnlyToPlayerUserNames", availableOnlyToPlayerUserNames);
        paramters.Add("extras", extras);
        paramters.Add("createdBy", createdBy);
        paramters.Add("useAutoClose", useAutoClose.ToString());
        paramters.Add("lifeTime", lifeTime.ToString());
        paramters.Add("inviteOnly", inviteOnly.ToString());

        WWW w = POST(GetUrl("CreateLeaderboard"), ref paramters);

        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            if (!string.IsNullOrEmpty(w.text))
            {
                result = ToStringResult(w.text, ref errorString);
            }
        }
        else
        {
            errorString = w.error;
        }
        onCreateLeaderboard(errorString, result, userParam);
    }


    public IEnumerator GetRewardAmountForLeaderboard(string leaderBoardId, IGLLongResult onGetRewardAmount, params object[] userParam)
    {
        string errorString = "";
        long result = -1;

        Dictionary<string, string> paramters = new Dictionary<string, string>();
        paramters.Add("developerUserId", m_DeveloperId);
        paramters.Add("integrationKey", m_IntegrationKey);
        paramters.Add("leaderboardId", leaderBoardId);
        WWW w = POST(GetUrl("GetRewardAmountForLeaderboard"), ref paramters);

        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            if (!string.IsNullOrEmpty(w.text))
            {
                result = ToLongResult(w.text, ref errorString);
            }
        }
        else
        {
            errorString = w.error;
        }

        onGetRewardAmount(errorString, result, userParam);
    }


    public IEnumerator GiveVoucher(string donorUserName, string donorPassword, string voucherProgramRefId,
                                   string receiverUsername, OnGetVoucherDetails onGetVoucherDetails, params object[] userParam)
    {
        string errorString = "";
        VoucherDetail result = null;

        Dictionary<string, string> paramters = new Dictionary<string, string>();
        paramters.Add("donorUserName", donorUserName);
        paramters.Add("donorPassword", donorPassword);
        paramters.Add("voucherProgramRefId", voucherProgramRefId);
        paramters.Add("receiverUsername", receiverUsername);

        WWW w = POST(GetUrl("GiveVoucher"), ref paramters);

        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            if (!string.IsNullOrEmpty(w.text))
            {
                result = ToVoucherDetail(w.text, ref errorString);
            }
        }
        else
        {
            errorString = w.error;
        }
        onGetVoucherDetails(errorString, result, userParam);
    }


    public IEnumerator GetGameDetails(string gameId, OnGetGamesDetail onGetGameDetails, params object[] userParam)
    {
        string errorString = "";
        GameDetail result = null;

        Dictionary<string, string> paramters = new Dictionary<string, string>();
        paramters.Add("developerUserId", m_DeveloperId);
        paramters.Add("integrationKey", m_IntegrationKey);
        paramters.Add("gameId", gameId);

        WWW w = POST(GetUrl("GetGameDetails"), ref paramters);

        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            if (!string.IsNullOrEmpty(w.text))
            {
                result = ToGameDetail(w.text, ref errorString);
            }
        }
        else
        {
            errorString = w.error;
        }
        onGetGameDetails(errorString, result, userParam);
    }

    public IEnumerator GetAllGames(bool activeOnly, string gameNameStartsWith, int gameSortType, long startIndex, long endIndex, OnGetGameDetails onGetGameDetails, params object[] userParam)
    {
        string errorString = "";
        GameDetail[] result = null;

        Dictionary<string, string> paramters = new Dictionary<string, string>();
        paramters.Add("developerUserId", m_DeveloperId);
        paramters.Add("integrationKey", m_IntegrationKey);
        paramters.Add("gameNameStartsWith", gameNameStartsWith);
        paramters.Add("gameSortType", gameSortType.ToString());
        paramters.Add("startIndex", startIndex.ToString());
        paramters.Add("endIndex", endIndex.ToString());

        WWW w = POST(GetUrl("GetAllGames"), ref paramters);

        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            if (!string.IsNullOrEmpty(w.text))
            {
                result = ToGameDetails(w.text, ref errorString);
            }
        }
        else
        {
            errorString = w.error;
        }
        onGetGameDetails(errorString, result, userParam);
    }

    public IEnumerator GetCurrentTime(IGLStringResult onGetCurrentTime, params object[] userParam)
    {
        string errorString = "";
        string result = null;

        Dictionary<string, string> paramters = new Dictionary<string, string>();
        paramters.Add("developerUserId", m_DeveloperId);
        paramters.Add("integrationKey", m_IntegrationKey);

        WWW w = POST(GetUrl("GetCurrentTime"), ref paramters);

        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            if (!string.IsNullOrEmpty(w.text))
            {
                result = ToStringResult(w.text, ref errorString);
            }
        }
        else
        {
            errorString = w.error;
        }
        onGetCurrentTime(errorString, result, userParam);
    }


    public IEnumerator GetPlayerRankDetail(string playerUserId, string leaderboardId, OnGetRankDetails onGetRankDetails, params object[] userParam)
    {
        string errorString = "";
        RankDetail result = null;

        Dictionary<string, string> paramters = new Dictionary<string, string>();
        paramters.Add("developerUserName", m_DeveloperId);
        paramters.Add("integrationKey", m_IntegrationKey);
        paramters.Add("playerUserId", playerUserId);
        paramters.Add("leaderboardId", leaderboardId);

        WWW w = POST(GetUrl("GetPlayerRankDetail"), ref paramters);

        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            if (!string.IsNullOrEmpty(w.text))
            {
                result = ToRankDetail(w.text, ref errorString);
            }
        }
        else
        {
            errorString = w.error;
        }
        onGetRankDetails(errorString, result, userParam);
    }

    public IEnumerator CheckLeaderboardValidity(string userName, string participationKey, string leaderBoardId, IGLBoolResult onCheckValidity, params object[] userParam)
    {
        string errorString = "";
        bool result = false;

        Dictionary<string, string> paramters = new Dictionary<string, string>();
        paramters.Add("developerUserId", m_DeveloperId);
        paramters.Add("integrationKey", m_IntegrationKey);
        paramters.Add("leaderboardId", leaderBoardId);
        WWW w = POST(GetUrl("CheckLeaderboardValidity"), ref paramters);

        yield return w;

        if (string.IsNullOrEmpty(w.error))
        {
            if (!string.IsNullOrEmpty(w.text))
            {
                result = ToBooleanResult(w.text, ref errorString);
            }
        }
        else
        {
            errorString = w.error;
        }

        onCheckValidity(errorString, result, userParam);
    }

    public void OpenRegistrationUrl()
    {
        Application.OpenURL(REGISTRATION_URL);
    }

    public void OpenPlayerPageUrl()
    {
        Application.OpenURL(PLAYER_PAGE_URL);
    }

    public void OpenLeaderboardUrl(string leaderBoardId)
    {
        Application.OpenURL(string.Format("{0}?LBID={1}", LEADERBOARD_URL, leaderBoardId));
    }

    public void OpenEntryKeyUrl(string leaderBoardId)
    {
        Application.OpenURL(string.Format("{0}?LBID={1}", ENTRYKEY_URL, leaderBoardId));
    }
}
