using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

public class DeleteSaleHandlerTests
{
    private readonly ISaleRepository _repository;
    private readonly DeleteSaleHandler _handler;

    public DeleteSaleHandlerTests()
    {
        _repository = Substitute.For<ISaleRepository>();
        _handler = new DeleteSaleHandler(_repository);
    }

    [Fact(DisplayName = "Should delete sale when it exists")]
    public async Task Should_DeleteSale_WhenExists()
    {
        var saleId = Guid.NewGuid();

        _repository.DeleteAsync(saleId, Arg.Any<CancellationToken>())
            .Returns(true);

        var command = new DeleteSaleCommand(saleId);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Success.Should().BeTrue();

        await _repository.Received(1).DeleteAsync(saleId, Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Should throw when sale not found")]
    public async Task Should_Throw_WhenSaleNotFound()
    {
        var saleId = Guid.NewGuid();

        _repository.DeleteAsync(saleId, Arg.Any<CancellationToken>())
            .Returns(false);

        var command = new DeleteSaleCommand(saleId);

        var act = async () => await _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Sale with ID {saleId} not found");
    }
}
