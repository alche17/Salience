﻿using Lexalytics;
using SalienceThemes.Models;
using SalienceThemes.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace SalienceThemes.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private Path _path;
        private Salience Engine = null;
        private Input _input;
        private Themes _themes;

        public MainWindowViewModel()
        {
            _path = new Path { LicensePath = Strings.Label_LicensePath, DataPath = Strings.Label_DataPath };
            _input = new Input { InputText = Strings.Label_InputText_SampleString };
            _themes = new Themes();
            try
            {
                Engine = new Salience(Path.LicensePath, Path.DataPath);
            }
            catch (SalienceException e)
            {
                //not sure how to handle this yet
            }
        }

        public Themes Themes
        {
            get { return _themes; }
            set { _themes = value; }
        }

        #region PathMembers
        public Path Path
        {
            get { return _path; }
            set { _path = value; }
        }
        
        public string LicensePath
        {
            get { return Path.LicensePath; }
            set
            {
                if (Path.LicensePath != value)
                {
                    Path.LicensePath = value;
                    RaisePropertyChanged("LicensePath");
                }
            }
        }

        public string DataPath
        {
            get { return Path.DataPath; }
            set
            {
                if (Path.DataPath != value)
                {
                    Path.DataPath = value;
                    RaisePropertyChanged("DataPath");
                }
            }
        }
        #endregion

        #region InputMembers
        public Input Input
        {
            get { return _input; }
            set { _input = value; }
        }

        public string InputText
        {
            get { return Input.InputText; }
            set
            {
                if (Input.InputText != value)
                {
                    Input.InputText = value;
                    RaisePropertyChanged("InputText");
                }
            }
        }
        #endregion

        #region PropertyChangedHandler
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region ClearButton
        public void ClearExecute()
        {
            InputText = String.Empty;
            _themes.Clear();
        }

        public bool CanClearExecute()
        {
            return true;
        }

        public ICommand Clear
        {
            get { return new RelayCommand(ClearExecute, CanClearExecute); }
        }
        #endregion

        #region ProcessButton
        public void ProcessExecute()
        {
            _themes.Clear();
            int nRet = Engine.PrepareText(InputText);
            if (nRet == 0)
            {
                List<SalienceTheme> myThemes = Engine.GetDocumentThemes(String.Empty);
                foreach (SalienceTheme aTheme in myThemes)
                {
                    _themes.Add(new Theme(aTheme.sNormalizedTheme, aTheme.fScore, aTheme.nThemeType, aTheme.fSentiment, aTheme.nEvidence));
                }
            }
            else
            {
                // there was an error, in which case this needs to be handled somehow
            }
        }

        public bool CanProcessExecute()
        {
            return true;
        }

        public ICommand Process
        {
            get { return new RelayCommand(ProcessExecute, CanProcessExecute); }
        }
        #endregion
    }
}