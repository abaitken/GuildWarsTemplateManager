@echo off
call BuildSupport\SetupEnvironment.cmd
msbuild /p:Configuration=Release /t:Rebuild
