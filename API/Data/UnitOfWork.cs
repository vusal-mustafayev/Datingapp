using API.Interfaces;

namespace API.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed;

        private readonly DataContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly ILikesRepository _likesRepository;
        private readonly IPhotoRepository _photoRepository;

        public UnitOfWork(DataContext context,
            IUserRepository userRepository,
            IMessageRepository messageRepository,
            ILikesRepository likesRepository,
            IPhotoRepository photoRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _messageRepository = messageRepository;
            _likesRepository = likesRepository;
            _photoRepository = photoRepository;
        }

        public IUserRepository UserRepository => _userRepository;

        public IMessageRepository MessageRepository => _messageRepository;

        public ILikesRepository LikesRepository => _likesRepository;

        public IPhotoRepository PhotoRepository => _photoRepository;

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            System.GC.SuppressFinalize(this);
        }
    }
}