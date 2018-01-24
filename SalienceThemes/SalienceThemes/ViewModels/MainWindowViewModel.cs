﻿using Lexalytics;
using SalienceThemes.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;

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
            _path = new Path { LicensePath = "C:/Program Files (x86)/Lexalytics/License.v5", DataPath = "C:/Program Files (x86)/Lexalytics/data" };
            _input = new Input { InputText = "This is an example of input text, which has been hard-coded." };
            _themes = new Themes();
            try
            {
                Engine = new Salience(Path.LicensePath, Path.DataPath);
            }
            catch (SalienceException e)
            {
                // not sure how to handle this yet
            }
        }

        public Themes Themes
        {
            get { return _themes; }
            set { _themes = value; }
        }

        //#region ThemeMembers
        //public Theme Theme
        //{
        //    get { return _theme; }
        //    set { _theme = value; }
        //}
        //public string Name
        //{
        //    get { return Theme.Name; }
        //    set
        //    {
        //        if (Theme.Name != value)
        //        {
        //            Theme.Name = value;
        //            RaisePropertyChanged("LicensePath");
        //        }
        //    }
        //}

        //public string Score
        //{
        //    get { return Theme.Score; }
        //    set
        //    {
        //        if (Theme.Score != value)
        //        {
        //            Theme.Score = value;
        //            RaisePropertyChanged("LicensePath");
        //        }
        //    }
        //}

        //public string Type
        //{
        //    get { return Theme.Type; }
        //    set
        //    {
        //        if (Theme.Type != value)
        //        {
        //            Theme.Type = value;
        //            RaisePropertyChanged("LicensePath");
        //        }
        //    }
        //}

        //public string Sentiment
        //{
        //    get { return Theme.Sentiment; }
        //    set
        //    {
        //        if (Theme.Sentiment != value)
        //        {
        //            Theme.Sentiment = value;
        //            RaisePropertyChanged("LicensePath");
        //        }
        //    }
        //}

        //public string Evidence
        //{
        //    get { return Theme.Evidence; }
        //    set
        //    {
        //        if (Theme.Evidence != value)
        //        {
        //            Theme.Evidence = value;
        //            RaisePropertyChanged("LicensePath");
        //        }
        //    }
        //}
        //#endregion

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

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AnalyseText()
        {
            int nRet = Engine.PrepareText(InputText);
            if (nRet == 0)
            {
                List<SalienceTheme> myThemes = Engine.GetDocumentThemes(String.Empty);
                foreach (SalienceTheme aTheme in myThemes)
                {

                }
             }
            else
            {
                // there was an error, in which case this needs to be handled somehow
            }
        }
    }
}