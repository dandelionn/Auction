//-----------------------------------------------------------------------
// <copyright file="Log4NetWrapper.cs" company="Transilvania University of Brasov">    
// Author: Paul Michea  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Log
{
    using System;
    using log4net;

    /// <summary>
    /// Defines the .<see cref="Log4NetWrapper" />.
    /// </summary>
    public class Log4NetWrapper
    {
        /// <summary>
        /// Defines the logger.
        /// </summary>
        private readonly ILog logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Log4NetWrapper"/> class.
        /// </summary>
        /// <param name="type">The type.<see cref="Type"/>.</param>
        public Log4NetWrapper(Type type)
        {
            this.logger = LogManager.GetLogger(type);
        }

        /// <summary>
        /// Function for logging a message.
        /// </summary>
        /// <param name="message">The Message.<see cref="object"/>.</param>
        public void Info(object message)
        {
            if (this.logger.IsInfoEnabled)
            {
                this.logger.Info(message);
            }
        }

        /// <summary>
        /// Function for logging Info.
        /// </summary>
        /// <param name="message">The Message.<see cref="object"/>.</param>
        /// <param name="exception">The exception.<see cref="Exception"/>.</param>
        public void Info(object message, Exception exception)
        {
            if (this.logger.IsInfoEnabled)
            {
                this.logger.Info(message, exception);
            }
        }

        /// <summary>
        /// The InfoFormat.
        /// </summary>
        /// <param name="provider">The provider.<see cref="IFormatProvider"/>.</param>
        /// <param name="format">The format.<see cref="string"/>.</param>
        /// <param name="args">The args.<see cref="object[]"/>.</param>
        public void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            if (this.logger.IsInfoEnabled)
            {
                this.logger.InfoFormat(provider, format, args);
            }
        }

        /// <summary>
        /// The InfoFormat.
        /// </summary>
        /// <param name="format">The format.<see cref="string"/>.</param>
        /// <param name="arg0">The arg0.<see cref="object"/>.</param>
        /// <param name="arg1">The arg1.<see cref="object"/>.</param>
        /// <param name="arg2">The arg2.<see cref="object"/>.</param>
        public void InfoFormat(string format, object arg0, object arg1, object arg2)
        {
            if (this.logger.IsInfoEnabled)
            {
                this.logger.InfoFormat(format, arg0, arg1, arg2);
            }
        }

        /// <summary>
        /// The InfoFormat.
        /// </summary>
        /// <param name="format">The format.<see cref="string"/>.</param>
        /// <param name="arg0">The arg0.<see cref="object"/>.</param>
        /// <param name="arg1">The arg1.<see cref="object"/>.</param>
        public void InfoFormat(string format, object arg0, object arg1)
        {
            if (this.logger.IsInfoEnabled)
            {
                this.logger.InfoFormat(format, arg0, arg1);
            }
        }

        /// <summary>
        /// The InfoFormat.
        /// </summary>
        /// <param name="format">The format.<see cref="string"/>.</param>
        /// <param name="arg0">The arg0.<see cref="object"/>.</param>
        public void InfoFormat(string format, object arg0)
        {
            if (this.logger.IsInfoEnabled)
            {
                this.logger.InfoFormat(format, arg0);
            }
        }

        /// <summary>
        /// The InfoFormat.
        /// </summary>
        /// <param name="format">The format.<see cref="string"/>.</param>
        /// <param name="args">The args.<see cref="object[]"/>.</param>
        public void InfoFormat(string format, params object[] args)
        {
            if (this.logger.IsInfoEnabled)
            {
                this.logger.InfoFormat(format, args);
            }
        }

        /// <summary>
        /// The Debug.
        /// </summary>
        /// <param name="message">The Message.<see cref="object"/>.</param>
        public void Debug(object message)
        {
            if (this.logger.IsDebugEnabled)
            {
                this.logger.Debug(message);
            }
        }

        /// <summary>
        /// The Debug.
        /// </summary>
        /// <param name="message">The Message.<see cref="object"/>.</param>
        /// <param name="exception">The exception.<see cref="Exception"/>.</param>
        public void Debug(object message, Exception exception)
        {
            if (this.logger.IsDebugEnabled)
            {
                this.logger.Debug(message, exception);
            }
        }

        /// <summary>
        /// The DebugFormat.
        /// </summary>
        /// <param name="provider">The provider.<see cref="IFormatProvider"/>.</param>
        /// <param name="format">The format.<see cref="string"/>.</param>
        /// <param name="args">The args.<see cref="object[]"/>.</param>
        public void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            if (this.logger.IsDebugEnabled)
            {
                this.logger.DebugFormat(provider, format, args);
            }
        }

        /// <summary>
        /// The DebugFormat.
        /// </summary>
        /// <param name="format">The format.<see cref="string"/>.</param>
        /// <param name="arg0">The arg0.<see cref="object"/>.</param>
        /// <param name="arg1">The arg1.<see cref="object"/>.</param>
        /// <param name="arg2">The arg2.<see cref="object"/>.</param>
        public void DebugFormat(string format, object arg0, object arg1, object arg2)
        {
            if (this.logger.IsDebugEnabled)
            {
                this.logger.DebugFormat(format, arg0, arg1, arg2);
            }
        }

        /// <summary>
        /// The DebugFormat.
        /// </summary>
        /// <param name="format">The format.<see cref="string"/>.</param>
        /// <param name="arg0">The arg0.<see cref="object"/>.</param>
        /// <param name="arg1">The arg1.<see cref="object"/>.</param>
        public void DebugFormat(string format, object arg0, object arg1)
        {
            if (this.logger.IsDebugEnabled)
            {
                this.logger.DebugFormat(format, arg0, arg1);
            }
        }

        /// <summary>
        /// The DebugFormat.
        /// </summary>
        /// <param name="format">The format.<see cref="string"/>.</param>
        /// <param name="arg0">The arg0.<see cref="object"/>.</param>
        public void DebugFormat(string format, object arg0)
        {
            if (this.logger.IsDebugEnabled)
            {
                this.logger.DebugFormat(format, arg0);
            }
        }

        /// <summary>
        /// The DebugFormat.
        /// </summary>
        /// <param name="format">The format.<see cref="string"/>.</param>
        /// <param name="args">The args.<see cref="object[]"/>.</param>
        public void DebugFormat(string format, params object[] args)
        {
            if (this.logger.IsDebugEnabled)
            {
                this.logger.DebugFormat(format, args);
            }
        }

        /// <summary>
        /// Function for logging a warning message.
        /// </summary>
        /// <param name="message">The Message.<see cref="object"/>.</param>
        public void Warn(object message)
        {
            if (this.logger.IsWarnEnabled)
            {
                this.logger.Warn(message);
            }
        }

        /// <summary>
        /// Function for logging a warning message with an exception.
        /// </summary>
        /// <param name="message">The Message.<see cref="object"/>.</param>
        /// <param name="exception">The exception.<see cref="Exception"/>.</param>
        public void Warn(object message, Exception exception)
        {
            if (this.logger.IsWarnEnabled)
            {
                this.logger.Warn(message, exception);
            }
        }

        /// <summary>
        /// The WarnFormat.
        /// </summary>
        /// <param name="provider">The provider.<see cref="IFormatProvider"/>.</param>
        /// <param name="format">The format.<see cref="string"/>.</param>
        /// <param name="args">The args.<see cref="object[]"/>.</param>
        public void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            if (this.logger.IsWarnEnabled)
            {
                this.logger.WarnFormat(provider, format, args);
            }
        }

        /// <summary>
        /// The WarnFormat.
        /// </summary>
        /// <param name="format">The format.<see cref="string"/>.</param>
        /// <param name="arg0">The arg0.<see cref="object"/>.</param>
        /// <param name="arg1">The arg1.<see cref="object"/>.</param>
        /// <param name="arg2">The arg2.<see cref="object"/>.</param>
        public void WarnFormat(string format, object arg0, object arg1, object arg2)
        {
            if (this.logger.IsWarnEnabled)
            {
                this.logger.WarnFormat(format, arg0, arg1, arg2);
            }
        }

        /// <summary>
        /// The WarnFormat.
        /// </summary>
        /// <param name="format">The format.<see cref="string"/>.</param>
        /// <param name="arg0">The arg0.<see cref="object"/>.</param>
        /// <param name="arg1">The arg1.<see cref="object"/>.</param>
        public void WarnFormat(string format, object arg0, object arg1)
        {
            if (this.logger.IsWarnEnabled)
            {
                this.logger.WarnFormat(format, arg0, arg1);
            }
        }

        /// <summary>
        /// The WarnFormat.
        /// </summary>
        /// <param name="format">The format.<see cref="string"/>.</param>
        /// <param name="arg0">The arg0.<see cref="object"/>.</param>
        public void WarnFormat(string format, object arg0)
        {
            if (this.logger.IsWarnEnabled)
            {
                this.logger.WarnFormat(format, arg0);
            }
        }

        /// <summary>
        /// The WarnFormat.
        /// </summary>
        /// <param name="format">The format.<see cref="string"/>.</param>
        /// <param name="args">The args.<see cref="object[]"/>.</param>
        public void WarnFormat(string format, params object[] args)
        {
            if (this.logger.IsWarnEnabled)
            {
                this.logger.WarnFormat(format, args);
            }
        }

        /// <summary>
        /// The Error.
        /// </summary>
        /// <param name="message">The Message.<see cref="object"/>.</param>
        public void Error(object message)
        {
            if (this.logger.IsErrorEnabled)
            {
                this.logger.Error(message);
            }
        }

        /// <summary>
        /// The Error.
        /// </summary>
        /// <param name="message">The Message.<see cref="object"/>.</param>
        /// <param name="exception">The exception.<see cref="Exception"/>.</param>
        public void Error(object message, Exception exception)
        {
            if (this.logger.IsErrorEnabled)
            {
                this.logger.Error(message, exception);
            }
        }

        /// <summary>
        /// The ErrorFormat.
        /// </summary>
        /// <param name="provider">The provider.<see cref="IFormatProvider"/>.</param>
        /// <param name="format">The format.<see cref="string"/>.</param>
        /// <param name="args">The args.<see cref="object[]"/>.</param>
        public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            if (this.logger.IsErrorEnabled)
            {
                this.logger.ErrorFormat(provider, format, args);
            }
        }

        /// <summary>
        /// The ErrorFormat.
        /// </summary>
        /// <param name="format">The format.<see cref="string"/>.</param>
        /// <param name="arg0">The arg0.<see cref="object"/>.</param>
        /// <param name="arg1">The arg1.<see cref="object"/>.</param>
        /// <param name="arg2">The arg2.<see cref="object"/>.</param>
        public void ErrorFormat(string format, object arg0, object arg1, object arg2)
        {
            if (this.logger.IsErrorEnabled)
            {
                this.logger.ErrorFormat(format, arg0, arg1, arg2);
            }
        }

        /// <summary>
        /// The ErrorFormat.
        /// </summary>
        /// <param name="format">The format.<see cref="string"/>.</param>
        /// <param name="arg0">The arg0.<see cref="object"/>.</param>
        /// <param name="arg1">The arg1.<see cref="object"/>.</param>
        public void ErrorFormat(string format, object arg0, object arg1)
        {
            if (this.logger.IsErrorEnabled)
            {
                this.logger.ErrorFormat(format, arg0, arg1);
            }
        }

        /// <summary>
        /// The ErrorFormat.
        /// </summary>
        /// <param name="format">The format.<see cref="string"/>.</param>
        /// <param name="arg0">The arg0.<see cref="object"/>.</param>
        public void ErrorFormat(string format, object arg0)
        {
            if (this.logger.IsErrorEnabled)
            {
                this.logger.ErrorFormat(format, arg0);
            }
        }

        /// <summary>
        /// The ErrorFormat.
        /// </summary>
        /// <param name="format">The format.<see cref="string"/>.</param>
        /// <param name="args">The args.<see cref="object[]"/>.</param>
        public void ErrorFormat(string format, params object[] args)
        {
            if (this.logger.IsErrorEnabled)
            {
                this.logger.ErrorFormat(format, args);
            }
        }

        /// <summary>
        /// The Fatal.
        /// </summary>
        /// <param name="message">The Message.<see cref="object"/>.</param>
        public void Fatal(object message)
        {
            if (this.logger.IsFatalEnabled)
            {
                this.logger.Fatal(message);
            }
        }

        /// <summary>
        /// The Fatal.
        /// </summary>
        /// <param name="message">The Message.<see cref="object"/>.</param>
        /// <param name="exception">The exception.<see cref="Exception"/>.</param>
        public void Fatal(object message, Exception exception)
        {
            if (this.logger.IsFatalEnabled)
            {
                this.logger.Fatal(message, exception);
            }
        }

        /// <summary>
        /// The FatalFormat.
        /// </summary>
        /// <param name="provider">The provider.<see cref="IFormatProvider"/>.</param>
        /// <param name="format">The format.<see cref="string"/>.</param>
        /// <param name="args">The args.<see cref="object[]"/>.</param>
        public void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            if (this.logger.IsFatalEnabled)
            {
                this.logger.FatalFormat(provider, format, args);
            }
        }

        /// <summary>
        /// The FatalFormat.
        /// </summary>
        /// <param name="format">The format.<see cref="string"/>.</param>
        /// <param name="arg0">The arg0.<see cref="object"/>.</param>
        /// <param name="arg1">The arg1.<see cref="object"/>.</param>
        /// <param name="arg2">The arg2.<see cref="object"/>.</param>
        public void FatalFormat(string format, object arg0, object arg1, object arg2)
        {
            if (this.logger.IsFatalEnabled)
            {
                this.logger.FatalFormat(format, arg0, arg1, arg2);
            }
        }

        /// <summary>
        /// The FatalFormat.
        /// </summary>
        /// <param name="format">The format.<see cref="string"/>.</param>
        /// <param name="arg0">The arg0.<see cref="object"/>.</param>
        /// <param name="arg1">The arg1.<see cref="object"/>.</param>
        public void FatalFormat(string format, object arg0, object arg1)
        {
            if (this.logger.IsFatalEnabled)
            {
                this.logger.FatalFormat(format, arg0, arg1);
            }
        }

        /// <summary>
        /// The FatalFormat.
        /// </summary>
        /// <param name="format">The format.<see cref="string"/>.</param>
        /// <param name="arg0">The arg0.<see cref="object"/>.</param>
        public void FatalFormat(string format, object arg0)
        {
            if (this.logger.IsFatalEnabled)
            {
                this.logger.FatalFormat(format, arg0);
            }
        }

        /// <summary>
        /// The FatalFormat.
        /// </summary>
        /// <param name="format">The format.<see cref="string"/>.</param>
        /// <param name="args">The args.<see cref="object[]"/>.</param>
        public void FatalFormat(string format, params object[] args)
        {
            if (this.logger.IsFatalEnabled)
            {
                this.logger.FatalFormat(format, args);
            }
        }
    }
}
