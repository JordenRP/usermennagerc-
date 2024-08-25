FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app/UserManagementApp
COPY . .
EXPOSE 5000

RUN dotnet restore "UserManagementApp.csproj"
RUN dotnet publish "UserManagementApp.csproj" -c Release -o /app/out

RUN dotnet tool install --global dotnet-ef --version 6.0.0 --add-source https://api.nuget.org/v3/index.json

ENV PATH="$PATH:/root/.dotnet/tools"

WORKDIR /app/out

ENTRYPOINT ["sh", "-c", "dotnet ef database update --project /app/UserManagementApp/UserManagementApp.csproj && dotnet /app/out/UserManagementApp.dll"]
