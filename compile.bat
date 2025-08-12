@echo off
cls

echo --- SecurePass Compiler ---
echo.
echo [1] Cleaning project...
dotnet clean -c Release
if %errorlevel% neq 0 (
    echo.
    echo !!! ERROR: Failed to clean the project. Check your .csproj file.
    goto end
)

echo.
echo [2] Publishing to a single EXE file...
echo    (This may take a moment)
echo.

dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true

if %errorlevel% neq 0 (
    echo.
    echo !!! ERROR: Publish failed. Make sure the .NET SDK is installed correctly.
    goto end
)

echo.
echo --- BUILD SUCCEEDED ---
echo.

set "output_folder=%cd%\bin\Release\net8.0-windows\win-x64\publish"

echo The final SecurePass.exe is located at:
echo %output_folder%
echo.
echo Opening the folder for you...

explorer "%output_folder%"

:end
echo.
echo Press any key to exit...
pause > nul
