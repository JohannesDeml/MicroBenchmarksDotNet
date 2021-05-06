
dotnet C:\sonarqube\scanner\SonarScanner.MSBuild.dll begin /k:"micro-benchmarks-dotnet" /d:sonar.host.url="http://localhost:9000"
dotnet build MicroBenchmarks\MicroBenchmarks.csproj --configuration Release
dotnet C:\sonarqube\scanner\SonarScanner.MSBuild.dll end
PAUSE