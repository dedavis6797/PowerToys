﻿// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text;
using Peek.UI.Native;
using Windows.Win32;
using Windows.Win32.Foundation;

namespace Peek.UI.Extensions
{
    public static class HWNDExtensions
    {
        internal static HWND GetActiveTab(this HWND windowHandle)
        {
            var activeTab = windowHandle.FindChildWindow("ShellTabWindowClass");
            if (activeTab == HWND.Null)
            {
                activeTab = windowHandle.FindChildWindow("TabWindowClass");
            }

            return activeTab;
        }

        internal static bool IsDesktopWindow(this HWND windowHandle)
        {
            StringBuilder strClassName = new StringBuilder(256);
            var result = NativeMethods.GetClassName(windowHandle, strClassName, 256);
            if (result == 0)
            {
                return false;
            }

            var className = strClassName.ToString();

            if (className != "Progman" && className != "WorkerW")
            {
                return false;
            }

            return windowHandle.FindChildWindow("SHELLDLL_DefView") != HWND.Null;
        }

        internal static HWND FindChildWindow(this HWND windowHandle, string className)
        {
            return PInvoke.FindWindowEx(windowHandle, HWND.Null, className, null);
        }
    }
}
