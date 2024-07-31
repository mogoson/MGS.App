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
using MGS.UI.Widget;

namespace MGS.App
{
    public class MainCpnt
    {
        public event Action<UserData> OnLogOutEvent;

        MainUI mainUI;
        UIDialog dialogUI;

        public MainCpnt()
        {
            dialogUI = UnityEngine.Object.FindObjectOfType<UIDialog>(true);
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
            var options = new UIDialogOption()
            {
                tittle = "Logout Confirm",
                closeButton = true,
                content = "Are you sure log out?",
                cancelButton = "Cancel",
                yesButton = "OK",
                callback = OnDialogResult
            };
            dialogUI.Refresh(options);
            dialogUI.SetActive();

            void OnDialogResult(UIDialogResult result)
            {
                if (result == UIDialogResult.Yes)
                {
                    mainUI.SetActive(false);
                    OnLogOutEvent?.Invoke(data);
                }
            }
        }
    }
}