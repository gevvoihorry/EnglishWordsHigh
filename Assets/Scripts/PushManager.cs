using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_IOS
using NotificationServices = UnityEngine.iOS.NotificationServices;
using LocalNotification = UnityEngine.iOS.LocalNotification;
#endif

public class PushManager : MonoBehaviour {

    static public PushManager instance;

    private int[] pushSeconds = new int[10];
    private string[] pushMessage = new string[15];
    private List<string> messageList = new List<string>();

    private int pushFlg = 0;

    private int continuedDays = 0;

    private int[] lessonCount = { 33, 26, 27, 35, 39 };

    private string appName = "高校入試に出る英単語";

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
        UniLocalNotification.Initialize();
        ClearNotification();

        pushFlg = PlayerPrefs.GetInt("PUSH_FLG", 0);
    }

    // Use this for initialization
    void Start() {
                
    }

    void SetMessegaList() {
        //continuedDays = PlayerPrefs.GetInt("CONTINUED_DAYS", 0);
        int clearLessonCount = 0;
        for (int i = 0; i < lessonCount.Length; i++) {
            for (int n = 0; n < lessonCount[i]; n++) {
                int readTestScore = PlayerPrefs.GetInt("READ_TEST_SCORE" + i + "_" + n, 0);
                int writeTestScore = PlayerPrefs.GetInt("WRITE_TEST_SCORE" + i + "_" + n, 0);
                if (readTestScore >= 80 && writeTestScore >= 80) {
                    clearLessonCount++;
                } else {
                    break;
                }
            }
        }

        int totalLessonCount = 0;
        for (int i = 0; i < lessonCount.Length; i++) {
            totalLessonCount += (lessonCount[i] * 10);
        }

        Debug.Log((totalLessonCount / 10));

        //messageList.Add("継続日数 " + continuedDays + "日目!!");
        messageList.Add("単語を制すものは入試を制す、ですよ!!");
        if (clearLessonCount != 0) {
            messageList.Add(clearLessonCount + "/" + (totalLessonCount / 10) + "Lessons clear!!");
        }
        if (clearLessonCount < totalLessonCount) {
            messageList.Add("Let's challenge lesson" + (clearLessonCount + 1) + "!!");
        }
        messageList.Add("Let's enjoy English!!");
        messageList.Add("Hello!!");
    }

    void SetPushMessage() {
        for (int i = 0; i < pushMessage.Length; i++) { //15
            int ram = UnityEngine.Random.Range(0, messageList.Count); //0~14
            pushMessage[i] = messageList[ram];
        }
    }

    void SetTime() {
        int day = 86400;
        int hour = 3600;
        int minute = 60;
        //pushSeconds[0] = 10 * minute;
        //pushSeconds[0] = 5;
        //pushSeconds[1] = 1 * hour;
        //pushSeconds[2] = 3 * hour;
        //pushSeconds[3] = 8 * hour;
        //pushSeconds[4] = 12 * hour;
        //pushSeconds[5] = 1 * day;
        //pushSeconds[6] = 2 * day;
        //pushSeconds[7] = 3 * day;
        //pushSeconds[8] = 5 * day;
        //pushSeconds[9] = 7 * day;
        //pushSeconds[10] = 14 * day;
        //pushSeconds[11] = 20 * day;
        //pushSeconds[12] = 30 * day;
        //pushSeconds[13] = 60 * day;
        //pushSeconds[14] = 100 * day;

        pushSeconds[0] = 1 * day;
        pushSeconds[1] = 2 * day;
        pushSeconds[2] = 3 * day;
        pushSeconds[3] = 5 * day;
        pushSeconds[4] = 7 * day;
        pushSeconds[5] = 14 * day;
        pushSeconds[6] = 20 * day;
        pushSeconds[7] = 30 * day;
        pushSeconds[8] = 60 * day;
        pushSeconds[9] = 100 * day;
    }

    // Update is called once per frame
    void Update() {

    }

    public void ChangePushFlg(int num) {
        pushFlg = num;
    }

    void OnApplicationPause(bool pauseStatus) {
        if (pauseStatus) {
            if (pushFlg == 0) {
                return;
            }
            SetTime();
            SetMessegaList();
            SetPushMessage();
            for (int i = 0; i < pushSeconds.Length; i++) {
                UniLocalNotification.Register(pushSeconds[i], pushMessage[i], appName);
            }
            AddNotification();
        } else {
            UniLocalNotification.CancelAll();
            ClearNotification();
        }
    }

    public void AddNotification() {
        #if UNITY_IOS
		LocalNotification l = new LocalNotification ();
		l.applicationIconBadgeNumber = 1;
		l.fireDate = System.DateTime.Now.AddSeconds (pushSeconds[0]);
		NotificationServices.ScheduleLocalNotification (l);
        #endif
    }

    public void ClearNotification() {
        #if UNITY_IOS
		LocalNotification l = new LocalNotification ();
		l.applicationIconBadgeNumber = -1;
		NotificationServices.PresentLocalNotificationNow (l);
		NotificationServices.CancelAllLocalNotifications ();
		NotificationServices.ClearLocalNotifications ();
        #endif
    }
}
