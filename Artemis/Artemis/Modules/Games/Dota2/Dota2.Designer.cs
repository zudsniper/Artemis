﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Artemis.Modules.Games.Dota2 {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class Dota2 : global::System.Configuration.ApplicationSettingsBase {
        
        private static Dota2 defaultInstance = ((Dota2)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Dota2())));
        
        public static Dota2 Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool Enabled {
            get {
                return ((bool)(this["Enabled"]));
            }
            set {
                this["Enabled"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string GameDirectory {
            get {
                return ((string)(this["GameDirectory"]));
            }
            set {
                this["GameDirectory"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool CanCastAbility {
            get {
                return ((bool)(this["CanCastAbility"]));
            }
            set {
                this["CanCastAbility"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ShowHealth {
            get {
                return ((bool)(this["ShowHealth"]));
            }
            set {
                this["ShowHealth"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ShowDayCycle {
            get {
                return ((bool)(this["ShowDayCycle"]));
            }
            set {
                this["ShowDayCycle"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool CanCastItem {
            get {
                return ((bool)(this["CanCastItem"]));
            }
            set {
                this["CanCastItem"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ShowMana {
            get {
                return ((bool)(this["ShowMana"]));
            }
            set {
                this["ShowMana"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ShowDead {
            get {
                return ((bool)(this["ShowDead"]));
            }
            set {
                this["ShowDead"] = value;
            }
        }
    }
}
