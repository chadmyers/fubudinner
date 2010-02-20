using System;
using Db4objects.Db4o;

namespace FubuDinner.Web.Infrastructure
{
    public interface IDb4OConnection
    {
        void Initialize();
        IObjectContainer Current { get; }
    }

    public class Db4OConnection : IDb4OConnection, IDisposable
    {
        private readonly DatabaseSettings _settings;
        private readonly object _syncRoot = new object();
        private bool _isInitialized;
        private IObjectContainer _objects;

        public Db4OConnection(DatabaseSettings settings)
        {
            _settings = settings;
        }

        public void Initialize()
        {
            should_not_already_be_initialized();
            lock (_syncRoot)
            {
                should_not_already_be_initialized();
                _objects = Db4oEmbedded.OpenFile(Db4oEmbedded.NewConfiguration(), _settings.DbFilePath);
                _isInitialized = true;
            }
        }

        private void should_not_already_be_initialized()
        {
            if (_isInitialized) throw new InvalidOperationException("Db4o connection is already initialized");
        }

        public IObjectContainer Current
        {
            get { return _objects; }
        }

        public void Dispose()
        {
            if (_objects != null) _objects.Dispose();
        }
    }
}