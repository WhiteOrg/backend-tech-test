using TechTest.Core.Interfaces;

namespace TechTest.Infrastructure.Handlers.Commands
{
    public class BaseCommandHandler
    {
        protected readonly IUnitOfWork UnitOfWork;

        public BaseCommandHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}