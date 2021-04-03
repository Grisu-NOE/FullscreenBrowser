// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScreenRefresher.cs" company="Freiwillige Feuerwehr Krems/Donau">
//     Freiwillige Feuerwehr Krems/Donau
//     Austraße 33
//     A-3500 Krems/Donau
//     Austria
// 
//     Tel.:   +43 (0)2732 85522
//     Fax.:   +43 (0)2732 85522 40
//     E-mail: office@feuerwehr-krems.at
// 
//     This software is furnished under a license and may be
//     used  and copied only in accordance with the terms of
//     such  license  and  with  the  inclusion of the above
//     copyright  notice.  This software or any other copies
//     thereof   may  not  be  provided  or  otherwise  made
//     available  to  any  other  person.  No  title  to and
//     ownership of the software is hereby transferred.
// 
//     The information in this software is subject to change
//     without  notice  and  should  not  be  construed as a
//     commitment by Freiwillige Feuerwehr Krems/Donau.
// 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace At.FF.Krems.Configuration.XML
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    /// <summary>The screen refresher.</summary>
    [GeneratedCode("xsd", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, XmlType(AnonymousType = true)]
    [Serializable]
    public class ScreenRefresher
    {
        #region Fields

        /// <summary>The enabled lock object</summary>
        private readonly object enabledLockObject = new object();

        /// <summary>The run at startup lock object</summary>
        private readonly object runAtStartupLockObject = new object();

        /// <summary>The duration lock object</summary>
        private readonly object durationLockObject = new object();

        /// <summary>The interval lock object</summary>
        private readonly object intervalLockObject = new object();

        /// <summary>The height lock object</summary>
        private readonly object heightLockObject = new object();

        /// <summary>The enabled</summary>
        private bool enabled;

        /// <summary>The run at startup</summary>
        private bool runAtStartup;

        /// <summary>The duration</summary>
        private int duration;

        /// <summary>The interval</summary>
        private int interval;

        /// <summary>The height</summary>
        private int height;

        #endregion

        #region Properties

        /// <summary>Gets or sets a value indicating whether enabled.</summary>
        public bool Enabled
        {
            get
            {
                lock (this.enabledLockObject)
                {
                    return this.enabled;
                }
            }

            set
            {
                lock (this.enabledLockObject)
                {
                    this.enabled = value;
                }
            }
        }

        /// <summary>Gets or sets a value indicating whether run at startup.</summary>
        public bool RunAtStartup
        {
            get
            {
                lock (this.runAtStartupLockObject)
                {
                    return this.runAtStartup;
                }
            }

            set
            {
                lock (this.runAtStartupLockObject)
                {
                    this.runAtStartup = value;
                }
            }
        }

        /// <summary>Gets or sets the duration.</summary>
        public int Duration
        {
            get
            {
                lock (this.durationLockObject)
                {
                    return this.duration <= 0 ? 30 : this.duration;
                }
            }

            set
            {
                lock (this.durationLockObject)
                {
                    this.duration = value;
                }
            }
        }

        /// <summary>Gets or sets the interval.</summary>
        public int Interval
        {
            get
            {
                lock (this.intervalLockObject)
                {
                    return this.interval <= 0 ? 60 : this.interval;
                }
            }

            set
            {
                lock (this.intervalLockObject)
                {
                    this.interval = value;
                }
            }
        }

        /// <summary>Gets or sets the height.</summary>
        public int Height
        {
            get
            {
                lock (this.heightLockObject)
                {
                    return this.height <= 0 ? 50 : this.height;
                }
            }

            set
            {
                lock (this.heightLockObject)
                {
                    this.height = value;
                }
            }
        }

        #endregion
    }
}