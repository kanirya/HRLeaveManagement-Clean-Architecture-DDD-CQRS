using Application.DTOs.LeaveType;
using Application.Features.LeaveTypes.Handlers.Queries;
using Application.Features.LeaveTypes.Requests.Queries;
using Application.Persistence.Contracts;
using AutoMapper;
using Domain;
using FakeItEasy;
using Moq;

public class GetLeaveTypeListRequestHandlerTests
{
    private readonly Mock<ILeaveTypeRepository> _mockRepo;
    private readonly Mock<IMapper> _mockMapper;
    private readonly GetLeaveTypeListRequestHandler _handler;

    public GetLeaveTypeListRequestHandlerTests()
    {
        _mockRepo = new Mock<ILeaveTypeRepository>();
        _mockMapper = new Mock<IMapper>();

        _handler = new GetLeaveTypeListRequestHandler(_mockRepo.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnMappedLeaveTypeList_WhenDataExists()
    {
        // Arrange
        var leaveTypes = new List<LeaveType>
        {
            new LeaveType { Id = 1, Name = "Annual", DefaultDays = 10 },
            new LeaveType { Id = 2, Name = "Sick", DefaultDays = 5 }
        };

        var leaveTypeDtos = new List<LeaveTypeDto>
        {
            new LeaveTypeDto { Id = 1, Name = "Annual", DefaultDays = 10 },
            new LeaveTypeDto { Id = 2, Name = "Sick", DefaultDays = 5 }
        };

        _mockRepo.Setup(r => r.GetLeaveTypesListWithDetails(It.IsAny<CancellationToken>()))
                 .ReturnsAsync(leaveTypes);

        _mockMapper.Setup(m => m.Map<List<LeaveTypeDto>>(leaveTypes))
                   .Returns(leaveTypeDtos);

        // Act
        var result = await _handler.Handle(new GetLeaveTypeListRequest(), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Annual", result[0].Name);

        _mockRepo.Verify(r => r.GetLeaveTypesListWithDetails(It.IsAny<CancellationToken>()), Moq.Times.Once);
        _mockMapper.Verify(m => m.Map<List<LeaveTypeDto>>(leaveTypes), Moq.Times.Once);
    }
}