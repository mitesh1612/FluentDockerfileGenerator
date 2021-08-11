# Fluent Dockerfile Generator

A simple Dockerfile generator to demonstrate how to create a "Fluent" API in C#.

The API Design might not be fully correct, but its an example to create your own Fluent API from scratch.

Following classes are present in this project.

## `SimpleFluentDockerFileGenerator`

This is a simple basic fluent class, with no interfaces used to guide the user for the order of methods. Due to this, a user can generate invalid dockerfiles using this.

### Example Usage

```cs
var dockerFile = SimpleFluentDockerFileGenerator.CreateBuilder()
                .FromImage("node:12")
                .CopyFiles("package*.json", "./")
                .RunCommand("npm install")
                .WithEnvironmentVariable("PORT","8080")
                .ExposePort(8080)
                .WithRunCommand("npm start")
                .GenerateDockerFile();
// Can now save the content of `dockerFile` to a file.
```

## `FluentDockerfileGenerator`

This is a better fluent class, with interfaces used to guide the user for the order of methods. This should avoid generating invalid dockerfiles.

### Example Usage

```cs
var dockerFile = FluentDockerfileGenerator.CreateBuilder()
                .FromImage("node:12")
                .CopyFiles("Package*.json", "./")
                .RunCommand("npm install")
                .CopyFiles(".", ".")
                .WithEnvironmentVariable("PORT","8080")
                .ExposePort(8080)
                .WithCommand("npm start")
                .GenerateDockerFile();
// Can now save the content of `dockerFile` to a file.
```

This also contains a driver `Program.cs` class to showing the usage of each of the classes.

# Blog Post

Consider reading [my blog post](https://mitesh1612.github.io/blog/2021/08/11/how-to-design-fluent-api) on how this API was designed and how to write your own Fluent API.