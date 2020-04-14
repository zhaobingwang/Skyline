set docfxPath=..\..\tools\docfx\docfx.exe

mkdir .docfx\Skyline.Utilities
cd .docfx\Skyline.Utilities

%docfxPath% init -q
%docfxPath% .\docfx_project\docfx.json

%docfxPath% metadata ..\..\src\Skyline.Utilities\Skyline.Utilities.csproj

xcopy /d /y "_api\*" "docfx_project\api\"

%docfxPath% .\docfx_project\docfx.json
%docfxPath% serve .\docfx_project\_site\
pause