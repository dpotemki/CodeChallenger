@echo off

echo Building Docker images....

:: Set the context to the src directory
cd src

:: Building CodeChallanger.UI
echo Building image for CodeChallanger.UI...
docker build -t code_challanger_ui:latest -f .\CodeChallanger.UI\Dockerfile .

:: Building CSharpCodeCompilator.Service
echo Building image for csharp_compilator...
docker build -t csharp_compilator_service:latest -f .\CodeCompilator.Service\Dockerfile .


:: Return to the initial directory
cd ..

echo All services built successfully.
