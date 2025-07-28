using Application.DTOs;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities;

public class Create
{
    public class Command: IRequest<string>
    {
        public CreateActivityDto ActivityDto { get; set; }
    }
    public class Handler: IRequestHandler<Command,string>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = _mapper.Map<Activity>(request.ActivityDto);
            _context.Activities.Add(activity);
            await _context.SaveChangesAsync();
            return activity.Id.ToString();
        }
    }
}