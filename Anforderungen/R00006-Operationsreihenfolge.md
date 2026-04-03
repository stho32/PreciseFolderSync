---
id: R00006
titel: "Operationsreihenfolge"
typ: Feature
status: Umgesetzt
erstellt: 2026-04-03
---

# Operationsreihenfolge

## Beschreibung

Die IO-Operationen werden vor der Ausfuehrung in eine definierte Reihenfolge sortiert, um sicherzustellen, dass Abhaengigkeiten zwischen Operationen korrekt beruecksichtigt werden. Die Sortierung folgt dieser Reihenfolge: Zuerst werden Verzeichnisse erstellt (aufsteigend nach Pfad sortiert, damit uebergeordnete Verzeichnisse vor Unterverzeichnissen erstellt werden), dann werden Dateien kopiert, anschliessend Dateien geloescht und zuletzt Verzeichnisse geloescht (absteigend nach Pfad sortiert, damit Unterverzeichnisse vor uebergeordneten Verzeichnissen geloescht werden).

## Akzeptanzkriterien

- [x] Verzeichnisse erstellen (DirectoryExistsIoCommand) wird vor allen anderen Operationstypen ausgefuehrt
- [x] Dateien kopieren (FileExistsIoCommand) wird nach Verzeichnisse erstellen ausgefuehrt
- [x] Dateien loeschen (FileDoesNotExistIoCommand) wird nach Dateien kopieren ausgefuehrt
- [x] Verzeichnisse loeschen (DirectoryDoesNotExistIoCommand) wird als letztes ausgefuehrt
- [x] Erstell-Operationen werden aufsteigend nach relativem Pfad sortiert (uebergeordnete zuerst)
- [x] Loesch-Operationen werden absteigend nach relativem Pfad sortiert (Unterverzeichnisse zuerst)
- [x] Die Sortierung wird vor der Ausfuehrung im IoCommandListExecutor angestossen

## Technische Details

- `Source/Pfs/Pfs.BL/Syncing/IoCommands/IoCommandSorter.cs` - Statische Klasse mit `SortCommands()`-Methode: Sortiert nach Operationstyp (Erstellen vor Loeschen) und innerhalb des Typs nach Pfad (aufsteigend fuer Erstellen, absteigend fuer Loeschen)
- `Source/Pfs/Pfs.BL/Syncing/IoCommands/IoCommandList.cs` - Enthaelt die Liste der IoCommands und delegiert `Sort()` an den IoCommandSorter
- `Source/Pfs/Pfs.BL/Syncing/IoCommandListExecutor.cs` - Ruft `commands.Sort()` vor der Vorbereitung der IO-Operationen auf
- `Source/Pfs/Pfs.BL.Tests/Syncing/IoCommandSorterTests.cs` - Tests fuer die korrekte Sortierreihenfolge
