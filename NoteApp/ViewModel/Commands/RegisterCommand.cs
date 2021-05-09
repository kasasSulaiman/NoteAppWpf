﻿using NoteApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NoteApp.ViewModel.Commands
{
    public class RegisterCommand : ICommand

    {
        public event EventHandler CanExecuteChanged
        {
           
              add { CommandManager.RequerySuggested += value; }
              remove { CommandManager.RequerySuggested -= value; }
            
        }
        public LoginController Controller { get; set; }

        public RegisterCommand(LoginController controller)
        {
            Controller = controller;
        }

        public bool CanExecute(object parameter)
        {
            User user = (parameter as User);
            if (user == null)
                return false;
            if (string.IsNullOrEmpty(user.Username))
                return false;
            if (string.IsNullOrEmpty(user.Password))
                return false;
            if (string.IsNullOrEmpty(user.ConfirmPassword))
                return false;

            if (user.Password != user.ConfirmPassword)
                return false;

            return true;
        }

        public void Execute(object parameter)
        {
            Controller.Register();
        }
    }
}
