---
id: R00005
titel: "Retry-Mechanismus"
typ: Feature
status: Umgesetzt
erstellt: 2026-04-03
---

# Retry-Mechanismus

## Beschreibung

Fehlgeschlagene IO-Operationen werden automatisch wiederholt, um transiente Fehler (z.B. gesperrte Dateien, Netzwerkprobleme bei Netzlaufwerken) abzufangen. Nach jedem Durchlauf werden fehlgeschlagene Operationen gesammelt und nach einer Wartezeit von 5 Sekunden erneut ausgefuehrt. Insgesamt werden bis zu 5 Versuche unternommen. Der Fortschritt wird farbig in der Konsole protokolliert: gruen fuer erfolgreiche Operationen, rot fuer fehlgeschlagene, gelb fuer Retry-Hinweise und grau fuer ignorierte Operationen.

## Akzeptanzkriterien

- [x] Fehlgeschlagene IO-Operationen werden in einer Liste gesammelt
- [x] Nach einem fehlgeschlagenen Durchlauf wird 5 Sekunden gewartet
- [x] Die fehlgeschlagenen Operationen werden erneut ausgefuehrt (maximal 5 Versuche insgesamt)
- [x] Bei vollstaendigem Erfolg wird Exit-Code 0 zurueckgegeben
- [x] Bei verbleibenden Fehlern nach allen Versuchen wird Exit-Code 1 zurueckgegeben
- [x] Erfolgreiche Operationen werden in gruener Schrift mit "SUCCESS" ausgegeben
- [x] Fehlgeschlagene Operationen werden in roter Schrift mit "FAILURE" ausgegeben
- [x] Retry-Hinweise werden in gelber Schrift mit "NOTE" ausgegeben
- [x] Ignorierte Operationen werden in grauer Schrift mit "IGNORED" ausgegeben
- [x] Jede Ausgabe enthaelt einen Zeitstempel und Fortschrittsinformation (z.B. "3/10")

## Technische Details

- `Source/Pfs/Pfs/Program.cs` - Methode `Synchronize()`: Implementiert die Retry-Schleife mit `maxRetries = 5` und `Thread.Sleep(5000)`. Sammelt fehlgeschlagene Operationen in `failedOperations` und fuehrt diese im naechsten Durchlauf erneut aus. Farbiges Konsolen-Logging ueber `Console.ForegroundColor`.
- `Source/Pfs/Pfs.BL/Syncing/IoHandlers/IoOperationResult.cs` - Ergebnis einer IO-Operation mit `IsSuccess` und `Message`
- `Source/Pfs/Pfs.BL/Syncing/IoHandlers/IIoOperation.cs` - Interface fuer ausfuehrbare IO-Operationen mit `Execute()`-Methode
