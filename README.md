# PreciseFolderSync

**PreciseFolderSync** is a robust console application designed for synchronizing directories across different locations with high accuracy and efficiency. It handles both file and directory synchronization operations, making it particularly useful for backup processes and mirror syncing.

## Features

- **Directory Synchronization**: Syncs all directories and files from a source to a destination path, ensuring both locations have identical content.
- **Selective Syncing**: Allows exclusion of specific files or directories.
- **Simulation Mode**: Includes a 'whatif' mode to simulate changes without actually applying them.
- **Extensible**: Supports custom implementations for directory walking and IO handling which can be extended for testing or specialized behavior.

## Usage

Run the application from the command line using the following format:

```plaintext
pfs.exe --fromPath <source_path> --toPath <destination_path> [--ignore <files_or_directories_to_ignore>] [--whatif]
```

## Command Line Arguments

- --fromPath: Specifies the source directory path.
- --toPath: Specifies the destination directory path.
- --whatif: Simulates the sync process and outputs the potential changes without applying them.
- --ignore: Specifies the files or directories to ignore during the sync process. Multiple files or directories can be specified as a comma-separated list. Directories should end with a backslash.

## Example

```powershell
PS C:\Projekte\Pfs.exe -f C:\Projekte\Test1 -t C:\Projekte\Test2 -i bob.txt,MahrBaseAPI\Schnapper,MahrBaseAPI\Schnapper\ -w
```

This command will synchronize the directories `Test1` and `Test2`, ignoring the file `bob.txt` and the directory `MahrBaseAPI\Schnapper` and all files inside `MahrBaseAPI\Schnapper\`. The `-w` option indicates that this is a simulation and no changes will be actually made.
