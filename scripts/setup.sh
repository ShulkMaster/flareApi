echo "The File end"
echo "$APP_SETTINGS" > build/appsettings.json
cat build/appsettings.json
tar -zcvf archive.tar.gz -C build/ .