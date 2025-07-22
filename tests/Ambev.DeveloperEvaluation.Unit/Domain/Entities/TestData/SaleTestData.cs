
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public static class SaleTestData
{
    public static Faker<Sale> GenerateValidSale()
    {
        return new Faker<Sale>()
            .RuleFor(s => s.SaleNumber, f => f.Random.AlphaNumeric(10))
            .RuleFor(s => s.CustomerName, f => f.Name.FullName())
            .RuleFor(s => s.CustomerEmail, (f, s) => f.Internet.Email(s.CustomerName))
            .RuleFor(s => s.CustomerDocument, f => f.Random.AlphaNumeric(11))
            .RuleFor(s => s.BranchName, f => f.Company.CompanyName())
            .RuleFor(s => s.BranchCode, f => f.Random.AlphaNumeric(5));
    }
}
