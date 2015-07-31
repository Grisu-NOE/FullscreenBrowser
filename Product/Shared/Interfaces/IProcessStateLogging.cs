// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProcessStateLogging.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
    /// <summary>The ProcessStateLogging interface.</summary>
    public interface IProcessStateLogging
    {
        /// <summary>Log process start.</summary>
        void LogProcessStart();

        /// <summary>Log process start communication.</summary>
        void LogProcessStartComm();

        /// <summary>Log process ready.</summary>
        void LogProcessReady();

        /// <summary>Log process stop.</summary>
        void LogProcessStop();

        /// <summary>Log process exited.</summary>
        void LogProcessExited();
    }
}