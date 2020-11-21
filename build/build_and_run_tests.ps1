Set-Location -Path $PSScriptRoot  -PassThru
Set-Location -Path .. -PassThru
dotnet build
dotnet test --logger trx