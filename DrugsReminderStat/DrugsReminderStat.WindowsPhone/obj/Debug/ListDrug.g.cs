﻿

#pragma checksum "C:\Users\BliD\Documents\Visual Studio 2013\Projects\DrugsReminderStat\DrugsReminderStat\DrugsReminderStat.WindowsPhone\ListDrug.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "3EA53A924EC2FF433AC8CE01DB446C09"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DrugsReminderStat
{
    partial class ListDrug : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 13 "..\..\ListDrug.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.adddrug_Tapped;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 40 "..\..\ListDrug.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.Selector)(target)).SelectionChanged += this.listDrugs_SelectionChanged;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


