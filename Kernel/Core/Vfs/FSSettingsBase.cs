using System;

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
	public class FSSettingsBase
	{
		#region Data members

		/// <summary>
		/// The volume label.
		/// </summary>
		private string label;

		#endregion // Data members

		#region Construction

		/// <summary>
		/// Initializes a new instance of <see cref="FSSettings"/>.
		/// </summary>
		public FSSettingsBase ()
		{
			label = "New Volume";
		}

		#endregion // Construction

		#region Methods

		/// <summary>
		/// Gets or sets the volume label.
		/// </summary>
		public string Label
		{
			get { return label; }
			set
			{
				if (value == null)
					throw new ArgumentNullException (@"value");

				label = value;
			}
		}

		#endregion // Methods
	}
}
