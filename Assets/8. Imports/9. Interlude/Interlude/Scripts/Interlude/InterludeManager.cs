using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;

namespace Interlude
{
    public class InterludeManager : MonoBehaviour
    {
        public enum Protocol
        {
            ScavengerHunt,
            Harvest
        }
        public Protocol gameProtocol;

        private static InterludeManager instance;

        string serverIP = "104.154.68.222:8082";
        int id = 8;
        string password = "^fZL5P4up5AqUpbWg^";

        public TextMeshProUGUI addressInput;
        string address;

        //state
        static int score;
        static bool requestOngoing;

        //scavenger hunt protocol fields
        public string ticket;
        string key;

        //harvest protocol fields
        int maxScore;
        int stakedAmount;
        int sessionDuration;
        int startTime;
        int rewardRate;
        int gameRewardCoef;
        string harvestReceipt;

        public UnityEvent OnConnectionError;
        public UnityEvent OnCheckSuccess;
        public UnityEvent OnNullTicket;

        /***********************
        * Interface
        ************************/

        public static bool allowReceipt
        {
            get;
            set;
        }

        public static void KeyFound()
        {
            instance.StartCoroutine(instance.FetchKeyAndDisplay());
        }

        public static void UpdateScore(int newScore)
        {
            if (instance == null)
            {
                return;
            }
            score = newScore;
            instance.UpdateShellDisplay();
        }

        public static int GetISHAmount(int score)
        {
            return instance.GetShellAmount(score);
        }

        public static void ShowReceipt()
        {
            if (!requestOngoing)
            {
                instance.StartCoroutine(instance.FetchHarvestReceiptAndDisplay());
            }
        }

        /***********************
        * Protocols
        ************************/
        private void Start()
        {
            instance = this;
            allowReceipt = true;
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (gameProtocol == Protocol.Harvest && allowReceipt)
            {
                if (Input.GetKeyDown(KeyCode.I) && !requestOngoing)
                {
                    StartCoroutine(FetchHarvestReceiptAndDisplay());
                }
            }
        }

        public void CheckPlayerInfo()
        {
            if (gameProtocol == Protocol.ScavengerHunt)
            {
                StartCoroutine(CheckPlayerInfoScavengerHuntNew());
            }
            else
            {
                StartCoroutine(CheckPlayerInfoHarvest());
            }
        }

        IEnumerator CheckPlayerInfoScavengerHunt()
        {
            address = CleanString(addressInput.text);

            WWWForm form = new WWWForm();
            form.AddField("player", address);
            UnityWebRequest www = UnityWebRequest.Post(GetURL("getTicket"), form);
            www.timeout = 20;
            yield return www.SendWebRequest();

            bool isError;

#if UNITY_2020_1_OR_NEWER
            isError = www.result == UnityWebRequest.Result.ConnectionError;
#else
            isError = www.isNetworkError || www.isHttpError;
#endif
            if (isError)
            {
                Debug.LogError(www.error);
                OnConnectionError.Invoke();
            }
            else
            {
                string info = CleanString(www.downloadHandler.text);
                Debug.Log(info);
                string[] infos = info.Split('|');
                ticket = infos[0];//infos[1] holds the key index, and 0 if the session is not valid;
                if (ticket == "0x0000000000000000000000000000000000000000000000000000000000000000" || ticket == "error" || int.Parse(infos[1]) != id)
                {
                    OnNullTicket.Invoke();
                }
                else
                {
                    SceneManager.LoadScene(1);
                }
            }
            www.Dispose();
        }

