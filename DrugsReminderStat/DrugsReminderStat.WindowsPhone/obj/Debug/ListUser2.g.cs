﻿

#pragma checksum "C:\Users\BliD\Documents\Visual Studio 2013\Projects\DrugsReminderStat\DrugsReminderStat\DrugsReminderStat.WindowsPhone\ListUser2.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "63159A630ECF686D81E0A6BD5BC457AB"
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
    partial class ListUser2 : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 13 "..\..\ListUser2.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Add_user;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 40 "..\..\ListUser2.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.Selector)(target)).SelectionChanged += this.sectionchange;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 49 "..\..\ListUser2.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.Image_Tapped_Drugs;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


