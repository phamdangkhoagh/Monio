using MediatR;
using Monio.Application.Features.Categories.DeleteCategory;
using Monio.Application.Interfaces.Persistence;
using Monio.Application.Interfaces.Services;
using Monio.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monio.Application.Features.Transactions.DeleteTransaction
{
    public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTransactionCommandHandler(
            ITransactionRepository transactionRepository,
            ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork)
        {
            _transactionRepository = transactionRepository;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var transaction = await _transactionRepository
                .GetByIdForUserAsync(request.Id, userId);

            if (transaction == null)
               throw new KeyNotFoundException("Transaction not found.");

            await _transactionRepository.DeleteAsync(transaction);

            await _unitOfWork.SaveChangeAsync(cancellationToken);
        }
    }
}
