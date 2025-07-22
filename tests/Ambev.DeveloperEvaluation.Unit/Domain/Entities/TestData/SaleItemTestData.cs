
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public static class SaleItemTestData
{
    public static Faker<SaleItem> GenerateValidSaleItem()
    {
        return new Faker<SaleItem>()
            .CustomInstantiator(f => new SaleItem(
                f.Commerce.ProductName(),
                f.Commerce.ProductAdjective().ToUpper() + f.Random.Number(1000, 9999).ToString(),
                f.Random.Int(1, 20),
                f.Random.Decimal(1, 100)
            ));
    }
}
