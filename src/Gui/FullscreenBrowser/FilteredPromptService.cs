// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilteredPromptService.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
namespace At.FF.Krems.FullscreenBrowser
{
    using System;
    using System.Reflection;

    using At.FF.Krems.Utils.Logging;

    using Gecko;

    using log4net;

    /// <summary>Used to prevent from displaying certain firefox alerts.</summary>
    internal class FilteredPromptService : nsIPromptService, nsIPrompt
    {
        /// <summary>The logger.</summary>
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>The prompt service.</summary>
        private static readonly PromptService PromptService = new PromptService();

        /// <summary>The alert.</summary>
        /// <param name="dialogTitle">The dialog title.</param>
        /// <param name="text">The text.</param>
        public void Alert(string dialogTitle, string text)
        {
            Logger.ErrorFormatFast("Title={0}; Text={1}", dialogTitle, text);
        }

        /// <summary>The alert check.</summary>
        /// <param name="dialogTitle">The dialog title.</param>
        /// <param name="text">The text.</param>
        /// <param name="checkMsg">The check message.</param>
        /// <param name="checkValue">The check value.</param>
        public void AlertCheck(string dialogTitle, string text, string checkMsg, ref bool checkValue)
        {
            PromptService.AlertCheck(dialogTitle, text, checkMsg, ref checkValue);
        }

        /// <summary>The confirm.</summary>
        /// <param name="dialogTitle">The dialog title.</param>
        /// <param name="text">The text.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool Confirm(string dialogTitle, string text)
        {
            return PromptService.Confirm(dialogTitle, text);
        }

        /// <summary>The confirm check.</summary>
        /// <param name="dialogTitle">The dialog title.</param>
        /// <param name="text">The text.</param>
        /// <param name="checkMsg">The check message.</param>
        /// <param name="checkValue">The check value.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool ConfirmCheck(string dialogTitle, string text, string checkMsg, ref bool checkValue)
        {
            return PromptService.ConfirmCheck(dialogTitle, text, checkMsg, ref checkValue);
        }

        /// <summary>The confirm ex.</summary>
        /// <param name="dialogTitle">The dialog title.</param>
        /// <param name="text">The text.</param>
        /// <param name="buttonFlags">The button flags.</param>
        /// <param name="button0Title">The button 0 title.</param>
        /// <param name="button1Title">The button 1 title.</param>
        /// <param name="button2Title">The button 2 title.</param>
        /// <param name="checkMsg">The check message.</param>
        /// <param name="checkValue">The check value.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int ConfirmEx(
            string dialogTitle,
            string text,
            uint buttonFlags,
            string button0Title,
            string button1Title,
            string button2Title,
            string checkMsg,
            ref bool checkValue)
        {
            return PromptService.ConfirmEx(dialogTitle, text, buttonFlags, button0Title, button1Title, button2Title, checkMsg, ref checkValue);
        }

        /// <summary>The prompt.</summary>
        /// <param name="dialogTitle">The dialog title.</param>
        /// <param name="text">The text.</param>
        /// <param name="value">The value.</param>
        /// <param name="checkMsg">The check message.</param>
        /// <param name="checkValue">The check value.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool Prompt(string dialogTitle, string text, ref string value, string checkMsg, ref bool checkValue)
        {
            return PromptService.Prompt(dialogTitle, text, ref value, checkMsg, ref checkValue);
        }

        /// <summary>The prompt password.</summary>
        /// <param name="dialogTitle">The dialog title.</param>
        /// <param name="text">The text.</param>
        /// <param name="password">The password.</param>
        /// <param name="checkMsg">The check message.</param>
        /// <param name="checkValue">The check value.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool PromptPassword(string dialogTitle, string text, ref string password, string checkMsg, ref bool checkValue)
        {
            return PromptService.PromptPassword(dialogTitle, text, ref password, checkMsg, ref checkValue);
        }

