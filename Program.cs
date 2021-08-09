using System;

namespace DockerfileGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*********** USING SIMPLE FLUENT GENERATOR ***********");
            var dockerFile = SimpleFluentDockerFileGenerator.CreateBuilder()
                .FromImage("node:12")
                .CopyFiles("package*.json", "./")
                .RunCommand("npm install")
                .WithEnvironmentVariable("PORT","8080")
                .ExposePort(8080)
                .WithRunCommand("npm start")
                .GenerateDockerFile();

            Console.WriteLine(dockerFile);

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("*********** USING BETTER FLUENT GENERATOR ***********");
            var dockerFile2 = FluentDockerfileGenerator.CreateBuilder()
                .FromImage("node:12")
                .CopyFiles("Package*.json", "./")
                .RunCommand("npm install")
                .CopyFiles(".", ".")
                .WithEnvironmentVariable("PORT","8080")
                .ExposePort(8080)
                .WithCommand("npm start")
                .GenerateDockerFile();

            Console.WriteLine(dockerFile2);
        }
    }
}
