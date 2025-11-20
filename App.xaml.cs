using System;
using System.Windows;

using Microsoft.Extensions.DependencyInjection;

using NotepadApp.Interfaces;
using NotepadApp.Services;
using NotepadApp.ViewModels;

namespace NotepadApp
{
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();

            // --- Регистрируем сервисы ---
            services.AddSingleton<IFileService, FileService>();

            // --- Регистрируем ViewModels ---
            services.AddSingleton<MainViewModel>();

            // --- Регистрируем окна ---
            services.AddSingleton<MainWindow>();

            Services = services.BuildServiceProvider();

            // запускаем главное окно
            var window = Services.GetRequiredService<MainWindow>();
            window.Show();
        }
    }
}
