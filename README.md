# nativemessaginghostjson
WiX CustomAction for generating Native Messaging Host manifest JSON files

Built as part of an experiment using WiX to create MSIs for Evergeen Hatch

This is only useful when used with WiX, not on its own.


## Example
```xml
      <Property Id='name' Value='org.evergreen_ils.hatch'/>
      <Property Id='description' Value='Hatch Native Messaging Host'/>
      <Property Id='type' Value='stdio'/>
      <Property Id="allowed_origins" Value='chrome-extension://ppooibdipmklfichpmkcgplfgdplgahl/'/>
      <Property Id="allowed_extensions" Value='hatch-native-messenger@evergreen-ils.org'/>
      
      <CustomAction Id="SetExePath" Property='path' Value='[INSTALLDIR]\hatch.exe'/>
      <CustomAction Id="SetJsonPath" Property='destpath' Value='[INSTALLDIR]\org.evergreen_ils.hatch.json'/>
      <CustomAction Id='MakeChromeManifest' BinaryKey='NativeMessagingHostJSON' DllEntry='MakeChromeManifest' />

      <InstallExecuteSequence>
         <Custom Action="SetExePath" Before="MakeChromeManifest"/>
         <Custom Action="SetJsonPath" Before="MakeChromeManifest"/>
         <Custom Action="MakeChromeManifest" Before="InstallFinalize"/>
      </InstallExecuteSequence>
```
Will Generate:
```json
{
  "allowed_origins": [
    "chrome-extension://ppooibdipmklfichpmkcgplfgdplgahl/"
  ],
  "name": "org.evergreen_ils.hatch",
  "description": "Hatch Native Messaging Host",
  "path": "C:\\Program Files (x86)\\Hatch\\\\hatch.exe",
  "type": "stdio"
}
```
