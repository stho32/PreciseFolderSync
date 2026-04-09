# CLAUDE.md

## Projektbeschreibung

PreciseFolderSync ist ein CLI-Tool zur praezisen Ordner-Synchronisation. Es vergleicht Quell- und Zielordner und fuehrt die notwendigen Datei- und Verzeichnisoperationen durch, um den Zielordner exakt an den Quellordner anzugleichen.

## TechStack

- .NET 10.0
- C#
- NUnit (Tests)
- Coverlet (Code Coverage)

## Build

```bash
dotnet build Source/Pfs/Pfs/Pfs.csproj
```

## Tests

```bash
dotnet test Source/Pfs/Pfs.BL.Tests/Pfs.BL.Tests.csproj
```

## Architektur-Vorlage

dotnet-cli-tool

## Projektstruktur

- `Source/Pfs/Pfs/` — CLI-Einstiegspunkt (Executable)
- `Source/Pfs/Pfs.BL/` — Business Logic (Kernlogik)
- `Source/Pfs/Pfs.BL.Tests/` — Unit- und Integrationstests

## Wichtige Konventionen

- **File-scoped Namespaces**: Alle C#-Dateien verwenden file-scoped namespace declarations (`namespace X.Y.Z;`)
- **TreatWarningsAsErrors**: In allen Projekten aktiviert — Warnungen sind Build-Fehler
- **Hand-written Mocks**: Keine Mocking-Frameworks — Test-Doubles werden manuell geschrieben
- **Keine PDB-Dateien im Release**: PDB-Dateien werden beim Publish entfernt
