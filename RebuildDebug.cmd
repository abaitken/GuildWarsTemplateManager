@echo off
call BuildSupport\SetupEnvironment.cmd
msbuild /p:Configuration=Debug /t:Rebuild
