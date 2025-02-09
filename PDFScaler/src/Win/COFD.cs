using System;
using System.Collections;
using System.Collections.Generic;

using Microsoft.WindowsAPICodePack.Dialogs;

namespace drz.Win
{
    /// <summary>
    /// Обертка над Microsoft.WindowsAPICodePack.Dialogs
    /// <br>для отлова эксепшн в WPF</br>
    /// </summary>
    internal class COFD : IEnumerable
    {
        /// <summary> CommonOpenFileDialog конструктор </summary>
        public COFD()
        {
            FSD = new CommonOpenFileDialog();
        }

        /// <summary>CommonOpenFileDialog</summary>
        public CommonOpenFileDialog FSD { get; set; }

        public string Title
        {
            get => FSD.Title;
            set => FSD.Title = value;
        }

        public bool Multiselect
        {
            get => FSD.Multiselect;
            set => FSD.Multiselect = value;
        }

        public string InitialDirectory
        {
            get => FSD.InitialDirectory;
            set => FSD.InitialDirectory = value;
        }

        public bool IsFolderPicker
        {
            get => FSD.IsFolderPicker;
            set => FSD.IsFolderPicker = value;
        }

        public string FileName
        {
            get => FSD.FileName;
        }

        public IEnumerable<string> FileNames
        {
            get => FSD.FileNames;
        }

        public bool ShowDialog(IntPtr wpfHandle)
        {
            if (FSD.ShowDialog(wpfHandle) == CommonFileDialogResult.Ok)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ShowDialog( )
        {
            if (FSD.ShowDialog( ) == CommonFileDialogResult.Ok)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
