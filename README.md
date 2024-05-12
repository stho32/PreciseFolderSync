# PreciseFolderSync

- The app is a console application.
- Usage : pfs.exe --fromPath fromPath --toPath toPath [--whatif]
- if the fromPath or toPath are missing, the usage help is printed to the console and the app exits.

- There is a data structure called a file-or-folder.
    - A file-or-folder is a c# record (short syntax) with the following properties:
        - The information, if it is a file or directory.
        - The absolute path.
        - The relatve path (measured on its base path, without a leading backslash)  

- There is a data structure called a FileOrFolderCollection. (A collection of "file-or-folder"-elements) 

- There is an Interface IIoHandler with the methods:
  - CreateDirectory(string path)
  - DeleteDirectory(string path)
  - CopyFile(string fromFilePath, string toFilePath)
  - RemoveFile(string filePath)
  - All methods return a Result that contains a message of what it has done. (string)
- There are two implementations of this:
  - IoHandler : The one that really performs the work.
  - WhatIfIoHandler: The one that does not really modify the file system but just returns what it would do instead.

----

- There is a data structure called an IoCommandList.
    - The list contains elements of IIoCommand. IIoCommand contains 2 methods:
        - Execute(string toPath)
        - WhatIf(string toPath)
    - There are some classes, that implement IIoCommand:
        - DirectoryExistsIoCommand
        - FileExistsIoCommand
        - DirectoryDoesNotExistIoCommand
        - FileDoesNotExistIoCommand
    - The DirectoryExistsIoCommand:
        - Execute(toBasePath):
            - The targetdirectory is formed out of the relativePathInFrom and the toBasePath
            - If the targetdirectory does not exist, it is created.
    - The DirectoryDoesNotExistIoCommand:
        - Execute(toBasePath):
            - The targetdirectory is formed out of the relativePathInFrom and the toBasePath
            - If the targetdirectory does exist, it is deleted.
    - The FileExistsIoCommand:
        - Execute(toBasePath):
            - The targetfilePath is formed out of the relativePathInFrom and the toBasePath
            - The file is copied from the source to the targetfilePath.
            - If the file already exists in the targetfilePath, then it is overwritten.
    - The FileDoesNotExistIoCommand:
        - Execute(toBasePath):
            - The targetfilePath is formed out of the relativePathInFrom and the toBasePath
            - If the targetfilePath does exist, it is deleted.
    - All whatif implementations are the same as their execute counterparts, but they do not execute the IO action that would "modify" the file system (create or delete a directory, copy or not copy a file). Instead they print to the screen what they would have done. 

- The app creates a file-and-folder-list from everything (recursively) in fromPath. Let's call it "from".
- The app creates a file-and-folder-list from everything (recursively) in toPath. Let's call it "to".
- Then a IoCommandList is created from the elements of the two lists as follows (based on the relative paths, ignoring casing):
    - If the folder exists in "from", then a new DirectoryExistsIoCommand with its relative path is created.
    - If the folder exists in "to", but not in from, then a new DirectoryDoesNotExistIoCommand with its relative path is created.
    - If the file exists in "from", then a new FileExistsIoCommand with its relative path is created.
    - If the folder exists in "to", but not in from, then a new FileExistsIoCommand with its relative path is created.

- The IoCommandList is then sorted:
    - DirectoryExistsIoCommand: Subdirectories are placed after their parent directories.
    - DirectoryDoesNotExistIoCommand: They are placed behind all their subdirectory commands and file commands of files that are inside of them

- Then the IoCommandList is executed as follows
    - if [--whatif] is not given at the command line:
        - For each IIoCommand in the list "Execute" is executed.
    - if [--whatif] is was given at the command line:
        - For each IIoCommand in the list "WhatIf" is executed.

## Next features
- ignoreFiles = ..., ignorePath = ...


# DotNet 6 app repository template

The project is based on these [Requirements](Documentation/requirements.md) and is considered https://github.com/stho32/Training .

## Badges

- [ ] Add a badge from the build workflow
- [ ] Add a badge from https://www.codefactor.io/
- [ ] Add a badge from sonarcloud
    - [ ] Code coverage
    - [ ] Lines of code
    - [ ] Maintainability rating
    - [ ] Security rating
    - [ ] Reliability rating

## What is this?

- [ ] add documentation about the usage here
- [ ] put a few screenshots in between


