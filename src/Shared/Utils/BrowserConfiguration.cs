// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowserConfiguration.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
namespace At.FF.Krems.Utils
{
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;

    using At.FF.Krems.Configuration.XML;
    using At.FF.Krems.Interfaces;

    /// <summary>The browser configuration.</summary>
    public class BrowserConfiguration : IBrowserConfiguration
    {
        #region Fields

        /// <summary>The configuration lock object</summary>
        private readonly object configLockObject = new object();

        /// <summary>The configuration</summary>
        private BrowserConfig config;

        #endregion

        #region Properties

        /// <summary>Gets the configuration.</summary>
        /// <value>The configuration.</value>
        public BrowserConfig Config
        {
            get
            {
                lock (this.configLockObject)
                {
                    return this.config;
                }
            }

            private set
            {
                lock (this.configLockObject)
                {
                    this.config = value;
                }
            }
        }

        #endregion

        #region Startup Tasks

        /// <summary>Initializes this instance.</summary>
        public void Init()
        {
            this.Load();
        }

        /// <summary>Resets this instance.</summary>
        public void Reset()
        {
            this.Init();
        }

        #endregion

        #region Methods

        /// <summary>Loads the XML configuration.</summary>
        public void Load()
        {
            var xmlSerializer = new XmlSerializer(typeof(BrowserConfig));
            using (var xmlReader = XmlReader.Create(Constants.XmlFile))
            {
                this.Config = (BrowserConfig)xmlSerializer.Deserialize(xmlReader);
            }
        }

        /// <summary>Saves the XML configuration.</summary>
        public void Save()
        {
            var serializer = new XmlSerializer(this.Config.GetType());
            using (var writer = File.Create(Constants.XmlFile))
            {
                serializer.Serialize(writer, this.Config);
            }
        }

        #endregion
    }
}