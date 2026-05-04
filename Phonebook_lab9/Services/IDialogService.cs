using System;
using System.Collections.Generic;
using System.Text;

namespace Phonebook_lab9.Services
{
    public interface IDialogService
    {
        void ShowInfo(string message, string title = "Информация");
        void ShowWarning(string message, string title = "Предупреждение");
        void ShowError(string message, string title = "Ошибка");
        bool ShowConfirmation(string message, string title = "Подтверждение");
    }
}
