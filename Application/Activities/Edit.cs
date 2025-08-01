﻿using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities;

public class Edit
{
    public class Command:IRequest<Result<Unit>>
    {
        
        public Activity Activity { get; set; }
    }
    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = await _context.Activities.FindAsync(request.Activity.Id,cancellationToken);
            if (activity == null) return Result<Unit>.Failure("Activity not found", 404);
            _mapper.Map(request.Activity, activity);
            var result = await _context.SaveChangesAsync(cancellationToken) > 0;
            if(!result) return Result<Unit>.Failure("Failed to update the activity", 400);
            return Result<Unit>.Success(Unit.Value);
        }
    }
}