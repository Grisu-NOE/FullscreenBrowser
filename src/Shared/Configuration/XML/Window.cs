// --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Window.cs" company="Freiwillige Feuerwehr Krems/Donau">
//       Freiwillige Feuerwehr Krems/Donau
//       Austraße 33
//       A-3500 Krems/Donau
//       Austria
// 
//       Tel.:   +43 (0)2732 85522
//       Fax.:   +43 (0)2732 85522 40
//       E-mail: office@feuerwehr-krems.at
// 
//       This software is furnished under a license and may be
//       used  and copied only in accordance with the terms of
//       such  license  and  with  the  inclusion of the above
//       copyright  notice.  This software or any other copies
//       thereof   may  not  be  provided  or  otherwise  made
//       available  to  any  other  person.  No  title  to and
//       ownership of the software is hereby transferred.
// 
//       The information in this software is subject to change
//       without  notice  and  should  not  be  construed as a
//       commitment by Freiwillige Feuerwehr Krems/Donau.
// 
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace At.FF.Krems.Configuration.XML
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    /// <summary>The window.</summary>
    [GeneratedCode("xsd", "2.0.50727.3038"), DesignerCategory("code"), DebuggerStepThrough, XmlRoot(Namespace = "", IsNullable = false), XmlType(AnonymousType = true)]
    [Serializable]
    public class Window
    {
        #region Fields

        /// <summary>The auto start lock object</summary>
        private readonly object autostartLockObject = new object();

        /// <summary>The URL lock object</summary>
        private readonly object urlLockObject = new object();

        /// <summary>The is alternative window lock object</summary>
        private readonly object isAlternativeWindowLockObject = new object();

        /// <summary>The position lock object</summary>
        private readonly object positionLockObject = new object();

        /// <summary>The show on screen lock object</summary>
        private readonly object showOnScreenLockObject = new object();

        /// <summary>The dimensions lock object</summary>
        private readonly object dimensionsLockObject = new object();

        /// <summary>The reload in seconds lock object</summary>
        private readonly object reloadInSecondsLockObject = new object();

        /// <summary>The name lock object</summary>
        private readonly object nameLockObject = new object();

        /// <summary>The on top lock object</summary>
        private readonly object onTopLockObject = new object();

        /// <summary>The zoom level lock object</summary>
        private readonly object zoomLevelLockObject = new object();

        /// <summary>The auto start</summary>
        private bool autostart;

        /// <summary>The URL</summary>
        private string url;

        /// <summary>The is alternative window</summary>
        private bool isAlternativeWindow;

        /// <summary>The show on screen</summary>
        private int showOnScreen;

        /// <summary>The position</summary>
        private WindowPosition position;

        /// <summary>The dimensions</summary>
        private WindowDimensions dimensions;

        /// <summary>The reload in seconds</summary>
        private int reloadInSeconds;

        /// <summary>The name</summary>
        private string name;

        /// <summary>The on top</summary>
        private bool onTop;

        /// <summary>The zoom level</summary>
        private float zoomLevel;

        #endregion

        #region Properties

        /// <summary>Gets or sets a value indicating whether auto start.</summary>
        public bool Autostart
        {
            get
            {
                lock (this.autostartLockObject)
                {
                    return this.autostart;
                }
            }

            set
            {
                lock (this.autostartLockObject)
                {
                    this.autostart = value;
                }
            }
        }

        /// <summary>Gets or sets the URL.</summary>
        public string Url
        {
            get
            {
                lock (this.urlLockObject)
                {
                    return this.url ?? (this.url = string.Empty);
                }
            }

            set
            {
                lock (this.urlLockObject)
                {
                    this.url = value;
                }
            }
        }

        /// <summary>Gets or sets a value indicating whether is alternative window.</summary>
        public bool IsAlternativeWindow
        {
            get
            {
                lock (this.isAlternativeWindowLockObject)
                {
                    return this.isAlternativeWindow;
                }
            }

            set
            {
                lock (this.isAlternativeWindowLockObject)
                {
                    this.isAlternativeWindow = value;
                }
            }
        }

        /// <summary>Gets or sets the show on screen.</summary>
        public int ShowOnScreen
        {
            get
            {
                lock (this.showOnScreenLockObject)
                {
                    return this.showOnScreen;
                }
            }

            set
            {
                lock (this.showOnScreenLockObject)
                {
                    this.showOnScreen = value;
                }
            }
        }

        /// <summary>Gets or sets the position.</summary>
        public WindowPosition Position
        {
            get
            {
                lock (this.positionLockObject)
                {
                    return this.position ?? (this.position = new WindowPosition());
                }
            }

            set
            {
                lock (this.positionLockObject)
                {
                    this.position = value;
                }
            }
        }

        /// <summary>Gets or sets the dimensions.</summary>
        /// <value>The dimensions.</value>
        public WindowDimensions Dimensions
        {
            get
            {
                lock (this.dimensionsLockObject)
                {
                    return this.dimensions ?? (this.dimensions = new WindowDimensions());
                }
            }

            set
            {
                lock (this.dimensionsLockObject)
                {
                    this.dimensions = value;
                }
            }
        }

        /// <summary>Gets or sets the reload in seconds.</summary>
        public int ReloadInSeconds
        {
            get
            {
                lock (this.reloadInSecondsLockObject)
                {
                    return this.reloadInSeconds;
                }
            }

            set
            {
                lock (this.reloadInSecondsLockObject)
                {
                    this.reloadInSeconds = value;
                }
            }
        }

        /// <summary>Gets or sets the name.</summary>
        public string Name
        {
            get
            {
                lock (this.nameLockObject)
                {
                    return this.name ?? (this.name = string.Empty);
                }
            }

            set
            {
                lock (this.nameLockObject)
                {
                    this.name = value;
                }
            }
        }

        /// <summary>Gets or sets a value indicating whether on top.</summary>
        public bool OnTop
        {
            get
            {
                lock (this.onTopLockObject)
                {
                    return this.onTop;
                }
            }

            set
            {
                lock (this.onTopLockObject)
                {
                    this.onTop = value;
                }
            }
        }

        /// <summary>Gets or sets the zoom level.</summary>
        public float ZoomLevel
        {
            get
            {
                lock (this.zoomLevelLockObject)
                {
                    return Math.Abs(this.zoomLevel) <= 0 ? (this.zoomLevel = 1f) : this.zoomLevel;
                }
            }

            set
            {
                lock (this.zoomLevelLockObject)
                {
                    this.zoomLevel = value;
                }
            }
        }

        #endregion
    }
}