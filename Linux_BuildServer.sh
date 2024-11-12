#!/bin/bash

if [ -n "$1" ]
then
  if [ -d $1 ]
  then
    rm -rf $1
  fi
  dotnet publish ./WebApi/WebApi.csproj --configuration Release --runtime linux-x64 --self-contained true --framework net8.0 --output $1 -p:PublishReadyToRun=true
  cp ./WebApi/certificate.pfx $1
else
  echo 'The output directory path argument is mandatory.'
fi
