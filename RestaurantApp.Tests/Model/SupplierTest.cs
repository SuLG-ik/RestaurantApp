using JetBrains.Annotations;
using RestaurantApp.Model;

namespace RestaurantApp.Tests.Model;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
[TestSubject(typeof(Supplier))]
[TestSubject(typeof(Supplier.Builder))]
public class SupplierTests
{
    [TestMethod]
    public void Build_WithValidParameters_ShouldCreateSupplier()
    {
        var builder = new Supplier.Builder()
            .SetName("Supplier 1")
            .SetAddress("123 Main St")
            .SetDirector("John Doe")
            .SetPhoneNumber("1234567890")
            .SetBank("Big Bank")
            .SetAccountNumber("9876543210")
            .SetInn("123456789");

        var supplier = builder.Build();

        Assert.AreEqual("Supplier 1", supplier.Name);
        Assert.AreEqual("123 Main St", supplier.Address);
        Assert.AreEqual("John Doe", supplier.Director);
        Assert.AreEqual("1234567890", supplier.PhoneNumber);
        Assert.AreEqual("Big Bank", supplier.Bank);
        Assert.AreEqual("9876543210", supplier.AccountNumber);
        Assert.AreEqual("123456789", supplier.Inn);
    }

    [TestMethod]
    public void SetName_WithEmptyString_ShouldThrowValidationNotBlankException()
    {
        var builder = new Supplier.Builder();

        Assert.ThrowsException<ValidationNotBlankException>(() => builder.SetName(""));
    }

    [TestMethod]
    public void SetAddress_WithEmptyString_ShouldThrowValidationNotBlankException()
    {
        var builder = new Supplier.Builder();

        Assert.ThrowsException<ValidationNotBlankException>(() => builder.SetAddress(""));
    }

    [TestMethod]
    public void SetDirector_WithEmptyString_ShouldThrowValidationNotBlankException()
    {
        var builder = new Supplier.Builder();

        Assert.ThrowsException<ValidationNotBlankException>(() => builder.SetDirector(""));
    }

    [TestMethod]
    public void SetPhoneNumber_WithNonNumericString_ShouldThrowValidationConvertException()
    {
        var builder = new Supplier.Builder();

        Assert.ThrowsException<ValidationCheckException<string>>(() => builder.SetPhoneNumber("ABC"));
    }

    [TestMethod]
    public void SetBank_WithEmptyString_ShouldThrowValidationNotBlankException()
    {
        var builder = new Supplier.Builder();

        Assert.ThrowsException<ValidationNotBlankException>(() => builder.SetBank(""));
    }

    [TestMethod]
    public void SetAccountNumber_WithNonNumericString_ShouldThrowValidationConvertException()
    {
        var builder = new Supplier.Builder();

        Assert.ThrowsException<ValidationCheckException<string>>(() => builder.SetAccountNumber("NotANumber"));
    }

    [TestMethod]
    public void SetInn_WithNonNumericString_ShouldThrowValidationConvertException()
    {
        var builder = new Supplier.Builder();

        Assert.ThrowsException<ValidationCheckException<string>>(() => builder.SetInn("ABC"));
    }

    [TestMethod]
    public void Build_WithoutName_ShouldThrowValidationNotNullException()
    {
        var builder = new Supplier.Builder()
            .SetAddress("123 Main St")
            .SetDirector("John Doe")
            .SetPhoneNumber("1234567890")
            .SetBank("Big Bank")
            .SetAccountNumber("9876543210")
            .SetInn("123456789");

        Assert.ThrowsException<ValidationNullException>(() => builder.Build());
    }

    [TestMethod]
    public void Build_WithoutAddress_ShouldThrowValidationNotNullException()
    {
        var builder = new Supplier.Builder()
            .SetName("Supplier 1")
            .SetDirector("John Doe")
            .SetPhoneNumber("1234567890")
            .SetBank("Big Bank")
            .SetAccountNumber("9876543210")
            .SetInn("123456789");

        Assert.ThrowsException<ValidationNullException>(() => builder.Build());
    }

    [TestMethod]
    public void Build_WithoutDirector_ShouldThrowValidationNotNullException()
    {
        var builder = new Supplier.Builder()
            .SetName("Supplier 1")
            .SetAddress("123 Main St")
            .SetPhoneNumber("1234567890")
            .SetBank("Big Bank")
            .SetAccountNumber("9876543210")
            .SetInn("123456789");

        Assert.ThrowsException<ValidationNullException>(() => builder.Build());
    }

    [TestMethod]
    public void Build_WithoutPhoneNumber_ShouldThrowValidationNotNullException()
    {
        var builder = new Supplier.Builder()
            .SetName("Supplier 1")
            .SetAddress("123 Main St")
            .SetDirector("John Doe")
            .SetBank("Big Bank")
            .SetAccountNumber("9876543210")
            .SetInn("123456789");

        Assert.ThrowsException<ValidationNullException>(() => builder.Build());
    }

    [TestMethod]
    public void Build_WithoutBank_ShouldThrowValidationNotNullException()
    {
        var builder = new Supplier.Builder()
            .SetName("Supplier 1")
            .SetAddress("123 Main St")
            .SetDirector("John Doe")
            .SetPhoneNumber("1234567890")
            .SetAccountNumber("9876543210")
            .SetInn("123456789");

        Assert.ThrowsException<ValidationNullException>(() => builder.Build());
    }

    [TestMethod]
    public void Build_WithoutAccountNumber_ShouldThrowValidationNotNullException()
    {
        var builder = new Supplier.Builder()
            .SetName("Supplier 1")
            .SetAddress("123 Main St")
            .SetDirector("John Doe")
            .SetPhoneNumber("1234567890")
            .SetBank("Big Bank")
            .SetInn("123456789");

        Assert.ThrowsException<ValidationNullException>(() => builder.Build());
    }

    [TestMethod]
    public void Build_WithoutInn_ShouldThrowValidationNotNullException()
    {
        var builder = new Supplier.Builder()
            .SetName("Supplier 1")
            .SetAddress("123 Main St")
            .SetDirector("John Doe")
            .SetPhoneNumber("1234567890")
            .SetBank("Big Bank")
            .SetAccountNumber("9876543210");

        Assert.ThrowsException<ValidationNullException>(() => builder.Build());
    }
}