        IEnumerator CheckPlayerInfoScavengerHuntNew()
        {
            address = CleanString(addressInput.text);

            WWWForm form = new WWWForm();
            form.AddField("player", address);
            form.AddField("keyId", id);
            UnityWebRequest www = UnityWebRequest.Post(GetURL("checkActiveSession"), form);
            www.timeout = 20;
            yield return www.SendWebRequest();

            bool isError;

#if UNITY_2020_1_OR_NEWER
            isError = www.result == UnityWebRequest.Result.ConnectionError;
#else
            isError = www.isNetworkError || www.isHttpError;
#endif
            if (isError)
            {
                Debug.LogError(www.error);
                OnConnectionError.Invoke();
            }
            else
            {
                string info = CleanString(www.downloadHandler.text);
                Debug.Log(info);
                if(info == "error")
                {
                    OnNullTicket.Invoke();
                }
                else
                {
                    string[] infos = info.Split('|');
                    bool active = bool.Parse(infos[0]);
                    if (!active)
                    {
                        OnNullTicket.Invoke();
                    }
                    else
                    {
                        ticket = infos[1];
                        SceneManager.LoadScene(1);
                    }
                }
            }
            www.Dispose();
        }
        IEnumerator FetchKeyAndDisplay()
        {
            int hash = GetStableHash(ticket + password);
            WWWForm form = new WWWForm();
            form.AddField("ticket", ticket);
            form.AddField("player", address);
            form.AddField("id", id);
            form.AddField("hash", hash);
            UnityWebRequest www = UnityWebRequest.Post(GetURL("getKey"), form);
            www.timeout = 20;

            yield return www.SendWebRequest();

            bool isError;

#if UNITY_2020_1_OR_NEWER
            isError = www.result == UnityWebRequest.Result.ConnectionError;
#else
            isError = www.isNetworkError || www.isHttpError;
#endif
            if (isError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                key = www.downloadHandler.text.Substring(1, www.downloadHandler.text.Length - 2);
                CopyKeyToClipboard();
                DisplayKey(id);
            }
            www.Dispose();
        }

        IEnumerator CheckPlayerInfoHarvest()
        {
            address = CleanString(addressInput.text);

            WWWForm form = new WWWForm();
            form.AddField("player", address);
            form.AddField("gameId", id);
            UnityWebRequest www = UnityWebRequest.Post(GetURL("getHarvestInfo"), form);
            www.timeout = 20;
            yield return www.SendWebRequest();

            bool isError;

#if UNITY_2020_1_OR_NEWER
            isError = www.result == UnityWebRequest.Result.ConnectionError;
#else
            isError = www.isNetworkError || www.isHttpError;
#endif
            if (isError)
            {
                Debug.LogError(www.error);
                OnConnectionError.Invoke();
            }
            else if (www.downloadHandler.text == "error")
            {
                OnNullTicket.Invoke();
            }
            else
            {
                string info = CleanString(www.downloadHandler.text);
                if (info == "error")
                {
                    OnNullTicket.Invoke();
                }
                else
                {
                    string[] infos = info.Split('|');
                    Debug.Log(info);
                    sessionDuration = int.Parse(infos[0]);
                    stakedAmount = int.Parse(infos[1]);
                    maxScore = int.Parse(infos[2]);
                    startTime = int.Parse(infos[3]);
                    rewardRate = int.Parse(infos[4]);
                    gameRewardCoef = int.Parse(infos[5]);
                    int gameId = int.Parse(infos[6]);

                    bool valid = id == gameId && stakedAmount > 0 && DateTimeOffset.Now.ToUnixTimeSeconds() < startTime + 60 * sessionDuration;
                    if (!valid)
                    {
                        OnNullTicket.Invoke();
                    }
                    else
                    {
                        SceneManager.LoadScene(1);
                    }
                }
            }

            www.Dispose();
        }

