# CODE SUMMARY
I implement 2 flows as below:

![Model](https://github.com/thuylammiu/ABS.FileGeneration/blob/main/Document/components.png)



- WebForm : Net Framework 4.7
- Excel Library : Net Framework 7.0
- Web Api : Net core Api, Net Framwwork 8.0


Flow 1: Browser call WehForm, WebForm send request to web API, web API communicate with Class Library to download file.
Server-Side Code is better suited for handling large file downloads, ensuring security, and implementing complex server-side logic. However, it can be slower and more resource-intensive on the server.

Flow 2 : Browser make request to Landing Page to WebForm, and get the Link download file form Web API, web API uses Class library to generate file. AJAX provides a more dynamic user experience and keeps the page interactive while the download is in progress. It is suitable for smaller files and requires careful handling of security.

## Class Diagram

![Model](https://github.com/thuylammiu/ABS.FileGeneration/blob/main/Document/ClassDiagram.png)

The FileGenerationService uses an IDataSourceProvider to fetch the data source, making the design flexible for multiple data source types. The client using the library can select the data source type through Dependency Injection. EmployeeFileDataProvider is an instance of IDataSourceProvider.

The FileGenerationService also uses an IFileGenerator to handle various file types. By using IFileGenerator, the design can generate files in different formats such as CSV, text, and more, depending on the specific instance of IFileGenerator. In this assignment, ExcelFileGenerator is injected as the instance of IFileGenerator to specify the Excel file format.

IEmployeeFileDataProvider,EmployeeFileDataProvider is the wrapper of IDataSourceProvider, to isolate the Excel library from the usage of API controller, the decision of data source type

The EmployeeGenerationService and IEmployeeGenerationService act as wrappers for the FileGenerationService. They inherit from FileGenerationService and IFileGenerationService, allowing them to utilize all the functions and behaviors of FileGenerationService and IFileGenerationService. Additionally, they can incorporate their own unique features if needed.

The FileService and IFileService are responsible for saving and opening files. This separation facilitates easier unit testing. Before downloading, the file is stored in the TEMP folder rather than in memory, preventing memory overflow. The files in the TEMP folder will be automatically deleted by the server.


## Connection of Framework 7.0 and 4.7

.NET Standard
- .NET Standard is a specification that all .NET implementations must support, allowing you to create libraries that can be used across different .NET platforms, including .NET Core, .NET 5/6/7, and .NET Framework 4.7.
- Code sharing among the project, reducing code duplication and simplifying maintenance. Internal communication, improving performance
- However, .NET Standard represents a subset of APIs available in .NET Core and .NET Framework. This means  might not have access to all the latest features available in .NET 7 when using .NET Standard. It is hard for scalability, security...

Web API
- Web API is easier to implement and integrate with your existing WebForms application.
- Good performance for most typical use cases.
- Simpler debugging and maintenance.
- A clear path for future enhancements or migrations.

  ## Note:
  - The source code includes unit tests using XUnit and Moq, as well as integration tests.
  - It also contains authentication code, which is not currently applied, to illustrate the concept that authentication and authorization are necessary for downloading files.

