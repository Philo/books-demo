using Xunit;
using Books.Service.Web.Models;
using AutoFixture.Xunit2;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Books.Service.Tests.Unit.Web.Validation;

public class ModelValidationTests 
{
    [Theory, AutoData]
    public void Book_EmptyTitle_Should_BeInvalid(string author, decimal price)
    {
        var book = new BookDto(string.Empty, author, price);
        var context = new ValidationContext(book);

        var results = new List<ValidationResult>();
        var valid = Validator.TryValidateObject(book, context, results, true);

        valid.Should().BeFalse();
    }

    [Theory, AutoData]
    public void Book_EmptyAuthor_Should_BeInvalid(string title, decimal price)
    {
        var book = new BookDto(title, string.Empty, price);
        var context = new ValidationContext(book);

        var results = new List<ValidationResult>();
        var valid = Validator.TryValidateObject(book, context, results, true);

        valid.Should().BeFalse();
    }

    [Theory, AutoData]
    public void Book_NegativePrice_Should_BeInvalid(string title, string author)
    {
        var book = new BookDto(title, author, -1);
        var context = new ValidationContext(book);

        var results = new List<ValidationResult>();
        var valid = Validator.TryValidateObject(book, context, results, true);

        valid.Should().BeFalse();
    }
}