        IEnumerator FetchHarvestReceiptAndDisplay()
        {
            requestOngoing = true;
            int hash = GetStableHash(score.ToString() + startTime.ToString() + password);
            WWWForm form = new WWWForm();
            form.AddField("player", address);
            form.AddField("gameId", id);
            form.AddField("score", score);
            form.AddField("hash", hash);
            form.AddField("startTime", startTime);
            form.AddField("password", password);
            UnityWebRequest www = UnityWebRequest.Post(GetURL("getHarvestProof"), form);
            www.timeout = 20;

            yield return www.SendWebRequest();

            bool isError;

#if UNITY_2020_1_OR_NEWER
            isError = www.result == UnityWebRequest.Result.ConnectionError;
#else
            isError = www.isNetworkError || www.isHttpError;
#endif
            if (isError)
            {
                Debug.Log(www.error);
            }
            else
            {
                harvestReceipt = www.downloadHandler.text.Substring(1, www.downloadHandler.text.Length - 2) + "g" + score;
                DisplayHarvestReceipt();
                CopyReceiptToClipboard();
            }
            www.Dispose();
            requestOngoing = false;
        }

        /***********************
        * util
        ************************/
        string CleanString(string s)
        {
            return s.Replace("\u200B", "").Replace(" ", "").Replace("\n", "").Replace("\r", "").Replace("\"", "");
        }

        string GetURL(string args)
        {
            return "http://" + serverIP + "/" + args;
        }

        int HashPassword(string password)
        {
            return GetStableHash(password + address);//use address as salt
        }

        int GetStableHash(string s)
        {
            Debug.Log(s);
            uint hash = 0;
            var bytes = System.Text.Encoding.ASCII.GetBytes(s);
            foreach (byte b in bytes)
            {
                hash += b;
                hash += (hash << 10);
                hash ^= (hash >> 6);
            }

            hash += (hash << 3);
            hash ^= (hash >> 11);
            hash += (hash << 15);

            Debug.Log((int)(hash % 100000000));
            return (int)(hash % 100000000);
        }

        public int GetShellAmount(int score)
        {
            return instance.stakedAmount * score / instance.maxScore * instance.rewardRate * instance.gameRewardCoef / (36 * 100 * 100);
        }

        public void CopyKeyToClipboard()
        {
            GUIUtility.systemCopyBuffer = instance.key;
        }

        public void CopyReceiptToClipboard()
        {
            GUIUtility.systemCopyBuffer = instance.harvestReceipt;
        }

        /************************
        * UI                    
        ************************/
        public GameObject canvas;
        public Animator keyFoundAnimator, harvestProofAnimator;
        public TextMeshProUGUI keyText, keyIdText;
        public TextMeshProUGUI receiptShellAmountText;
        public TextMeshProUGUI hudShellAmount;

        void ShowKeyWindow()
        {
            GetComponent<Pauser>().PauseGame();
            canvas.SetActive(true);
            keyFoundAnimator.CrossFade("Menu In", 0);
        }

        void ShowHarvestProofWindow()
        {
            GetComponent<Pauser>().PauseGame();
            canvas.SetActive(true);
            harvestProofAnimator.CrossFade("Menu In", 0);
        }


        public void CloseWindow(Animator animator)
        {
            animator.CrossFade("Menu Out", 0);
        }

        Coroutine closeWindowRoutine;
        void CloseWindowIn(int seconds, Animator animator)
        {
            StopCoroutine("closeWindowRoutine");
            closeWindowRoutine = StartCoroutine(CloseWindowInRoutine(5, animator));
        }

        IEnumerator CloseWindowInRoutine(int seconds, Animator animator)
        {
            yield return new WaitForSeconds(seconds);
            CloseWindow(animator);
        }

        void UpdateShellDisplay()
        {
            hudShellAmount.text = GetShellAmount(score).ToString() + " ISH";
        }

        void DisplayKey(int id)
        {
            keyIdText.text = id.ToString();
            ShowKeyWindow();
            CloseWindowIn(5, keyFoundAnimator);
        }

        void DisplayHarvestReceipt()
        {
            receiptShellAmountText.text = GetShellAmount(score).ToString();
            ShowHarvestProofWindow();
            CloseWindowIn(5, harvestProofAnimator);
        }

        public void UnpauseGame()
        {
            GetComponent<Pauser>().UnpauseGame();
        }
    }
}

