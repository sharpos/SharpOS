using System;
using System.Collections.Generic;
using System.Text;

namespace SharpOS.Kernel.Vfs
{
    /// <summary>
    /// File system settings base class for formatting purposes.
    /// </summary>
    /// <remarks>
    /// This base class holds properties and data members common to most file systems. A specialized
    /// derived class should be created for specific file systems and its type should be returned from
    /// IFileSystemService.SettingsType to allow mkfs style commands to automate most processing.
    /// </remarks>
    public class FSSettings
    {
        #region Data members

        /// <summary>
        /// The volume label.
        /// </summary>
        private char[] _label;

        #endregion // Data members

        #region Construction

        /// <summary>
        /// Initializes a new instance of <see cref="FSSettings"/>.
        /// </summary>
        public FSSettings()
        {
            _label = new char[0];
        }

        #endregion // Construction

        #region Methods

        /// <summary>
        /// Gets or sets the volume label.
        /// </summary>
        public char[] Label
        {
            get { return _label; }
            set
            {
                if (null == value)
                    throw new ArgumentNullException(@"value");

                _label = value;
            }
        }

        #endregion // Methods
    }
}
