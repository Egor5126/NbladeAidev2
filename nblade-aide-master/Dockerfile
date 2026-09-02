FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY NbladeAide.sln ./
COPY ./NbladeAide/NbladeAide.csproj ./NbladeAide/
COPY ./Models/Models.csproj ./Models/

RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o out

FROM nginx:1.23.0-alpine
WORKDIR /app
EXPOSE 8080
COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=build /app/out/wwwroot /usr/share/nginx/html