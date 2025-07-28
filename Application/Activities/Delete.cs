using AutoMapper;
using MediatR;
using Persistence;

namespace Application.Activities;

public class Delete
{
    public class Command : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = await _context.Activities.FindAsync(request.Id);
            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();
            return Unit.Value;
        }
    }
}