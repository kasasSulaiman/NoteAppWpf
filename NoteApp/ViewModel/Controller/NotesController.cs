﻿using NoteApp.DB;
using NoteApp.Model;
using NoteApp.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.ViewModel
{
    public class NotesController : INotifyPropertyChanged
    {
        
        public ObservableCollection<Notebook> Notebooks { get; set; }
        public ObservableCollection<Note> Notes { get; set; }

        private Notebook selectedNotebook;
        public Notebook SelectedNotebook
        {
            get { return selectedNotebook; }
            set 
            { 
                selectedNotebook = value;
              
                OnPropertyChanged("SelectedNotebook");
                GetNotes();
              
            }
        }


       



        //call comamnd methods 
        public NewNotebookCommand NewNotebookCommand { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }
        
        
        public event PropertyChangedEventHandler PropertyChanged;


        public NotesController()
        {
            NewNotebookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);

            Notebooks = new ObservableCollection<Notebook>();
            Notes = new ObservableCollection<Note>();

            GetNotebooks();

        }

       
        
        
        
        
        //use it to created a new note by calling insert method from database class 
        public void CreateNote(int notebookeId)
        {
            Note newNote = new Note()
            {
                NotebookId = notebookeId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Title = $"Note for {DateTime.Now.ToString()}"
            };
            Database.Insert(newNote);
            GetNotes();
        }

        //use it to created a new notebook by calling insert method from database class 
        public void CreateNotebook()
        {
            Notebook newNotebook = new Notebook()
            {
                Name = "New Notebook"
            };
            Database.Insert(newNotebook);
            GetNotebooks();
        }




        private void GetNotebooks()
        {
            var notebooks = Database.Read<Notebook>();

            Notebooks.Clear();

            foreach (var notebook in notebooks)
            {
                Notebooks.Add(notebook);
            }
        }

        private void GetNotes()
        {
            if (selectedNotebook != null)
            {
                var notes = Database.Read<Note>().Where(n => n.NotebookId == SelectedNotebook.Id).ToList();

                Notes.Clear();

                foreach (var note in notes)
                {
                    Notes.Add(note);
                }
            }
        }


        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

       
    }
}