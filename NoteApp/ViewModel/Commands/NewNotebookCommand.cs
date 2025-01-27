﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NoteApp.ViewModel.Commands
{
    public class NewNotebookCommand : ICommand
    {

        public NotesController Controller { get; set; }


        public NewNotebookCommand(NotesController controller)
        {
            Controller = controller;
        }


        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            //todo: create a new notebook
            Controller.CreateNotebook();
        }
    }
}
