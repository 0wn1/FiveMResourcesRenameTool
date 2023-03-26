## FiveM Resources Rename Tool
This is a simple tool for renaming FiveM resources folders and updating file references accordingly.

------------

### Preview
Streamable: [View](https://streamable.com/tkdpda "View")

------------
### How to Use
1. Run the program
2. Enter the path of the "resources" folder you want to rename.
3. Enter the prefix that you want to replace.
4. Enter the new prefix that will be added.
5. The program will find all folders in the "resources" folder that have a `fxmanifest.lua` or `__resource.lua` file and whose name starts with the old prefix, and rename them with the new prefix. It will also update any references to the old folder name in Lua and JavaScript files.
6. Note: The tool will not rename any files in the "resources" folder that are not Lua or JavaScript files.

------------
### Dependencies
This program is written in C# and requires the .NET Framework v4.7.2.

------------

### License
This program is released under the [MIT](https://github.com/0wn1/FiveMResourcesRenameTool/blob/main/LICENSE "MIT") license.