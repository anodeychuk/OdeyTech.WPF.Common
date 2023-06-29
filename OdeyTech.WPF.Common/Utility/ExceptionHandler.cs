// --------------------------------------------------------------------------
// <copyright file="ExceptionHandler.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Extensions.DependencyInjection;
using OdeyTech.ProductivityKit;
using OdeyTech.ProductivityKit.Extension;
using OdeyTech.WPF.Common.Manager;

namespace OdeyTech.WPF.Common.Utility
{
    /// <summary>
    /// Provides exception handling extension methods for WPF Application objects.
    /// </summary>
    public static class ExceptionHandler
    {
        private static IServiceProvider serviceProvider;

        /// <summary>
        /// Sets up exception handling for the given WPF application.
        /// </summary>
        /// <param name="application">The WPF application instance.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="application"/> or <paramref name="serviceProvider"/> is null.</exception>
        /// <example>
        /// var services = new ServiceCollection();
        /// services.AddSingleton<IViewManager, ViewManager>();
        /// Application.Current.SetupExceptionHandling(serviceProvider);
        /// </example>
        public static void SetupExceptionHandling(this Application application, IServiceProvider serviceProvider)
        {
            ThrowHelper.ThrowIfNull(application, nameof(application));
            ThrowHelper.ThrowIfNull(serviceProvider, nameof(serviceProvider));

            ExceptionHandler.serviceProvider = serviceProvider;
            AppDomain.CurrentDomain.UnhandledException += (sender, args) => CurrentDomainOnUnhandledException(application, args);
            application.Dispatcher.UnhandledException += (sender, args) => DispatcherUnhandledException(application, args);
            application.DispatcherUnhandledException += (sender, args) => CurrentDispatcherUnhandledException(application, args);
            TaskScheduler.UnobservedTaskException += (sender, args) => TaskSchedulerOnUnobservedTaskException(application, args);
        }

        /// <summary>
        /// Handles the unhandled exception event for the current application domain.
        /// </summary>
        /// <param name="application">The WPF application instance.</param>
        /// <param name="args">The event arguments containing the unhandled exception information.</param>
        private static void CurrentDomainOnUnhandledException(Application application, UnhandledExceptionEventArgs args)
            => UnhandledExceptionHandler(application, (Exception)args.ExceptionObject, "AppDomain.CurrentDomain.UnhandledException");

        /// <summary>
        /// Handles the unhandled exception event for the application's dispatcher.
        /// </summary>
        /// <param name="application">The WPF application instance.</param>
        /// <param name="args">The event arguments containing the unhandled exception information.</param>
        private static void DispatcherUnhandledException(Application application, DispatcherUnhandledExceptionEventArgs args)
        {
            UnhandledExceptionHandler(application, args.Exception, "Application.Current.Dispatcher.UnhandledException");
            args.Handled = true;
        }

        /// <summary>
        /// Handles the unhandled exception event for the current application's dispatcher.
        /// </summary>
        /// <param name="application">The WPF application instance.</param>
        /// <param name="args">The event arguments containing the unhandled exception information.</param>
        private static void CurrentDispatcherUnhandledException(Application application, DispatcherUnhandledExceptionEventArgs args)
        {
            UnhandledExceptionHandler(application, args.Exception, "Application.Current.DispatcherUnhandledException");
            args.Handled = true;
        }

        /// <summary>
        /// Handles the unobserved task exception event for the application.
        /// </summary>
        /// <param name="application">The WPF application instance.</param>
        /// <param name="args">The event arguments containing the unobserved task exception information.</param>
        private static void TaskSchedulerOnUnobservedTaskException(Application application, UnobservedTaskExceptionEventArgs args)
        {
            UnhandledExceptionHandler(application, args.Exception, "TaskScheduler.UnobservedTaskException");
            args.SetObserved();
        }

        /// <summary>
        /// Handles unhandled exceptions and displays an error message using the IViewManager service.
        /// </summary>
        /// <param name="application">The WPF application instance.</param>
        /// <param name="exception">The unhandled exception.</param>
        /// <param name="source">The source of the unhandled exception.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="application"/> is null.</exception>
        private static void UnhandledExceptionHandler(Application application, Exception exception, string source)
        {
            var title = $"Exception source: {source}";
            try
            {
                AssemblyName assemblyName = Assembly.GetExecutingAssembly().GetName();
                title = "Unhandled exception in {0} v{1}\n\n{2}".Format(assemblyName.Name, assemblyName.Version, title);
            }
            catch (Exception ex)
            {
                title = $"Exception in {nameof(UnhandledExceptionHandler)}";
                exception = ex;
            }
            finally
            {
                application.Dispatcher.Invoke(delegate
                {
                    serviceProvider.GetService<IViewManager>().ShowError(title, exception);
                });
            }
        }
    }
}
