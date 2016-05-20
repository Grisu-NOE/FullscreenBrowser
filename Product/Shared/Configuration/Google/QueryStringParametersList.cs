// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryStringParametersList.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
namespace At.FF.Krems.Configuration.Google
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>The query string parameter list</summary>
    public class QueryStringParametersList
    {
        /// <summary>Gets the list.</summary>
        /// <value>The list.</value>
        private List<KeyValuePair<string, string>> List { get; } = new List<KeyValuePair<string, string>>();

        /// <summary>Adds the specified key.</summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Add(string key, string value)
        {
            this.List.Add(new KeyValuePair<string, string>(key, value));
        }

        /// <summary>Gets the query string postfix.</summary>
        /// <returns>The <see cref="string"/></returns>
        public string GetQueryStringPostfix()
        {
            return string.Join("&", this.List.Select(p => Uri.EscapeDataString(p.Key) + "=" + Uri.EscapeDataString(p.Value)));
        }
    }
}
