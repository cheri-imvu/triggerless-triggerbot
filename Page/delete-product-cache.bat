@echo off
setlocal

set "DB_PATH=%APPDATA%\IMVU\productInfoCache.db"

echo Are you sure you want to delete the IMVU productInfoCache database?
echo File: %DB_PATH%
echo Note: If you delete this file, IMVU Classic will rebuild it the next time you go to Dress Up.
echo.

set /p choice=Type Y to delete, or N to cancel: 

if /i "%choice%"=="Y" (
    if exist "%DB_PATH%" (
        del "%DB_PATH%"
        echo File deleted.
		echo Please run IMVU Classic to rebuild the product cache DB.
    ) else (
        echo File not found.
		echo You probably don't have IMVU Classic installed yet.
    )
) else (
    echo Deletion cancelled.
	echo Run this batch script again if you change your mind.
)

pause
