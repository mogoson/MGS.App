/*************************************************************************
 *  Copyright (C) 2024 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MainCpnt.cs
 *  Description  :  Default.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  2024/7/29
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Text;

namespace MGS.App
{
    public class MainCpnt
    {
        public event Action<UserData> OnLogOutEvent;

        MainUI mainUI;

        public MainCpnt()
        {
            mainUI = UnityEngine.Object.FindObjectOfType<MainUI>(true);
            mainUI.mePageUI.OnLogOutEvent += OnLogOut;

            var appInfo = Config.LoadAppInfo();
            mainUI.mePageUI.Refresh(appInfo);

            var agreement = Config.LoadAgreement();
            var about = BuildAbout(appInfo);
            mainUI.mePageUI.Refresh(agreement, about);

        }

        string BuildAbout(AppInfo appInfo)
        {
            var builder = new StringBuilder();
            builder.Append($"{appInfo.appName}\r\n\r\n");
            builder.Append($"Version: {appInfo.version}\r\n\r\n");
            builder.Append($"Last Updated: {appInfo.lastUpdated}\r\n\r\n");
            builder.Append($"Organization: {appInfo.organization}\r\n\r\n");
            builder.Append($"Developer: {appInfo.developer}\r\n\r\n");
            builder.Append($"Contact: {appInfo.contact}\r\n\r\n");
            builder.Append($"{appInfo.copyright}");
            return builder.ToString();
        }

        public void LogIn(UserData data)
        {
            mainUI.SetActive(true);
            mainUI.Refresh(data);
        }

        void OnLogOut(UserData data)
        {
            mainUI.SetActive(false);
            OnLogOutEvent?.Invoke(data);
        }
    }
}