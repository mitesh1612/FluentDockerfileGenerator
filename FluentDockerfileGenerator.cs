using System;
using System.Collections.Generic;
using System.Text;

namespace DockerfileGenerator
{
    public class FluentDockerfileGenerator :
        ICanSetBaseImage,
        ICanGenerateDockerFile,
        ICanSetContainerProperties,
        ICanAddRunCommand
    {
        private StringBuilder _dockerFileContent;

        private FluentDockerfileGenerator()
        {
            this._dockerFileContent = new StringBuilder();
        }

        public static ICanSetBaseImage CreateBuilder()
        {
            return new FluentDockerfileGenerator();
        }

        public ICanSetContainerProperties FromImage(string imageName)
        {
            this._dockerFileContent.AppendLine($"FROM {imageName}");
            return this;
        }

        public ICanSetContainerProperties CopyFiles(string source, string destination)
        {
            this._dockerFileContent.AppendLine($"COPY {source} {destination}");
            return this;
        }

        public ICanSetContainerProperties RunCommand(string command)
        {
            this._dockerFileContent.AppendLine($"RUN {command}");
            return this;
        }

        public ICanAddRunCommand ExposePort(int port)
        {
            this._dockerFileContent.AppendLine($"EXPOSE {port}");
            return this;
        }

        public ICanSetContainerProperties WithEnvironmentVariable(string variableName, string variableValue)
        {
            this._dockerFileContent.AppendLine($"ENV {variableName}={variableValue}");
            return this;
        }

        public ICanGenerateDockerFile WithCommand(string command)
        {
            var commandList = command.Split(" ");
            this._dockerFileContent.Append("CMD [ ");
            foreach (var c in commandList)
            {
                this._dockerFileContent.Append($"\"{c}\", ");
            }

            this._dockerFileContent.Remove(this._dockerFileContent.Length - 2, 1);
            this._dockerFileContent.Append("]");
            return this;
        }

        public string GenerateDockerFile()
        {
            return this._dockerFileContent.ToString();
        }
    }

    public interface ICanSetBaseImage
    {
        public ICanSetContainerProperties FromImage(string image);
    }

    public interface ICanGenerateDockerFile
    {
        public string GenerateDockerFile();
    }

    public interface ICanSetContainerProperties : ICanAddRunCommand
    {
        public ICanSetContainerProperties CopyFiles(string source, string destination);
        public ICanSetContainerProperties RunCommand(string command);
        public ICanAddRunCommand ExposePort(int port);
        public ICanSetContainerProperties WithEnvironmentVariable(string variableName, string variableValue);
    }

    public interface ICanAddRunCommand
    {
        public ICanGenerateDockerFile WithCommand(string command);
    }
}
