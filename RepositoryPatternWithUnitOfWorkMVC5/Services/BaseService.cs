using RepositoryPatternWithUnitOfWorkMVC5.Repositories.Interfaces;

namespace RepositoryPatternWithUnitOfWorkMVC5.Services
{
    public class BaseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
        }
    }
}