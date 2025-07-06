param(
    [ValidateNotNullOrEmpty()]
    [string]$name = $(Read-Host "Enter the migration name")
)

Set-Location -Path "./PortalApi"
dotnet ef migrations add "$name" --context "PortalDbContext" --startup-project "../PortalApi"
