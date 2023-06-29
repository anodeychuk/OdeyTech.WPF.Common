// --------------------------------------------------------------------------
// <copyright file="IWindowFactory.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System.Windows;

namespace OdeyTech.WPF.Common.Manager
{
    /// <summary>
    /// Represents a factory for creating windows.
    /// </summary>
    public interface IWindowFactory
    {
        /// <summary>
        /// Creates an instance of a window of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the window to create.</typeparam>
        /// <returns>An instance of the created window.</returns>
        T Create<T>() where T : Window, new();
    }
}
