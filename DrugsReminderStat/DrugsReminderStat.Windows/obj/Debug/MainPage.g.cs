﻿

#pragma checksum "C:\Users\BliD\Documents\Visual Studio 2013\Projects\DrugsReminderStat\DrugsReminderStat\DrugsReminderStat.Windows\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2D7258003A658DD46223F8C19BC6A655"
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
    partial class MainPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 112 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.Selector)(target)).SelectionChanged += this.lstRSS_SelectionChanged;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 56 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.adduser_Tapped;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 63 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.listuser_Tapped;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 70 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.btnprecaution;
                 #line default
                 #line hidden
                break;
            case 5:
                #line 76 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.btnAbout;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


