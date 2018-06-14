echo "Arguments for updating:"
echo " - CRYPTO_KEY: $CRYPTO_KEY"
echo " - FUNCTION_URL: $FUNCTION_URL"
echo " - STORAGE_URL: $STORAGE_URL"

# Updating ids

IdFile=$BUILD_REPOSITORY_LOCALPATH/source/mobile-app/TuVotoCuenta/SettingsInitializer.cs

sed -i '' "s#CRYPTO_KEY#$CRYPTO_KEY#g" $IdFile
sed -i '' "s#FUNCTION_URL#$FUNCTION_URL#g" $IdFile
sed -i '' "s#STORAGE_URL#$STORAGE_URL#g" $IdFile
sed -i '' "s#ACCOUNT_IMAGE_CONTAINER#$ACCOUNT_IMAGE_CONTAINER#g" $IdFile
sed -i '' "s#RECORDS_IMAGE_CONTAINER#$RECORDS_IMAGE_CONTAINER#g" $IdFile

# Print out file for reference
cat $IdFile

echo "Updated id!"