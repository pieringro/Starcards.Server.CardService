#!/bin/bash

echo "!!! Begin CardService: docker-build.sh !!!"

cd $(dirname "$0")

docker build . -t cardservice

echo "!!! End CardService: docker-build.sh !!!"
