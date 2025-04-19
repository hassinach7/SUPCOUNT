using FluentValidation;
using SupCountBE.Application.Commands.Group;
using SupCountBE.Application.Responses.Group;
using SupCountBE.Application.Validations.Group;
using SupCountBE.Core.Repositories;

namespace SupCountBE.Application.Handlers.Group;

public class UpdateGroupHandler : IRequestHandler<UpdateGroupCommand, GroupResponse>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IMapper _mapper;

    public UpdateGroupHandler(IGroupRepository groupRepository, IMapper mapper)
    {
        _groupRepository = groupRepository;
        _mapper = mapper;
    }

    public async Task<GroupResponse> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateGroupValidator();
        var validation = await validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
            throw new ValidationException(validation.Errors);

        var group = await _groupRepository.GetByIdAsync(request.Id);
        if (group is null)
            throw new Exception("Group not found.");

        group.Name = request.Name;
        group.Description = request.Description;

        await _groupRepository.UpdateAsync(group);

        return _mapper.Map<GroupResponse>(group);
    }
}
