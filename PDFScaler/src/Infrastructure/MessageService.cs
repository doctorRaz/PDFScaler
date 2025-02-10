using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

using drz.Abstractions.Interfaces;
using drz.PdfSharp_ConversionFactor;
using drz.Servise;

using Microsoft.VisualBasic;

using MessageBox = System.Windows.MessageBox;

#if NC
using HostMgd.ApplicationServices;
using HostMgd.EditorInput;
using Application = HostMgd.ApplicationServices.Application;
#endif

namespace drz.Infrastructure
{
    internal partial class MessageService :  IMessageService,IInputBoxService,IQuestionService
    {
        
        #region Message


        public void InfoMessage(string Message,
                                string Title = null,
                                    [CallerMemberName] string CallerName = null)
        {
            _title = Title;

#if DEBUG
            MessageBox.Show($"{CallerName} : {Message}", title, MessageBoxButton.OK, MessageBoxImage.Information);
#else
            MessageBox.Show($"{Message}", title, MessageBoxButton.OK, MessageBoxImage.Information);
#endif
        }

        public void WarningMessage(string Message,
                                    string Title = null,
                                   [CallerMemberName] string CallerName = null)
        {
            _title = Title;

#if DEBUG
            MessageBox.Show($"{CallerName} : {Message}", title, MessageBoxButton.OK, MessageBoxImage.Warning);
#else
            MessageBox.Show($"{Message}", title, MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
        }
        public void ErrorMessage(string Message,
                                 string Title = null,
                                 [CallerMemberName] string CallerName = null)
        {
            _title = Title;
#if DEBUG
            MessageBox.Show($"{CallerName} : {Message}", title, MessageBoxButton.OK, MessageBoxImage.Error);
#else
            MessageBox.Show($"{Message}", title, MessageBoxButton.OK, MessageBoxImage.Error);
#endif
        }

        public void ExceptionMessage(Exception Ex,
                                     string Title = null,
                                     [CallerMemberName] string CallerName = null)
        {
            _title = Title;
            MessageBox.Show($"{Ex.Message}\n{CallerName}\n{Ex.StackTrace}", title, MessageBoxButton.OK,
                MessageBoxImage.Stop);
        }
        #endregion


        #region InputBox
        public string GetTextByInputBox(string Message,
                                        string Title = null,
                                        string DefaultValue = "")
        {
            _title = Title;
            return Interaction.InputBox(Message, title, DefaultValue);
        }
        #endregion


        #region Question

        public WindowResult QuestionYesNo(string Message,
                                          string Title = null,
                                          WindowResult DefaultResult = WindowResult.Yes)
        {
            _title = Title;

            MessageBoxResult defaultresult = ConvertEnumWindowToMsgBox(DefaultResult);

            MessageBoxResult result = MessageBox.Show(Message,
                                                      title,
                                                      MessageBoxButton.YesNo,
                                                      MessageBoxImage.Question,
                                                      defaultresult);
            return ConvertEnumMsgBoxToWindow(result);

        }

        public WindowResult QuestionYesNoCancel(string Message,
                                                string Title = null,
                                                WindowResult DefaultResult = WindowResult.Yes)
        {
            _title = Title;

            MessageBoxResult defaultresult = ConvertEnumWindowToMsgBox(DefaultResult);

            MessageBoxResult result = MessageBox.Show(Message,
                                                      title,
                                                      MessageBoxButton.YesNoCancel,
                                                      MessageBoxImage.Question,
                                                      defaultresult);

            return ConvertEnumMsgBoxToWindow(result);
        }

        public WindowResult QuestionOKCancel(string Message,
                                             string Title = null,
                                             WindowResult DefaultResult = WindowResult.OK)
        {
            _title = Title;
            MessageBoxResult defaultresult = ConvertEnumWindowToMsgBox(DefaultResult);
            MessageBoxResult result = MessageBox.Show(Message,
                                                      title,
                                                      MessageBoxButton.OKCancel,
                                                      MessageBoxImage.Question,
                                                      defaultresult);

            return ConvertEnumMsgBoxToWindow(result);
        }

        #endregion


        #region Convert Enum

        /// <summary>
        /// Converts the enum MessageBoxResult to WindowResult.
        /// </summary>
        /// <param name="MsgBoxRes">MessageBoxResult</param>
        /// <returns>WindowResult</returns>
        /// <exception cref="System.ComponentModel.InvalidEnumArgumentException"></exception>
        WindowResult ConvertEnumMsgBoxToWindow(MessageBoxResult MsgBoxRes)
        {
            switch (MsgBoxRes)
            {
                case MessageBoxResult.None: return WindowResult.None;
                case MessageBoxResult.OK: return WindowResult.OK;
                case MessageBoxResult.Cancel: return WindowResult.Cancel;
                case MessageBoxResult.Yes: return WindowResult.Yes;
                case MessageBoxResult.No: return WindowResult.No;
                default: throw new InvalidEnumArgumentException();
            }
        }

        /// <summary>
        /// Converts the enum WindowResult to MessageBoxResult.
        /// </summary>
        /// <param name="WinRes">WindowResult</param>
        /// <returns>MessageBoxResult</returns>
        /// <exception cref="System.ComponentModel.InvalidEnumArgumentException"></exception>
        MessageBoxResult ConvertEnumWindowToMsgBox(WindowResult WinRes)
        {
            switch (WinRes)
            {
                case WindowResult.None: return MessageBoxResult.None;
                case WindowResult.OK: return MessageBoxResult.OK;
                case WindowResult.Cancel: return MessageBoxResult.Cancel;
                case WindowResult.Yes: return MessageBoxResult.Yes;
                case WindowResult.No: return MessageBoxResult.No;
                default: throw new InvalidEnumArgumentException();
            }
        }
        #endregion

        string _title;

        string title
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_title)) return DataSetWpfOpt.sTitleAttribute + " " + DataSetWpfOpt.sVersion;
                else return _title;
            }
        }
    }

}
