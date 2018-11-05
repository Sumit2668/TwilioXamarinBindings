#!/usr/bin/env bash

PARENT_PATH=$( cd "$(dirname "${BASH_SOURCE[0]}")" ; pwd -P )
# URL="https://github.com/twilio/twilio-video-ios/releases/download/1.4.2/TwilioVideo.framework.zip" # google drive is faster
URL="https://drive.google.com/uc?authuser=0&id=1By7CFRCYRSPJ0qIxmpAESy1PrIwn5lM2&export=download"
ZIP_NAME="TwilioVideo.framework.zip"
LIB_DIR="build"

cd "$PARENT_PATH"
curl -L $URL > $ZIP_NAME
tar -xf $ZIP_NAME
touch "Native References"
mv $LIB_DIR/iOS/TwilioVideo.framework "Native References"/TwilioVideo.framework
rm -rf $LIB_DIR
rm $ZIP_NAME 