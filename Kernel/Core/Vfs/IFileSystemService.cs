//
// (C) 2006-2008 The SharpOS Project Team (http://www.sharpos.org)
//
// Authors:
//	Michael Ruck (aka grover) <sharpos@michaelruck.de>
//
// Licensed under the terms of the GNU GPL v3,
//  with Classpath Linking Exception for Libraries
//

using System;

namespace SharpOS.Kernel.Vfs {

    /// <summary>
    /// Interface, which a filesystem driver must implement.
    /// </summary>
    /// <remarks>
    /// A filesystem driver, which implements this interface should register itself beneath /proc/filesystems
    /// to make the filesystem available for mounting and other operations.
    /// </remarks>
	interface IFileSystemService
    {
        #region Properties

        /// <summary>
        /// Retrieves the type of the filesystem settings class to pass to IFileSystemService.Format
        /// </summary>
        // FIXME: Should be System.Type
        object SettingsType
        {
            get;
        }

        #endregion // Properties

        #region Methods

        /// <summary>
        /// Mounts a file system from the specified stream/device.
        /// </summary>
        /// <param name="path">The path to the device or file, which holds the filesystem to open.</param>
        /// <returns>The mounted filesystem.</returns>
        /// <remarks>
        /// File system implementations should not blindly assume that the block device or file really
        /// contain the expected filesystem. An implementation should run some checks for integrity and
        /// validity before returning an object implementing IFileSystem.
        /// <para/>
        /// Also this method should not throw. In contrast to other operating systems, the user will not
        /// be forced to know the file system on disk. The file system manager will try all file systems
        /// until it finds one, which returns a non-null IFileSystem. So a failure in a mount operation
        /// is not considered an exception, but a normal process.
        /// </remarks>
		IFileSystem Mount(char[] path);

        /// <summary>
        /// Formats the media with the filesystem.
        /// </summary>
        /// <param name="path">The path of the stream/device to format with the file system.</param>
        /// <param name="settings">The settings for the filesystem to create.</param>
        /// <returns>The created and mounted filesystem.</returns>
		IFileSystem Format(char[] path, FSSettings settings);

        #endregion // Methods
    }
}
