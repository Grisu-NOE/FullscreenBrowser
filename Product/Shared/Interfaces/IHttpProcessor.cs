// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHttpProcessor.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
namespace At.FF.Krems.Interfaces
{
    using System.IO;
    using System.Net.Sockets;

    /// <summary>The HttpProcessor interface.</summary>
    public interface IHttpProcessor
    {
        /// <summary>Gets the HTTP URL.</summary>
        /// <value>The HTTP URL.</value>
        string HttpUrl { get; }

        /// <summary>Gets the output stream.</summary>
        /// <value>The output stream.</value>
        StreamWriter OutputStream { get; }

        /// <summary>Initializes the specified TCP client.</summary>
        /// <param name="tcpClient">The TCP client.</param>
        void Initialize(TcpClient tcpClient);

        /// <summary>Processes this instance.</summary>
        void Process();

        /// <summary>Writes the failure.</summary>
        void WriteFailure();

        /// <summary>Writes the success.</summary>
        void WriteSuccess();
    }
}