using Microsoft.Extensions.DependencyInjection;

using NotepadApp.Interfaces;
using NotepadApp.Models;
using NotepadApp.Services;
using NotepadApp.ViewModels;

using System.Windows;

namespace NotepadApp
{
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();

            services.AddSingleton<DocumentModel>();

            services.AddSingleton<MainViewModel>();
            services.AddSingleton<FileViewModel>();
            services.AddSingleton<EditViewModel>();

            services.AddSingleton<MainWindow>();

            services.AddSingleton<IFileService, FileService>();
            services.AddSingleton<IDialogService, DialogService>();

            Services = services.BuildServiceProvider();

            var window = Services.GetRequiredService<MainWindow>();
            window.Show();
        }
    }
}
