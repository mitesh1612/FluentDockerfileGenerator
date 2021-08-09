using System;
using System.Collections.Generic;
using System.Text;

namespace DockerfileGenerator
{
    public class SimpleFluentDockerFileGenerator
    {
        private StringBuilder _dockerFileContent;

        private SimpleFluentDockerFileGenerator()
        {
            this._dockerFileContent = new StringBuilder();
        }

        public static SimpleFluentDockerFileGenerator CreateBuilder()
        {
            return new SimpleFluentDockerFileGenerator();
        }

        public SimpleFluentDockerFileGenerator FromImage(string imageName)
        {
            this._dockerFileContent.AppendLine($"FROM {imageName}");
            return this;
        }

        public SimpleFluentDockerFileGenerator CopyFiles(string source, string destination)
        {
            this._dockerFileContent.AppendLine($"COPY {source} {destination}");
            return this;
        }

        public SimpleFluentDockerFileGenerator RunCommand(string command)
        {
            this._dockerFileContent.AppendLine($"RUN {command}");
            return this;
        }

        public SimpleFluentDockerFileGenerator ExposePort(int port)
        {
            this._dockerFileContent.AppendLine($"EXPOSE {port}");
            return this;
        }

        public SimpleFluentDockerFileGenerator WithEnvironmentVariable(string variableName, string variableValue)
        {
            this._dockerFileContent.AppendLine($"ENV {variableName}={variableValue}");
            return this;
        }

        public SimpleFluentDockerFileGenerator WithRunCommand(string command)
        {
            var commandList = command.Split(" ");
            this._dockerFileContent.Append("CMD [ ");
            foreach(var c in commandList)
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
}
