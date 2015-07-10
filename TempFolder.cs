public sealed class TempFolder : IDisposable
	{
		private string _path;

		public TempFolder(string prefix = null)
		{
			if (prefix == null)
				prefix = AppDomain.CurrentDomain.FriendlyName;

			var timestamp = (int) (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

			_path = System.IO.Path.GetTempPath() + "/" + prefix + "_" + timestamp;
		}

		public string Path
		{
			get
			{
				if (_path == null) throw new ObjectDisposedException(GetType().Name);
				return _path;
			}
		}

		public void Dispose() => Dispose(true);

		~TempFolder()
		{
			Dispose(false);
		}

		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				GC.SuppressFinalize(this);
			}
			if (_path != null)
			{
				try
				{
					Directory.Delete(_path);
				}
				catch
				{
					// best effort, ignored
				}
				_path = null;
			}
		}
	}
