public sealed class TempFile : IDisposable
	{
		private string _path;

		public TempFile(string content = null) : this(content != null ? Encoding.UTF8.GetBytes(content) : null)
		{
		}

		public TempFile(byte[] content = null)
		{
			_path = System.IO.Path.GetTempFileName();

			if (content != null && content.Length > 0)
				File.WriteAllBytes(_path, content);
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

		~TempFile()
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
					File.Delete(_path);
				}
				catch
				{
					// best effort, ignored
				}
				_path = null;
			}
		}
	}
