echo "Setup star"
echo "$APP_SETTINGS" > build/appsettings.json
tar -zcvf archive.tar.gz -C build/ .
cat build/appsettings.json
ls
