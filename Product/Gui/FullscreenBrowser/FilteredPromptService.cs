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
    internal class FilteredPromptService : nsIPromptService2, nsIPrompt
    {
        #region Fields

        /// <summary>The logger.</summary>
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>The prompt service.</summary>
        private static readonly PromptService PromptService = new PromptService();

        #endregion

        /// <summary>The alert.</summary>
        /// <param name="parent">The parent.</param>
        /// <param name="dialogTitle">The dialog Title.</param>
        /// <param name="text">The text.</param>
        public void Alert(nsIDOMWindow parent, string dialogTitle, string text)
        {
            PromptService.Alert(dialogTitle, text);
        }

        /// <summary>The alert check.</summary>
        /// <param name="parent">The parent.</param>
        /// <param name="dialogTitle">The dialog title.</param>
        /// <param name="text">The text.</param>
        /// <param name="checkMsg">The check message.</param>
        /// <param name="checkState">The check state.</param>
        public void AlertCheck(nsIDOMWindow parent, string dialogTitle, string text, string checkMsg, ref bool checkState)
        {
            PromptService.AlertCheck(dialogTitle, text, checkMsg, ref checkState);
        }

        /// <summary>The confirm.</summary>
        /// <param name="parent">The parent.</param>
        /// <param name="dialogTitle">The dialog title.</param>
        /// <param name="text">The text.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool Confirm(nsIDOMWindow parent, string dialogTitle, string text)
        {
            return PromptService.Confirm(dialogTitle, text);
        }

        /// <summary>The confirm check.</summary>
        /// <param name="parent">The parent.</param>
        /// <param name="dialogTitle">The dialog title.</param>
        /// <param name="text">The text.</param>
        /// <param name="checkMsg">The check message.</param>
        /// <param name="checkState">The check state.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool ConfirmCheck(nsIDOMWindow parent, string dialogTitle, string text, string checkMsg, ref bool checkState)
        {
            return PromptService.ConfirmCheck(dialogTitle, text, checkMsg, ref checkState);
        }

        /// <summary>The confirm ex.</summary>
        /// <param name="parent">The parent.</param>
        /// <param name="dialogTitle">The dialog title.</param>
        /// <param name="text">The text.</param>
        /// <param name="buttonFlags">The button flags.</param>
        /// <param name="button0Title">The button 0 title.</param>
        /// <param name="button1Title">The button 1 title.</param>
        /// <param name="button2Title">The button 2 title.</param>
        /// <param name="checkMsg">The check message.</param>
        /// <param name="checkState">The check state.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int ConfirmEx(
            nsIDOMWindow parent,
            string dialogTitle,
            string text,
            uint buttonFlags,
            string button0Title,
            string button1Title,
            string button2Title,
            string checkMsg,
            ref bool checkState)
        {
            return PromptService.ConfirmEx(dialogTitle, text, buttonFlags, button0Title, button1Title, button2Title, checkMsg, ref checkState);
        }

        /// <summary>The prompt.</summary>
        /// <param name="parent">The parent.</param>
        /// <param name="dialogTitle">The dialog title.</param>
        /// <param name="text">The text.</param>
        /// <param name="value">The value.</param>
        /// <param name="checkMsg">The check message.</param>
        /// <param name="checkState">The check state.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool Prompt(nsIDOMWindow parent, string dialogTitle, string text, ref string value, string checkMsg, ref bool checkState)
        {
            return PromptService.Prompt(dialogTitle, text, ref value, checkMsg, ref checkState);
        }

        /// <summary>The prompt username and password.</summary>
        /// <param name="parent">The parent.</param>
        /// <param name="dialogTitle">The dialog title.</param>
        /// <param name="text">The text.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="checkMsg">The check message.</param>
        /// <param name="checkState">The check state.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool PromptUsernameAndPassword(
            nsIDOMWindow parent,
            string dialogTitle,
            string text,
            ref string username,
            ref string password,
            string checkMsg,
            ref bool checkState)
        {
            return PromptService.PromptUsernameAndPassword(dialogTitle, text, ref username, ref password, checkMsg, ref checkState);
        }

        /// <summary>The prompt password.</summary>
        /// <param name="parent">The parent.</param>
        /// <param name="dialogTitle">The dialog title.</param>
        /// <param name="text">The text.</param>
        /// <param name="password">The password.</param>
        /// <param name="checkMsg">The check message.</param>
        /// <param name="checkState">The check state.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool PromptPassword(
            nsIDOMWindow parent,
            string dialogTitle,
            string text,
            ref string password,
            string checkMsg,
            ref bool checkState)
        {
            return PromptService.PromptPassword(dialogTitle, text, ref password, checkMsg, ref checkState);
        }

        /// <summary>The select.</summary>
        /// <param name="parent">The parent.</param>
        /// <param name="dialogTitle">The dialog title.</param>
        /// <param name="text">The text.</param>
        /// <param name="count">The a count.</param>
        /// <param name="selectList">The select list.</param>
        /// <param name="outSelection">The out selection.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool Select(nsIDOMWindow parent, string dialogTitle, string text, uint count, IntPtr[] selectList, ref int outSelection)
        {
            return PromptService.Select(dialogTitle, text, count, selectList, ref outSelection);
        }

        /// <summary>The prompt authentication.</summary>
        /// <param name="parent">The parent.</param>
        /// <param name="channel">The channel.</param>
        /// <param name="level">The level.</param>
        /// <param name="authInfo">The authentication info.</param>
        /// <param name="checkboxLabel">The checkbox label.</param>
        /// <param name="checkValue">The check value.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool PromptAuth(
            nsIDOMWindow parent,
            nsIChannel channel,
            uint level,
            nsIAuthInformation authInfo,
            string checkboxLabel,
            ref bool checkValue)
        {
            return PromptService.PromptAuth(channel, level, authInfo);
        }

        /// <summary>The async prompt authentication.</summary>
        /// <param name="parent">The parent.</param>
        /// <param name="channel">The channel.</param>
        /// <param name="callback">The callback.</param>
        /// <param name="context">The context.</param>
        /// <param name="level">The level.</param>
        /// <param name="authInfo">The authentication info.</param>
        /// <param name="checkboxLabel">The checkbox label.</param>
        /// <param name="checkValue">The check value.</param>
        /// <returns>The <see cref="nsICancelable"/>.</returns>
        public nsICancelable AsyncPromptAuth(
            nsIDOMWindow parent,
            nsIChannel channel,
            nsIAuthPromptCallback callback,
            nsISupports context,
            uint level,
            nsIAuthInformation authInfo,
            string checkboxLabel,
            ref bool checkValue)
        {
            return PromptService.AsyncPromptAuth(channel, callback, context, level, authInfo);
        }

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
    }
}