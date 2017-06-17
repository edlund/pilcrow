#!/bin/bash

apt_host="https://apt-mo.trafficmanager.net/repos/dotnet-release/"
apt_version="xenial"
apt_list="/etc/apt/sources.list.d/dotnetdev.list"

if [ ! -f "${apt_list}" ] ; then
    sh -c "echo \"deb [arch=amd64] ${apt_host} ${apt_version}  main\" > ${apt_list}"
    apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 417A0893
    apt-get update
fi

apt-get install --yes \
    dotnet-dev-1.0.4

