SonarScanner.MSBuild.exe begin^
    /k:"jornada-automacao-testes"^
    /n:"jornada-automacao-testes"^
    /v:"master"^
    /d:sonar.cs.opencover.reportsPaths="%CD%\opencover.xml"^
    /d:sonar.coverage.exclusions="**/GestaoContratos.Repositorio.Mock/*,**/GestaoContratos.InjetorDependencias/*,**/GestaoContratos/Controllers/*,**/GestaoContratos/App_Start/*,**/GestaoContratos/Global.asax.cs"
MSBuild.exe /t:Rebuild
"%LOCALAPPDATA%\Apps\OpenCover\OpenCover.Console.exe"^
    -output:"%CD%\opencover.xml"^
    -register:user -target:"vstest.console.exe"^
    -targetargs:"GestaoContratos.Teste.Integracao\bin\Debug\GestaoContratos.Teste.Integracao.dll GestaoContratos.Teste.Processo\bin\Debug\GestaoContratos.Teste.Processo.dll"
SonarScanner.MSBuild.exe end
pause