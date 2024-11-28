# From solution root:
# docker build -f src/ClassicCalculatorBlazorApp/Dockerfile -t classiccalculatorblazorapp .

# Use the official Microsoft .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /src

# Copy the project files
COPY src/ClassicCalculatorBlazorApp/ClassicCalculatorBlazorApp.csproj ClassicCalculatorBlazorApp/
COPY src/ClassicCalculator/ClassicCalculator.csproj ClassicCalculator/

# Restore the dependencies
RUN dotnet restore "ClassicCalculatorBlazorApp/ClassicCalculatorBlazorApp.csproj"

# Copy the rest of the application files
COPY src/ClassicCalculatorBlazorApp ClassicCalculatorBlazorApp/
COPY src/ClassicCalculator ClassicCalculator/

# Build the application
WORKDIR "/src/ClassicCalculatorBlazorApp"
RUN dotnet build "ClassicCalculatorBlazorApp.csproj" -c Release -o /app/build

# Publish the application
RUN dotnet publish "ClassicCalculatorBlazorApp.csproj" -c Release -o /app/publish

# Use the official Nginx image to serve the Blazor WebAssembly application
FROM nginx:alpine

# Copy the published files to the Nginx HTML directory
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html

# Expose port 80
EXPOSE 80

# Start Nginx
CMD ["nginx", "-g", "daemon off;"]