        /// <summary>The prompt username and password.</summary>
        /// <param name="dialogTitle">The dialog title.</param>
        /// <param name="text">The text.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="checkMsg">The check message.</param>
        /// <param name="checkValue">The check value.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool PromptUsernameAndPassword(
            string dialogTitle,
            string text,
            ref string username,
            ref string password,
            string checkMsg,
            ref bool checkValue)
        {
            return PromptService.PromptUsernameAndPassword(dialogTitle, text, ref username, ref password, checkMsg, ref checkValue);
        }

        /// <summary>The select.</summary>
        /// <param name="dialogTitle">The dialog title.</param>
        /// <param name="text">The text.</param>
        /// <param name="count">The count.</param>
        /// <param name="selectList">The select list.</param>
        /// <param name="outSelection">The out selection.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool Select(string dialogTitle, string text, uint count, IntPtr[] selectList, ref int outSelection)
        {
            return PromptService.Select(dialogTitle, text, count, selectList, ref outSelection);
        }

        public void Alert(mozIDOMWindowProxy aParent, string aDialogTitle, string aText)
        {
            PromptService.Alert(aDialogTitle, aText);
        }

        public void AlertCheck(mozIDOMWindowProxy aParent, string aDialogTitle, string aText, string aCheckMsg, ref bool aCheckState)
        {
            PromptService.AlertCheck(aDialogTitle, aText, aCheckMsg, ref aCheckState);
        }

        public bool Confirm(mozIDOMWindowProxy aParent, string aDialogTitle, string aText)
        {
            return PromptService.Confirm(aDialogTitle, aText);
        }

        public bool ConfirmCheck(
            mozIDOMWindowProxy aParent,
            string aDialogTitle,
            string aText,
            string aCheckMsg,
            ref bool aCheckState)
        {
            return PromptService.ConfirmCheck(aDialogTitle, aText, aCheckMsg, ref aCheckState);
        }

        public int ConfirmEx(
            mozIDOMWindowProxy aParent,
            string aDialogTitle,
            string aText,
            uint aButtonFlags,
            string aButton0Title,
            string aButton1Title,
            string aButton2Title,
            string aCheckMsg,
            ref bool aCheckState)
        {
            return PromptService.ConfirmEx(aDialogTitle, aText, aButtonFlags, aButton0Title, aButton1Title, aButton2Title, aCheckMsg, ref aCheckState);
        }

        public bool Prompt(
            mozIDOMWindowProxy aParent,
            string aDialogTitle,
            string aText,
            ref string aValue,
            string aCheckMsg,
            ref bool aCheckState)
        {
            return PromptService.Prompt(aDialogTitle, aText, ref aValue, aCheckMsg, ref aCheckState);
        }

        public bool PromptUsernameAndPassword(
            mozIDOMWindowProxy aParent,
            string aDialogTitle,
            string aText,
            ref string aUsername,
            ref string aPassword,
            string aCheckMsg,
            ref bool aCheckState)
        {
            return PromptService.PromptUsernameAndPassword(aDialogTitle, aText, ref aUsername, ref aPassword, aCheckMsg, ref aCheckState);
        }

        public bool PromptPassword(
            mozIDOMWindowProxy aParent,
            string aDialogTitle,
            string aText,
            ref string aPassword,
            string aCheckMsg,
            ref bool aCheckState)
        {
            return PromptService.PromptPassword(aDialogTitle, aText, ref aPassword, aCheckMsg, ref aCheckState);
        }

        public bool Select(
            mozIDOMWindowProxy aParent,
            string aDialogTitle,
            string aText,
            uint aCount,
            IntPtr[] aSelectList,
            ref int aOutSelection)
        {
            return PromptService.Select(aDialogTitle, aText, aCount, aSelectList, ref aOutSelection);
        }

        public bool PromptAuth(
            mozIDOMWindowProxy aParent,
            nsIChannel aChannel,
            uint level,
            nsIAuthInformation authInfo,
            string checkboxLabel,
            ref bool checkValue)
        {
            return PromptService.PromptAuth(aChannel, level, authInfo);
        }

        public nsICancelable AsyncPromptAuth(
            mozIDOMWindowProxy aParent,
            nsIChannel aChannel,
            nsIAuthPromptCallback aCallback,
            nsISupports aContext,
            uint level,
            nsIAuthInformation authInfo,
            string checkboxLabel,
            ref bool checkValue)
        {
            return PromptService.AsyncPromptAuth(aChannel, aCallback, aContext, level, authInfo);
        }
    }
}