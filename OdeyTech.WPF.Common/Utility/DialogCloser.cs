// --------------------------------------------------------------------------
// <copyright file="DialogCloser.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System.Windows;
using OdeyTech.ProductivityKit;

namespace OdeyTech.WPF.Common.Utility
{
    /// <summary>
    /// Class for closing dialogs programmatically.
    /// </summary>
    public static class DialogCloser
    {
        /// <summary>
        /// Constant to force close the window.
        /// </summary>
        public const bool ForceClose = true;

        /// <summary>
        /// Dependency property for dialog result.
        /// </summary>
        public static readonly DependencyProperty DialogResultProperty =
            DependencyProperty.RegisterAttached(
                nameof(Window.DialogResult),
                typeof(bool?),
                typeof(DialogCloser),
                new PropertyMetadata(DialogResultChanged));

        /// <summary>
        /// Sets the DialogResult for the target window.
        /// </summary>
        /// <param name="target">The target window.</param>
        /// <param name="value">The DialogResult value.</param>
        public static void SetDialogResult(Window target, bool? value)
        {
            ThrowHelper.ThrowIfNull(target, nameof(target));
            target.SetValue(DialogResultProperty, value);
        }

        /// <summary>
        /// Handles the change event of the DialogResultProperty.
        /// </summary>
        /// <param name="d">The dependency object that had its property changed.</param>
        /// <param name="e">Details about the property change.</param>
        private static void DialogResultChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not Window window)
            {
                return;
            }

            try
            {
                window.DialogResult = e.NewValue as bool?;
                if (window.DialogResult == ForceClose)
                {
                    window.Close();
                }
            }
            catch
            {
                // In case setting DialogResult throws an exception (e.g., if the window was not shown modally), close the window directly.
                window.Close();
            }
        }
    }
}
