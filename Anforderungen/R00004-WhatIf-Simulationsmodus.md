---
id: R00004
titel: "WhatIf-Simulationsmodus"
typ: Feature
status: Umgesetzt
erstellt: 2026-04-03
---

# WhatIf-Simulationsmodus

## Beschreibung

Der WhatIf-Modus ermoeglicht einen Trockenlauf der Synchronisation, bei dem alle geplanten Aenderungen angezeigt werden, ohne dass tatsaechlich Dateisystem-Operationen ausgefuehrt werden. Ueber das CLI-Flag `--whatif` wird der Modus aktiviert. Die Implementierung nutzt das Factory-Pattern: Eine eigene `WhatIfIoOperationFactory` erzeugt Simulations-Operationen (z.B. `WhatIfCopyFile`, `WhatIfCreateDirectoryIoOperation`), die anstelle der echten IO-Operationen ausgefuehrt werden und lediglich protokollieren, was geschehen wuerde.

## Akzeptanzkriterien

- [x] CLI-Flag `--whatif` / `-w` ist verfuegbar (Default: false)
- [x] Bei aktiviertem WhatIf-Flag werden keine echten Dateisystem-Aenderungen vorgenommen
- [x] Alle geplanten Operationen werden in der Konsole angezeigt
- [x] Die `WhatIfIoOperationFactory` erzeugt Simulations-Operationen fuer alle vier Operationstypen (Verzeichnis erstellen, Verzeichnis loeschen, Datei kopieren, Datei loeschen)
- [x] Die Auswahl der Factory erfolgt zur Laufzeit basierend auf dem WhatIf-Flag

## Technische Details

- `Source/Pfs/Pfs.BL/CommandLineArguments/CommandLineOptions.cs` - Definition der `WhatIf`-Option mit `Default = false`
- `Source/Pfs/Pfs/Program.cs` - Factory-Auswahl: `options.WhatIf ? new WhatIfIoOperationFactory() : new IoOperationFactory()`
- `Source/Pfs/Pfs.BL/Syncing/IoHandlers/IIoOperationFactory.cs` - Interface fuer die IO-Operation-Factory
- `Source/Pfs/Pfs.BL/Syncing/IoHandlers/IoOperationFactory.cs` - Echte Factory, erzeugt Operationen die das Dateisystem veraendern
- `Source/Pfs/Pfs.BL/Syncing/IoHandlers/WhatIfIoOperationFactory.cs` - WhatIf-Factory, erzeugt Simulations-Operationen
- `Source/Pfs/Pfs.BL/Syncing/IoHandlers/WhatIfCopyFile.cs` - Simuliert das Kopieren einer Datei
- `Source/Pfs/Pfs.BL/Syncing/IoHandlers/WhatIfCreateDirectoryIoOperation.cs` - Simuliert das Erstellen eines Verzeichnisses
- `Source/Pfs/Pfs.BL/Syncing/IoHandlers/WhatIfDeleteDirectoryIoOperation.cs` - Simuliert das Loeschen eines Verzeichnisses
- `Source/Pfs/Pfs.BL/Syncing/IoHandlers/WhatIfRemoveFile.cs` - Simuliert das Loeschen einer Datei
