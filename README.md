# SecurePass - Локальный менеджер паролей
### Простой, безопасный и локальный менеджер паролей, созданный с помощью C# и WPF. Все ваши данные хранятся в одном зашифрованном файле на вашем компьютере и никогда не покидают его.

✨ **Особенности**

Полная локальность: Все данные хранятся только у вас. Никаких облаков и серверов.

Надежное шифрование: Используется современный алгоритм AES-GCM для защиты ваших данных.

Один файл: Все приложение компилируется в один .exe файл, который не требует установки .NET на целевом компьютере.

Простой интерфейс: Современный темный дизайн без лишних деталей.

🚀 **Сборка проекта**

Для сборки проекта вам понадобится .NET 8 SDK.

Клонируйте репозиторий.

Запустите файл compile.bat.

Скрипт автоматически очистит проект, скомпилирует его и упакует в один .exe файл.

Готовый файл SecurePass.exe будет находиться в папке:
```
\bin\Release\net8.0-windows\win-x64\publish\
```
Папка с готовым файлом откроется автоматически после успешной сборки.

📖 **Как использовать**

Запустите SecurePass.exe.

При первом запуске программа предложит вам создать или выбрать файл хранилища (с расширением .spv). Этот файл будет содержать все ваши зашифрованные данные.

Придумайте и введите мастер-пароль. Этот пароль — единственный ключ к вашим данным. Если вы его забудете, восстановить доступ будет невозможно!

Используйте кнопки "Добавить" и "Удалить" для управления вашими записями.

Все изменения автоматически сохраняются в зашифрованный файл при закрытии программы.

made by t.me/hixov
___________________________________________________________________________________________________________________________


# SecurePass - Local Password Manager
### A simple, secure, and local password manager built with C# and WPF. All your data is stored in a single encrypted file on your computer and never leaves it.

✨ **Features**

Fully Local: All data is stored only on your machine. No clouds, no servers.

Strong Encryption: Uses the modern AES-GCM algorithm to protect your data.

Single File: The entire application compiles into a single .exe file that doesn't require .NET to be installed on the target machine.

Simple Interface: A modern, dark-themed design without unnecessary clutter.

🚀 **How to Build**

You will need the .NET 8 SDK to build the project.

Clone the repository.

Run the compile.bat script.

The script will automatically clean the project, compile it, and package it into a single .exe file.

The final SecurePass.exe will be located in the following folder:
```
\bin\Release\net8.0-windows\win-x64\publish\
```
The folder containing the final executable will open automatically after a successful build.

📖 **How to Use**

Run SecurePass.exe.

On the first launch, the program will prompt you to create or select a vault file (with a .spv extension). This file will contain all your encrypted data.

Create and enter a master password. This password is the only key to your data. If you forget it, it will be impossible to recover your data!

Use the "Add" and "Remove" buttons to manage your entries.

All changes are automatically saved to the encrypted file when you close the program.


made by t.me/hixov
