namespace DotNetBasics.Module4.MessagingService.Helpers
{
	internal class FileHelper
	{
		private readonly FileInfo _file;

		public FileHelper(string filePath)
		{
			if (string.IsNullOrWhiteSpace(filePath))
				throw new ArgumentException($"'{nameof(filePath)}' cannot be null or whitespace.", nameof(filePath));

			_file = new FileInfo(filePath);
		}

		public string GetFileContent()
		{
			try
			{
				if (File.Exists(_file.FullName))
				{
					return File.ReadAllText(_file.FullName);
				}
				else
				{
					throw new FileNotFoundException($"ERROR - File '{_file.FullName}' not found.");
				}
			}
			catch (Exception)
			{
				throw new InvalidOperationException("ERROR - Unable to get the file contents.");
			}
		}
	}
}
