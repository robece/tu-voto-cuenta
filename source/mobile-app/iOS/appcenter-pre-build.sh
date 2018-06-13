#!/usr/bin/env bash

echo "Arguments for updating:"
echo " - Function: $FUNCTION_URL"
echo " - Storage: $STORAGE_URL"
echo " - Crypto: $CRYPTO_KEY"

# Updating ids

IdFile=$BUILD_REPOSITORY_LOCALPATH/source/mobile-app/TuVotoCuenta/SettingsInitializer.cs

sed -i '' "s/FUNCTION_URL/$FUNCTION_URL/g" $IdFile
sed -i '' "s/STORAGE_URL/$STORAGE_URL/g" $IdFile
sed -i '' "s/CRYPTO_KEY/$CRYPTO_KEY/g" $IdFile

# Print out file for reference
cat $IdFile

echo "Updated id!"