echo "Arguments for updating:"
echo " - CRYPTO_KEY: $CRYPTO_KEY"
echo " - FUNCTION_URL: $FUNCTION_URL"
echo " - STORAGE_URL: $STORAGE_URL"

# Updating ids

IdFile=$BUILD_REPOSITORY_LOCALPATH/src/MyApp/Ids.cs

sed -i '' "s/APP_SECRET/$APP_SECRET/g" $IdFile

# Print out file for reference
cat $IdFile

echo "Updated id!"