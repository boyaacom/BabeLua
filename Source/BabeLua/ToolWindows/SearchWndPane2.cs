﻿using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;
using LuaLanguage.DataModel;

namespace Babe.Lua
{
    [Guid(GuidList.SearchWindowString2)]
    public class SearchWndPane2 : ToolWindowPane,ISearchWnd
    {
        SearchToolControl wnd;
        string CurrentSearchWord;
        public static SearchWndPane2 Current;
        
        /// <summary>
        /// Standard constructor for the tool window.
        /// </summary>
        public SearchWndPane2() :
            base(null)
        {
            this.Caption = Properties.Resources.SearchlWindowTitle2;
            
            this.BitmapResourceID = 301;

            this.BitmapIndex = 1;

            wnd = new SearchToolControl();
            base.Content = wnd;
            Current = this;
        }

        public void Search(string txt, bool AllFile)
        {
            wnd.Dispatcher.Invoke(() =>
            {
                if (string.IsNullOrWhiteSpace(txt))
                {
                    this.Caption = Properties.Resources.SearchlWindowTitle2;
                }
                else if (this.CurrentSearchWord == txt) return;
                else
                {
                    var list = FileManager.Instance.FindAllRef(txt, AllFile);
                    this.Caption = string.Format("{0} - find {1} matches", Properties.Resources.SearchlWindowTitle2, list.Count);

                    wnd.Refresh(list);

                    this.CurrentSearchWord = txt;
                }
            });
        }
    }
}
