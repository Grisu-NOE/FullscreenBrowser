// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowserRegistry.cs" company="Freiwillige Feuerwehr Krems/Donau">
//      Freiwillige Feuerwehr Krems/Donau
//      Austraße 33
//      A-3500 Krems/Donau
//      Austria
//      Tel.:   +43 (0)2732 85522
//      Fax.:   +43 (0)2732 85522 40
//      E-mail: office@feuerwehr-krems.at
//
//      This software is furnished under a license and may be
//      used  and copied only in accordance with the terms of
//      such  license  and  with  the  inclusion of the above
//      copyright  notice.  This software or any other copies
//      thereof   may  not  be  provided  or  otherwise  made
//      available  to  any  other  person.  No  title  to and
//      ownership of the software is hereby transferred.
//
//      The information in this software is subject to change
//      without  notice  and  should  not  be  construed as a
//      commitment by Freiwillige Feuerwehr Krems/Donau.
//
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using JetBrains.Annotations;

namespace At.FF.Krems.Configuration.XML
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    /// <summary>The browser registry, see about:config</summary>
    [GeneratedCode("xsd", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, XmlRoot(Namespace = "", IsNullable = false), XmlType(AnonymousType = true)]
    [Serializable]
    public class BrowserRegistry : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>The name</summary>
        private string name;

        /// <summary>The type</summary>
        private BrowserRegistryType type;

        /// <summary>The value</summary>
        private object value;

        #endregion

        #region Properties

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (this.name == value)
                {
                    return;
                }

                this.name = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>Gets or sets the type of the <see cref="BrowserRegistry"/>.</summary>
        /// <value>The type.</value>
        public BrowserRegistryType Type
        {
            get
            {
                return this.type;
            }

            set
            {
                if (this.type == value)
                {
                    return;
                }

                this.type = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>Gets or sets the value.</summary>
        /// <value>The value.</value>
        public object Value
        {
            get
            {
                return this.value;
            }

            set
            {
                if (this.value == value)
                {
                    return;
                }

                this.value = value;
                this.OnPropertyChanged();
            }
        }

        #endregion

        /// <summary>Occurs when a property value changes.</summary>
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>Called when [property changed].</summary>
        /// <param name="propertyName">Name of the property.</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}