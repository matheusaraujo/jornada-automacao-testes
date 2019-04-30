SonarScanner.MSBuild.exe begin /k:"jornada-automacao-testes" /n:"jornada-automacao-testes" /v:"master"
MSBuild.exe /t:Rebuild
SonarScanner.MSBuild.exe end
pause