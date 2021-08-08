//using System;
//using System.Collections;
//using System.Collections.Generic;
//#if UNITY_ANDROID
//using Unity.Notifications.Android;
//#endif
//#if UNITY_IOS
//using Unity.Notifications.iOS;
//#endif
//using UnityEngine;

//public class LocalNotificationManager : MonoBehaviour
//{
//    // khai bao cac notifi
//    private static string OFF_1_DAY = "OFF_1_DAY"; private long time_ScheOff1Day = 10; private int ID_OFF_1_DAY = 0;


//#if UNITY_ANDROID
//    private int SendNotifiANDROID(string id, string name, string des = "", long time = 0)
//    {
//        var c = new AndroidNotificationChannel()
//        {
//            Id = id,
//            Name = name,
//            Importance = Importance.High,
//            Description = des,
//        };
//        AndroidNotificationCenter.RegisterNotificationChannel(c);
//        var notification = new AndroidNotification();
//        notification.Title = name;
//        notification.Text = des;
//        notification.FireTime = System.DateTime.Now.AddSeconds(time);

//        return AndroidNotificationCenter.SendNotification(notification, id);
//    }
//#endif

//#if UNITY_IOS
//    private void CreateNotifiIOS(TimeSpan time, string id, string title, string body="", string subtitle="", bool repeat = false) {
//        var timeTrigger = new iOSNotificationTimeIntervalTrigger()
//        {
//            TimeInterval = time,
//            Repeats = repeat
//        };

//        var notification = new iOSNotification()
//        {
//            Identifier = id,
//            Title = title,
//            Body = body,
//            Subtitle = subtitle,
//            ShowInForeground = true,
//            ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
//            CategoryIdentifier = "category_a",
//            ThreadIdentifier = "thread1",
//            Trigger = timeTrigger,
//        };

//        iOSNotificationCenter.ScheduleNotification(notification);
//    } 

//#endif

//    private void OnApplicationPause(bool pause)
//    {
//#if UNITY_ANDROID
//        ID_OFF_1_DAY = SendNotifiANDROID(OFF_1_DAY, "Đến giờ điểm danh rồi!", "Bạn đã offline 1 ngày, vào điểm danh để nhận quà ngay nào! ", time_ScheOff1Day);
//#endif
//#if UNITY_IOS
//        CreateNotifiIOS(TimeSpan.FromSeconds(time_ScheOff1Day), OFF_1_DAY, "Đến giờ điểm danh rồi!", "Bạn đã offline 1 ngày, vào điểm danh để nhận quà ngay nào! ");
//#endif
//    }
//}
