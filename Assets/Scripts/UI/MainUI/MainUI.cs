/*************************************************************************
 *  Copyright (C) 2024 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MainUI.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  2024/7/24
 *  Description  :  Initial development version.
 *************************************************************************/

using MGS.UI.Widget;
using UnityEngine.UI;

namespace MGS.App
{
    public class MainUI : UIRefreshable<UserData>
    {
        public MainPageUI mainPageUI;
        public MePageUI mePageUI;
        public Toggle togMain;
        public Toggle togMe;

        private void Awake()
        {
            togMain.onValueChanged.AddListener(isOn => mainPageUI.SetActive(isOn));
            togMe.onValueChanged.AddListener(isOn => mePageUI.SetActive(isOn));
        }

        protected override void OnRefresh(UserData option)
        {
            mainPageUI.Refresh(option);
            mePageUI.Refresh(option);
            togMain.isOn = true;
        }
    }
}