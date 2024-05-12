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
pfs.exe --fromPath <source_path> --toPath <destination_path> [--whatif]
```

## Command Line Arguments

- --fromPath: Specifies the source directory path.
- --toPath: Specifies the destination directory path.
- --whatif: Simulates the sync process and outputs the potential changes without applying them.