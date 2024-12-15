using RestaurantApp.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace RestaurantApp.Tests.Model;

[TestClass]
public class SavedModelTests
{
    [TestMethod]
    public void Builder_WithValidIdAndData_ShouldCreateSavedModel()
    {
        // Arrange
        int id = 1;
        var data = "Test Data";

        // Act
        var model = new SavedModel<string>.Builder()
            .SetId(id)
            .SetData(data)
            .Build();

        // Assert
        Assert.AreEqual(id, model.Id);
        Assert.AreEqual(data, model.Data);
    }

    [TestMethod]
    public void Builder_SetIdWithZero_ShouldThrowArgumentException()
    {
        // Arrange
        var builder = new SavedModel<string>.Builder();
        int invalidId = 0;

        // Act & Assert
        Assert.ThrowsException<ValidationNotCourseInException<int>>(() => builder.SetId(invalidId));
    }

    [TestMethod]
    public void Builder_SetDataWithNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var builder = new SavedModel<string>.Builder();

        // Act & Assert
        Assert.ThrowsException<ValidationNullException>(() => builder.SetData(null!));
    }

    [TestMethod]
    public void Build_WithoutSettingId_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var builder = new SavedModel<string>.Builder();
        var data = "Test Data";
        builder.SetData(data);

        // Act & Assert
        Assert.ThrowsException<ValidationNullException>(() => builder.Build());
    }

    [TestMethod]
    public void Build_WithoutSettingData_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var builder = new SavedModel<string>.Builder();
        int id = 1;
        builder.SetId(id);

        // Act & Assert
        Assert.ThrowsException<ValidationNullException>(() => builder.Build());
    }
    
    [TestMethod]
    public void From_ShouldCreateBuilderWithExistingSavedModelProperties()
    {
        // Arrange
        var originalModel = new SavedModel<string>.Builder()
            .SetId(1)
            .SetData("Test Data")
            .Build();

        // Act
        var builder = SavedModel<string>.Builder.From(originalModel);
        var copiedModel = builder.Build();

        // Assert
        Assert.AreEqual(originalModel.Id, copiedModel.Id);
        Assert.AreEqual(originalModel.Data, copiedModel.Data);
    }
}
