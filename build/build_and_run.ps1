Set-Location -Path $PSScriptRoot  -PassThru
Set-Location -Path .. -PassThru
dotnet build
Set-Location -Path .\src\TrainingTask.Web
dotnet run
