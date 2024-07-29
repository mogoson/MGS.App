/*************************************************************************
 *  Copyright (C) 2024 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  LoginCpnt.cs
 *  Description  :  Default.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  2024/7/29
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.IO;
using MGS.UI.Widget;
using Newtonsoft.Json;
using UnityEngine;

namespace MGS.App
{
    public class LoginCpnt
    {
        public Action<UserData> onLogined;
        string loginFile;
        UserData userData;

        LoginUI loginUI;
        UIDialog dialogUI;

        public LoginCpnt()
        {
            loginFile = $"{Application.persistentDataPath}/Login.json";

            loginUI = UnityEngine.Object.FindObjectOfType<LoginUI>(true);
            loginUI.OnDirtyEvent += LoginUI_OnDirtyEvent;
            loginUI.RefreshAppInfo(Config.LoadAppInfo());
            loginUI.RefreshAgreement(Config.LoadAgreement());

            dialogUI = UnityEngine.Object.FindObjectOfType<UIDialog>(true);
        }

        private void LoginUI_OnDirtyEvent(UserData data)
        {
            CheckLoginData(userData, OnFinished);
            void OnFinished(bool succed, Exception error)
            {
                if (succed)
                {
                    loginUI.SetActive(false);
                    userData = data;
                    userData.isLogined = true;
                    UpdateLoginData(userData);
                    onLogined?.Invoke(userData);
                }
                else
                {
                    var options = new UIDialogOption()
                    {
                        tittle = "Login Error",
                        closeButton = true,
                        content = error.Message,
                        yesButton = "OK"
                    };
                    dialogUI.Refresh(options);
                    dialogUI.SetActive();
                }
            }
        }

        void CheckLoginData(UserData data, Action<bool, Exception> finished)
        {
            loginUI.ToggleLoading(true);

            //TODO...
            Debug.LogWarning("CheckLogin is not implemented.");

            loginUI.ToggleLoading(false);
            finished?.Invoke(true, null);
        }

        public void LogIn()
        {
            userData = LoadLoginData(loginFile);
            if (userData.CheckValid() && userData.isLogined)
            {
                onLogined?.Invoke(userData);
            }
            else
            {
                LogIn(userData);
            }
        }

        public void LogIn(UserData data)
        {
            userData = data;
            loginUI.Refresh(userData);
            loginUI.SetActive();
        }

        public void LogOut()
        {
            userData.isLogined = false;
            UpdateLoginData(userData);
        }

        UserData LoadLoginData(string loginFile)
        {
            if (File.Exists(loginFile))
            {
                var json = File.ReadAllText(loginFile);
                return JsonConvert.DeserializeObject<UserData>(json);
            }
            return new UserData();
        }

        void UpdateLoginData(UserData data)
        {
            var json = string.Empty;
            if (data != null)
            {
                json = JsonConvert.SerializeObject(data);
            }
            File.WriteAllText(loginFile, json);
        }
    }
}