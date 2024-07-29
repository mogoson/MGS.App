/*************************************************************************
 *  Copyright (C) 2024 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  MainPageUI.cs
 *  Description  :  Default.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  2024/7/29
 *  Description  :  Initial development version.
 *************************************************************************/

using MGS.UI.Widget;
using UnityEngine;

namespace MGS.App
{
    public class MainPageUI : UIRefreshable<UserData>
    {
        protected override void OnRefresh(UserData option)
        {
            //TODO...
            Debug.LogWarning("MainPageUI.OnRefresh is not implemented.");
        }
    }
}