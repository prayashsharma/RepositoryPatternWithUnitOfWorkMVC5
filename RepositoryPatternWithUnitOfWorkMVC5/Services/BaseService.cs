using RepositoryPatternWithUnitOfWorkMVC5.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepositoryPatternWithUnitOfWorkMVC5.Services
{
    public abstract class BaseService
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