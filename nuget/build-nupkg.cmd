@ECHO OFF

REM
REM Version is read from the VERSION file.
REM

SETLOCAL

PUSHD "%~dp0"

WHERE /Q nuget >NUL
IF %ERRORLEVEL% NEQ 0 ( 
    ECHO nuget not found.
    ECHO.
    ECHO Run "%~pd0download-nuget.cmd" to download the latest version, or update PATH as appropriate.
    GOTO END
)

SET /p VERSION=<VERSION

SET BIN=\AdaptiveTriggerLibrary\AdaptiveTriggerLibrary\bin
SET OUTDIR=..\nugetRelease
SET LICENSE_URL=https://github.com/Herdo/AdaptiveTriggerLibrary/blob/master/LICENSE
SET REQUIRE_LICENSE_ACCEPTANCE=true

IF NOT "%1" == "" (
    SET VERSION=%VERSION%-%1
)

SET NUGET_ARGS=^
    -nopackageanalysis ^
    -basepath ..\.. ^
    -outputdirectory %OUTDIR% ^
    -version %VERSION% ^
    -properties bin=%BIN%;LicenseUrl=%LICENSE_URL%;RequireLicenseAcceptance=%REQUIRE_LICENSE_ACCEPTANCE%

nuget pack AdaptiveTriggerLibrary.nuspec %NUGET_ARGS%
IF %ERRORLEVEL% NEQ 0 GOTO END

:END

POPD
EXIT /B %ERRORLEVEL%