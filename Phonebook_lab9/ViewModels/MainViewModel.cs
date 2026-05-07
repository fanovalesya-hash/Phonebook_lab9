using Phonebook_lab9.Models;
using Phonebook_lab9.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Phonebook_lab9.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly IDialogService _dialogService;

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
        public MainViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            Contacts = new ObservableCollection<Contact>();

            AddCommand = new RelayCommand(
                AddContact,
                () => CanAddContact());

            DeleteCommand = new RelayCommand<Contact?>(
                contact => DeleteContact(contact),
                contact => CanDeleteContact(contact));

        }
        private void AddContact()
        {
            if (Contacts.Any(c => c.Phone == _phone))
            {
                _dialogService.ShowWarning("Контакт с таким номером уже существует!");
                return;
            }
            try
            {
                var newContact = new Contact(Name, Phone);
                Contacts.Add(newContact);
                Name = string.Empty;
                Phone = string.Empty;
                _dialogService.ShowInfo("Контакт успешно добавлен!");
            }
            catch
            {
                _dialogService.ShowError("Ошибка при добавлении контакта (проверьте формат номера).");
            }
        }
        private bool CanAddContact()
        {
            return Contact.Validate(Name, Phone);
        }
        private void DeleteContact(Contact? contact)
        {
            if (contact == null) return;
            bool result = _dialogService.ShowConfirmation(
                $"Удалить контакт {contact.Name}?",
                "Удаление");
            if (result)
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
