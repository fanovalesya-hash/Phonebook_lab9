using Phonebook_lab9.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Phonebook_lab9.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        // Коллекция контактов
        public ObservableCollection<Contact> Contacts { get; }
        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        private string _phone = string.Empty;
        public string Phone
        {
            get => _phone;
            set => Set(ref _phone, value);
        }

        private Contact? _selectedContact;
        public Contact? SelectedContact
        {
            get => _selectedContact;
            set => Set(ref _selectedContact, value);
        }
        // Команды
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public MainViewModel()
        {
            Contacts = new ObservableCollection<Contact>();
            AddCommand = new RelayCommand(
            AddContact,() => CanAddContact());

            DeleteCommand = new RelayCommand<Contact?>(
                (contact) => DeleteContact(contact), 
                (contact) => CanDeleteContact(contact));
           
        }
        private void AddContact()
        {
            try
            {
                var newContact = new Contact(Name, Phone);
                Contacts.Add(newContact);
                Name = string.Empty;
                Phone = string.Empty;
            }
            catch
            {

            }
        }
        private bool CanAddContact()
        {
            return Contact.Validate(Name, Phone);
        }
        private void DeleteContact(Contact? contact)
        {
            if (contact != null)
            {
                Contacts.Remove(contact);
                SelectedContact = null;
            }
        }
        private bool CanDeleteContact(Contact? contact)
        {
            return contact != null;
        }
    }
}
