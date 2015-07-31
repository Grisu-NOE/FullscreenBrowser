// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Log4NetExtensions.cs" company="Freiwillige Feuerwehr Krems/Donau">
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
[module: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
// ReSharper disable CheckNamespace
namespace At.FF.Krems.Utils.Logging
// ReSharper restore CheckNamespace
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Reflection;

    using log4net;
    using log4net.Core;
    using log4net.Util;

    /// <summary>Log4net Extension Methods.</summary>
    public static class Log4NetExtensions
    {
        /// <overloads>Log a message object with the <see cref="F:log4net.Core.Level.Debug" /> level.</overloads>
        /// <summary>
        /// Log a message object with the <see cref="F:log4net.Core.Level.Debug" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="message">The message object to log.</param>
        /// <remarks>
        /// <para>
        /// This method first checks if this logger is <c>Debug</c>
        /// enabled by comparing the level of this logger with the 
        /// <see cref="F:log4net.Core.Level.Debug" /> level. If this logger is
        /// <c>Debug</c> enabled, then it converts the message object
        /// (passed as parameter) to a string by invoking the appropriate
        /// <see cref="T:log4net.ObjectRenderer.IObjectRenderer" />. It then 
        /// proceeds to call all the registered appenders in this logger 
        /// and also higher in the hierarchy depending on the value of 
        /// the additivity flag.
        /// </para>
        /// <para><b>WARNING</b> Note that passing an <see cref="T:System.Exception" /> 
        /// to this method will print the name of the <see cref="T:System.Exception" /> 
        /// but no stack trace. To print a stack trace use the 
        /// <see cref="M:log4net.ILog.Debug(System.Object,System.Exception)" /> form instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:log4net.ILog.Debug(System.Object,System.Exception)" />
        /// <seealso cref="P:log4net.ILog.IsDebugEnabled" />
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public static void DebugFast(this ILog logger, object message)
        {
            if (logger.IsDebugEnabled)
            {
                logger.Debug(message);
            }
        }

        /// <summary>
        /// Log a message object with the <see cref="F:log4net.Core.Level.Debug" /> level including
        /// the stack trace of the <see cref="T:System.Exception" /> passed
        /// as a parameter.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <remarks>
        /// <para>
        /// See the <see cref="M:log4net.ILog.Debug(System.Object)" /> form for more detailed information.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:log4net.ILog.Debug(System.Object)" />
        /// <seealso cref="P:log4net.ILog.IsDebugEnabled" />
        public static void DebugFast(this ILog logger, object message, Exception exception)
        {
            if (logger.IsDebugEnabled)
            {
                logger.Debug(message, exception);
            }
        }

        /// <overloads>Log a formatted string with the <see cref="F:log4net.Core.Level.Debug" /> level.</overloads>
        /// <summary>
        /// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Debug" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])" /> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception" /> object to include in the
        /// log event. To pass an <see cref="T:System.Exception" /> use one of the <see cref="M:log4net.ILog.Debug(System.Object,System.Exception)" />
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:log4net.ILog.Debug(System.Object)" />
        /// <seealso cref="P:log4net.ILog.IsDebugEnabled" />
        public static void DebugFormatFast(this ILog logger, string format, params object[] args)
        {
            if (logger.IsDebugEnabled)
            {
                logger.DebugFormat(format, args);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Debug" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])" /> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception" /> object to include in the
        /// log event. To pass an <see cref="T:System.Exception" /> use one of the <see cref="M:log4net.ILog.Debug(System.Object,System.Exception)" />
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:log4net.ILog.Debug(System.Object)" />
        /// <seealso cref="P:log4net.ILog.IsDebugEnabled" />
        public static void DebugFormatFast(this ILog logger, string format, object arg0)
        {
            if (logger.IsDebugEnabled)
            {
                logger.DebugFormat(format, arg0);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Debug" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object0 to format</param>
        /// <param name="arg1">An Object1 to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])" /> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception" /> object to include in the
        /// log event. To pass an <see cref="T:System.Exception" /> use one of the <see cref="M:log4net.ILog.Debug(System.Object,System.Exception)" />
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:log4net.ILog.Debug(System.Object)" />
        /// <seealso cref="P:log4net.ILog.IsDebugEnabled" />
        public static void DebugFormatFast(this ILog logger, string format, object arg0, object arg1)
        {
            if (logger.IsDebugEnabled)
            {
                logger.DebugFormat(format, arg0, arg1);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Debug" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object0 to format</param>
        /// <param name="arg1">An Object1 to format</param>
        /// <param name="arg2">An Object2 to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])" /> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception" /> object to include in the
        /// log event. To pass an <see cref="T:System.Exception" /> use one of the <see cref="M:log4net.ILog.Debug(System.Object,System.Exception)" />
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:log4net.ILog.Debug(System.Object)" />
        /// <seealso cref="P:log4net.ILog.IsDebugEnabled" />
        public static void DebugFormatFast(this ILog logger, string format, object arg0, object arg1, object arg2)
        {
            if (logger.IsDebugEnabled)
            {
                logger.DebugFormat(format, arg0, arg1, arg2);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Debug" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="provider">An <see cref="T:System.IFormatProvider" /> that supplies culture-specific formatting information</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])" /> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception" /> object to include in the
        /// log event. To pass an <see cref="T:System.Exception" /> use one of the <see cref="M:log4net.ILog.Debug(System.Object,System.Exception)" />
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:log4net.ILog.Debug(System.Object)" />
        /// <seealso cref="P:log4net.ILog.IsDebugEnabled" />
        public static void DebugFormatFast(this ILog logger, IFormatProvider provider, string format, params object[] args)
        {
            if (logger.IsDebugEnabled)
            {
                logger.DebugFormat(provider, format, args);
            }
        }

        /// <overloads>Log a message object with the <see cref="F:log4net.Core.Level.Debug" /> level.</overloads>
        /// <summary>
        /// Log a message object with the <see cref="F:log4net.Core.Level.Info" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="message">The message object to log.</param>
        /// <remarks>
        /// <para>
        /// This method first checks if this logger is <c>Info</c>
        /// enabled by comparing the level of this logger with the 
        /// <see cref="F:log4net.Core.Level.Info" /> level. If this logger is
        /// <c>Info</c> enabled, then it converts the message object
        /// (passed as parameter) to a string by invoking the appropriate
        /// <see cref="T:log4net.ObjectRenderer.IObjectRenderer" />. It then 
        /// proceeds to call all the registered appenders in this logger 
        /// and also higher in the hierarchy depending on the value of 
        /// the additivity flag.
        /// </para>
        /// <para><b>WARNING</b> Note that passing an <see cref="T:System.Exception" /> 
        /// to this method will print the name of the <see cref="T:System.Exception" /> 
        /// but no stack trace. To print a stack trace use the 
        /// <see cref="M:log4net.ILog.Info(System.Object,System.Exception)" /> form instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:log4net.ILog.Info(System.Object,System.Exception)" />
        /// <seealso cref="P:log4net.ILog.IsInfoEnabled" />
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public static void InfoFast(this ILog logger, object message)
        {
            if (logger.IsInfoEnabled)
            {
                logger.Info(message);
            }
        }

        /// <summary>
        /// Log a message object with the <see cref="F:log4net.Core.Level.Info" /> level including
        /// the stack trace of the <see cref="T:System.Exception" /> passed
        /// as a parameter.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <remarks>
        /// <para>
        /// See the <see cref="M:log4net.ILog.Info(System.Object)" /> form for more detailed information.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:log4net.ILog.Info(System.Object)" />
        /// <seealso cref="P:log4net.ILog.IsInfoEnabled" />
        public static void InfoFast(this ILog logger, object message, Exception exception)
        {
            if (logger.IsInfoEnabled)
            {
                logger.Info(message, exception);
            }
        }

        /// <overloads>Log a formatted string with the <see cref="F:log4net.Core.Level.Info" /> level.</overloads>
        /// <summary>
        /// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Info" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])" /> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception" /> object to include in the
        /// log event. To pass an <see cref="T:System.Exception" /> use one of the <see cref="M:log4net.ILog.Info(System.Object,System.Exception)" />
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:log4net.ILog.Info(System.Object)" />
        /// <seealso cref="P:log4net.ILog.IsInfoEnabled" />
        public static void InfoFormatFast(this ILog logger, string format, params object[] args)
        {
            if (logger.IsInfoEnabled)
            {
                logger.InfoFormat(format, args);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Info" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])" /> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception" /> object to include in the
        /// log event. To pass an <see cref="T:System.Exception" /> use one of the <see cref="M:log4net.ILog.Info(System.Object,System.Exception)" />
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:log4net.ILog.Info(System.Object)" />
        /// <seealso cref="P:log4net.ILog.IsInfoEnabled" />
        public static void InfoFormatFast(this ILog logger, string format, object arg0)
        {
            if (logger.IsInfoEnabled)
            {
                logger.InfoFormat(format, arg0);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Info" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object0 to format</param>
        /// <param name="arg1">An Object1 to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])" /> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception" /> object to include in the
        /// log event. To pass an <see cref="T:System.Exception" /> use one of the <see cref="M:log4net.ILog.Info(System.Object,System.Exception)" />
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:log4net.ILog.Info(System.Object)" />
        /// <seealso cref="P:log4net.ILog.IsInfoEnabled" />
        public static void InfoFormatFast(this ILog logger, string format, object arg0, object arg1)
        {
            if (logger.IsInfoEnabled)
            {
                logger.InfoFormat(format, arg0, arg1);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Info" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object0 to format</param>
        /// <param name="arg1">An Object1 to format</param>
        /// <param name="arg2">An Object2 to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])" /> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception" /> object to include in the
        /// log event. To pass an <see cref="T:System.Exception" /> use one of the <see cref="M:log4net.ILog.Info(System.Object,System.Exception)" />
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:log4net.ILog.Info(System.Object)" />
        /// <seealso cref="P:log4net.ILog.IsInfoEnabled" />
        public static void InfoFormatFast(this ILog logger, string format, object arg0, object arg1, object arg2)
        {
            if (logger.IsInfoEnabled)
            {
                logger.InfoFormat(format, arg0, arg1, arg2);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Info" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="provider">An <see cref="T:System.IFormatProvider" /> that supplies culture-specific formatting information</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])" /> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception" /> object to include in the
        /// log event. To pass an <see cref="T:System.Exception" /> use one of the <see cref="M:log4net.ILog.Info(System.Object,System.Exception)" />
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:log4net.ILog.Info(System.Object)" />
        /// <seealso cref="P:log4net.ILog.IsInfoEnabled" />
        public static void InfoFormatFast(this ILog logger, IFormatProvider provider, string format, params object[] args)
        {
            if (logger.IsInfoEnabled)
            {
                logger.InfoFormat(provider, format, args);
            }
        }

        /// <overloads>Log a message object with the <see cref="F:log4net.Core.Level.Debug" /> level.</overloads>
        /// <summary>
        /// Log a message object with the <see cref="F:log4net.Core.Level.Warn" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="message">The message object to log.</param>
        /// <remarks>
        /// <para>
        /// This method first checks if this logger is <c>Warn</c>
        /// enabled by comparing the level of this logger with the 
        /// <see cref="F:log4net.Core.Level.Warn" /> level. If this logger is
        /// <c>Warn</c> enabled, then it converts the message object
        /// (passed as parameter) to a string by invoking the appropriate
        /// <see cref="T:log4net.ObjectRenderer.IObjectRenderer" />. It then 
        /// proceeds to call all the registered appenders in this logger 
        /// and also higher in the hierarchy depending on the value of 
        /// the additivity flag.
        /// </para>
        /// <para><b>WARNING</b> Note that passing an <see cref="T:System.Exception" /> 
        /// to this method will print the name of the <see cref="T:System.Exception" /> 
        /// but no stack trace. To print a stack trace use the 
        /// <see cref="M:log4net.ILog.Warn(System.Object,System.Exception)" /> form instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:log4net.ILog.Warn(System.Object,System.Exception)" />
        /// <seealso cref="P:log4net.ILog.IsWarnEnabled" />
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public static void WarnFast(this ILog logger, object message)
        {
            if (logger.IsWarnEnabled)
            {
                logger.Warn(message);
            }
        }

        /// <summary>
        /// Log a message object with the <see cref="F:log4net.Core.Level.Warn" /> level including
        /// the stack trace of the <see cref="T:System.Exception" /> passed
        /// as a parameter.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <remarks>
        /// <para>
        /// See the <see cref="M:log4net.ILog.Warn(System.Object)" /> form for more detailed information.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:log4net.ILog.Warn(System.Object)" />
        /// <seealso cref="P:log4net.ILog.IsWarnEnabled" />
        public static void WarnFast(this ILog logger, object message, Exception exception)
        {
            if (logger.IsWarnEnabled)
            {
                logger.Warn(message, exception);
            }
        }

        /// <overloads>Log a formatted string with the <see cref="F:log4net.Core.Level.Warn" /> level.</overloads>
        /// <summary>
        /// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Warn" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])" /> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception" /> object to include in the
        /// log event. To pass an <see cref="T:System.Exception" /> use one of the <see cref="M:log4net.ILog.Warn(System.Object,System.Exception)" />
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:log4net.ILog.Warn(System.Object)" />
        /// <seealso cref="P:log4net.ILog.IsWarnEnabled" />
        public static void WarnFormatFast(this ILog logger, string format, params object[] args)
        {
            if (logger.IsWarnEnabled)
            {
                logger.WarnFormat(format, args);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Warn" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])" /> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception" /> object to include in the
        /// log event. To pass an <see cref="T:System.Exception" /> use one of the <see cref="M:log4net.ILog.Warn(System.Object,System.Exception)" />
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:log4net.ILog.Warn(System.Object)" />
        /// <seealso cref="P:log4net.ILog.IsWarnEnabled" />
        public static void WarnFormatFast(this ILog logger, string format, object arg0)
        {
            if (logger.IsWarnEnabled)
            {
                logger.WarnFormat(format, arg0);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Warn" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object0 to format</param>
        /// <param name="arg1">An Object1 to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])" /> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception" /> object to include in the
        /// log event. To pass an <see cref="T:System.Exception" /> use one of the <see cref="M:log4net.ILog.Warn(System.Object,System.Exception)" />
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:log4net.ILog.Warn(System.Object)" />
        /// <seealso cref="P:log4net.ILog.IsWarnEnabled" />
        public static void WarnFormatFast(this ILog logger, string format, object arg0, object arg1)
        {
            if (logger.IsWarnEnabled)
            {
                logger.WarnFormat(format, arg0, arg1);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Warn" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object0 to format</param>
        /// <param name="arg1">An Object1 to format</param>
        /// <param name="arg2">An Object2 to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])" /> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception" /> object to include in the
        /// log event. To pass an <see cref="T:System.Exception" /> use one of the <see cref="M:log4net.ILog.Warn(System.Object,System.Exception)" />
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:log4net.ILog.Warn(System.Object)" />
        /// <seealso cref="P:log4net.ILog.IsWarnEnabled" />
        public static void WarnFormatFast(this ILog logger, string format, object arg0, object arg1, object arg2)
        {
            if (logger.IsWarnEnabled)
            {
                logger.WarnFormat(format, arg0, arg1, arg2);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Warn" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="provider">An <see cref="T:System.IFormatProvider" /> that supplies culture-specific formatting information</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])" /> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception" /> object to include in the
        /// log event. To pass an <see cref="T:System.Exception" /> use one of the <see cref="M:log4net.ILog.Warn(System.Object,System.Exception)" />
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:log4net.ILog.Warn(System.Object)" />
        /// <seealso cref="P:log4net.ILog.IsWarnEnabled" />
        public static void WarnFormatFast(this ILog logger, IFormatProvider provider, string format, params object[] args)
        {
            if (logger.IsWarnEnabled)
            {
                logger.WarnFormat(provider, format, args);
            }
        }

        /// <overloads>Log a message object with the <see cref="F:log4net.Core.Level.Debug" /> level.</overloads>
        /// <summary>
        /// Log a message object with the <see cref="F:log4net.Core.Level.Error" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="message">The message object to log.</param>
        /// <remarks>
        /// <para>
        /// This method first checks if this logger is <c>Error</c>
        /// enabled by comparing the level of this logger with the 
        /// <see cref="F:log4net.Core.Level.Error" /> level. If this logger is
        /// <c>Error</c> enabled, then it converts the message object
        /// (passed as parameter) to a string by invoking the appropriate
        /// <see cref="T:log4net.ObjectRenderer.IObjectRenderer" />. It then 
        /// proceeds to call all the registered appenders in this logger 
        /// and also higher in the hierarchy depending on the value of 
        /// the additivity flag.
        /// </para>
        /// <para><b>WARNING</b> Note that passing an <see cref="T:System.Exception" /> 
        /// to this method will print the name of the <see cref="T:System.Exception" /> 
        /// but no stack trace. To print a stack trace use the 
        /// <see cref="M:log4net.ILog.Error(System.Object,System.Exception)" /> form instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:log4net.ILog.Error(System.Object,System.Exception)" />
        /// <seealso cref="P:log4net.ILog.IsErrorEnabled" />
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public static void ErrorFast(this ILog logger, object message)
        {
            if (logger.IsErrorEnabled)
            {
                logger.Error(message);
            }
        }

        /// <summary>
        /// Log a message object with the <see cref="F:log4net.Core.Level.Error" /> level including
        /// the stack trace of the <see cref="T:System.Exception" /> passed
        /// as a parameter.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <remarks>
        /// <para>
        /// See the <see cref="M:log4net.ILog.Error(System.Object)" /> form for more detailed information.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:log4net.ILog.Error(System.Object)" />
        /// <seealso cref="P:log4net.ILog.IsErrorEnabled" />
        public static void ErrorFast(this ILog logger, object message, Exception exception)
        {
            if (logger.IsErrorEnabled)
            {
                logger.Error(message, exception);
            }
        }

        /// <overloads>Log a formatted string with the <see cref="F:log4net.Core.Level.Error" /> level.</overloads>
        /// <summary>
        /// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Error" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])" /> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception" /> object to include in the
        /// log event. To pass an <see cref="T:System.Exception" /> use one of the <see cref="M:log4net.ILog.Error(System.Object,System.Exception)" />
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:log4net.ILog.Error(System.Object)" />
        /// <seealso cref="P:log4net.ILog.IsErrorEnabled" />
        public static void ErrorFormatFast(this ILog logger, string format, params object[] args)
        {
            if (logger.IsErrorEnabled)
            {
                logger.ErrorFormat(format, args);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Error" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])" /> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception" /> object to include in the
        /// log event. To pass an <see cref="T:System.Exception" /> use one of the <see cref="M:log4net.ILog.Error(System.Object,System.Exception)" />
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:log4net.ILog.Error(System.Object)" />
        /// <seealso cref="P:log4net.ILog.IsErrorEnabled" />
        public static void ErrorFormatFast(this ILog logger, string format, object arg0)
        {
            if (logger.IsErrorEnabled)
            {
                logger.ErrorFormat(format, arg0);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Error" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object0 to format</param>
        /// <param name="arg1">An Object1 to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])" /> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception" /> object to include in the
        /// log event. To pass an <see cref="T:System.Exception" /> use one of the <see cref="M:log4net.ILog.Error(System.Object,System.Exception)" />
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:log4net.ILog.Error(System.Object)" />
        /// <seealso cref="P:log4net.ILog.IsErrorEnabled" />
        public static void ErrorFormatFast(this ILog logger, string format, object arg0, object arg1)
        {
            if (logger.IsErrorEnabled)
            {
                logger.ErrorFormat(format, arg0, arg1);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Error" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object0 to format</param>
        /// <param name="arg1">An Object1 to format</param>
        /// <param name="arg2">An Object2 to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])" /> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception" /> object to include in the
        /// log event. To pass an <see cref="T:System.Exception" /> use one of the <see cref="M:log4net.ILog.Error(System.Object,System.Exception)" />
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:log4net.ILog.Error(System.Object)" />
        /// <seealso cref="P:log4net.ILog.IsErrorEnabled" />
        public static void ErrorFormatFast(this ILog logger, string format, object arg0, object arg1, object arg2)
        {
            if (logger.IsErrorEnabled)
            {
                logger.ErrorFormat(format, arg0, arg1, arg2);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Error" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="provider">An <see cref="T:System.IFormatProvider" /> that supplies culture-specific formatting information</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])" /> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception" /> object to include in the
        /// log event. To pass an <see cref="T:System.Exception" /> use one of the <see cref="M:log4net.ILog.Error(System.Object,System.Exception)" />
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:log4net.ILog.Error(System.Object)" />
        /// <seealso cref="P:log4net.ILog.IsErrorEnabled" />
        public static void ErrorFormatFast(this ILog logger, IFormatProvider provider, string format, params object[] args)
        {
            if (logger.IsErrorEnabled)
            {
                logger.ErrorFormat(provider, format, args);
            }
        }

        /// <overloads>Log a message object with the <see cref="F:log4net.Core.Level.Debug" /> level.</overloads>
        /// <summary>
        /// Log a message object with the <see cref="F:log4net.Core.Level.Fatal" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="message">The message object to log.</param>
        /// <remarks>
        /// <para>
        /// This method first checks if this logger is <c>Fatal</c>
        /// enabled by comparing the level of this logger with the 
        /// <see cref="F:log4net.Core.Level.Fatal" /> level. If this logger is
        /// <c>Fatal</c> enabled, then it converts the message object
        /// (passed as parameter) to a string by invoking the appropriate
        /// <see cref="T:log4net.ObjectRenderer.IObjectRenderer" />. It then 
        /// proceeds to call all the registered appenders in this logger 
        /// and also higher in the hierarchy depending on the value of 
        /// the additivity flag.
        /// </para>
        /// <para><b>WARNING</b> Note that passing an <see cref="T:System.Exception" /> 
        /// to this method will print the name of the <see cref="T:System.Exception" /> 
        /// but no stack trace. To print a stack trace use the 
        /// <see cref="M:log4net.ILog.Fatal(System.Object,System.Exception)" /> form instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:log4net.ILog.Fatal(System.Object,System.Exception)" />
        /// <seealso cref="P:log4net.ILog.IsFatalEnabled" />
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public static void FatalFast(this ILog logger, object message)
        {
            if (logger.IsFatalEnabled)
            {
                logger.Fatal(message);
            }
        }

        /// <summary>
        /// Log a message object with the <see cref="F:log4net.Core.Level.Fatal" /> level including
        /// the stack trace of the <see cref="T:System.Exception" /> passed
        /// as a parameter.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <remarks>
        /// <para>
        /// See the <see cref="M:log4net.ILog.Fatal(System.Object)" /> form for more detailed information.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:log4net.ILog.Fatal(System.Object)" />
        /// <seealso cref="P:log4net.ILog.IsFatalEnabled" />
        public static void FatalFast(this ILog logger, object message, Exception exception)
        {
            if (logger.IsFatalEnabled)
            {
                logger.Fatal(message, exception);
            }
        }

        /// <overloads>Log a formatted string with the <see cref="F:log4net.Core.Level.Fatal" /> level.</overloads>
        /// <summary>
        /// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Fatal" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])" /> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception" /> object to include in the
        /// log event. To pass an <see cref="T:System.Exception" /> use one of the <see cref="M:log4net.ILog.Fatal(System.Object,System.Exception)" />
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:log4net.ILog.Fatal(System.Object)" />
        /// <seealso cref="P:log4net.ILog.IsFatalEnabled" />
        public static void FatalFormatFast(this ILog logger, string format, params object[] args)
        {
            if (logger.IsFatalEnabled)
            {
                logger.FatalFormat(format, args);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Fatal" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])" /> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception" /> object to include in the
        /// log event. To pass an <see cref="T:System.Exception" /> use one of the <see cref="M:log4net.ILog.Fatal(System.Object,System.Exception)" />
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:log4net.ILog.Fatal(System.Object)" />
        /// <seealso cref="P:log4net.ILog.IsFatalEnabled" />
        public static void FatalFormatFast(this ILog logger, string format, object arg0)
        {
            if (logger.IsFatalEnabled)
            {
                logger.FatalFormat(format, arg0);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Fatal" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object0 to format</param>
        /// <param name="arg1">An Object1 to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])" /> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception" /> object to include in the
        /// log event. To pass an <see cref="T:System.Exception" /> use one of the <see cref="M:log4net.ILog.Fatal(System.Object,System.Exception)" />
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:log4net.ILog.Fatal(System.Object)" />
        /// <seealso cref="P:log4net.ILog.IsFatalEnabled" />
        public static void FatalFormatFast(this ILog logger, string format, object arg0, object arg1)
        {
            if (logger.IsFatalEnabled)
            {
                logger.FatalFormat(format, arg0, arg1);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Fatal" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object0 to format</param>
        /// <param name="arg1">An Object1 to format</param>
        /// <param name="arg2">An Object2 to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])" /> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception" /> object to include in the
        /// log event. To pass an <see cref="T:System.Exception" /> use one of the <see cref="M:log4net.ILog.Fatal(System.Object,System.Exception)" />
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:log4net.ILog.Fatal(System.Object)" />
        /// <seealso cref="P:log4net.ILog.IsFatalEnabled" />
        public static void FatalFormatFast(this ILog logger, string format, object arg0, object arg1, object arg2)
        {
            if (logger.IsFatalEnabled)
            {
                logger.FatalFormat(format, arg0, arg1, arg2);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Fatal" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="provider">An <see cref="T:System.IFormatProvider" /> that supplies culture-specific formatting information</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])" /> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception" /> object to include in the
        /// log event. To pass an <see cref="T:System.Exception" /> use one of the <see cref="M:log4net.ILog.Fatal(System.Object,System.Exception)" />
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:log4net.ILog.Fatal(System.Object)" />
        /// <seealso cref="P:log4net.ILog.IsFatalEnabled" />
        public static void FatalFormatFast(this ILog logger, IFormatProvider provider, string format, params object[] args)
        {
            if (logger.IsFatalEnabled)
            {
                logger.FatalFormat(provider, format, args);
            }
        }

        /// <overloads>Log a message object with the <see cref="F:log4net.Core.Level.Debug" /> level.</overloads>
        /// <summary>
        /// Log a message object with the <see cref="F:log4net.Core.Level.Trace" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="message">The message object to log.</param>
        /// <remarks>
        /// <para>
        /// This method first checks if this logger is <c>Trace</c>
        /// enabled by comparing the level of this logger with the 
        /// <see cref="F:log4net.Core.Level.Trace" /> level. If this logger is
        /// <c>Trace</c> enabled, then it converts the message object
        /// (passed as parameter) to a string by invoking the appropriate
        /// <see cref="T:log4net.ObjectRenderer.IObjectRenderer" />. It then 
        /// proceeds to call all the registered appenders in this logger 
        /// and also higher in the hierarchy depending on the value of 
        /// the additivity flag.
        /// </para>
        /// <para><b>WARNING</b> Note that passing an <see cref="T:System.Exception" /> 
        /// to this method will print the name of the <see cref="T:System.Exception" /> 
        /// but no stack trace. To print a stack trace use the 
        /// <see><cref>M:log4net.ILog.Trace(System.Object,System.Exception)</cref></see> form instead.
        /// </para>
        /// </remarks>
        /// <seealso><cref>M:log4net.ILog.Trace(System.Object,System.Exception)</cref></seealso>
        /// <seealso><cref>P:log4net.ILog.IsTraceEnabled</cref></seealso>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public static void TraceFast(this ILog logger, object message)
        {
            if (logger.IsTraceEnabled())
            {
                logger.Trace(message);
            }
        }

        /// <summary>
        /// Log a message object with the <see cref="F:log4net.Core.Level.Trace" /> level including
        /// the stack trace of the <see cref="T:System.Exception" /> passed
        /// as a parameter.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <remarks>
        /// <para>
        /// See the <see><cref>M:log4net.ILog.Trace(System.Object)</cref></see> form for more detailed information.
        /// </para>
        /// </remarks>
        /// <seealso><cref>M:log4net.ILog.Trace(System.Object)</cref></seealso>
        /// <seealso><cref>P:log4net.ILog.IsTraceEnabled</cref></seealso>
        public static void TraceFast(this ILog logger, object message, Exception exception)
        {
            if (logger.IsTraceEnabled())
            {
                logger.Trace(message, exception);
            }
        }

        /// <overloads>Log a formatted string with the <see cref="F:log4net.Core.Level.Trace" /> level.</overloads>
        /// <summary>
        /// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Trace" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])" /> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception" /> object to include in the
        /// log event. To pass an <see cref="T:System.Exception" /> use one of the <see><cref>M:log4net.ILog.Trace(System.Object,System.Exception)</cref></see>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso><cref>M:log4net.ILog.Trace(System.Object)</cref></seealso>
        /// <seealso><cref>P:log4net.ILog.IsTraceEnabled</cref></seealso>
        public static void TraceFormatFast(this ILog logger, string format, params object[] args)
        {
            if (logger.IsTraceEnabled())
            {
                logger.TraceFormat(format, args);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Trace" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])" /> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception" /> object to include in the
        /// log event. To pass an <see cref="T:System.Exception" /> use one of the <see><cref>M:log4net.ILog.Trace(System.Object,System.Exception)</cref></see>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso><cref>M:log4net.ILog.Trace(System.Object)</cref></seealso>
        /// <seealso><cref>P:log4net.ILog.IsTraceEnabled</cref></seealso>
        public static void TraceFormatFast(this ILog logger, string format, object arg0)
        {
            if (logger.IsTraceEnabled())
            {
                logger.TraceFormat(format, arg0);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Trace" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object0 to format</param>
        /// <param name="arg1">An Object1 to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])" /> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception" /> object to include in the
        /// log event. To pass an <see cref="T:System.Exception" /> use one of the <see><cref>M:log4net.ILog.Trace(System.Object,System.Exception)</cref></see>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso><cref>M:log4net.ILog.Trace(System.Object)</cref></seealso>
        /// <seealso><cref>P:log4net.ILog.IsTraceEnabled</cref></seealso>
        public static void TraceFormatFast(this ILog logger, string format, object arg0, object arg1)
        {
            if (logger.IsTraceEnabled())
            {
                logger.TraceFormat(format, arg0, arg1);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Trace" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object0 to format</param>
        /// <param name="arg1">An Object1 to format</param>
        /// <param name="arg2">An Object2 to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])" /> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception" /> object to include in the
        /// log event. To pass an <see cref="T:System.Exception" /> use one of the <see><cref>M:log4net.ILog.Trace(System.Object,System.Exception)</cref></see>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso><cref>M:log4net.ILog.Trace(System.Object)</cref></seealso>
        /// <seealso><cref>P:log4net.ILog.IsTraceEnabled</cref></seealso>
        public static void TraceFormatFast(this ILog logger, string format, object arg0, object arg1, object arg2)
        {
            if (logger.IsTraceEnabled())
            {
                logger.TraceFormat(format, arg0, arg1, arg2);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Trace" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="provider">An <see cref="T:System.IFormatProvider" /> that supplies culture-specific formatting information</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])" /> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception" /> object to include in the
        /// log event. To pass an <see cref="T:System.Exception" /> use one of the <see><cref>M:log4net.ILog.Trace(System.Object,System.Exception)</cref></see>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso><cref>M:log4net.ILog.Trace(System.Object)</cref></seealso>
        /// <seealso><cref>P:log4net.ILog.IsTraceEnabled</cref></seealso>
        public static void TraceFormatFast(this ILog logger, IFormatProvider provider, string format, params object[] args)
        {
            if (logger.IsTraceEnabled())
            {
                logger.TraceFormat(provider, format, args);
            }
        }

        /// <overloads>Log a message object with the <see cref="F:log4net.Core.Level.Debug" /> level.</overloads>
        /// <summary>
        /// Log a message object with the <see cref="F:log4net.Core.Level.Notice" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="message">The message object to log.</param>
        /// <remarks>
        /// <para>
        /// This method first checks if this logger is <c>Notice</c>
        /// enabled by comparing the level of this logger with the 
        /// <see cref="F:log4net.Core.Level.Notice" /> level. If this logger is
        /// <c>Notice</c> enabled, then it converts the message object
        /// (passed as parameter) to a string by invoking the appropriate
        /// <see cref="T:log4net.ObjectRenderer.IObjectRenderer" />. It then 
        /// proceeds to call all the registered appenders in this logger 
        /// and also higher in the hierarchy depending on the value of 
        /// the additivity flag.
        /// </para>
        /// <para><b>WARNING</b> Note that passing an <see cref="T:System.Exception" /> 
        /// to this method will print the name of the <see cref="T:System.Exception" /> 
        /// but no stack trace. To print a stack trace use the 
        /// <see><cref>M:log4net.ILog.Notice(System.Object,System.Exception)</cref></see> form instead.
        /// </para>
        /// </remarks>
        /// <seealso><cref>M:log4net.ILog.Notice(System.Object,System.Exception)</cref></seealso>
        /// <seealso><cref>P:log4net.ILog.IsNoticeEnabled</cref></seealso>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public static void NoticeFast(this ILog logger, object message)
        {
            if (logger.IsNoticeEnabled())
            {
                logger.Notice(message);
            }
        }

        /// <summary>
        /// Log a message object with the <see cref="F:log4net.Core.Level.Notice" /> level including
        /// the stack trace of the <see cref="T:System.Exception" /> passed
        /// as a parameter.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        /// <remarks>
        /// <para>
        /// See the <see><cref>M:log4net.ILog.Notice(System.Object)</cref></see>
        /// form for more detailed information.
        /// </para>
        /// </remarks>
        /// <seealso><cref>M:log4net.ILog.Notice(System.Object)</cref></seealso>
        /// <seealso><cref>P:log4net.ILog.IsNoticeEnabled</cref></seealso>
        public static void NoticeFast(this ILog logger, object message, Exception exception)
        {
            if (logger.IsNoticeEnabled())
            {
                logger.Notice(message, exception);
            }
        }

        /// <overloads>Log a formatted string with the <see cref="F:log4net.Core.Level.Notice" /> level.</overloads>
        /// <summary>
        /// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Notice" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])" /> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception" /> object to include in the
        /// log event. To pass an <see cref="T:System.Exception" /> use one of the <see><cref>M:log4net.ILog.Notice(System.Object,System.Exception)</cref></see>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso><cref>M:log4net.ILog.Notice(System.Object)</cref></seealso>
        /// <seealso><cref>P:log4net.ILog.IsNoticeEnabled</cref></seealso>
        public static void NoticeFormatFast(this ILog logger, string format, params object[] args)
        {
            if (logger.IsNoticeEnabled())
            {
                logger.NoticeFormat(format, args);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Notice" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])" /> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception" /> object to include in the
        /// log event. To pass an <see cref="T:System.Exception" /> use one of the <see><cref>M:log4net.ILog.Notice(System.Object,System.Exception)</cref></see>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso><cref>M:log4net.ILog.Notice(System.Object)</cref></seealso>
        /// <seealso><cref>P:log4net.ILog.IsNoticeEnabled</cref></seealso>
        public static void NoticeFormatFast(this ILog logger, string format, object arg0)
        {
            if (logger.IsNoticeEnabled())
            {
                logger.NoticeFormat(format, arg0);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Notice" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object0 to format</param>
        /// <param name="arg1">An Object1 to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])" /> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception" /> object to include in the
        /// log event. To pass an <see cref="T:System.Exception" /> use one of the <see><cref>M:log4net.ILog.Notice(System.Object,System.Exception)</cref></see>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso><cref>M:log4net.ILog.Notice(System.Object)</cref></seealso>
        /// <seealso><cref>P:log4net.ILog.IsNoticeEnabled</cref></seealso>
        public static void NoticeFormatFast(this ILog logger, string format, object arg0, object arg1)
        {
            if (logger.IsNoticeEnabled())
            {
                logger.NoticeFormat(format, arg0, arg1);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Notice" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object0 to format</param>
        /// <param name="arg1">An Object1 to format</param>
        /// <param name="arg2">An Object2 to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])" /> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception" /> object to include in the
        /// log event. To pass an <see cref="T:System.Exception" /> use one of the <see><cref>M:log4net.ILog.Notice(System.Object,System.Exception)</cref></see>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso><cref>M:log4net.ILog.Notice(System.Object)</cref></seealso>
        /// <seealso><cref>P:log4net.ILog.IsNoticeEnabled</cref></seealso>
        public static void NoticeFormatFast(this ILog logger, string format, object arg0, object arg1, object arg2)
        {
            if (logger.IsNoticeEnabled())
            {
                logger.NoticeFormat(format, arg0, arg1, arg2);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the <see cref="F:log4net.Core.Level.Notice" /> level.
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <param name="provider">An <see cref="T:System.IFormatProvider" /> that supplies culture-specific formatting information</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        /// <remarks>
        /// <para>
        /// The message is formatted using the <c>String.Format</c> method. See
        /// <see cref="M:System.String.Format(System.String,System.Object[])" /> for details of the syntax of the format string and the behavior
        /// of the formatting.
        /// </para>
        /// <para>
        /// This method does not take an <see cref="T:System.Exception" /> object to include in the
        /// log event. To pass an <see cref="T:System.Exception" /> use one of the <see><cref>M:log4net.ILog.Notice(System.Object,System.Exception)</cref></see>
        /// methods instead.
        /// </para>
        /// </remarks>
        /// <seealso><cref>M:log4net.ILog.Notice(System.Object)</cref></seealso>
        /// <seealso><cref>P:log4net.ILog.IsNoticeEnabled</cref></seealso>
        public static void NoticeFormatFast(this ILog logger, IFormatProvider provider, string format, params object[] args)
        {
            if (logger.IsNoticeEnabled())
            {
                logger.NoticeFormat(provider, format, args);
            }
        }

        /// <summary>The trace.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        public static void Trace(this ILog logger, object message)
        {
            logger.Logger.Log(MethodBase.GetCurrentMethod().DeclaringType, Level.Trace, message, null);
        }

        /// <summary>The trace.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public static void Trace(this ILog logger, object message, Exception exception)
        {
            logger.Logger.Log(MethodBase.GetCurrentMethod().DeclaringType, Level.Trace, message, exception);
        }

        /// <summary>The trace format.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        public static void TraceFormat(this ILog logger, string format, params object[] args)
        {
            logger.Logger.Log(MethodBase.GetCurrentMethod().DeclaringType, Level.Trace, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), null);
        }

        /// <summary>The trace format.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        public static void TraceFormat(this ILog logger, IFormatProvider provider, string format, params object[] args)
        {
            logger.Logger.Log(MethodBase.GetCurrentMethod().DeclaringType, Level.Notice, new SystemStringFormat(provider, format, args), null);
        }

        /// <summary>
        /// IsTraceEnabled
        /// </summary>
        /// <param name="logger">log4net logger</param>
        /// <returns>Returns true if enabled.</returns>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed. Suppression is OK here.")]
        public static bool IsTraceEnabled(this ILog logger)
        {
            return logger.Logger.IsEnabledFor(Level.Trace);
        }

        /// <summary>The notice.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        public static void Notice(this ILog logger, object message)
        {
            logger.Logger.Log(MethodBase.GetCurrentMethod().DeclaringType, Level.Notice, message, null);
        }

        /// <summary>The notice.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public static void Notice(this ILog logger, object message, Exception exception)
        {
            logger.Logger.Log(MethodBase.GetCurrentMethod().DeclaringType, Level.Notice, message, exception);
        }

        /// <summary>The notice format.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        public static void NoticeFormat(this ILog logger, string format, params object[] args)
        {
            logger.Logger.Log(MethodBase.GetCurrentMethod().DeclaringType, Level.Notice, new SystemStringFormat(CultureInfo.InvariantCulture, format, args), null);
        }

        /// <summary>The notice format.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        public static void NoticeFormat(this ILog logger, IFormatProvider provider, string format, params object[] args)
        {
            logger.Logger.Log(MethodBase.GetCurrentMethod().DeclaringType, Level.Notice, new SystemStringFormat(provider, format, args), null);
        }

        /// <summary>Is notice enabled.</summary>
        /// <param name="logger">log4net logger</param>
        /// <returns>Returns true if enabled.</returns>
        public static bool IsNoticeEnabled(this ILog logger)
        {
            return logger.Logger.IsEnabledFor(Level.Notice);
        }
    }
}