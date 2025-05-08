using FluentValidation;
using SupCountBE.Application.Commands.Group;
using SupCountBE.Application.Validations.Group;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Group;

public class CreateGroupHandler : IRequestHandler<CreateGroupCommand, int>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IMapper _mapper;

    public CreateGroupHandler(IGroupRepository groupRepository, IMapper mapper)
    {
        _groupRepository = groupRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateGroupValidator();
        var validation = await validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
            throw new ValidationException(validation.Errors);

        var group = new Core.Entities.Group
        {
            Name = request.Name,
            Description = request.Description
        };

        await _groupRepository.AddAsync(group);
        return group.Id;
    }
}
