//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConnectorArchicad.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    ///   This class was generated by MSBuild using the GenerateResource task.
    ///   To add or remove a member, edit your .resx file then rerun MSBuild.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Build.Tasks.StronglyTypedResourceBuilder", "15.1.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class OperationNameTemplates {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal OperationNameTemplates() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ConnectorArchicad.Properties.OperationNameTemplates", typeof(OperationNameTemplates).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Time: Convert to native: {0}.
        /// </summary>
        internal static string ConvertToNative {
            get {
                return ResourceManager.GetString("ConvertToNative", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Time: HTTP command API: {0}.
        /// </summary>
        internal static string HttpCommandAPI {
            get {
                return ResourceManager.GetString("HttpCommandAPI", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Time: HTTP command execute: {0}.
        /// </summary>
        internal static string HttpCommandExecute {
            get {
                return ResourceManager.GetString("HttpCommandExecute", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Time: Mesh to native.
        /// </summary>
        internal static string MeshToNative {
            get {
                return ResourceManager.GetString("MeshToNative", resourceCulture);
            }
        }
    }
}