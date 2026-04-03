---
id: R00002
titel: "Verzeichnissynchronisation"
typ: Feature
status: Umgesetzt
erstellt: 2026-04-03
---

# Verzeichnissynchronisation

## Beschreibung

PreciseFolderSync fuehrt eine Mirror-Synchronisation von einem Quellverzeichnis zu einem Zielverzeichnis durch. Alle Dateien und Verzeichnisse aus der Quelle werden in das Ziel kopiert. Dateien und Verzeichnisse, die im Ziel existieren aber nicht in der Quelle vorhanden sind, werden geloescht. Damit wird sichergestellt, dass das Zielverzeichnis ein exaktes Abbild des Quellverzeichnisses darstellt.

Die Synchronisation wird ueber die CLI-Argumente `--fromPath` (Quellverzeichnis) und `--toPath` (Zielverzeichnis) gesteuert.

## Akzeptanzkriterien

- [x] Dateien aus dem Quellverzeichnis werden in das Zielverzeichnis kopiert
- [x] Dateien im Zielverzeichnis, die nicht im Quellverzeichnis existieren, werden geloescht
- [x] Verzeichnisse aus der Quelle werden im Ziel erstellt
- [x] Verzeichnisse im Ziel, die nicht in der Quelle existieren, werden geloescht
- [x] CLI-Argument `--fromPath` / `-f` definiert das Quellverzeichnis
- [x] CLI-Argument `--toPath` / `-t` definiert das Zielverzeichnis
- [x] Beide Argumente sind als Required markiert

## Technische Details

- `Source/Pfs/Pfs/Program.cs` - Einstiegspunkt, orchestriert den Synchronisationsablauf
- `Source/Pfs/Pfs.BL/Syncing/Synchronizer.cs` - Kernlogik: vergleicht Quelle und Ziel, erzeugt IoCommands
- `Source/Pfs/Pfs.BL/CommandLineArguments/CommandLineOptions.cs` - Definition der CLI-Optionen `FromPath` und `ToPath`
- `Source/Pfs/Pfs.BL/CommandLineArguments/CommandLineArgumentsParser.cs` - Parsing der Kommandozeilenargumente
- `Source/Pfs/Pfs.BL/Syncing/FileOrFolderCollection.cs` - Sammlung der Dateien und Verzeichnisse eines Pfades
- `Source/Pfs/Pfs.BL/Syncing/DirectoryWalkers/DirectoryWalker.cs` - Traversiert das Dateisystem und erstellt FileOrFolder-Eintraege
- `Source/Pfs/Pfs.BL/Syncing/IoCommands/FileExistsIoCommand.cs` - Kommando: Datei soll im Ziel existieren (kopieren)
- `Source/Pfs/Pfs.BL/Syncing/IoCommands/FileDoesNotExistIoCommand.cs` - Kommando: Datei soll im Ziel nicht existieren (loeschen)
- `Source/Pfs/Pfs.BL/Syncing/IoCommands/DirectoryExistsIoCommand.cs` - Kommando: Verzeichnis soll im Ziel existieren (erstellen)
- `Source/Pfs/Pfs.BL/Syncing/IoCommands/DirectoryDoesNotExistIoCommand.cs` - Kommando: Verzeichnis soll im Ziel nicht existieren (loeschen)
- `Source/Pfs/Pfs.BL/Syncing/IoHandlers/CopyFileIoOperation.cs` - Fuehrt das Kopieren einer Datei aus
- `Source/Pfs/Pfs.BL/Syncing/IoHandlers/RemoveFileIoOperation.cs` - Fuehrt das Loeschen einer Datei aus
- `Source/Pfs/Pfs.BL/Syncing/IoHandlers/CreateDirectoryIoOperation.cs` - Fuehrt das Erstellen eines Verzeichnisses aus
- `Source/Pfs/Pfs.BL/Syncing/IoHandlers/DeleteDirectoryIoOperation.cs` - Fuehrt das Loeschen eines Verzeichnisses aus
- `Source/Pfs/Pfs.BL.Tests/Syncing/SynchronizerTests.cs` - Tests fuer die Synchronisationslogik
