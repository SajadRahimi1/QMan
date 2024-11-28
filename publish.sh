git pull
dotnet publish
rm -rf ../api/runtimes/
cp -r QMan.Api/bin/Release/net8.0/publish/* ../api/
service qman-api restart