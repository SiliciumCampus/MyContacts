using MyContacts.shared.Interfaces;
using MyContacts.shared.Services;

namespace MyContacts.Tests;

public class FileService_Tests
{
    [Fact]
    public void SaveContentToFileShould_ReturnTrue_IfFilePathExists()
    {
        // Arrange
        IFileService fileService = new FileService();
        string filePath = @"c:\Projects\MyContacts\test.txt";
        string content = "Test content";

        // Act
        bool result = fileService.SaveContentToFile(filePath, content);

        // Assert
        Assert.True(result);

    }

    [Fact]
    public void SaveContentToFileShould_ReturnFalse_IfFilePathDoNotExists()
    {
        // Arrange
        IFileService fileService = new FileService();
        string filePath = @$"c:\{Guid.NewGuid()}\test.txt";
        string content = "Test content";

        // Act
        bool result = fileService.SaveContentToFile(filePath, content);

        // Assert
        Assert.False(result);

    }
}
